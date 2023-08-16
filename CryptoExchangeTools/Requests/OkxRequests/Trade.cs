using System;
using System.Diagnostics;
using System.Net;
using CryptoExchangeTools.Exceptions.Okx;
using CryptoExchangeTools.Models.Okx;
using CryptoExchangeTools.Requests.BinanceRequests;
using CryptoExchangeTools.Requests.BinanceRequests.Futures.USDM;
using Newtonsoft.Json;
using RestSharp;

namespace CryptoExchangeTools.Requests.OkxRequests;

public class Trade
{
	private readonly OkxClient Client;

	public Trade(OkxClient client)
	{
		Client = client;
	}

    #region Original Methods

    #region Place order

    /// <summary>
    /// You can place an order only if you have sufficient funds. For leading contracts, this endpoint supports placement, but can't close positions.
    /// </summary>
    /// <param name="instId">Instrument ID, e.g. BTC-USD-190927-5000-C.</param>
    /// <param name="tdMode">Trade mode.</param>
    /// <param name="side">Order side, buy sell.</param>
    /// <param name="ordType">Order type.</param>
    /// <param name="sz">Quantity to buy or sell.</param>
    /// <param name="ccy">Margin currency. Only applicable to cross MARGIN orders in Single-currency margin.</param>
    /// <param name="clOrdId">Client Order ID as assigned by the client. A combination of case-sensitive alphanumerics, all numbers, or all letters of up to 32 characters.</param>
    /// <param name="tag">Order tag. A combination of case-sensitive alphanumerics, all numbers, or all letters of up to 16 characters.</param>
    /// <param name="posSide">Position side.</param>
    /// <param name="px">Order price. Only applicable to limit,post_only,fok,ioc order.</param>
    /// <param name="reduceOnly">Whether orders can only reduce in position size. Valid options: true or false. The default value is false. Only applicable to MARGIN orders, and FUTURES/SWAP orders in net mode. Only applicable to Single-currency margin and Multi-currency margin</param>
    /// <param name="tgtCcy">Whether the target currency uses the quote or base currency.</param>
    /// <param name="banAmend">Whether to disallow the system from amending the size of the SPOT Market Order. Valid options: true or false. The default value is false. If true, system will not amend and reject the market order if user does not have sufficient funds. Only applicable to SPOT Market Orders</param>
    /// <param name="tpTriggerPx">Take-profit trigger price. If you fill in this parameter, you should fill in the take-profit order price as well.</param>
    /// <param name="tpOrdPx">Take-profit order price. If you fill in this parameter, you should fill in the take-profit trigger price as well. If the price is -1, take-profit will be executed at the market price.</param>
    /// <param name="slTriggerPx">Stop-loss trigger price. If you fill in this parameter, you should fill in the stop-loss order price.</param>
    /// <param name="slOrdPx">Stop-loss order price. If you fill in this parameter, you should fill in the stop-loss trigger price. If the price is -1, stop-loss will be executed at the market price.</param>
    /// <param name="tpTriggerPxType">Take-profit trigger price type.</param>
    /// <param name="slTriggerPxType">Stop-loss trigger price type.</param>
    /// <param name="quickMgnType">Quick Margin type. Only applicable to Quick Margin Mode of isolated margin.</param>
    /// <param name="stpId">Self trade prevention ID. Orders from the same master account with the same ID will be prevented from self trade. Numerical integers defined by user in the range of 1<= x<= 999999999</param>
    /// <param name="stpMode">Self trade prevention mode.</param>
    /// <returns></returns>
    public OrderResult[] PlaceOrder(
        string instId,
        TradeMode tdMode,
        OrderSide side,
        OrderType ordType,
        decimal sz,
        string? ccy = null,
        string? clOrdId = null,
        string? tag = null,
        PositionSide? posSide = null,
        decimal px = -1,
        bool? reduceOnly = null,
        TargetCurrency? tgtCcy = null,
        bool? banAmend = null,
        decimal tpTriggerPx = -1,
        decimal tpOrdPx = -1,
        decimal slTriggerPx = -1,
        decimal slOrdPx = -1,
        TriggerPriceType? tpTriggerPxType = null,
        TriggerPriceType? slTriggerPxType = null,
        MarginType? quickMgnType = null,
        string? stpId = null,
        SelfTradePreventionMode? stpMode = null)
    {
        var request = BuildPlaceOrderRequest(
            instId,
            tdMode,
            side,
            ordType,
            sz,
            ccy,
            clOrdId,
            tag,
            posSide,
            px,
            reduceOnly,
            tgtCcy,
            banAmend,
            tpTriggerPx,
            tpOrdPx,
            slTriggerPx,
            slOrdPx,
            tpTriggerPxType,
            slTriggerPxType,
            quickMgnType,
            stpId,
            stpMode);

        return Client.ExecuteRequest<OrderResult[]>(request);
    }

