﻿using System.Net;
using CryptoExchangeTools.Models.ICex;
using RestSharp;

namespace CryptoExchangeTools;

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously

public abstract class CexClient : IDisposable
{
    private string url;

    protected readonly string ApiKey;
    protected readonly string ApiSecret;
    protected readonly string? PassPhrase;

    private WebProxy? proxy;

    private RestClient restClient;

    public event EventHandler<string>? OnMessage;

    public virtual void Message(string message)
    {
        OnMessage?.Invoke(this, message);
    }

    public CexClient(string apiKey, string apiSecret, string url, WebProxy? proxy)
    {
        this.url = url;
        ApiKey = apiKey;
        ApiSecret = apiSecret;
        this.proxy = proxy;

        restClient = CreateRestClient();

        TryLogin();
    }

    public CexClient(string apiKey, string apiSecret, string passPhrase, string url, WebProxy? proxy)
    {
        this.url = url;
        ApiKey = apiKey;
        ApiSecret = apiSecret;
        PassPhrase = passPhrase;
        this.proxy = proxy;

        restClient = CreateRestClient();

        TryLogin();
    }

    protected virtual RestClient CreateRestClient()
    {
        var restOptions = new RestClientOptions()
        {
            BaseUrl = new Uri(url),
            Proxy = proxy
        };

        return new RestClient(restOptions);
    }

    protected virtual void TryLogin()
    {
    }

    public virtual void ChangeProxy(WebProxy? proxy)
    {
        var restOptions = new RestClientOptions()
        {
            BaseUrl = new Uri(url),
            Proxy = proxy
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

    #region ICex Methods

    public virtual decimal GetWithdrawalFee(string currency, string network)
    {
        throw new NotImplementedException();
    }

    public virtual async Task<decimal> GetWithdrawalFeeAsync(string currency, string network)
    {
        throw new NotImplementedException();
    }

    public virtual int QueryWithdrawalPrecision(string currency, string network)
    {
        throw new NotImplementedException();
    }

    public virtual async Task<int> QueryWithdrawalPrecisionAsync(string currency, string network)
    {
        throw new NotImplementedException();
    }

    public virtual decimal QueryWithdrawalMinAmount(string currency, string network)
    {
        throw new NotImplementedException();
    }

    public virtual async Task<decimal> QueryWithdrawalMinAmountAsync(string currency, string network)
    {
        throw new NotImplementedException();
    }

    public virtual string GetDepositAddress(string currency, string network)
    {
        throw new NotImplementedException();
    }

    public virtual async Task<string> GetDepositAddressAsync(string currency, string network)
    {
        throw new NotImplementedException();
    }

    public virtual decimal ApproveReceiving(string hash)
    {
        throw new NotImplementedException();
    }

    public virtual Task<decimal> ApproveReceivingAsync(string hash)
    {
        throw new NotImplementedException();
    }

    public virtual decimal GetBalance(string currency)
    {
        throw new NotImplementedException();
    }

    public virtual async Task<decimal> GetBalanceAsync(string currency)
    {
        throw new NotImplementedException();
    }

    #endregion

    public void Dispose()
    {
        restClient.Dispose();
        GC.SuppressFinalize(this);
    }
}

