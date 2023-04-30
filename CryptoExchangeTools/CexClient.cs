using System;
using System.Net;
using CryptoExchangeTools.Models.ICex;
using RestSharp;

namespace CryptoExchangeTools;

public abstract class CexClient : ICexClient, IDisposable
{
    public Guid Guid = Guid.NewGuid();

    private string Url;

    protected readonly string ApiKey;
    protected readonly string ApiSecret;
    protected readonly string? PassPhrase;

    public WebProxy? Proxy;

    private RestClient restClient;

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

        restClient = CreateRestClient();

        TryLogin();
    }

    public CexClient(string apiKey, string apiSecret, string passPhrase, string url, WebProxy? proxy)
    {
        Url = url;
        ApiKey = apiKey;
        ApiSecret = apiSecret;
        PassPhrase = passPhrase;
        Proxy = proxy;

        restClient = CreateRestClient();

        TryLogin();
    }

    protected virtual RestClient CreateRestClient()
    {
        var restOptions = new RestClientOptions()
        {
            BaseUrl = new Uri(Url),
            Proxy = Proxy
        };

        return new RestClient(restOptions);
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

    internal abstract void SignRequest(RestRequest request);

    protected virtual T DeserializeResponse<T>(RestResponse response)
    {
        return response.Deserialize<T>();
    }

    internal virtual T ExecuteRequest<T>(RestRequest request, bool needSign = true)
    {
        if(needSign)
            SignRequest(request);

        var response = restClient.Execute(request);

        return DeserializeResponse<T>(response);
    }

    internal async virtual Task<T> ExecuteRequestAsync<T>(RestRequest request, bool needSign = true)
    {
        if (needSign)
            SignRequest(request);

        var response = await restClient.ExecuteAsync(request);

        return DeserializeResponse<T>(response);
    }

    internal virtual RestResponse ExecuteRequestWithoutResponse(RestRequest request, bool needSign = true)
    {
        if (needSign)
            SignRequest(request);

        return restClient.Execute(request);
    }

    internal async virtual Task<RestResponse> ExecuteRequestWithoutResponseAsync(RestRequest request, bool needSign = true)
    {
        if (needSign)
            SignRequest(request);

        return await restClient.ExecuteAsync(request);
    }


    public abstract WithdrawalRecord Withdraw(string currency, decimal amount, string address, string network, bool waitForApprove = true);

    public abstract Task<WithdrawalRecord> WithdrawAsync(string currency, decimal amount, string address, string network, bool waitForApprove = true);


    internal abstract decimal CustomReceive(string hash, int timeoutMin = 3600);

    internal abstract Task<decimal> CustomReceiveAsync(string hash, int timeoutMin = 3600);



    public void Dispose()
    {
        restClient.Dispose();
        GC.SuppressFinalize(this);
    }
}

