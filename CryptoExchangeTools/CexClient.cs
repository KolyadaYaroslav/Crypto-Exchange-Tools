using System;
using System.Net;
using RestSharp;

namespace CryptoExchangeTools;

public abstract class CexClient : IDisposable
{
    public Guid Guid = Guid.NewGuid();

    protected readonly string ApiKey;
    protected readonly string ApiSecret;

    public WebProxy? Proxy;

    internal readonly RestClient restClient;

    public CexClient(string apiKey, string apiSecret, string url, WebProxy? proxy)
    {
        ApiKey = apiKey;
        ApiSecret = apiSecret;
        Proxy = proxy;

        var restOptions = new RestClientOptions()
        {
            BaseUrl = new Uri(url),
            Proxy = Proxy
        };

        restClient = new RestClient(restOptions);

        TryLogin();
    }

    protected abstract void TryLogin();

    public void Dispose()
    {
        restClient.Dispose();
        GC.SuppressFinalize(this);
    }
}

