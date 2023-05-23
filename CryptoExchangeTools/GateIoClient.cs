using System.Net;
using System.Security.Cryptography;
using System.Text;
using CryptoExchangeTools.Models.ICex;
using CryptoExchangeTools.Requests.GateIoRequests;
using RestSharp;

namespace CryptoExchangeTools;

public class GateIoClient : CexClient, ICexClient
{
    #region Initialize

    private const string url = "https://api.gateio.ws";

    public Wallet Wallet { get; }

    public Withdrawal Withdrawal { get; }

    public GateIoClient(string apiKey, string apiSecret, WebProxy? proxy = null) : base(apiKey, apiSecret, url, proxy)
    {
        Wallet = new Wallet(this);
        Withdrawal = new Withdrawal(this);
    }

    protected sealed override void TryLogin()
    {
        var request = new RestRequest("api/v4/spot/time");
        var response = ExecuteRequestWithoutResponse(request, false);

        if (!response.IsSuccessful)
            throw new ConnectionNotSetException("Couldn't connect to GateIo Server.", response.StatusCode, response.Content);
    }

    #endregion Initialize

    #region Signature

    internal sealed override void SignRequest(RestRequest request)
    {
        var ts = DateTimeOffset.UtcNow.ToUnixTimeSeconds();

        request.AddHeader("KEY", ApiKey);
        request.AddHeader("Timestamp", ts.ToString());
        request.AddHeader("SIGN", Sign(request, ts));
    }

    private string Sign(RestRequest request, long ts)
    {
        var preHashString = BuildPreHashString(request, ts);

        var key = Encoding.UTF8.GetBytes(ApiSecret);

        using (var hmacsha512 = new HMACSHA512(key))
        {
            byte[] payloadBytes = Encoding.UTF8.GetBytes(preHashString);
            byte[] hash = hmacsha512.ComputeHash(payloadBytes);

            return System.Convert.ToHexString(hash).ToLower();
        }
    }

    private static string BuildPreHashString(RestRequest request, long ts)
    {
        string method = request.Method.ToString().ToUpper();

        string path = "/" + request.Resource;

        string queryString = request.GetQueryString() ?? "";

        string bodyHash = HashBody(request);

        return $"{method}\n{path}\n{queryString}\n{bodyHash}\n{ts}";
    }

    private static string HashBody(RestRequest request)
    {
        var bodyParams = request.Parameters.Where(x => x.Type == ParameterType.RequestBody);
        var body = bodyParams.Any() ? bodyParams.Single().Value?.ToString() : null;

        byte[] payloadBytes = Encoding.UTF8.GetBytes(body ?? "");
        byte[] hash = SHA512.HashData(payloadBytes);

        return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
    }

    #endregion Signature

    #region Global Methods

    public WithdrawalRecord Withdraw(string currency, decimal amount, string address, string network, bool waitForApprove = true)
    {
        if(!waitForApprove)
        {
            var result = Withdrawal.WithdrawCurrency(currency, amount, address, network);

            return new WithdrawalRecord
            {
                TxId = result.Id.ToString(),
                RequestedAmount = amount,
                WaitedForApproval = waitForApprove,
                TxHash = result.Txid
            };
        }
        else
        {
            var result = Withdrawal.WithdrawAndWaitForSent(currency, amount, address, network);

            return new WithdrawalRecord
            {
                TxId = result.Id.ToString(),
                RequestedAmount = amount,
                WaitedForApproval = waitForApprove,
                TxHash = result.Txid
            };
        }
    }

    public async Task<WithdrawalRecord> WithdrawAsync(string currency, decimal amount, string address, string network, bool waitForApprove = true)
    {
        if (!waitForApprove)
        {
            var result = await Withdrawal.WithdrawCurrencyAsync(currency, amount, address, network);

            return new WithdrawalRecord
            {
                TxId = result.Id.ToString(),
                RequestedAmount = amount,
                WaitedForApproval = waitForApprove,
                TxHash = result.Txid
            };
        }
        else
        {
            var result = await Withdrawal.WithdrawAndWaitForSentAsync(currency, amount, address, network);

            return new WithdrawalRecord
            {
                TxId = result.Id.ToString(),
                RequestedAmount = amount,
                WaitedForApproval = waitForApprove,
                TxHash = result.Txid
            };
        }
    }

    public decimal GetWithdrawalFee(string currency, string network)
    {
        throw new NotImplementedException();
    }

    public async Task<decimal> GetWithdrawalFeeAsync(string currency, string network)
    {
        throw new NotImplementedException();
    }

    public int QueryWithdrawalPrecision(string currency, string network)
    {
        throw new NotImplementedException();
    }

    public async Task<int> QueryWithdrawalPrecisionAsync(string currency, string network)
    {
        throw new NotImplementedException();
    }

    public sealed override decimal CustomReceive(string hash, int timeoutMin = 3600)
    {
        throw new NotImplementedException();
    }

    public sealed override async Task<decimal> CustomReceiveAsync(string hash, int timeoutMin = 3600)
    {
        throw new NotImplementedException();
    }

    #endregion Global Methods
}

