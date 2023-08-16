using CryptoExchangeTools.Models.Binance;
using RestSharp;
using System.Globalization;

namespace CryptoExchangeTools.Requests.BinanceRequests;

public class Trade
{
    private BinanceClient Client;

    public Trade(BinanceClient client)
    {
        Client = client;
    }

    #region Original Methods

    #region New Order

    /// <summary>
    /// Send in a new order.
    /// </summary>
    /// <param name="symbol"></param>
    /// <param name="side"></param>
    /// <param name="type"></param>
    /// <param name="timeInForce"></param>
    /// <param name="quantity"></param>
    /// <param name="quoteOrderQty"></param>
    /// <param name="price"></param>
    /// <param name="newClientOrderId">A unique id among open orders. Automatically generated if not sent.</param>
    /// <param name="strategyId"></param>
    /// <param name="strategyType">The value cannot be less than 1000000.</param>
    /// <param name="stopPrice">Used with STOP_LOSS, STOP_LOSS_LIMIT, TAKE_PROFIT, and TAKE_PROFIT_LIMIT orders.</param>
    /// <param name="trailingDelta">Used with STOP_LOSS, STOP_LOSS_LIMIT, TAKE_PROFIT, and TAKE_PROFIT_LIMIT orders. For more details on SPOT implementation on trailing stops, please refer to Trailing Stop FAQ</param>
    /// <param name="icebergQty">Used with LIMIT, STOP_LOSS_LIMIT, and TAKE_PROFIT_LIMIT to create an iceberg order.</param>
    /// <param name="newOrderRespType">Set the response JSON. ACK, RESULT, or FULL; MARKET and LIMIT order types default to FULL, all other orders default to ACK.</param>
    /// <param name="selfTradePreventionMode">The allowed enums is dependent on what is configured on the symbol. The possible supported values are EXPIRE_TAKER, EXPIRE_MAKER, EXPIRE_BOTH, NONE.</param>
    /// <param name="recvWindow">The value cannot be greater than 60000</param>
    /// <returns></returns>
    public Order NewOrder(
        string symbol,
        OrderSide side,
        OrderType type,
        TimeInForce? timeInForce = null,
        decimal quantity = -1,
        decimal quoteOrderQty = -1,
        decimal price = -1,
        string? newClientOrderId = null,
        int strategyId = -1,
        int strategyType = -1,
        decimal stopPrice = -1,
        long trailingDelta = -1,
        decimal icebergQty = -1,
        NewOrderRespType? newOrderRespType = null,
        SelfTradePreventionMode? selfTradePreventionMode = null,
        long recvWindow = -1
        )
    {
        var request = BuildNewOrderRequest(
            symbol,
            side,
            type,
            timeInForce,
            quantity,
            quoteOrderQty,
            price,
            newClientOrderId,
            strategyId,
            strategyType,
            stopPrice,
            trailingDelta,
            icebergQty,
            newOrderRespType,
            selfTradePreventionMode,
            recvWindow);

        return Client.ExecuteRequest<Order>(request);
    }

    /// <summary>
    /// Send in a new order.
    /// </summary>
    /// <param name="symbol"></param>
    /// <param name="side"></param>
    /// <param name="type"></param>
    /// <param name="timeInForce"></param>
    /// <param name="quantity"></param>
    /// <param name="quoteOrderQty"></param>
    /// <param name="price"></param>
    /// <param name="newClientOrderId">A unique id among open orders. Automatically generated if not sent.</param>
    /// <param name="strategyId"></param>
    /// <param name="strategyType">The value cannot be less than 1000000.</param>
    /// <param name="stopPrice">Used with STOP_LOSS, STOP_LOSS_LIMIT, TAKE_PROFIT, and TAKE_PROFIT_LIMIT orders.</param>
    /// <param name="trailingDelta">Used with STOP_LOSS, STOP_LOSS_LIMIT, TAKE_PROFIT, and TAKE_PROFIT_LIMIT orders. For more details on SPOT implementation on trailing stops, please refer to Trailing Stop FAQ</param>
    /// <param name="icebergQty">Used with LIMIT, STOP_LOSS_LIMIT, and TAKE_PROFIT_LIMIT to create an iceberg order.</param>
    /// <param name="newOrderRespType">Set the response JSON. ACK, RESULT, or FULL; MARKET and LIMIT order types default to FULL, all other orders default to ACK.</param>
    /// <param name="selfTradePreventionMode">The allowed enums is dependent on what is configured on the symbol. The possible supported values are EXPIRE_TAKER, EXPIRE_MAKER, EXPIRE_BOTH, NONE.</param>
    /// <param name="recvWindow">The value cannot be greater than 60000</param>
    /// <returns></returns>
    public async Task<Order> NewOrderAsync(
        string symbol,
        OrderSide side,
        OrderType type,
        TimeInForce? timeInForce = null,
        decimal quantity = -1,
        decimal quoteOrderQty = -1,
        decimal price = -1,
        string? newClientOrderId = null,
        int strategyId = -1,
        int strategyType = -1,
        decimal stopPrice = -1,
        long trailingDelta = -1,
        decimal icebergQty = -1,
        NewOrderRespType? newOrderRespType = null,
        SelfTradePreventionMode? selfTradePreventionMode = null,
        long recvWindow = -1
        )
    {
        var request = BuildNewOrderRequest(
            symbol,
            side,
            type,
            timeInForce,
            quantity,
            quoteOrderQty,
            price,
            newClientOrderId,
            strategyId,
            strategyType,
            stopPrice,
            trailingDelta,
            icebergQty,
            newOrderRespType,
            selfTradePreventionMode,
            recvWindow);

        return await Client.ExecuteRequestAsync<Order>(request);
    }

