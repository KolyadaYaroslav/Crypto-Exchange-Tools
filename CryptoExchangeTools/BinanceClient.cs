using System.Net;
using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json.Linq;
using RestSharp;
using CryptoExchangeTools.BinanceRequests.Wallet;

namespace CryptoExchangeTools;

public class BinanceClient : CexClient
{
	private const string Url = "https://api.binance.com";

    /// <summary>
    /// Initializes a client with account information and checks connection to servers.
    /// </summary>
    /// <param name="apiKey">Your api key</param>
    /// <param name="apiSecret">Your api secret</param>
    /// <param name="proxy">Proxy to be used with client</param>
    public BinanceClient(string apiKey, string apiSecret, WebProxy? proxy = null) : base(apiKey, apiSecret, Url, proxy)
    {
    }

    protected sealed override void TryLogin()
    {
        Message("Checking Server.");

        var request = new RestRequest("sapi/v1/system/status");
        var response = restClient.ExecuteGet(request);

        if (!response.IsSuccessful)
            throw new ConnectionNotSetException("Couldn't connect to Binance Server.", response.StatusCode, response.Content);

        Message("Server OK.");

        CheckAccountStatus();
    }

    private void CheckAccountStatus()
    {
        Message("Checking Account status.");

        var request = new RestRequest("sapi/v1/account/status");

        SignRequest(request);

        var response = restClient.ExecuteGet(request);

        if (!response.IsSuccessful || response.Content is null)
            throw new BadAccountStatusException("Couldn't connect to Binance Account.", response.StatusCode, response.Content);

        dynamic json = JObject.Parse(response.Content);
        if (json["data"] != "Normal")
            throw new BadAccountStatusException("Couldn't connect to Binance Account.", response.StatusCode, response.Content);

        Message("Account status OK.");
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

    internal sealed override string CustomWithdraw(
        string coin, decimal amount, string address, string network, string? addressTag = null, int walletType = -1)
    {
        return this.WithdrawAndWaitForSent(coin, amount, address, network, addressTag, walletType);
    }

    internal sealed override async Task<string> CustomWithdrawAsync(
        string coin, decimal amount, string address, string network, string? addressTag = null, int walletType = -1)
    {
        return await this.WithdrawAndWaitForSentAsync(coin, amount, address, network, addressTag, walletType);
    }

    internal sealed override decimal CustomReceive(string hash, int timeoutMin = 3600)
    {
        var attempts = 0;

        while (attempts < timeoutMin)
        {
            var history = this.GetDepositHistory(txId: hash);

            if (history.Any())
            {
                Message($"Received {history.Single().amount} {history.Single().coin}");
                return history.Single().amount;
            }

            Message("Waiting Receive.");

            attempts += 10;

            Thread.Sleep(10000);
        }

        throw new TimeoutException("Timeout recahed while waiting for receiving");
    }

    internal sealed override async Task<decimal> CustomReceiveAsync(string hash, int timeoutMin = 3600)
    {
        var attempts = 0;

        while(attempts < timeoutMin)
        {
            var history = await this.GetDepositHistoryAsync(txId: hash);

            if (history.Any())
            {
                Message($"Received {history.Single().amount} {history.Single().coin}");
                return history.Single().amount;
            }

            Message("Waiting Receive.");

            attempts +=10;

            await Task.Delay(10000);
        }

        throw new TimeoutException("Timeout recahed while waiting for receiving");
    }
}

