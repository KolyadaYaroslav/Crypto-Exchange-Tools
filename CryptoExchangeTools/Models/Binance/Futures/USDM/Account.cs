﻿using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CryptoExchangeTools.Models.Binance.Futures.USDM;

public enum OrderSide
{
    BUY,
    SELL
}

public enum OrderType
{
    LIMIT,
    MARKET,
    STOP,
    STOP_MARKET,
    TAKE_PROFIT,
    TAKE_PROFIT_MARKET,
    TRAILING_STOP_MARKET,
}

public enum OrderStatus
{
    NEW,
    PARTIALLY_FILLED,
    FILLED,
    CANCELED,
    REJECTED,
    EXPIRED,
}

public enum PositionSide
{
    BOTH,
    LONG,
    SHORT
}

public enum PositionMarginChange
{
    ADD = 1,
    REDUCE = 2
}

public enum TimeInForce
{
    /// <summary>
    /// Good Till Cancel.
    /// </summary>
    GTC,
    /// <summary>
    /// Immediate or Cancel.
    /// </summary>
    IOC,
    /// <summary>
    /// Fill or Kill.
    /// </summary>
    FOK,
    /// <summary>
    /// Good Till Crossing (Post Only).
    /// </summary>
    GTX
}

public enum WorkingType
{
    MARK_PRICE,
    CONTRACT_PRICE
}

public enum NewOrderRespType
{
    ACK,
    RESULT
}

public partial class NewOrderResult
{
    [JsonProperty("clientOrderId")]
    public string? ClientOrderId { get; set; }

    [JsonProperty("cumQty")]
    [JsonConverter(typeof(StringToDecimalConverter))]
    public decimal CumQty { get; set; }

    [JsonProperty("cumQuote")]
    [JsonConverter(typeof(StringToDecimalConverter))]
    public decimal CumQuote { get; set; }

    [JsonProperty("executedQty")]
    [JsonConverter(typeof(StringToDecimalConverter))]
    public decimal ExecutedQty { get; set; }

    [JsonProperty("orderId")]
    public long OrderId { get; set; }

    [JsonProperty("avgPrice")]
    [JsonConverter(typeof(StringToDecimalConverter))]
    public decimal AvgPrice { get; set; }

    [JsonProperty("origQty")]
    [JsonConverter(typeof(StringToDecimalConverter))]
    public decimal OrigQty { get; set; }

    [JsonProperty("price")]
    [JsonConverter(typeof(StringToDecimalConverter))]
    public decimal Price { get; set; }

    [JsonProperty("reduceOnly")]
    public bool ReduceOnly { get; set; }

    [JsonProperty("side")]
    [JsonConverter(typeof(StringEnumConverter))]
    public OrderSide Side { get; set; }

    [JsonProperty("positionSide")]
    [JsonConverter(typeof(StringEnumConverter))]
    public PositionSide PositionSide { get; set; }

    [JsonProperty("status")]
    [JsonConverter(typeof(StringEnumConverter))]
    public OrderStatus Status { get; set; }

    /// <summary>
    /// Please ignore when order type is TRAILING_STOP_MARKET.
    /// </summary>
    [JsonProperty("stopPrice")]
    [JsonConverter(typeof(StringToDecimalConverter))]
    public decimal StopPrice { get; set; }

    /// <summary>
    /// If Close-All.
    /// </summary>
    [JsonProperty("closePosition")]
    public bool ClosePosition { get; set; }

    [JsonProperty("symbol")]
    public required string Symbol { get; set; }

    [JsonProperty("timeInForce")]
    [JsonConverter(typeof(StringEnumConverter))]
    public TimeInForce TimeInForce { get; set; }

    [JsonProperty("type")]
    [JsonConverter(typeof(StringEnumConverter))]
    public OrderType Type { get; set; }

    [JsonProperty("origType")]
    [JsonConverter(typeof(StringEnumConverter))]
    public OrderType OrigType { get; set; }

    /// <summary>
    /// Activation price, only return with TRAILING_STOP_MARKET order.
    /// </summary>
    [JsonProperty("activatePrice")]
    [JsonConverter(typeof(StringToDecimalConverter))]
    public decimal ActivatePrice { get; set; }

    /// <summary>
    /// Callback rate, only return with TRAILING_STOP_MARKET order.
    /// </summary>
    [JsonProperty("priceRate")]
    [JsonConverter(typeof(StringToDecimalConverter))]
    public decimal PriceRate { get; set; }

    [JsonProperty("updateTime")]
    public long UpdateTime { get; set; }

    [JsonProperty("workingType")]
    [JsonConverter(typeof(StringEnumConverter))]
    public WorkingType WorkingType { get; set; }

    /// <summary>
    /// If conditional order trigger is protected.
    /// </summary>
    [JsonProperty("priceProtect")]
    public bool PriceProtect { get; set; }
}

public class ModifyIsolatedPositionMarginresponse
{
    [JsonProperty("amount")]
    [JsonConverter(typeof(StringToDecimalConverter))]
    public decimal Amount { get; set; }

    [JsonProperty("code")]
    [JsonConverter(typeof(StringToLongConverter))]
    public long Code { get; set; }

    [JsonProperty("msg")]
    public required string Message { get; set; }

    [JsonProperty("type")]
    public int Type { get; set; }
}