    /// <summary>
    /// You can place an order only if you have sufficient funds. For leading contracts, this endpoint supports placement, but can't close positions.
    /// </summary>
    /// <param name="instId">Instrument ID, e.g. BTC-USD-190927-5000-C.</param>
    /// <param name="tdMode">Trade mode.</param>
    /// <param name="side">Order side, buy sell.</param>
    /// <param name="ordType">Order type.</param>
    /// <param name="sz">Quantity to buy or sell.</param>
    /// <param name="ccy">Margin currency. Only applicable to cross MARGIN orders in Single-currency margin.</param>
    /// <param name="clOrdId">Client Order ID as assigned by the client. A combination of case-sensitive alphanumerics, all numbers, or all letters of up to 32 characters.</param>
    /// <param name="tag">Order tag. A combination of case-sensitive alphanumerics, all numbers, or all letters of up to 16 characters.</param>
    /// <param name="posSide">Position side.</param>
    /// <param name="px">Order price. Only applicable to limit,post_only,fok,ioc order.</param>
    /// <param name="reduceOnly">Whether orders can only reduce in position size. Valid options: true or false. The default value is false. Only applicable to MARGIN orders, and FUTURES/SWAP orders in net mode. Only applicable to Single-currency margin and Multi-currency margin</param>
    /// <param name="tgtCcy">Whether the target currency uses the quote or base currency.</param>
    /// <param name="banAmend">Whether to disallow the system from amending the size of the SPOT Market Order. Valid options: true or false. The default value is false. If true, system will not amend and reject the market order if user does not have sufficient funds. Only applicable to SPOT Market Orders</param>
    /// <param name="tpTriggerPx">Take-profit trigger price. If you fill in this parameter, you should fill in the take-profit order price as well.</param>
    /// <param name="tpOrdPx">Take-profit order price. If you fill in this parameter, you should fill in the take-profit trigger price as well. If the price is -1, take-profit will be executed at the market price.</param>
    /// <param name="slTriggerPx">Stop-loss trigger price. If you fill in this parameter, you should fill in the stop-loss order price.</param>
    /// <param name="slOrdPx">Stop-loss order price. If you fill in this parameter, you should fill in the stop-loss trigger price. If the price is -1, stop-loss will be executed at the market price.</param>
    /// <param name="tpTriggerPxType">Take-profit trigger price type.</param>
    /// <param name="slTriggerPxType">Stop-loss trigger price type.</param>
    /// <param name="quickMgnType">Quick Margin type. Only applicable to Quick Margin Mode of isolated margin.</param>
    /// <param name="stpId">Self trade prevention ID. Orders from the same master account with the same ID will be prevented from self trade. Numerical integers defined by user in the range of 1<= x<= 999999999</param>
    /// <param name="stpMode">Self trade prevention mode.</param>
    /// <returns></returns>
    public async Task<OrderResult[]> PlaceOrderAsync(
        string instId,
        TradeMode tdMode,
        OrderSide side,
        OrderType ordType,
        decimal sz,
        string? ccy = null,
        string? clOrdId = null,
        string? tag = null,
        PositionSide? posSide = null,
        decimal px = -1,
        bool? reduceOnly = null,
        TargetCurrency? tgtCcy = null,
        bool? banAmend = null,
        decimal tpTriggerPx = -1,
        decimal tpOrdPx = -1,
        decimal slTriggerPx = -1,
        decimal slOrdPx = -1,
        TriggerPriceType? tpTriggerPxType = null,
        TriggerPriceType? slTriggerPxType = null,
        MarginType? quickMgnType = null,
        string? stpId = null,
        SelfTradePreventionMode? stpMode = null)
    {
        var request = BuildPlaceOrderRequest(
            instId,
            tdMode,
            side,
            ordType,
            sz,
            ccy,
            clOrdId,
            tag,
            posSide,
            px,
            reduceOnly,
            tgtCcy,
            banAmend,
            tpTriggerPx,
            tpOrdPx,
            slTriggerPx,
            slOrdPx,
            tpTriggerPxType,
            slTriggerPxType,
            quickMgnType,
            stpId,
            stpMode);

        return await Client.ExecuteRequestAsync<OrderResult[]>(request);
    }

