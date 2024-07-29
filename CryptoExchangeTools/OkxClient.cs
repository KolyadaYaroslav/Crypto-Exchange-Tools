using System.Globalization;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using CryptoExchangeTools.Models.ICex;
using CryptoExchangeTools.Models.Okx;
using CryptoExchangeTools.Requests.OkxRequests;
using RestSharp;

namespace CryptoExchangeTools;

public class OkxClient : CexClient, ICexClient
{
    private const string Url = "https://www.okx.com";

    public Account Account { get; }

    public Funding Funding { get; }

    public SubAccount SubAccount { get; }

    public Trade Trade { get; }

    public PublicData PublicData { get; }

    public Market Market { get; }

    public OkxClient(string apiKey, string apiSecret, string passPhrase, WebProxy? proxy = null) : base(apiKey, apiSecret, passPhrase, Url, proxy)
    {
        Account = new(this);
        Funding = new(this);
        SubAccount = new(this);
        Trade = new(this);
        PublicData = new(this);
        Market = new(this);
    }

    protected sealed override void TryLogin()
    {
        var request = new RestRequest("api/v5/system/status");

        var response = ExecuteRequestWithoutResponse(request, false);

        if (!response.IsSuccessful)
            throw new ConnectionNotSetException("Couldn't connect to Okx Server.", response.StatusCode, response.Content);
    }

    protected sealed override void SignRequest(RestRequest request)
    {
        if (string.IsNullOrEmpty(PassPhrase))
            throw new Exception("PassPhrase is not set for Okx client.");

        var ts = DateTimeOffset.UtcNow.ToString("yyyy-MM-ddTHH:mm:ssZ", CultureInfo.InvariantCulture);

        request.AddHeader("OK-ACCESS-KEY", ApiKey);
        request.AddHeader("OK-ACCESS-SIGN", Sign(request, ts));
        request.AddHeader("OK-ACCESS-TIMESTAMP", ts);
        request.AddHeader("OK-ACCESS-PASSPHRASE", PassPhrase);
    }

    private string Sign(RestRequest request, string ts)
    {
        var bodyParams = request.Parameters.Where(x => x.Type == ParameterType.RequestBody);
        var parameters = bodyParams as Parameter[] ?? bodyParams.ToArray();
        var body = parameters.Any() ? parameters.Single().Value?.ToString() : null;

        var queryString = request.GetQueryString();
        var path = request.Resource + (!string.IsNullOrEmpty(queryString) ? $"?{queryString}" : null);

        var method = request.Method.ToString().ToUpper();

        var preHashString = ts + method + "/" + path + body;

        var key = Encoding.UTF8.GetBytes(ApiSecret);

        using var hmacsha256 = new HMACSHA256(key);
        byte[] payloadBytes = Encoding.UTF8.GetBytes(preHashString);
        byte[] hash = hmacsha256.ComputeHash(payloadBytes);

        return Convert.ToBase64String(hash);
    }

    protected sealed override T DeserializeResponse<T>(RestResponse response)
    {
        return response
            .Deserialize<BaseResponse>()
            .ParseData<T>();
    }

    /// <summary>
    /// Okx has weird behaviour with networks, this may help! E.g. You have USDT on arbitrum, but you write USDT on ETH-Arbitrum One, this will format network name to USDT-Arbitrum One.
    /// </summary>
    /// <param name="currency"></param>
    /// <param name="network"></param>
    /// <returns>Formatted network name.</returns>
    public static string FormatNetworkName(string currency, string network)
    {
        var rawChain = network.Split('-')[1];

        return $"{currency}-{rawChain}";
    }

    public WithdrawalRecord Withdraw(string currency, decimal amount, string address, string network, bool waitForApprove = true)
    {
        if(waitForApprove)
        {
            var result = Funding.WithdrawAndWaitForSent(currency, amount, address, network);

            return new WithdrawalRecord
            {
                TxId = result.WdId,
                RequestedAmount = amount,
                WaitedForApproval = waitForApprove,
                TxHash = result.TxId
            };
        }
        else
        {
            var result = Funding.WithdrawCurrency(currency, amount, address, network);

            return new WithdrawalRecord
            {
                TxId = result.Single().WdId,
                RequestedAmount = amount,
                WaitedForApproval = waitForApprove
            };
        }
    }

