using System.Net;
using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace CryptoExchangeTools;

public class BinanceClient : CexClient
{
	private const string Url = "https://api.binance.com";

    public BinanceClient(string apiKey, string apiSecret, WebProxy? proxy = null) : base(apiKey, apiSecret, Url, proxy)
    {
    }

    protected override void TryLogin()
    {
        var request = new RestRequest("sapi/v1/system/status");
        var response = restClient.ExecuteGet(request);
        if (!response.IsSuccessful)
            throw new ConnectionNotSetException("Couldn't connect to Binance Server.", response.StatusCode, response.Content);

        CheckAccountStatus();
    }

    private void CheckAccountStatus()
    {
        var request = new RestRequest("sapi/v1/account/status");

        SignRequest(request);

        var response = restClient.ExecuteGet(request);

        if (!response.IsSuccessful || response.Content is null)
            throw new BadAccountStatusException("Couldn't connect to Binance Account.", response.StatusCode, response.Content);

        dynamic json = JObject.Parse(response.Content);
        if (json["data"] != "Normal")
            throw new BadAccountStatusException("Couldn't connect to Binance Account.", response.StatusCode, response.Content);
    }

    internal void SignRequest(RestRequest request)
    {
        request.AddHeader("X-MBX-APIKEY", ApiKey);
        request.AddParameter("timestamp", DateTimeOffset.UtcNow.ToUnixTimeMilliseconds());

        var signature = Sign(request.GetQueryString());

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
}