    private static RestRequest BuildPlaceOrderRequest(
		string instId,
		TradeMode tdMode,
		OrderSide side,
		OrderType ordType,
		decimal sz,
		string? ccy = null,
		string? clOrdId = null,
		string? tag = null,
		PositionSide? posSide = null,
		decimal px = -1,
		bool? reduceOnly = null,
		TargetCurrency? tgtCcy = null,
		bool? banAmend = null,
		decimal tpTriggerPx = -1,
		decimal tpOrdPx = -1,
		decimal slTriggerPx = -1,
		decimal slOrdPx = -1,
        TriggerPriceType? tpTriggerPxType = null,
		TriggerPriceType? slTriggerPxType = null,
        MarginType? quickMgnType = null,
		string? stpId = null,
        SelfTradePreventionMode? stpMode = null)
	{
		var request = new RestRequest("api/v5/trade/order", Method.Post);

		var body = new NewOrder(
            instId,
            tdMode,
            side,
            ordType,
            sz,
            ccy,
            clOrdId,
            tag,
            posSide,
            px,
            reduceOnly,
            tgtCcy,
            banAmend,
            tpTriggerPx,
            tpOrdPx,
            slTriggerPx,
            slOrdPx,
            tpTriggerPxType,
            slTriggerPxType,
            quickMgnType,
            stpId,
            stpMode);

        request.AddBody(JsonConvert.SerializeObject(body), "application/json");

        return request;
	}

    #endregion Place order

    #region Get Order Details

    /// <summary>
    /// Retrieve order details.
    /// </summary>
    /// <param name="instId">Instrument ID, e.g. BTC-USD-190927</param>
    /// <param name="ordId">Order ID. Either ordId or clOrdId is required, if both are passed, ordId will be used</param>
    /// <param name="clOrdId">Client Order ID as assigned by the client. If the clOrdId is associated with multiple orders, only the latest one will be returned.</param>
    /// <returns></returns>
    public OrderDetails[] GetOrderDetails(
        string instId,
        string? ordId = null,
        string? clOrdId = null)
    {
        var request = BuildGetOrderDetails(instId, ordId, clOrdId);

        return Client.ExecuteRequest<OrderDetails[]>(request);
    }

    /// <summary>
    /// Retrieve order details.
    /// </summary>
    /// <param name="instId">Instrument ID, e.g. BTC-USD-190927</param>
    /// <param name="ordId">Order ID. Either ordId or clOrdId is required, if both are passed, ordId will be used</param>
    /// <param name="clOrdId">Client Order ID as assigned by the client. If the clOrdId is associated with multiple orders, only the latest one will be returned.</param>
    /// <returns></returns>
    public async Task<OrderDetails[]> GetOrderDetailsAsync(
        string instId,
        string? ordId = null,
        string? clOrdId = null)
    {
        var request = BuildGetOrderDetails(instId, ordId, clOrdId);

        return await Client.ExecuteRequestAsync<OrderDetails[]>(request);
    }

    private static RestRequest BuildGetOrderDetails(
        string instId,
        string? ordId = null,
        string? clOrdId = null)
    {
        var request = new RestRequest("api/v5/trade/order", Method.Get);

        request.AddParameter("instId", instId);

        if(!string.IsNullOrEmpty(ordId))
            request.AddParameter("ordId", ordId);

        if (!string.IsNullOrEmpty(clOrdId))
            request.AddParameter("clOrdId", clOrdId);

        return request;
    }

    #endregion Get Order Details

    #region Cancel Order

    /// <summary>
    /// Cancel an incomplete order.
    /// </summary>
    /// <param name="instId">Instrument ID, e.g. BTC-USD-190927</param>
    /// <param name="ordId">Order ID. Either ordId or clOrdId is required. If both are passed, ordId will be used.</param>
    /// <param name="clOrdId">Client Order ID as assigned by the client</param>
    /// <returns></returns>
    public CancelOrderResult[] CancelOrder(
        string instId,
        string? ordId = null,
        string? clOrdId = null)
    {
        var request = BuildCancelOrder(instId, ordId, clOrdId);

        return Client.ExecuteRequest<CancelOrderResult[]>(request);
    }

