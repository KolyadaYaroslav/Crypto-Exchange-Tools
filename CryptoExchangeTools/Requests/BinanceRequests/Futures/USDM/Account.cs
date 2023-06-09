using CryptoExchangeTools.Models.Binance.Futures.USDM;
using RestSharp;

namespace CryptoExchangeTools.Requests.BinanceRequests.Futures.USDM;

public class Account
{
    private BinanceFuturesClient Client;

    public Account(BinanceFuturesClient client)
    {
        Client = client;
    }

    #region Original Methods

    #region New Order

    /// <summary>
    /// Place a new futures order.
    /// </summary>
    /// <param name="symbol"></param>
    /// <param name="side"></param>
    /// <param name="type"></param>
    /// <param name="quantity">Cannot be sent with closePosition=true(Close-All).</param>
    /// <param name="price"></param>
    /// <param name="positionSide">Default BOTH for One-way Mode ; LONG or SHORT for Hedge Mode. It must be sent in Hedge Mode.</param>
    /// <param name="timeInForce"></param>
    /// <param name="reduceOnly">"true" or "false". default "false". Cannot be sent in Hedge Mode; cannot be sent with closePosition=true</param>
    /// <param name="newClientOrderId">A unique id among open orders. Automatically generated if not sent. Can only be string following the rule: ^[\.A-Z\:/a-z0-9_-]{1,36}$</param>
    /// <param name="stopPrice">Used with STOP/STOP_MARKET or TAKE_PROFIT/TAKE_PROFIT_MARKET orders.</param>
    /// <param name="closePosition">true, false；Close-All，used with STOP_MARKET or TAKE_PROFIT_MARKET.</param>
    /// <param name="activationPrice">Used with TRAILING_STOP_MARKET orders, default as the latest price(supporting different workingType)</param>
    /// <param name="callbackRate">Used with TRAILING_STOP_MARKET orders, min 0.1, max 5 where 1 for 1%</param>
    /// <param name="workingType">stopPrice triggered by: "MARK_PRICE", "CONTRACT_PRICE". Default "CONTRACT_PRICE"</param>
    /// <param name="priceProtect">"TRUE" or "FALSE", default "FALSE". Used with STOP/STOP_MARKET or TAKE_PROFIT/TAKE_PROFIT_MARKET orders.</param>
    /// <param name="newOrderRespType"></param>
    /// <param name="recvWindow">"ACK", "RESULT", default "ACK"</param>
    /// <returns></returns>
    public NewOrderResult NewOrder(
        string symbol,
        OrderSide side,
        OrderType type,
        decimal quantity = -1,
        decimal price = -1,
        PositionSide? positionSide = null,
        TimeInForce? timeInForce = null,
        string? reduceOnly = null,
        string? newClientOrderId = null,
        decimal stopPrice = -1,
        string? closePosition = null,
        decimal activationPrice = -1,
        decimal callbackRate = -1,
        WorkingType? workingType = null,
        bool priceProtect = false,
        NewOrderRespType? newOrderRespType = null,
        long recvWindow = -1)
    {
        var request = BuildNewOrderRequest(
            symbol,
            side,
            type,
            quantity,
            price,
            positionSide,
            timeInForce,
            reduceOnly,
            newClientOrderId,
            stopPrice,
            closePosition,
            activationPrice,
            callbackRate,
            workingType,
            priceProtect,
            newOrderRespType,
            recvWindow);

        return Client.ExecuteRequest<NewOrderResult>(request);
    }

    /// <summary>
    /// Place a new futures order.
    /// </summary>
    /// <param name="symbol"></param>
    /// <param name="side"></param>
    /// <param name="type"></param>
    /// <param name="quantity">Cannot be sent with closePosition=true(Close-All).</param>
    /// <param name="price"></param>
    /// <param name="positionSide">Default BOTH for One-way Mode ; LONG or SHORT for Hedge Mode. It must be sent in Hedge Mode.</param>
    /// <param name="timeInForce"></param>
    /// <param name="reduceOnly">"true" or "false". default "false". Cannot be sent in Hedge Mode; cannot be sent with closePosition=true</param>
    /// <param name="newClientOrderId">A unique id among open orders. Automatically generated if not sent. Can only be string following the rule: ^[\.A-Z\:/a-z0-9_-]{1,36}$</param>
    /// <param name="stopPrice">Used with STOP/STOP_MARKET or TAKE_PROFIT/TAKE_PROFIT_MARKET orders.</param>
    /// <param name="closePosition">true, false；Close-All，used with STOP_MARKET or TAKE_PROFIT_MARKET.</param>
    /// <param name="activationPrice">Used with TRAILING_STOP_MARKET orders, default as the latest price(supporting different workingType)</param>
    /// <param name="callbackRate">Used with TRAILING_STOP_MARKET orders, min 0.1, max 5 where 1 for 1%</param>
    /// <param name="workingType">stopPrice triggered by: "MARK_PRICE", "CONTRACT_PRICE". Default "CONTRACT_PRICE"</param>
    /// <param name="priceProtect">"TRUE" or "FALSE", default "FALSE". Used with STOP/STOP_MARKET or TAKE_PROFIT/TAKE_PROFIT_MARKET orders.</param>
    /// <param name="newOrderRespType"></param>
    /// <param name="recvWindow">"ACK", "RESULT", default "ACK"</param>
    /// <returns></returns>
    public async Task<NewOrderResult> NewOrderAsync(
        string symbol,
        OrderSide side,
        OrderType type,
        decimal quantity = -1,
        decimal price = -1,
        PositionSide? positionSide = null,
        TimeInForce? timeInForce = null,
        string? reduceOnly = null,
        string? newClientOrderId = null,
        decimal stopPrice = -1,
        string? closePosition = null,
        decimal activationPrice = -1,
        decimal callbackRate = -1,
        WorkingType? workingType = null,
        bool priceProtect = false,
        NewOrderRespType? newOrderRespType = null,
        long recvWindow = -1)
    {
        var request = BuildNewOrderRequest(
            symbol,
            side,
            type,
            quantity,
            price,
            positionSide,
            timeInForce,
            reduceOnly,
            newClientOrderId,
            stopPrice,
            closePosition,
            activationPrice,
            callbackRate,
            workingType,
            priceProtect,
            newOrderRespType,
            recvWindow);

        return await Client.ExecuteRequestAsync<NewOrderResult>(request);
    }