public class PositionInformation
{
    [JsonProperty("entryPrice")]
    [JsonConverter(typeof(StringToDecimalConverter))]
    public decimal EntryPrice { get; set; }

    [JsonProperty("marginType")]
    public required string MarginType { get; set; }

    [JsonProperty("isAutoAddMargin")]
    public required string IsAutoAddMargin { get; set; }

    [JsonProperty("isolatedMargin")]
    [JsonConverter(typeof(StringToDecimalConverter))]
    public decimal IsolatedMargin { get; set; }

    [JsonProperty("leverage")]
    [JsonConverter(typeof(StringToDecimalConverter))]
    public decimal Leverage { get; set; }

    [JsonProperty("liquidationPrice")]
    [JsonConverter(typeof(StringToDecimalConverter))]
    public decimal LiquidationPrice { get; set; }

    [JsonProperty("markPrice")]
    [JsonConverter(typeof(StringToDecimalConverter))]
    public decimal MarkPrice { get; set; }

    [JsonProperty("maxNotionalValue")]
    [JsonConverter(typeof(StringToDecimalConverter))]
    public decimal MaxNotionalValue { get; set; }

    [JsonProperty("positionAmt")]
    [JsonConverter(typeof(StringToDecimalConverter))]
    public decimal PositionAmt { get; set; }

    [JsonProperty("notional")]
    [JsonConverter(typeof(StringToDecimalConverter))]
    public decimal Notional { get; set; }

    [JsonProperty("isolatedWallet")]
    [JsonConverter(typeof(StringToDecimalConverter))]
    public decimal IsolatedWallet { get; set; }

    [JsonProperty("symbol")]
    public required string Symbol { get; set; }

    [JsonProperty("unRealizedProfit")]
    [JsonConverter(typeof(StringToDecimalConverter))]
    public decimal UnRealizedProfit { get; set; }

    [JsonProperty("positionSide")]
    [JsonConverter(typeof(StringEnumConverter))]
    public PositionSide PositionSide { get; set; }

    [JsonProperty("updateTime")]
    public long UpdateTime { get; set; }
}

public class ChangeLeverageResponse
{
    [JsonProperty("leverage")]
    [JsonConverter(typeof(StringToIntConverter))]
    public int leverage { get; set; }

    [JsonProperty("maxNotionalValue")]
    [JsonConverter(typeof(StringToLongConverter))]
    public long MaxNotionalValue { get; set; }

    [JsonProperty("symbol")]
    public required string Symbol { get; set; }
}

public partial class Order
{
    [JsonProperty("avgPrice")]
    [JsonConverter(typeof(StringToDecimalConverter))]
    public decimal AvgPrice { get; set; }

    [JsonProperty("clientOrderId")]
    public string? ClientOrderId { get; set; }

    [JsonProperty("cumQuote")]
    [JsonConverter(typeof(StringToDecimalConverter))]
    public decimal CumQuote { get; set; }

    [JsonProperty("executedQty")]
    [JsonConverter(typeof(StringToDecimalConverter))]
    public decimal ExecutedQty { get; set; }

    [JsonProperty("orderId")]
    public long OrderId { get; set; }

    [JsonProperty("origQty")]
    [JsonConverter(typeof(StringToDecimalConverter))]
    public decimal OrigQty { get; set; }

    [JsonProperty("origType")]
    [JsonConverter(typeof(StringEnumConverter))]
    public OrderType OrigType { get; set; }

    [JsonProperty("price")]
    [JsonConverter(typeof(StringToDecimalConverter))]
    public decimal Price { get; set; }

    [JsonProperty("reduceOnly")]
    public bool ReduceOnly { get; set; }

    [JsonProperty("side")]
    [JsonConverter(typeof(StringEnumConverter))]
    public OrderSide Side { get; set; }

    [JsonProperty("positionSide")]
    [JsonConverter(typeof(StringEnumConverter))]
    public PositionSide PositionSide { get; set; }

    [JsonProperty("status")]
    [JsonConverter(typeof(StringEnumConverter))]
    public OrderStatus Status { get; set; }

    [JsonProperty("stopPrice")]
    [JsonConverter(typeof(StringToDecimalConverter))]
    public decimal StopPrice { get; set; }

    [JsonProperty("closePosition")]
    public bool ClosePosition { get; set; }

    [JsonProperty("symbol")]
    public required string Symbol { get; set; }

    [JsonProperty("time")]
    public long Time { get; set; }

    [JsonProperty("timeInForce")]
    [JsonConverter(typeof(StringEnumConverter))]
    public TimeInForce TimeInForce { get; set; }

    [JsonProperty("type")]
    [JsonConverter(typeof(StringEnumConverter))]
    public OrderType Type { get; set; }

    [JsonProperty("activatePrice")]
    [JsonConverter(typeof(StringToDecimalConverter))]
    public decimal ActivatePrice { get; set; }

    [JsonProperty("priceRate")]
    [JsonConverter(typeof(StringToDecimalConverter))]
    public decimal PriceRate { get; set; }

    [JsonProperty("updateTime")]
    public long UpdateTime { get; set; }

    [JsonProperty("workingType")]
    [JsonConverter(typeof(StringEnumConverter))]
    public WorkingType WorkingType { get; set; }

    [JsonProperty("priceProtect")]
    public bool PriceProtect { get; set; }
}