    /// <summary>
    /// Cancel an incomplete order.
    /// </summary>
    /// <param name="instId">Instrument ID, e.g. BTC-USD-190927</param>
    /// <param name="ordId">Order ID. Either ordId or clOrdId is required. If both are passed, ordId will be used.</param>
    /// <param name="clOrdId">Client Order ID as assigned by the client</param>
    /// <returns></returns>
    public async Task<CancelOrderResult[]> CancelOrderAsync(
        string instId,
        string? ordId = null,
        string? clOrdId = null)
    {
        var request = BuildCancelOrder(instId, ordId, clOrdId);

        return await Client.ExecuteRequestAsync<CancelOrderResult[]>(request);
    }

    private static RestRequest BuildCancelOrder(
        string instId,
        string? ordId = null,
        string? clOrdId = null)
    {
        var request = new RestRequest("api/v5/trade/cancel-order", Method.Post);

        var body = new CancelOrderRequest(instId, ordId, clOrdId);

        request.AddBody(JsonConvert.SerializeObject(body), "application/json");

        return request;
    }

    #endregion Cancel Order

    #endregion Original Methods

    #region Derived Methods

    #region Forced Market Order

    /// <summary>
    /// Place an order that will return only after it is fully filled. If it is not filled for long time - it will create a new order to fill the remaining amount.
    /// </summary>
    /// <param name="instId"></param>
    /// <param name="side"></param>
    /// <param name="sz"></param>
    /// <returns>Resulting order amount.</returns>
    public decimal ForcedMarketOrder(
        string instId,
        OrderSide side,
        decimal sz)
    {
        var targetCcy = GetTargetCcy(instId, side);

        var balanceBefore = Client.Account.GetCurrencyTradingAvailableBalance(targetCcy);

        var info = PlaceOrder(instId, TradeMode.cash, side, OrderType.market, sz);

        var id = info.Single().OrderId;

        var orderInfo = GetOrderDetails(instId, id).Single();

        Client.Message(orderInfo.State.ToString());

        if (orderInfo.State == OrderState.filled)
        {
            var balanceNow = Client.Account.GetCurrencyTradingAvailableBalance(targetCcy);

            return balanceNow - balanceBefore;
        }

        for (int i = 0; i < 20; i++)
        {
            Task.Delay(30 * 1000).Wait();

            orderInfo = GetOrderDetails(instId, id).Single();

            Client.Message(orderInfo.State.ToString());

            if (orderInfo.State == OrderState.filled)
            {
                var balanceNow = Client.Account.GetCurrencyTradingAvailableBalance(targetCcy);

                return balanceNow - balanceBefore;
            }
        }

        Client.Message("Canceling order as it was not filled after time limit.");

        try
        {
            CancelOrder(instId, id);
        }
        catch (OkxException ex) when (ex.Message.Contains("Cancellation failed as the order doesn't exist"))
        {
            var balanceNow = Client.Account.GetCurrencyTradingAvailableBalance(targetCcy);

            return balanceNow - balanceBefore;
        }

        var orderInfoResult = GetOrderDetails(instId, id).Single();

        if (orderInfoResult.State == OrderState.filled)
        {
            Client.Message("Order filled.");

            var balanceNow = Client.Account.GetCurrencyTradingAvailableBalance(targetCcy);

            return balanceNow - balanceBefore;
        }

        var newAmt = sz - orderInfoResult.AccFillSz;

        ForcedMarketOrder(instId, side, newAmt);

        var finalBalance = Client.Account.GetCurrencyTradingAvailableBalance(targetCcy);

        return finalBalance - balanceBefore;
    }

