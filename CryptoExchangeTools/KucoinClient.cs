using CryptoExchangeTools.Models.ICex;
using CryptoExchangeTools.Models.Kucoin;
using CryptoExchangeTools.Requests.KucoinRequests;
using Newtonsoft.Json;
using RestSharp;
using System.Globalization;
using System.Net;
using System.Security.Cryptography;
using System.Text;

namespace CryptoExchangeTools;

public class KucoinClient : CexClient, ICexClient
{
    #region Initialize

    private const string url = "https://api.kucoin.com";

    private readonly int apiKeyVersion;

    public Withdrawals Withdrawals { get; }

    public Currencies Currencies { get; }

    public Deposit Deposit { get; }

    public Account Account { get; }

    public KucoinClient(string apiKey, string apiSecret, string PassPhrase, WebProxy? proxy = null, int apiKeyVersion = 2) : base(apiKey, apiSecret, PassPhrase, url, proxy)
    {
        this.apiKeyVersion = apiKeyVersion;

        Withdrawals = new(this);

        Currencies = new(this);

        Deposit = new(this);

        Account = new(this);
    }

    #endregion Initialize

    #region Signature

    internal sealed override void SignRequest(RestRequest request)
    {
        var ts = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();

        request.AddHeader("KC-API-KEY", ApiKey);
        request.AddHeader("KC-API-SIGN", Sign(request, ts));
        request.AddHeader("KC-API-TIMESTAMP", ts.ToString());
        request.AddHeader("KC-API-PASSPHRASE", HashPassPhrase());
        request.AddHeader("KC-API-KEY-VERSION", apiKeyVersion.ToString());
    }

    private string Sign(RestRequest request, long ts)
    {
        var preHashString = BuildPreHashString(request, ts);

        var key = Encoding.UTF8.GetBytes(ApiSecret);

        using (var hmacsha256 = new HMACSHA256(key))
        {
            byte[] payloadBytes = Encoding.UTF8.GetBytes(preHashString);
            byte[] hash = hmacsha256.ComputeHash(payloadBytes);

            return System.Convert.ToBase64String(hash);
        }
    }

    private static string BuildPreHashString(RestRequest request, long ts)
    {
        string method = request.Method.ToString().ToUpper();

        string endpoint = BuildEndpointForSignature(request);

        var bodyParams = request.Parameters.Where(x => x.Type == ParameterType.RequestBody);
        var body = bodyParams.Any() ? bodyParams.Single().Value?.ToString() : null;

        return ts + method + endpoint + body;
    }

    private static string BuildEndpointForSignature(RestRequest request)
    {
        string endpoint = "/" + request.Resource;

        var quesryString = request.GetQueryString();

        if (!string.IsNullOrEmpty(quesryString))
            quesryString = '?' + quesryString;

        if (request.Method == Method.Get || request.Method == Method.Delete)
            return endpoint + quesryString;

        else
            return endpoint;
    }

    private string HashPassPhrase()
    {
        if (string.IsNullOrEmpty(PassPhrase))
            throw new Exception("PassPhrase is not set for Kucoin client.");

        if (apiKeyVersion == 1)
        {
            return PassPhrase;
        }
        else if(apiKeyVersion == 2)
        {
            var key = Encoding.UTF8.GetBytes(ApiSecret);

            using (var hmacsha256 = new HMACSHA256(key))
            {
                byte[] payloadBytes = Encoding.UTF8.GetBytes(PassPhrase);
                byte[] hash = hmacsha256.ComputeHash(payloadBytes);

                return System.Convert.ToBase64String(hash);
            }
        }
        else
        {
            throw new Exception("Api key version is not supported.");
        }
    }

    #endregion Signature

    #region Global Methods

    protected sealed override T DeserializeResponse<T>(RestResponse response)
    {
        var baseResponse = response.Deserialize<BaseResponse>();

        if (baseResponse.Code != 200000)
            throw new Exception($"[{baseResponse.Code}] {baseResponse.Message}");

        return baseResponse.ParseData<T>();
    }

    public WithdrawalRecord Withdraw(string currency, decimal amount, string address, string network, bool waitForApprove = true)
    {
        if(waitForApprove)
        {
            var result = Withdrawals.WithdrawAndWaitForSent(currency, address, amount, network);

            return new WithdrawalRecord
            {
                TxId = result.Id,
                RequestedAmount = amount,
                WaitedForApproval = waitForApprove,
                TxHash = result.WalletTxId
            };
        }
        else
        {
            var result = Withdrawals.WithdrawCurrency(currency, address, amount, network);

            return new WithdrawalRecord
            {
                RequestedAmount = amount,
                WaitedForApproval = waitForApprove,
                TxId = result.WithdrawalId
            };
        }
    }

    public async Task<WithdrawalRecord> WithdrawAsync(string currency, decimal amount, string address, string network, bool waitForApprove = true)
    {
        if (waitForApprove)
        {
            var result = await Withdrawals.WithdrawAndWaitForSentAsync(currency, address, amount, network);

            return new WithdrawalRecord
            {
                TxId = result.Id,
                RequestedAmount = amount,
                WaitedForApproval = waitForApprove,
                TxHash = result.WalletTxId
            };
        }
        else
        {
            var result = await Withdrawals.WithdrawCurrencyAsync(currency, address, amount, network);

            return new WithdrawalRecord
            {
                RequestedAmount = amount,
                WaitedForApproval = waitForApprove,
                TxId = result.WithdrawalId
            };
        }
    }

    public sealed override decimal GetWithdrawalFee(string currency, string network)
    {
        var quota = Withdrawals.GetWithdrawalQuotas(currency, network);

        return quota.WithdrawMinFee;
    }

    public sealed override async Task<decimal> GetWithdrawalFeeAsync(string currency, string network)
    {
        var quota = await Withdrawals.GetWithdrawalQuotasAsync(currency, network);

        return quota.WithdrawMinFee;
    }

    public sealed override int QueryWithdrawalPrecision(string currency, string network)
    {
        var quota = Withdrawals.GetWithdrawalQuotas(currency, network);

        return quota.Precision;
    }

    public sealed override async Task<int> QueryWithdrawalPrecisionAsync(string currency, string network)
    {
        var quota = await Withdrawals.GetWithdrawalQuotasAsync(currency, network);

        return quota.Precision;
    }

    public sealed override decimal QueryWithdrawalMinAmount(string currency, string network)
    {
        var quota = Withdrawals.GetWithdrawalQuotas(currency, network);

        return quota.WithdrawMinSize;
    }

    public sealed override async Task<decimal> QueryWithdrawalMinAmountAsync(string currency, string network)
    {
        var quota = await Withdrawals.GetWithdrawalQuotasAsync(currency, network);

        return quota.WithdrawMinSize;
    }

    public sealed override string GetDepositAddress(string currency, string network)
    {
        var address = Deposit.GetDepositAddress(currency, network);

        return address.Address;
    }

    public sealed override async Task<string> GetDepositAddressAsync(string currency, string network)
    {
        var address = await Deposit.GetDepositAddressAsync(currency, network);

        return address.Address;
    }

    #endregion Global Methods
}

