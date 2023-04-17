using System;
using System.Net;
using RestSharp;

namespace CryptoExchangeTools;

public abstract class CexClient : IDisposable
{
    public Guid Guid = Guid.NewGuid();

    private string Url;

    protected readonly string ApiKey;
    protected readonly string ApiSecret;

    public WebProxy? Proxy;

    internal RestClient restClient;

    public event EventHandler<string>? OnMessage;

    public virtual void Message(string message)
    {
        OnMessage?.Invoke(this, message);
    }

    public CexClient(string apiKey, string apiSecret, string url, WebProxy? proxy)
    {
        Url = url;
        ApiKey = apiKey;
        ApiSecret = apiSecret;
        Proxy = proxy;

        var restOptions = new RestClientOptions()
        {
            BaseUrl = new Uri(Url),
            Proxy = Proxy
        };

        restClient = new RestClient(restOptions);

        TryLogin();
    }

    protected abstract void TryLogin();

    public virtual void ChangeProxy(WebProxy? proxy)
    {
        var restOptions = new RestClientOptions()
        {
            BaseUrl = new Uri(Url),
            Proxy = Proxy
        };

        restClient = new RestClient(restOptions);
    }



    internal abstract string CustomWithdraw(string coin,
        decimal amount,
        string address,
        string network,
        string? addressTag = null,
        int walletType = -1);

    internal abstract Task<string> CustomWithdrawAsync(string coin,
        decimal amount,
        string address,
        string network,
        string? addressTag = null,
        int walletType = -1);

    internal abstract decimal CustomReceive(string hash, int timeoutMin = 3600);

    internal abstract Task<decimal> CustomReceiveAsync(string hash, int timeoutMin = 3600);



    public void Dispose()
    {
        restClient.Dispose();
        GC.SuppressFinalize(this);
    }

}