    private static RestRequest BuildNewOrderRequest(
        string symbol,
        OrderSide side,
        OrderType type,
        decimal quantity = - 1,
        decimal price = - 1,
        PositionSide? positionSide = null,
        TimeInForce? timeInForce = null,
        string? reduceOnly = null,
        string? newClientOrderId = null,
        decimal stopPrice = - 1,
        string? closePosition = null,
        decimal activationPrice = - 1,
        decimal callbackRate = - 1,
        WorkingType? workingType = null,
        bool priceProtect = false,
        NewOrderRespType? newOrderRespType = null,
        long recvWindow = -1)
    {
        var request = new RestRequest("fapi/v1/order", Method.Post);

        request.AddParameter("symbol", symbol);
        request.AddParameter("side", side.ToString());
        request.AddParameter("type", type.ToString());

        if(quantity != -1)
            request.AddParameter("quantity", quantity);

        if (price != -1)
            request.AddParameter("price", price);

        if(positionSide is not null)
            request.AddParameter("positionSide", positionSide.ToString());

        if (timeInForce is not null)
            request.AddParameter("timeInForce", timeInForce.ToString());

        if (!string.IsNullOrEmpty(reduceOnly))
            request.AddParameter("reduceOnly", reduceOnly.ToLower());

        if (!string.IsNullOrEmpty(newClientOrderId))
            request.AddParameter("newClientOrderId", newClientOrderId);

        if (stopPrice != -1)
            request.AddParameter("stopPrice", stopPrice);

        if (!string.IsNullOrEmpty(closePosition))
            request.AddParameter("closePosition", closePosition);

        if (activationPrice != -1)
            request.AddParameter("activationPrice", activationPrice);

        if (callbackRate != -1)
            request.AddParameter("callbackRate", callbackRate);

        if (workingType is not null)
            request.AddParameter("workingType", workingType.ToString());

        request.AddParameter("priceProtect", priceProtect.ToString().ToUpper());

        if (newOrderRespType is not null)
            request.AddParameter("newOrderRespType", newOrderRespType.ToString());

        if (recvWindow != -1)
            request.AddParameter("recvWindow", recvWindow);

        return request;
    }

    #endregion New Order

    #region Modifiy Isolated Position Margin

    /// <summary>
    /// Modify Isolated Position Margin.
    /// </summary>
    /// <param name="symbol"></param>
    /// <param name="amount"></param>
    /// <param name="type">1: Add position margin，2: Reduce position margin.</param>
    /// <param name="positionSide">Default BOTH for One-way Mode ; LONG or SHORT for Hedge Mode. It must be sent with Hedge Mode.</param>
    /// <param name="recvWindow"></param>
    /// <returns></returns>
    public ModifyIsolatedPositionMarginresponse ModifiyIsolatedPositionMargin(
        string symbol,
        decimal amount,
        PositionMarginChange type,
        PositionSide? positionSide = null,
        long recvWindow = -1)
    {
        var request = BuildModifiyIsolatedPositionMargin(symbol, amount, type, positionSide, recvWindow);

        return Client.ExecuteRequest<ModifyIsolatedPositionMarginresponse>(request);
    }

    /// <summary>
    /// Modify Isolated Position Margin.
    /// </summary>
    /// <param name="symbol"></param>
    /// <param name="amount"></param>
    /// <param name="type">1: Add position margin，2: Reduce position margin.</param>
    /// <param name="positionSide">Default BOTH for One-way Mode ; LONG or SHORT for Hedge Mode. It must be sent with Hedge Mode.</param>
    /// <param name="recvWindow"></param>
    /// <returns></returns>
    public async Task<ModifyIsolatedPositionMarginresponse> ModifiyIsolatedPositionMarginAsync(
        string symbol,
        decimal amount,
        PositionMarginChange type,
        PositionSide? positionSide = null,
        long recvWindow = -1)
    {
        var request = BuildModifiyIsolatedPositionMargin(symbol, amount, type, positionSide, recvWindow);

        return await Client.ExecuteRequestAsync<ModifyIsolatedPositionMarginresponse>(request);
    }

