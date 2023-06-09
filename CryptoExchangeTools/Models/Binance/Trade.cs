using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CryptoExchangeTools.Models.Binance;

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

public enum OrderStatus
{
    NEW,
    PARTIALLY_FILLED,
    FILLED,
    CANCELED,
    REJECTED,
    EXPIRED,
}

public enum NewOrderRespType
{
    ACK,
    RESULT,
    FULL
}

public enum SelfTradePreventionMode
{
    EXPIRE_TAKER,
    EXPIRE_MAKER,
    EXPIRE_BOTH,
    NONE
}

public class Order
{
    [JsonProperty("symbol")]
    public required string Symbol { get; set; }

    [JsonProperty("orderId")]
    public long OrderId { get; set; }

    [JsonProperty("orderListId")]
    public long OrderListId { get; set; }

    [JsonProperty("clientOrderId")]
    public string? ClientOrderId { get; set; }

    [JsonProperty("transactTime")]
    public long TransactTime { get; set; }

    [JsonProperty("price")]
    [JsonConverter(typeof(StringToDecimalConverter))]
    public decimal Price { get; set; }

    [JsonProperty("origQty")]
    [JsonConverter(typeof(StringToDecimalConverter))]
    public decimal OrigQty { get; set; }

    [JsonProperty("executedQty")]
    [JsonConverter(typeof(StringToDecimalConverter))]
    public decimal ExecutedQty { get; set; }

    [JsonProperty("cummulativeQuoteQty")]
    [JsonConverter(typeof(StringToDecimalConverter))]
    public decimal CummulativeQuoteQty { get; set; }

    [JsonProperty("status")]
    [JsonConverter(typeof(StringEnumConverter))]
    public OrderStatus Status { get; set; }

    [JsonProperty("timeInForce")]
    [JsonConverter(typeof(StringEnumConverter))]
    public TimeInForce TimeInForce { get; set; }

    [JsonProperty("type")]
    [JsonConverter(typeof(StringEnumConverter))]
    public OrderType Type { get; set; }

    [JsonProperty("side")]
    [JsonConverter(typeof(StringEnumConverter))]
    public OrderSide Side { get; set; }

    [JsonProperty("workingTime")]
    public long WorkingTime { get; set; }

    [JsonProperty("selfTradePreventionMode")]
    [JsonConverter(typeof(StringEnumConverter))]
    public SelfTradePreventionMode SelfTradePreventionMode { get; set; }
}


