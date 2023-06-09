using System.Net;
using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json.Linq;
using RestSharp;
using CryptoExchangeTools.Requests.BinanceRequests;
using CryptoExchangeTools.Models.ICex;

namespace CryptoExchangeTools;

public class BinanceClient : CexClient, ICexClient
{
    private const string Url = "https://api.binance.com";

    public Wallet Wallet { get; }

    public Margin Margin { get; }

    public Trade Trade { get;}

    public Market Market { get; }

    public FuturesTransfer FuturesTransfer { get; }

    public BinanceFuturesClient Futures { get; }

    /// <summary>
    /// Initializes a client with account information and checks connection to servers.
    /// </summary>
    /// <param name="apiKey">Your api key</param>
    /// <param name="apiSecret">Your api secret</param>
    /// <param name="proxy">Proxy to be used with client</param>
    public BinanceClient(string apiKey, string apiSecret, WebProxy? proxy = null) : base(apiKey, apiSecret, Url, proxy)
    {
        Wallet = new(this);
        Margin = new(this);
        Trade = new(this);
        FuturesTransfer = new(this);
        Market = new(this);
        Futures = new(apiKey, apiSecret, proxy);
    }

    protected sealed override void TryLogin()
    {
        var request = new RestRequest("sapi/v1/system/status");

        var response = ExecuteRequestWithoutResponse(request, false);

        if (!response.IsSuccessful)
            throw new ConnectionNotSetException("Couldn't connect to Binance Server.", response.StatusCode, response.Content);

        CheckAccountStatus();
    }

    private void CheckAccountStatus()
    {
        var request = new RestRequest("sapi/v1/account/status");

        var response = ExecuteRequestWithoutResponse(request);

        if (!response.IsSuccessful || response.Content is null)
            throw new BadAccountStatusException("Couldn't connect to Binance Account.", response.StatusCode, response.Content);

        dynamic json = JObject.Parse(response.Content);
        if (json["data"] != "Normal")
            throw new BadAccountStatusException("Couldn't connect to Binance Account.", response.StatusCode, response.Content);
    }

    internal sealed override void SignRequest(RestRequest request)
    {
        request.AddHeader("X-MBX-APIKEY", ApiKey);
        request.AddParameter("timestamp", DateTimeOffset.UtcNow.ToUnixTimeMilliseconds());

        var queryString = request.GetQueryString();

        if (string.IsNullOrEmpty(queryString))
            return;

        var signature = Sign(queryString);

        request.AddParameter("signature", signature);
    }

    private string? Sign(string payload)
    {
        var key = Encoding.UTF8.GetBytes(ApiSecret);

        using (HMACSHA256 hmacsha256 = new HMACSHA256(key))
        {
            byte[] payloadBytes = Encoding.UTF8.GetBytes(payload);
            byte[] hash = hmacsha256.ComputeHash(payloadBytes);

            return BitConverter.ToString(hash).Replace("-", string.Empty).ToLower();
        }
    }

    public WithdrawalRecord Withdraw(string currency, decimal amount, string address, string network, bool waitForApprove = true)
    {
        if(!waitForApprove)
        {
            var id = this.Wallet.WithdrawCurrency(currency, amount, address, network);

            return new WithdrawalRecord
            {
                TxId = id,
                RequestedAmount = amount,
                WaitedForApproval = waitForApprove,
            };
        }
        else
        {
            (var id, var hash) = this.Wallet.WithdrawAndWaitForSent(currency, amount, address, network);

            return new WithdrawalRecord
            {
                TxId = id,
                RequestedAmount = amount,
                WaitedForApproval = waitForApprove,
                TxHash = hash
            };
        }
    }

    public async Task<WithdrawalRecord> WithdrawAsync(string currency, decimal amount, string address, string network, bool waitForApprove = true)
    {
        if (!waitForApprove)
        {
            var id = await this.Wallet.WithdrawCurrencyAsync(currency, amount, address, network);

            return new WithdrawalRecord
            {
                TxId = id,
                RequestedAmount = amount,
                WaitedForApproval = waitForApprove,
            };
        }
        else
        {
            (var id, var hash) = await this.Wallet.WithdrawAndWaitForSentAsync(currency, amount, address, network);

            return new WithdrawalRecord
            {
                TxId = id,
                RequestedAmount = amount,
                WaitedForApproval = waitForApprove,
                TxHash = hash
            };
        }
    }

    public sealed override decimal GetWithdrawalFee(string currency, string network)
    {
        var coinInfo = Wallet.GetCoinInformation(currency, network);

        return coinInfo.NetworkList
            .Where(x => x.NetworkName.ToLower() == network.ToLower())
            .First()
            .WithdrawFee;
    }

    public sealed override async Task<decimal> GetWithdrawalFeeAsync(string currency, string network)
    {
        var coinInfo = await Wallet.GetCoinInformationAsync(currency, network);

        return coinInfo.NetworkList
            .Where(x => x.NetworkName.ToLower() == network.ToLower())
            .First()
            .WithdrawFee;
    }

    public sealed override int QueryWithdrawalPrecision(string currency, string network)
    {
        var coinInfo = Wallet.GetCoinInformation(currency, network);

        var decimals = coinInfo.NetworkList
            .Where(x => x.NetworkName.ToLower() == network.ToLower())
            .First()
            .WithdrawIntegerMultiple;

        return - (int)Math.Log10((double)decimals);
    }

    public sealed override async Task<int> QueryWithdrawalPrecisionAsync(string currency, string network)
    {
        var coinInfo = await Wallet.GetCoinInformationAsync(currency, network);

        var decimals = coinInfo.NetworkList
            .Where(x => x.NetworkName.ToLower() == network.ToLower())
            .First()
            .WithdrawIntegerMultiple;

        return - (int)Math.Log10((double)decimals);
    }

    public sealed override decimal QueryWithdrawalMinAmount(string currency, string network)
    {
        var data = Wallet.GetCoinInformation(currency, network);

        return data.NetworkList.Single().WithdrawMin;
    }

    public sealed override async Task<decimal> QueryWithdrawalMinAmountAsync(string currency, string network)
    {
        var data = await Wallet.GetCoinInformationAsync(currency, network);

        return data.NetworkList.Single().WithdrawMin;
    }

    public sealed override string GetDepositAddress(string currency, string network)
    {
        var address = Wallet.GetDepositAddress(currency, network);

        return address.Address;
    }

    public sealed override async Task<string> GetDepositAddressAsync(string currency, string network)
    {
        var address = await Wallet.GetDepositAddressAsync(currency, network);

        return address.Address;
    }

    public sealed override decimal ApproveReceiving(string hash)
    {
        return Wallet.WaitForReceive(hash);
    }

    public sealed override async Task<decimal> ApproveReceivingAsync(string hash)
    {
        return await Wallet.WaitForReceiveAsync(hash);
    }

    public sealed override decimal GetBalance(string currency)
    {
        var data = Wallet.GetUserAsset("ETH");

        return data.Free;
    }

    public sealed override async Task<decimal> GetBalanceAsync(string currency)
    {
        var data = await Wallet.GetUserAssetAsync("ETH");

        return data.Free;
    }
}