    public async Task<WithdrawalRecord> WithdrawAsync(string currency, decimal amount, string address, string network, bool waitForApprove = true)
    {
        if (waitForApprove)
        {
            var result = await Funding.WithdrawAndWaitForSentAsync(currency, amount, address, network);

            return new WithdrawalRecord
            {
                TxId = result.WdId,
                RequestedAmount = amount,
                WaitedForApproval = waitForApprove,
                TxHash = result.TxId
            };
        }
        else
        {
            var result = await Funding.WithdrawCurrencyAsync(currency, amount, address, network);

            return new WithdrawalRecord
            {
                TxId = result.Single().WdId,
                RequestedAmount = amount,
                WaitedForApproval = waitForApprove
            };
        }
    }

    public sealed override decimal GetWithdrawalFee(string currency, string network)
    {
        var data = Funding.GetSingleCurrency(currency, network);

        return data.MinFee;
    }

    public sealed override async Task<decimal> GetWithdrawalFeeAsync(string currency, string network)
    {
        var data = await Funding.GetSingleCurrencyAsync(currency, network);

        return data.MinFee;
    }

    public sealed override int QueryWithdrawalPrecision(string currency, string network)
    {
        var data = Funding.GetSingleCurrency(currency, network);

        return data.WdTickSz;
    }

    public sealed override async Task<int> QueryWithdrawalPrecisionAsync(string currency, string network)
    {
        var data = await Funding.GetSingleCurrencyAsync(currency, network);

        return data.WdTickSz;
    }

    public sealed override decimal QueryWithdrawalMinAmount(string currency, string network)
    {
        var data = Funding.GetSingleCurrency(currency, network);

        return data.MinWd;
    }

    public sealed override async Task<decimal> QueryWithdrawalMinAmountAsync(string currency, string network)
    {
        var data = await Funding.GetSingleCurrencyAsync(currency, network);

        return data.MinWd;
    }

    public sealed override string GetDepositAddress(string currency, string network)
    {
        var address = Funding.GetDepositAddress(currency, network);

        return address.Addr;
    }

    public sealed override async Task<string> GetDepositAddressAsync(string currency, string network)
    {
        var address = await Funding.GetDepositAddressAsync(currency, network);

        return address.Addr;
    }

    public sealed override decimal GetBalance(string currency)
    {
        var data = Funding.GetFundingBalance(new List<string> { currency });

        if (!data.Any())
            return 0;

        return data.Single().AvailBal;
    }

    public sealed override async Task<decimal> GetBalanceAsync(string currency)
    {
        var data = await Funding.GetFundingBalanceAsync(new List<string> { currency });

        if (!data.Any())
            return 0;

        return data.Single().AvailBal;
    }

    public sealed override decimal ApproveReceiving(string hash)
    {
        return Funding.WaitForReceive(hash);
    }

    public sealed override async Task<decimal> ApproveReceivingAsync(string hash)
    {
        return await Funding.WaitForReceiveAsync(hash);
    }

    public sealed override decimal GetMarketPrice(string symbol)
    {
        return Market.GetMarketPrice(symbol);
    }

    public sealed override async Task<decimal> GetMarketPriceAsync(string symbol)
    {
        return await Market.GetMarketPriceAsync(symbol);
    }

    public sealed override decimal GetAmountIn(string currencyIn, string currencyOut, decimal amountOut, decimal slippage = 0.998m)
    {
        bool reversed = false;

        string instId;

        try
        {
            instId = PublicData.GetInstrumentName(currencyIn, currencyOut, InstrumentType.SPOT);
        }
        catch (Exception ex) when (ex.Message == "Can't find instrument!")
        {
            instId = PublicData.GetInstrumentName(currencyOut, currencyIn, InstrumentType.SPOT);
            reversed = true;
        }

        decimal price = GetMarketPrice(instId);

        if (reversed)
            price = (1 / price);

        Message($"Price: {price}");

        return amountOut / slippage / price;
    }

    public sealed override async Task<decimal> GetAmountInAsync(string currencyIn, string currencyOut, decimal amountOut, decimal slippage = 0.998m)
    {
        bool reversed = false;

        string instId;

        try
        {
            instId = await PublicData.GetInstrumentNameAsync(currencyIn, currencyOut, InstrumentType.SPOT);
        }
        catch (Exception ex) when (ex.Message == "Can't find instrument!")
        {
            instId = await PublicData.GetInstrumentNameAsync(currencyOut, currencyIn, InstrumentType.SPOT);
            reversed = true;
        }

        decimal price = await GetMarketPriceAsync(instId);

        if (reversed)
            price = (1 / price);

        Message($"Price: {price}");

        return amountOut / slippage / price;
    }

