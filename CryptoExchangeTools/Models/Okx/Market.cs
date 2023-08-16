using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CryptoExchangeTools.Models.Okx;

public class TickerInfo
{
    [JsonProperty("instType")]
    [JsonConverter(typeof(StringEnumConverter))]
    public InstrumentType InstType { get; set; }

    [JsonProperty("instId")]
    public required string InstId { get; set; }

    [JsonProperty("last")]
    [JsonConverter(typeof(StringToDecimalConverter))]
    public decimal Last { get; set; }

    [JsonProperty("lastSz")]
    [JsonConverter(typeof(StringToDecimalConverter))]
    public decimal LastSz { get; set; }

    [JsonProperty("askPx")]
    [JsonConverter(typeof(StringToDecimalConverter))]
    public decimal AskPx { get; set; }

    [JsonProperty("askSz")]
    [JsonConverter(typeof(StringToDecimalConverter))]
    public decimal AskSz { get; set; }

    [JsonProperty("bidPx")]
    [JsonConverter(typeof(StringToDecimalConverter))]
    public decimal BidPx { get; set; }

    [JsonProperty("bidSz")]
    [JsonConverter(typeof(StringToDecimalConverter))]
    public decimal BidSz { get; set; }

    [JsonProperty("open24h")]
    [JsonConverter(typeof(StringToDecimalConverter))]
    public decimal Open24H { get; set; }

    [JsonProperty("high24h")]
    [JsonConverter(typeof(StringToDecimalConverter))]
    public decimal High24H { get; set; }

    [JsonProperty("low24h")]
    [JsonConverter(typeof(StringToDecimalConverter))]
    public decimal Low24H { get; set; }

    [JsonProperty("volCcy24h")]
    [JsonConverter(typeof(StringToDecimalConverter))]
    public decimal VolCcy24H { get; set; }

    [JsonProperty("vol24h")]
    [JsonConverter(typeof(StringToDecimalConverter))]
    public decimal Vol24H { get; set; }

    [JsonProperty("sodUtc0")]
    [JsonConverter(typeof(StringToDecimalConverter))]
    public decimal SodUtc0 { get; set; }

    [JsonProperty("sodUtc8")]
    [JsonConverter(typeof(StringToDecimalConverter))]
    public decimal SodUtc8 { get; set; }

    [JsonProperty("ts")]
    [JsonConverter(typeof(StringToLongConverter))]
    public long Ts { get; set; }
}