    /// <summary>
    /// Place an order that will return only after it is fully filled. If it is not filled for long time - it will create a new order to fill the remaining amount.
    /// </summary>
    /// <param name="instId"></param>
    /// <param name="side"></param>
    /// <param name="sz"></param>
    /// <returns>Resulting order amount.</returns>
    public async Task<decimal> ForcedMarketOrderAsync(
        string instId,
        OrderSide side,
        decimal sz)
    {
        var targetCcy = GetTargetCcy(instId, side);

        var balanceBefore = await Client.Account.GetCurrencyTradingAvailableBalanceAsync(targetCcy);

        var info = await PlaceOrderAsync(instId, TradeMode.cash, side, OrderType.market, sz);

        var id = info.Single().OrderId;

        var orderInfo = (await GetOrderDetailsAsync(instId, id)).Single();

        Client.Message(orderInfo.State.ToString());

        if (orderInfo.State == OrderState.filled)
        {
            var balanceNow = await Client.Account.GetCurrencyTradingAvailableBalanceAsync(targetCcy);

            return balanceNow - balanceBefore;
        }

        for (int i = 0; i < 20; i++)
        {
            await Task.Delay(30 * 1000);

            orderInfo = (await GetOrderDetailsAsync(instId, id)).Single();

            Client.Message(orderInfo.State.ToString());

            if (orderInfo.State == OrderState.filled)
            {
                var balanceNow = await Client.Account.GetCurrencyTradingAvailableBalanceAsync(targetCcy);

                return balanceNow - balanceBefore;
            }
        }

        Client.Message("Canceling order as it was not filled after time limit.");

        try
        {
            await CancelOrderAsync(instId, id);
        }
        catch (OkxException ex) when (ex.Message.Contains("Cancellation failed as the order doesn't exist"))
        {
            Client.Message("Order filled.");

            var balanceNow = await Client.Account.GetCurrencyTradingAvailableBalanceAsync(targetCcy);

            return balanceNow - balanceBefore;
        }

        var orderInfoResult = (await GetOrderDetailsAsync(instId, id)).Single();

        if (orderInfoResult.State == OrderState.filled)
        {
            var balanceNow = await Client.Account.GetCurrencyTradingAvailableBalanceAsync(targetCcy);

            return balanceNow - balanceBefore;
        }

        var newAmt = sz - orderInfoResult.AccFillSz;

        await ForcedMarketOrderAsync(instId, side, newAmt);

        var finalBalance = await Client.Account.GetCurrencyTradingAvailableBalanceAsync(targetCcy);

        return finalBalance - balanceBefore;
    }

    #endregion Forced Market Order

    #region Trade From Funding

    /// <summary>
    /// Transfer funds from funding to trade, execute forced market trade and return new currency to funding.
    /// </summary>
    /// <param name="instId"></param>
    /// <param name="side"></param>
    /// <param name="sz">Amount in base currency.</param>
    /// <returns>Received amount of quote currency.</returns>
    public decimal TradeFromFunding(
        string instId,
        OrderSide side,
        decimal sz)
    {
        var currencyToTransferFromFunding = GetBaseCcy(instId, side);

        var currencyToReturnToFunding = GetTargetCcy(instId, side);

        Client.Message($"Transfering {sz} {currencyToTransferFromFunding} from fundig to trade.");

        Client.Funding.FundsTransfer(currencyToTransferFromFunding, sz, AccountType.Funding, AccountType.Trading);

        Client.Message($"Trading {sz} {currencyToTransferFromFunding} for {currencyToReturnToFunding}");

        var resultingQuoteAmount = ForcedMarketOrder(instId, side, sz);

        Client.Message($"Transfering {resultingQuoteAmount} {currencyToReturnToFunding} from trade to funding.");

        Client.Funding.FundsTransfer(currencyToReturnToFunding, resultingQuoteAmount, AccountType.Trading, AccountType.Funding);

        return resultingQuoteAmount;
    }

    /// <summary>
    /// Transfer funds from funding to trade, execute forced market trade and return new currency to funding.
    /// </summary>
    /// <param name="instId"></param>
    /// <param name="side"></param>
    /// <param name="sz">Amount in base currency.</param>
    /// <returns>Received amount of quote currency.</returns>
    public async Task<decimal> TradeFromFundingAsync(
        string instId,
        OrderSide side,
        decimal sz)
    {
        var currencyToTransferFromFunding = GetBaseCcy(instId, side);

        var currencyToReturnToFunding = GetTargetCcy(instId, side);

        Client.Message($"Transfering {sz} {currencyToTransferFromFunding} from fundig to trade.");

        await Client.Funding.FundsTransferAsync(currencyToTransferFromFunding, sz, AccountType.Funding, AccountType.Trading);

        Client.Message($"Trading {sz} {currencyToTransferFromFunding} for {currencyToReturnToFunding}");

        var resultingQuoteAmount = await ForcedMarketOrderAsync(instId, side, sz);

        Client.Message($"Transfering {resultingQuoteAmount} {currencyToReturnToFunding} from trade to funding.");

        await Client.Funding.FundsTransferAsync(currencyToReturnToFunding, resultingQuoteAmount, AccountType.Trading, AccountType.Funding);

        return resultingQuoteAmount;
    }

    #endregion Trade From Funding

    #region Helper Methods

    public static string GetTargetCcy(string instId, OrderSide side)
    {
        var split = instId.Split('-');

        if (side == OrderSide.buy)
            return split[0];

        return split[1];
    }

    public static string GetBaseCcy(string instId, OrderSide side)
    {
        var split = instId.Split('-');

        if (side == OrderSide.buy)
            return split[1];

        return split[0];
    }

    #endregion HelperMethods

    #endregion Derived Methods
}