    public sealed override (decimal, decimal) ForcedMarketOrder(string baseCurrency, string quoteCurrency, OrderDirection direction, decimal amount, CalculationBase calculationBase = CalculationBase.Base)
    {
        string instId;

        try
        {
            instId = PublicData.GetInstrumentName(baseCurrency, quoteCurrency, InstrumentType.SPOT);
        }
        catch (Exception ex) when (ex.Message == "Can't find instrument!")
        {
            instId = PublicData.GetInstrumentName(quoteCurrency, baseCurrency, InstrumentType.SPOT);

            (quoteCurrency, baseCurrency) = (baseCurrency, quoteCurrency);

            direction = HelperMethods.ReverseOrderDirection(direction);

            calculationBase = HelperMethods.ReverseCalculationBase(calculationBase);
        }

        var orderSide = direction == OrderDirection.Buy ? OrderSide.buy : OrderSide.sell;

        amount = orderSide switch
        {
            OrderSide.buy when calculationBase == CalculationBase.Base => GetAmountIn(quoteCurrency, baseCurrency,
                amount),
            OrderSide.sell when calculationBase == CalculationBase.Quote => GetAmountIn(baseCurrency, quoteCurrency,
                amount),
            _ => amount
        };

        var executedQuantity = Trade.TradeFromFunding(instId, orderSide, amount);

        return (executedQuantity, amount);
    }

    public sealed override async Task<(decimal, decimal)> ForcedMarketOrderAsync(string baseCurrency, string quoteCurrency, OrderDirection direction, decimal amount, CalculationBase calculationBase = CalculationBase.Base)
    {
        string instId;

        try
        {
            instId = await PublicData.GetInstrumentNameAsync(baseCurrency, quoteCurrency, InstrumentType.SPOT);
        }
        catch (Exception ex) when (ex.Message == "Can't find instrument!")
        {
            instId = await PublicData.GetInstrumentNameAsync(quoteCurrency, baseCurrency, InstrumentType.SPOT);

            (quoteCurrency, baseCurrency) = (baseCurrency, quoteCurrency);

            direction = HelperMethods.ReverseOrderDirection(direction);

            calculationBase = HelperMethods.ReverseCalculationBase(calculationBase);
        }

        var orderSide = direction == OrderDirection.Buy ? OrderSide.buy : OrderSide.sell;

        amount = orderSide switch
        {
            OrderSide.buy when calculationBase == CalculationBase.Base => await GetAmountInAsync(quoteCurrency,
                baseCurrency, amount),
            OrderSide.sell when calculationBase == CalculationBase.Quote => await GetAmountInAsync(baseCurrency,
                quoteCurrency, amount),
            _ => amount
        };

        var executedQuantity = await Trade.TradeFromFundingAsync(instId, orderSide, amount);
        
        return (executedQuantity, amount);
    }

    public sealed override decimal GetMinOrderSizeForPair(string baseCurrency, string quoteCurrency, CalculationBase calculationBase = CalculationBase.Base)
    {
        string instId;

        try
        {
            instId = PublicData.GetInstrumentName(baseCurrency, quoteCurrency, InstrumentType.SPOT);
        }
        catch (Exception ex) when (ex.Message == "Can't find instrument!")
        {
            instId = PublicData.GetInstrumentName(quoteCurrency, baseCurrency, InstrumentType.SPOT);

            (quoteCurrency, baseCurrency) = (baseCurrency, quoteCurrency);

            calculationBase = HelperMethods.ReverseCalculationBase(calculationBase);
        }

        var data = PublicData.GetSingleInstrument(instId, InstrumentType.SPOT);

        var amount = data.MinSz;

        if (calculationBase == CalculationBase.Quote)
            return GetAmountIn(quoteCurrency, baseCurrency, amount, 1);

        return amount;
    }

    public decimal GetDepositMinimum(string currency, string network)
    {
        var data =  Funding.GetCurrencies(new List<string> { currency });

        return data.Where(x => x.Chain == network).Single().MinDep;
    }

    public async Task<decimal> GetDepositMinimumAsync(string currency, string network)
    {
        var data = await Funding.GetCurrenciesAsync(new List<string> { currency });

        return data.Where(x => x.Chain == network).Single().MinDep;
    }
}

