using System.Net;
using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json.Linq;
using RestSharp;
using CryptoExchangeTools.Requests.BinanceRequests;
using CryptoExchangeTools.Models.ICex;

namespace CryptoExchangeTools;

public class BinanceClient : CexClient
{
	private const string Url = "https://api.binance.com";

    public Wallet Wallet { get; }

    /// <summary>
    /// Initializes a client with account information and checks connection to servers.
    /// </summary>
    /// <param name="apiKey">Your api key</param>
    /// <param name="apiSecret">Your api secret</param>
    /// <param name="proxy">Proxy to be used with client</param>
    public BinanceClient(string apiKey, string apiSecret, WebProxy? proxy = null) : base(apiKey, apiSecret, Url, proxy)
    {
        Wallet = new(this);
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

    public sealed override WithdrawalRecord Withdraw(string currency, decimal amount, string address, string network, bool waitForApprove = true)
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

    public async sealed override Task<WithdrawalRecord> WithdrawAsync(string currency, decimal amount, string address, string network, bool waitForApprove = true)
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

    public sealed override decimal CustomReceive(string hash, int timeoutMin = 3600)
    {
        var attempts = 0;

        while (attempts < timeoutMin)
        {
            var history = this.Wallet.GetDepositHistory(txId: hash);

            if (history.Any())
            {
                Message($"Received {history.Single().Amount} {history.Single().Coin}");
                return history.Single().Amount;
            }

            Message("Waiting Receive.");

            attempts += 10;

            Thread.Sleep(10000);
        }

        throw new TimeoutException("Timeout recahed while waiting for receiving");
    }

    public sealed override async Task<decimal> CustomReceiveAsync(string hash, int timeoutMin = 3600)
    {
        var attempts = 0;

        while(attempts < timeoutMin)
        {
            var history = await this.Wallet.GetDepositHistoryAsync(txId: hash);

            if (history.Any())
            {
                Message($"Received {history.Single().Amount} {history.Single().Coin}");
                return history.Single().Amount;
            }

            Message("Waiting Receive.");

            attempts +=10;

            await Task.Delay(10000);
        }

        throw new TimeoutException("Timeout recahed while waiting for receiving");
    }
}

