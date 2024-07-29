using System.Net;
using CryptoExchangeTools.Models.ICex;
using RestSharp;

namespace CryptoExchangeTools;

public abstract class CexClient : IDisposable
{
    private readonly string url;

    protected readonly string ApiKey;
    protected readonly string ApiSecret;
    protected readonly string? PassPhrase;

    private readonly WebProxy? proxy;

    private RestClient restClient;

    public event EventHandler<string>? OnMessage;

    public virtual void Message(string message)
    {
        OnMessage?.Invoke(this, message);
    }

    public CexClient(string apiKey, string apiSecret, string url, WebProxy? proxy, bool checkLogin = false)
    {
        this.url = url;
        ApiKey = apiKey;
        ApiSecret = apiSecret;
        this.proxy = proxy;

        restClient = CreateRestClient();

        if(checkLogin)
            TryLogin();
    }

    public CexClient(string apiKey, string apiSecret, string passPhrase, string url, WebProxy? proxy, bool checkLogin = false)
    {
        this.url = url;
        ApiKey = apiKey;
        ApiSecret = apiSecret;
        PassPhrase = passPhrase;
        this.proxy = proxy;

        restClient = CreateRestClient();

        if (checkLogin)
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

    protected abstract void SignRequest(RestRequest request);

    protected virtual T DeserializeResponse<T>(RestResponse response)
    {
        return response.Deserialize<T>();
    }

    internal virtual T ExecuteRequest<T>(RestRequest request, bool needSign = true)
    {
        if(needSign)
            SignRequest(request);

        var response = restClient.Execute<T>(request);

        return DeserializeResponse<T>(response);
    }

    internal virtual async Task<T> ExecuteRequestAsync<T>(RestRequest request, bool needSign = true)
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

        return restClient.Execute<int>(request);
    }

    internal virtual async Task<RestResponse> ExecuteRequestWithoutResponseAsync(RestRequest request, bool needSign = true)
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

    public virtual Task<decimal> GetWithdrawalFeeAsync(string currency, string network)
    {
        throw new NotImplementedException();
    }

    public virtual int QueryWithdrawalPrecision(string currency, string network)
    {
        throw new NotImplementedException();
    }

    public virtual Task<int> QueryWithdrawalPrecisionAsync(string currency, string network)
    {
        throw new NotImplementedException();
    }

    public virtual decimal QueryWithdrawalMinAmount(string currency, string network)
    {
        throw new NotImplementedException();
    }

    public virtual Task<decimal> QueryWithdrawalMinAmountAsync(string currency, string network)
    {
        throw new NotImplementedException();
    }

    public virtual string GetDepositAddress(string currency, string network)
    {
        throw new NotImplementedException();
    }

    public virtual Task<string> GetDepositAddressAsync(string currency, string network)
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

    public virtual Task<decimal> GetBalanceAsync(string currency)
    {
        throw new NotImplementedException();
    }

    public virtual decimal GetMarketPrice(string symbol)
    {
        throw new NotImplementedException();
    }

    public virtual Task<decimal> GetMarketPriceAsync(string symbol)
    {
        throw new NotImplementedException();
    }

    public virtual decimal GetAmountIn(string currencyIn, string currencyOut, decimal amountOut, decimal slippage = 0.998m)
    {
        throw new NotImplementedException();
    }

    public virtual Task<decimal> GetAmountInAsync(string currencyIn, string currencyOut, decimal amountOut, decimal slippage = 0.998m)
    {
        throw new NotImplementedException();
    }

    public virtual decimal FlattenOrderAmount(string symbol, decimal amount, int stepSizeDown = 0)
    {
        throw new NotImplementedException();
    }

    public virtual Task<decimal> FlattenOrderAmountAsync(string symbol, decimal amount, int stepSizeDown = 0)
    {
        throw new NotImplementedException();
    }

    public virtual (decimal, decimal) ForcedMarketOrder(string baseCurrency, string quoteCurrency, OrderDirection direction, decimal amount, CalculationBase calculationBase = CalculationBase.Base)
    {
        throw new NotImplementedException();
    }

    public virtual Task<(decimal, decimal)> ForcedMarketOrderAsync(string baseCurrency, string quoteCurrency, OrderDirection direction, decimal amount, CalculationBase calculationBase = CalculationBase.Base)
    {
        throw new NotImplementedException();
    }

    public virtual decimal GetMinOrderSizeForPair(string baseCurrency, string quoteCurrency, CalculationBase calculationBase = CalculationBase.Base)
    {
        throw new NotImplementedException();
    }

    public virtual Task<decimal> GetMinOrderSizeForPairAsync(string baseCurrency, string quoteCurrency, CalculationBase calculationBase = CalculationBase.Base)
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