    private static RestRequest BuildNewOrderRequest(
        string symbol,
        OrderSide side,
        OrderType type,
        TimeInForce? timeInForce = null,
        decimal quantity = -1,
        decimal quoteOrderQty = -1,
        decimal price = -1,
        string? newClientOrderId = null,
        int strategyId = -1,
        int strategyType = -1,
        decimal stopPrice = -1,
        long trailingDelta = -1,
        decimal icebergQty = -1,
        NewOrderRespType? newOrderRespType = null,
        SelfTradePreventionMode? selfTradePreventionMode = null,
        long recvWindow = -1
        )
    {
        CultureInfo.DefaultThreadCurrentCulture = CultureInfo.GetCultureInfo("en-US");

        var request = new RestRequest("api/v3/order", Method.Post);

        request.AddParameter("symbol", symbol.ToUpper());
        request.AddParameter("side", side.ToString());
        request.AddParameter("type", type.ToString());

        if(timeInForce is not null)
            request.AddParameter("timeInForce", timeInForce.ToString());

        if(quantity != -1)
            request.AddParameter("quantity", quantity);

        if (quoteOrderQty != -1)
            request.AddParameter("quoteOrderQty", quoteOrderQty);

        if (price != -1)
            request.AddParameter("price", price);

        if(!string.IsNullOrEmpty(newClientOrderId))
            request.AddParameter("newClientOrderId", newClientOrderId);

        if (strategyId != -1)
            request.AddParameter("strategyId", strategyId);

        if (strategyType != -1)
            request.AddParameter("strategyType", strategyType);

        if (stopPrice != -1)
            request.AddParameter("stopPrice", stopPrice);

        if (trailingDelta != -1)
            request.AddParameter("trailingDelta", trailingDelta);

        if (icebergQty != -1)
            request.AddParameter("icebergQty", icebergQty);

        if (newOrderRespType is not null)
            request.AddParameter("newOrderRespType", newOrderRespType.ToString());

        if (selfTradePreventionMode is not null)
            request.AddParameter("selfTradePreventionMode", selfTradePreventionMode.ToString());

        if (recvWindow != -1)
            request.AddParameter("recvWindow", recvWindow);

        return request;
    }

    #endregion New Order

    #endregion Original Methods

    #region Derived Methods

    #region Flatten Order Amount

    /// <summary>
    /// Flatten order mount according to exchange trading filters.
    /// </summary>
    /// <param name="symbol">Trading ticekr</param>
    /// <param name="amount">Initial amount</param>
    /// <param name="stepSizeDown">Determines by how many trading steps the resulting amount will be decremented.</param>
    /// <returns></returns>
    public decimal FlattenOrderAmount(string symbol, decimal amount, int stepSizeDown = 0)
    {
        var stepSize = Client.Market.GetTradeStepSize(symbol);

        if (stepSizeDown > 0)
            amount -= stepSizeDown * stepSize;

        var decimals = Math.Log10((double)stepSize);

        var multiplyer = (decimal)Math.Pow(10, -decimals);

        return Math.Floor(amount * multiplyer) / multiplyer;
    }

    /// <summary>
    /// Flatten order mount according to exchange trading filters.
    /// </summary>
    /// <param name="symbol">Trading ticekr</param>
    /// <param name="amount">Initial amount</param>
    /// <param name="stepSizeDown">Determines by how many trading steps the resulting amount will be decremented.</param>
    /// <returns></returns>
    public async Task<decimal> FlattenOrderAmountAsync(string symbol, decimal amount, int stepSizeDown = 0)
    {
        var stepSize = await Client.Market.GetTradeStepSizeAsync(symbol);

        if (stepSizeDown > 0)
            amount -= stepSizeDown * stepSize;

        var decimals = Math.Log10((double)stepSize);

        var multiplyer = (decimal)Math.Pow(10, -decimals);

        return Math.Floor(amount * multiplyer) / multiplyer;
    }

    #endregion Flatten Order Amount

    #endregion Derived Methods
}

