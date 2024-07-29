using System.Net;
using System.Security.Cryptography;
using System.Text;
using CryptoExchangeTools.Models.Binance;
using CryptoExchangeTools.Models.ICex;
using CryptoExchangeTools.Requests.CommexRequests;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace CryptoExchangeTools;

public class CommexClient : CexClient
{
	private const string Url = "https://api.commex.com";

    public Wallet Wallet { get; }

    public Margin Margin { get; }

    public Trade Trade { get;}

    public Market Market { get; }

    public FuturesTransfer FuturesTransfer { get; }

    public BinanceFuturesClient Futures { get; }

    /// <summary>
    /// Initializes a client with account information and checks connection to servers.
    /// </summary>
    /// <param name="apiKey">Your api key</param>
    /// <param name="apiSecret">Your api secret</param>
    /// <param name="proxy">Proxy to be used with client</param>
    public CommexClient(string apiKey, string apiSecret, WebProxy? proxy = null) : base(apiKey, apiSecret, Url, proxy)
    {
        Wallet = new Wallet(this);
        Margin = new Margin(this);
        Trade = new Trade(this);
        FuturesTransfer = new FuturesTransfer(this);
        Market = new Market(this);
        Futures = new BinanceFuturesClient(apiKey, apiSecret, proxy);
    }

    protected sealed override void TryLogin()
    {
        var request = new RestRequest("sapi/v1/system/status");

        var response = ExecuteRequestWithoutResponse(request, false);

        if (!response.IsSuccessful)
            throw new ConnectionNotSetException("Couldn't connect to Binance Server.", response.StatusCode, response.Content);

        CheckAccountStatus();
    }

