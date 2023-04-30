using System.Net;
using System.Security.Cryptography;
using System.Text;
using CryptoExchangeTools.Models.ICex;
using CryptoExchangeTools.Requests.GateIoRequests;
using RestSharp;

namespace CryptoExchangeTools;

public partial class GateIoClient : CexClient
{
    private const string Url = "https://api.gateio.ws";

    public Wallet Wallet { get; }

    public Withdrawal Withdrawal { get; }

    public GateIoClient(string apiKey, string apiSecret, WebProxy? proxy = null) : base(apiKey, apiSecret, Url, proxy)
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

    internal sealed override void SignRequest(RestRequest request)
    {
        var ts = DateTimeOffset.UtcNow.ToUnixTimeSeconds();

        request.AddHeader("KEY", ApiKey);
        request.AddHeader("Timestamp", ts);
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

            return System.Convert.ToHexString(hash);
        }
    }

    private static string BuildPreHashString(RestRequest request, long ts)
    {
        var method = request.Method.ToString().ToUpper();

        var queryString = request.GetQueryString();
        var path = "/" + request.Resource + (!string.IsNullOrEmpty(queryString) ? $"?{queryString}" : null);

        var bodyParams = request.Parameters.Where(x => x.Type == ParameterType.RequestBody);
        var body = bodyParams.Any() ? bodyParams.Single().Value?.ToString() : null;

        string? hashedBody = string.Empty;

        byte[] payloadBytes = Encoding.UTF8.GetBytes(string.IsNullOrEmpty(body) ? "" : body);
        byte[] hash = SHA512.HashData(payloadBytes);

        hashedBody = System.Convert.ToHexString(hash);

        return method + "\n" + path + "\n" + queryString + "\n" + hashedBody + "\n" + ts;
    }

    public sealed override WithdrawalRecord Withdraw(string currency, decimal amount, string address, string network, bool waitForApprove = true)
    {
        throw new NotImplementedException();
    }

    public async sealed override Task<WithdrawalRecord> WithdrawAsync(string currency, decimal amount, string address, string network, bool waitForApprove = true)
    {
        throw new NotImplementedException();
    }

    internal sealed override decimal CustomReceive(string hash, int timeoutMin = 3600)
    {
        throw new NotImplementedException();
    }

    internal sealed override async Task<decimal> CustomReceiveAsync(string hash, int timeoutMin = 3600)
    {
        throw new NotImplementedException();
    }
}

