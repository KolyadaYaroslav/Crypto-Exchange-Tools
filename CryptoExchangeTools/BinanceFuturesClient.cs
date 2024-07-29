using CryptoExchangeTools.Requests.BinanceRequests;
using CryptoExchangeTools.Requests.BinanceRequests.Futures;
using CryptoExchangeTools.Requests.BinanceRequests.Futures.USDM;
using RestSharp;
using System.Net;
using System.Security.Cryptography;
using System.Text;

namespace CryptoExchangeTools
{
	public class BinanceFuturesClient : CexClient
    {
        private const string Url = "https://fapi.binance.com";


        public USDM USDM { get; }

        public BinanceFuturesClient(string apiKey, string apiSecret, WebProxy? proxy = null) : base(apiKey, apiSecret, Url, proxy)
        {
            USDM = new(this);
        }

        protected sealed override void SignRequest(RestRequest request)
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
    }
}

