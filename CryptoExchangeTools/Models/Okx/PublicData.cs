using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CryptoExchangeTools.Models.Okx;

public class InstrumentInfo
{
    [JsonProperty("alias")]
    public string? Alias { get; set; }

    [JsonProperty("baseCcy")]
    public required string BaseCcy { get; set; }

    [JsonProperty("ctMult")]
    public string? CtMult { get; set; }

    [JsonProperty("ctType")]
    public string? CtType { get; set; }

    [JsonProperty("ctVal")]
    public string? CtVal { get; set; }

    [JsonProperty("ctValCcy")]
    public string? CtValCcy { get; set; }

    [JsonProperty("expTime")]
    public string? ExpTime { get; set; }

    [JsonProperty("instFamily")]
    public string? InstFamily { get; set; }

    [JsonProperty("instId")]
    public required string InstId { get; set; }

    [JsonProperty("instType")]
    [JsonConverter(typeof(StringEnumConverter))]
    public InstrumentType InstType { get; set; }

    [JsonProperty("lever")]
    public string? Lever { get; set; }

    [JsonProperty("listTime")]
    [JsonConverter(typeof(StringToLongConverter))]
    public long ListTime { get; set; }

    [JsonProperty("lotSz")]
    [JsonConverter(typeof(StringToDecimalConverter))]
    public decimal LotSz { get; set; }

    [JsonProperty("maxIcebergSz")]
    [JsonConverter(typeof(StringToDecimalConverter))]
    public decimal MaxIcebergSz { get; set; }

    [JsonProperty("maxLmtSz")]
    [JsonConverter(typeof(StringToDecimalConverter))]
    public decimal MaxLmtSz { get; set; }

    [JsonProperty("maxMktSz")]
    [JsonConverter(typeof(StringToDecimalConverter))]
    public decimal MaxMktSz { get; set; }

    [JsonProperty("maxStopSz")]
    [JsonConverter(typeof(StringToDecimalConverter))]
    public decimal MaxStopSz { get; set; }

    [JsonProperty("maxTriggerSz")]
    [JsonConverter(typeof(StringToDecimalConverter))]
    public decimal MaxTriggerSz { get; set; }

    [JsonProperty("maxTwapSz")]
    [JsonConverter(typeof(StringToDecimalConverter))]
    public decimal MaxTwapSz { get; set; }

    [JsonProperty("minSz")]
    [JsonConverter(typeof(StringToDecimalConverter))]
    public decimal MinSz { get; set; }

    [JsonProperty("optType")]
    public string? OptType { get; set; }

    [JsonProperty("quoteCcy")]
    public required string QuoteCcy { get; set; }

    [JsonProperty("settleCcy")]
    public string? SettleCcy { get; set; }

    [JsonProperty("state")]
    public string? State { get; set; }

    [JsonProperty("stk")]
    public string? Stk { get; set; }

    [JsonProperty("tickSz")]
    [JsonConverter(typeof(StringToDecimalConverter))]
    public decimal TickSz { get; set; }

    [JsonProperty("uly")]
    public string? Uly { get; set; }
}