    private static RestRequest BuildModifiyIsolatedPositionMargin(
        string symbol,
        decimal amount,
        PositionMarginChange type,
        PositionSide? positionSide = null,
        long recvWindow = -1)
    {
        var request = new RestRequest("fapi/v1/positionMargin", Method.Post);

        request.AddParameter("symbol", symbol);
        request.AddParameter("amount", amount);

        request.AddParameter("type", (int)type);

        if (positionSide is not null)
            request.AddParameter("positionSide", positionSide.ToString());

        if (recvWindow != -1)
            request.AddParameter("recvWindow", recvWindow);

        return request;
    }

    #endregion Modifiy Isolated Position Margin

    #region Position Information

    /// <summary>
    /// Get current position information.
    /// </summary>
    /// <param name="symbol"></param>
    /// <param name="recvWindow"></param>
    /// <returns></returns>
    public PositionInformation[] GetPositionInformation(string symbol, long recvWindow = -1)
    {
        var request = BuilfGetPositionInformation(symbol, recvWindow);

        return Client.ExecuteRequest<PositionInformation[]>(request);
    }

    /// <summary>
    /// Get current position information.
    /// </summary>
    /// <param name="symbol"></param>
    /// <param name="recvWindow"></param>
    /// <returns></returns>
    public async Task<PositionInformation[]> GetPositionInformationAsync(string symbol, long recvWindow = -1)
    {
        var request = BuilfGetPositionInformation(symbol, recvWindow);

        return await Client.ExecuteRequestAsync<PositionInformation[]>(request);
    }

    private static RestRequest BuilfGetPositionInformation(string symbol, long recvWindow = -1)
    {
        var request = new RestRequest("fapi/v2/positionRisk", Method.Get);

        request.AddParameter("symbol", symbol);

        if (recvWindow != -1)
            request.AddParameter("recvWindow", recvWindow);

        return request;
    }

    #endregion Position Information

    #region Change Leverage

    /// <summary>
    /// Change user's initial leverage of specific symbol market.
    /// </summary>
    /// <param name="symbol"></param>
    /// <param name="leverage">target initial leverage: int from 1 to 125.</param>
    /// <param name="recvWindow"></param>
    /// <returns></returns>
    public ChangeLeverageResponse ChangeLeverage(string symbol, int leverage, long recvWindow = -1)
    {
        var request = BuildChangeLeverage(symbol, leverage, recvWindow);

        return Client.ExecuteRequest<ChangeLeverageResponse>(request);
    }

    /// <summary>
    /// Change user's initial leverage of specific symbol market.
    /// </summary>
    /// <param name="symbol"></param>
    /// <param name="leverage">target initial leverage: int from 1 to 125.</param>
    /// <param name="recvWindow"></param>
    /// <returns></returns>
    public async Task<ChangeLeverageResponse> ChangeLeverageAsync(string symbol, int leverage, long recvWindow = -1)
    {
        var request = BuildChangeLeverage(symbol, leverage, recvWindow);

        return await Client.ExecuteRequestAsync<ChangeLeverageResponse>(request);
    }

    private static RestRequest BuildChangeLeverage(string symbol, int leverage, long recvWindow = -1)
    {
        var request = new RestRequest("fapi/v1/leverage", Method.Post);

        request.AddParameter("symbol", symbol);
        request.AddParameter("leverage", leverage);

        if (recvWindow != -1)
            request.AddParameter("recvWindow", recvWindow);

        return request;
    }

    #endregion Change Leverage

    #region Query Order

    public Order QueryOrder(string symbol, long orderId = -1, string? origClientOrderId = null, long recvWindow = -1)
    {
        var request = BuildQueryOrder(symbol, orderId, origClientOrderId, recvWindow);

        return Client.ExecuteRequest<Order>(request);
    }

    public async Task<Order> QueryOrderAsync(string symbol, long orderId = -1, string? origClientOrderId = null, long recvWindow = -1)
    {
        var request = BuildQueryOrder(symbol, orderId, origClientOrderId, recvWindow);

        return await Client.ExecuteRequestAsync<Order>(request);
    }

    private static RestRequest BuildQueryOrder(string symbol, long orderId = -1, string? origClientOrderId = null, long recvWindow = -1)
    {
        var request = new RestRequest("fapi/v1/order", Method.Get);

        request.AddParameter("symbol", symbol);

        if (orderId != -1)
            request.AddParameter("orderId", orderId);

        if (!string.IsNullOrEmpty(origClientOrderId))
            request.AddParameter("origClientOrderId", origClientOrderId.ToLower());

        if (recvWindow != -1)
            request.AddParameter("recvWindow", recvWindow);

        return request;
    }

    #endregion Query Order

    #endregion Original Methods

    #region Derived Methods

    #endregion Derived Methods
}