    private void CheckAccountStatus()
    {
        var request = new RestRequest("sapi/v1/account/status");

        var response = ExecuteRequestWithoutResponse(request);

        if (!response.IsSuccessful || response.Content is null)
            throw new BadAccountStatusException("Couldn't connect to Binance Account.", response.StatusCode, response.Content);

        dynamic json = JObject.Parse(response.Content);
        if (json["data"] != "Normal")
            throw new BadAccountStatusException("Couldn't connect to Binance Account.", response.StatusCode, response.Content);
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

    private string Sign(string payload)
    {
        var key = Encoding.UTF8.GetBytes(ApiSecret);

        using HMACSHA256 hmacsha256 = new(key);

        byte[] payloadBytes = Encoding.UTF8.GetBytes(payload);
        byte[] hash = hmacsha256.ComputeHash(payloadBytes);

        return BitConverter.ToString(hash).Replace("-", string.Empty).ToLower();
    }

    public WithdrawalRecord Withdraw(string currency, decimal amount, string address, string network, bool waitForApprove = true)
    {
        if(!waitForApprove)
        {
            var id = Wallet.WithdrawCurrency(currency, amount, address, network);

            return new WithdrawalRecord
            {
                TxId = id,
                RequestedAmount = amount,
                WaitedForApproval = waitForApprove,
            };
        }
        else
        {
            (var id, var hash) = Wallet.WithdrawAndWaitForSent(currency, amount, address, network);

            return new WithdrawalRecord
            {
                TxId = id,
                RequestedAmount = amount,
                WaitedForApproval = waitForApprove,
                TxHash = hash
            };
        }
    }

    public async Task<WithdrawalRecord> WithdrawAsync(string currency, decimal amount, string address, string network, bool waitForApprove = true)
    {
        if (!waitForApprove)
        {
            var id = await Wallet.WithdrawCurrencyAsync(currency, amount, address, network);

            return new WithdrawalRecord
            {
                TxId = id,
                RequestedAmount = amount,
                WaitedForApproval = waitForApprove,
            };
        }
        else
        {
            (var id, var hash) = await Wallet.WithdrawAndWaitForSentAsync(currency, amount, address, network);

            return new WithdrawalRecord
            {
                TxId = id,
                RequestedAmount = amount,
                WaitedForApproval = waitForApprove,
                TxHash = hash
            };
        }
    }

    public sealed override decimal GetWithdrawalFee(string currency, string network)
    {
        var coinInfo = Wallet.GetCoinInformation(currency, network);

        return coinInfo.NetworkList
            .Where(x => x.NetworkName.ToLower() == network.ToLower())
            .First()
            .WithdrawFee;
    }

    public sealed override async Task<decimal> GetWithdrawalFeeAsync(string currency, string network)
    {
        var coinInfo = await Wallet.GetCoinInformationAsync(currency, network);

        return coinInfo.NetworkList
            .Where(x => x.NetworkName.ToLower() == network.ToLower())
            .First()
            .WithdrawFee;
    }

    public sealed override int QueryWithdrawalPrecision(string currency, string network)
    {
        var coinInfo = Wallet.GetCoinInformation(currency, network);

        var decimals = coinInfo.NetworkList
            .Where(x => x.NetworkName.ToLower() == network.ToLower())
            .First()
            .WithdrawIntegerMultiple;

        return - (int)Math.Log10((double)decimals);
    }

    public sealed override async Task<int> QueryWithdrawalPrecisionAsync(string currency, string network)
    {
        var coinInfo = await Wallet.GetCoinInformationAsync(currency, network);

        var decimals = coinInfo.NetworkList
            .Where(x => x.NetworkName.ToLower() == network.ToLower())
            .First()
            .WithdrawIntegerMultiple;

        return - (int)Math.Log10((double)decimals);
    }

    public sealed override decimal QueryWithdrawalMinAmount(string currency, string network)
    {
        var data = Wallet.GetCoinInformation(currency, network);

        return data.NetworkList.Single().WithdrawMin;
    }

    public sealed override async Task<decimal> QueryWithdrawalMinAmountAsync(string currency, string network)
    {
        var data = await Wallet.GetCoinInformationAsync(currency, network);

        return data.NetworkList.Single().WithdrawMin;
    }

    public sealed override string GetDepositAddress(string currency, string network)
    {
        var address = Wallet.GetDepositAddress(currency, network);

        return address.Address;
    }

    public sealed override async Task<string> GetDepositAddressAsync(string currency, string network)
    {
        var address = await Wallet.GetDepositAddressAsync(currency, network);

        return address.Address;
    }

    public sealed override decimal ApproveReceiving(string hash)
    {
        return Wallet.WaitForReceive(hash);
    }

    public sealed override async Task<decimal> ApproveReceivingAsync(string hash)
    {
        return await Wallet.WaitForReceiveAsync(hash);
    }

    public sealed override decimal GetBalance(string currency)
    {
        var data = Wallet.GetUserAsset(currency);

        return data.Free;
    }

    public sealed override async Task<decimal> GetBalanceAsync(string currency)
    {
        var data = await Wallet.GetUserAssetAsync(currency);

        return data.Free;
    }

    public sealed override decimal GetMarketPrice(string symbol)
    {
        return Market.GetPriceTicker(symbol).Price;
    }

    public sealed override async Task<decimal> GetMarketPriceAsync(string symbol)
    {
        var data = await Market.GetPriceTickerAsync(symbol);

        return data.Price;
    }

    public sealed override decimal GetAmountIn(string currencyIn, string currencyOut, decimal amountOut, decimal slippage = 0.998m)
    {
        bool reversed = false;

        string symbol = currencyIn.ToUpper() + currencyOut.ToUpper();

        try
        {
            Market.GetPriceTicker(symbol);
        }
        catch (RequestNotSuccessfulException ex) when (ex.Response == "{\"code\":-1121,\"msg\":\"Invalid symbol.\"}")
        {
            symbol = currencyOut.ToUpper() + currencyIn.ToUpper();

            reversed = true;
        }

        var price = GetMarketPrice(symbol);

        if (reversed)
            price = (1 / price);

        Message($"Price: {price}");

        return amountOut / slippage / price;
    }

    public sealed override async Task<decimal> GetAmountInAsync(string currencyIn, string currencyOut, decimal amountOut, decimal slippage = 0.998m)
    {
        bool reversed = false;

        string symbol = currencyIn.ToUpper() + currencyOut.ToUpper();

        try
        {
            await Market.GetPriceTickerAsync(symbol);
        }
        catch (RequestNotSuccessfulException ex) when (ex.Response == "{\"code\":-1121,\"msg\":\"Invalid symbol.\"}")
        {
            symbol = currencyOut.ToUpper() + currencyIn.ToUpper();

            reversed = true;
        }

        var price = await GetMarketPriceAsync(symbol);

        if (reversed)
            price = 1 / price;

        Message($"Price: {price}");

        return amountOut / slippage / price;
    }

    public sealed override decimal FlattenOrderAmount(string symbol, decimal amount, int stepSizeDown = 0)
    {
        return Trade.FlattenOrderAmount(symbol, amount, stepSizeDown);
    }

    public sealed override async Task<decimal> FlattenOrderAmountAsync(string symbol, decimal amount, int stepSizeDown = 0)
    {
        return await Trade.FlattenOrderAmountAsync(symbol, amount, stepSizeDown);
    }

    public sealed override (decimal, decimal) ForcedMarketOrder(string baseCurrency, string quoteCurrency, OrderDirection direction, decimal amount, CalculationBase calculationBase = CalculationBase.Base)
    {
        string symbol = baseCurrency.ToUpper() + quoteCurrency.ToUpper();

        try
        {
            Market.GetPriceTicker(symbol);
        }
        catch (RequestNotSuccessfulException ex) when (ex.Response == "{\"code\":-1121,\"msg\":\"Invalid symbol.\"}")
        {
            symbol = quoteCurrency.ToUpper() + baseCurrency.ToUpper();

            (quoteCurrency, baseCurrency) = (baseCurrency, quoteCurrency);

            direction = HelperMethods.ReverseOrderDirection(direction);

            calculationBase = HelperMethods.ReverseCalculationBase(calculationBase);
        }

        var orderSide = direction == OrderDirection.Buy ? OrderSide.BUY : OrderSide.SELL;

        if(calculationBase == CalculationBase.Quote)
            amount = GetAmountIn(baseCurrency, quoteCurrency, amount);

        var flattenedAmount = FlattenOrderAmount(symbol, amount);

        for (int i = 0; i < 50; i++)
        {
            try
            {
                var order = Trade.NewOrder(symbol, orderSide, OrderType.MARKET, quantity: flattenedAmount);

                Message($"Executed Order {symbol}, amount: {flattenedAmount}, side: {orderSide}");

                if (orderSide == OrderSide.BUY)
                    return (order.ExecutedQty, order.CummulativeQuoteQty);
                
                return (order.CummulativeQuoteQty, order.ExecutedQty);
            }
            catch (RequestNotSuccessfulException ex) when (ex.Response == "{\"code\":-1013,\"msg\":\"Filter failure: LOT_SIZE\"}")
            {
                Message(ex.Response);

                Message("Retrying order.");

                flattenedAmount = FlattenOrderAmount(symbol, flattenedAmount * 0.995m);
            }
        }

        throw new Exception("Can not place an order for 50 attempts!");
    }

    public sealed override async Task<(decimal, decimal)> ForcedMarketOrderAsync(string baseCurrency, string quoteCurrency, OrderDirection direction, decimal amount, CalculationBase calculationBase = CalculationBase.Base)
    {
        string symbol = baseCurrency.ToUpper() + quoteCurrency.ToUpper();

        try
        {
            await Market.GetPriceTickerAsync(symbol);
        }
        catch (RequestNotSuccessfulException ex) when (ex.Response == "{\"code\":-1121,\"msg\":\"Invalid symbol.\"}")
        {
            symbol = quoteCurrency.ToUpper() + baseCurrency.ToUpper();

            (quoteCurrency, baseCurrency) = (baseCurrency, quoteCurrency);

            direction = HelperMethods.ReverseOrderDirection(direction);

            calculationBase = HelperMethods.ReverseCalculationBase(calculationBase);
        }

        var orderSide = direction == OrderDirection.Buy ? OrderSide.BUY : OrderSide.SELL;

        if (calculationBase == CalculationBase.Quote)
            amount = await GetAmountInAsync(baseCurrency, quoteCurrency, amount);

        var flattenedAmount = await FlattenOrderAmountAsync(symbol, amount);

        for (int i = 0; i < 50; i++)
        {
            try
            {
                var order = await Trade.NewOrderAsync(symbol, orderSide, OrderType.MARKET, quantity: flattenedAmount);

                Message($"Executed Order {symbol}, amount: {flattenedAmount}, side: {orderSide}");

                if (orderSide == OrderSide.BUY)
                    return (order.ExecutedQty, order.CummulativeQuoteQty);
                
                return (order.CummulativeQuoteQty, order.ExecutedQty);
            }
            catch (RequestNotSuccessfulException ex) when (ex.Response == "{\"code\":-1013,\"msg\":\"Filter failure: LOT_SIZE\"}")
            {
                Message(ex.Response);

                Message("Retrying order.");

                flattenedAmount = await FlattenOrderAmountAsync(symbol, flattenedAmount * 0.995m);
            }
        }

        throw new Exception("Can not place an order for 50 attempts!");
    }

    public sealed override decimal GetMinOrderSizeForPair(string baseCurrency, string quoteCurrency, CalculationBase calculationBase = CalculationBase.Base)
    {
        return Market.GetMinOrderSizeForPair(baseCurrency, quoteCurrency, calculationBase);
    }

    public sealed override async Task<decimal> GetMinOrderSizeForPairAsync(string baseCurrency, string quoteCurrency, CalculationBase calculationBase = CalculationBase.Base)
    {
        return await Market.GetMinOrderSizeForPairAsync(baseCurrency, quoteCurrency, calculationBase);
    }
}