using Newtonsoft.Json;

namespace CryptoExchangeTools.Models.Binance;

public partial class ExchangeInformation
{
    [JsonProperty("timezone")]
    public required string Timezone { get; set; }

    [JsonProperty("serverTime")]
    public long ServerTime { get; set; }

    [JsonProperty("rateLimits")]
    public RateLimit[]? RateLimits { get; set; }

    [JsonProperty("exchangeFilters")]
    public object[]? ExchangeFilters { get; set; }

    [JsonProperty("symbols")]
    public required SymbolData[] Symbols { get; set; }
}

public partial class RateLimit
{
    [JsonProperty("rateLimitType")]
    public required string RateLimitType { get; set; }

    [JsonProperty("interval")]
    public required string Interval { get; set; }

    [JsonProperty("intervalNum")]
    public long IntervalNum { get; set; }

    [JsonProperty("limit")]
    public long Limit { get; set; }
}

public partial class SymbolData
{
    [JsonProperty("symbol")]
    public required string Symbol { get; set; }

    [JsonProperty("status")]
    public required string Status { get; set; }

    [JsonProperty("baseAsset")]
    public required string BaseAsset { get; set; }

    [JsonProperty("baseAssetPrecision")]
    public long BaseAssetPrecision { get; set; }

    [JsonProperty("quoteAsset")]
    public required string QuoteAsset { get; set; }

    [JsonProperty("quotePrecision")]
    public long QuotePrecision { get; set; }

    [JsonProperty("quoteAssetPrecision")]
    public long QuoteAssetPrecision { get; set; }

    [JsonProperty("baseCommissionPrecision")]
    public long BaseCommissionPrecision { get; set; }

    [JsonProperty("quoteCommissionPrecision")]
    public long QuoteCommissionPrecision { get; set; }

    [JsonProperty("orderTypes")]
    public required string[] OrderTypes { get; set; }

    [JsonProperty("icebergAllowed")]
    public bool IcebergAllowed { get; set; }

    [JsonProperty("ocoAllowed")]
    public bool OcoAllowed { get; set; }

    [JsonProperty("quoteOrderQtyMarketAllowed")]
    public bool QuoteOrderQtyMarketAllowed { get; set; }

    [JsonProperty("allowTrailingStop")]
    public bool AllowTrailingStop { get; set; }

    [JsonProperty("cancelReplaceAllowed")]
    public bool CancelReplaceAllowed { get; set; }

    [JsonProperty("isSpotTradingAllowed")]
    public bool IsSpotTradingAllowed { get; set; }

    [JsonProperty("isMarginTradingAllowed")]
    public bool IsMarginTradingAllowed { get; set; }

    [JsonProperty("filters")]
    public Filter[]? Filters { get; set; }

    [JsonProperty("permissions")]
    public string[]? Permissions { get; set; }

    [JsonProperty("defaultSelfTradePreventionMode")]
    public required string DefaultSelfTradePreventionMode { get; set; }

    [JsonProperty("allowedSelfTradePreventionModes")]
    public string[]? AllowedSelfTradePreventionModes { get; set; }
}

public partial class Filter
{
    [JsonProperty("filterType")]
    public required string FilterType { get; set; }

    [JsonProperty("minPrice", NullValueHandling = NullValueHandling.Ignore)]
    [JsonConverter(typeof(StringToDecimalConverter))]
    public decimal MinPrice { get; set; }

    [JsonProperty("maxPrice", NullValueHandling = NullValueHandling.Ignore)]
    [JsonConverter(typeof(StringToDecimalConverter))]
    public decimal MaxPrice { get; set; }

    [JsonProperty("tickSize", NullValueHandling = NullValueHandling.Ignore)]
    [JsonConverter(typeof(StringToDecimalConverter))]
    public decimal TickSize { get; set; }

    [JsonProperty("minQty", NullValueHandling = NullValueHandling.Ignore)]
    [JsonConverter(typeof(StringToDecimalConverter))]
    public decimal MinQty { get; set; }

    [JsonProperty("maxQty", NullValueHandling = NullValueHandling.Ignore)]
    [JsonConverter(typeof(StringToDecimalConverter))]
    public decimal MaxQty { get; set; }

    [JsonProperty("stepSize", NullValueHandling = NullValueHandling.Ignore)]
    [JsonConverter(typeof(StringToDecimalConverter))]
    public decimal StepSize { get; set; }

    [JsonProperty("limit", NullValueHandling = NullValueHandling.Ignore)]
    public long Limit { get; set; }

    [JsonProperty("minTrailingAboveDelta", NullValueHandling = NullValueHandling.Ignore)]
    public long MinTrailingAboveDelta { get; set; }

    [JsonProperty("maxTrailingAboveDelta", NullValueHandling = NullValueHandling.Ignore)]
    public long MaxTrailingAboveDelta { get; set; }

    [JsonProperty("minTrailingBelowDelta", NullValueHandling = NullValueHandling.Ignore)]
    public long MinTrailingBelowDelta { get; set; }

    [JsonProperty("maxTrailingBelowDelta", NullValueHandling = NullValueHandling.Ignore)]
    public long MaxTrailingBelowDelta { get; set; }

    [JsonProperty("bidMultiplierUp", NullValueHandling = NullValueHandling.Ignore)]
    [JsonConverter(typeof(StringToDecimalConverter))]
    public decimal BidMultiplierUp { get; set; }

    [JsonProperty("bidMultiplierDown", NullValueHandling = NullValueHandling.Ignore)]
    [JsonConverter(typeof(StringToDecimalConverter))]
    public decimal BidMultiplierDown { get; set; }

    [JsonProperty("askMultiplierUp", NullValueHandling = NullValueHandling.Ignore)]
    [JsonConverter(typeof(StringToDecimalConverter))]
    public decimal AskMultiplierUp { get; set; }

    [JsonProperty("askMultiplierDown", NullValueHandling = NullValueHandling.Ignore)]
    [JsonConverter(typeof(StringToDecimalConverter))]
    public decimal AskMultiplierDown { get; set; }

    [JsonProperty("avgPriceMins", NullValueHandling = NullValueHandling.Ignore)]
    [JsonConverter(typeof(StringToDecimalConverter))]
    public decimal AvgPriceMins { get; set; }

    [JsonProperty("minNotional", NullValueHandling = NullValueHandling.Ignore)]
    [JsonConverter(typeof(StringToDecimalConverter))]
    public decimal MinNotional { get; set; }

    [JsonProperty("applyMinToMarket", NullValueHandling = NullValueHandling.Ignore)]
    public bool ApplyMinToMarket { get; set; }

    [JsonProperty("maxNotional", NullValueHandling = NullValueHandling.Ignore)]
    [JsonConverter(typeof(StringToDecimalConverter))]
    public decimal MaxNotional { get; set; }

    [JsonProperty("applyMaxToMarket", NullValueHandling = NullValueHandling.Ignore)]
    public bool ApplyMaxToMarket { get; set; }

    [JsonProperty("maxNumOrders", NullValueHandling = NullValueHandling.Ignore)]
    public long MaxNumOrders { get; set; }

    [JsonProperty("maxNumAlgoOrders", NullValueHandling = NullValueHandling.Ignore)]
    public long MaxNumAlgoOrders { get; set; }
}

