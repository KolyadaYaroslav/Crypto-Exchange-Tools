using System.Globalization;
using Newtonsoft.Json;

namespace CryptoExchangeTools.Models.Binance.Futures.USDM;

public class MarkData
{
    [JsonProperty("symbol")]
    public required string Symbol { get; set; }

    [JsonProperty("markPrice")]
    [JsonConverter(typeof(StringToDecimalConverter))]
    public decimal MarkPrice { get; set; }

    [JsonProperty("indexPrice")]
    [JsonConverter(typeof(StringToDecimalConverter))]
    public decimal IndexPrice { get; set; }

    [JsonProperty("estimatedSettlePrice")]
    [JsonConverter(typeof(StringToDecimalConverter))]
    public decimal EstimatedSettlePrice { get; set; }

    [JsonProperty("lastFundingRate")]
    [JsonConverter(typeof(StringToDecimalConverter))]
    public decimal LastFundingRate { get; set; }

    [JsonProperty("nextFundingTime")]
    public long NextFundingTime { get; set; }

    [JsonProperty("interestRate")]
    [JsonConverter(typeof(StringToDecimalConverter))]
    public decimal InterestRate { get; set; }

    [JsonProperty("time")]
    public long Time { get; set; }
}

public class ExchangeInformation
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

public class RateLimit
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

public class SymbolData
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

public class Filter
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

public class FundingRateData
{
    [JsonProperty("symbol")]
    public required string Symbol { get; set; }

    [JsonProperty("markPrice")]
    [JsonConverter(typeof(StringToDecimalConverter))]
    public decimal MarkPrice { get; set; }

    [JsonProperty("fundingRate")]
    [JsonConverter(typeof(StringToDecimalConverter))]
    public decimal FundingRate { get; set; }

    [JsonProperty("fundingTime")]
    public long FundingTime { get; set; }
}

public enum KlineInterval
{
    _1m,
    _3m,
    _5m,
    _15m,
    _30m,
    _1h,
    _2h,
    _4h,
    _6h,
    _8h,
    _12h,
    _1d,
    _3d,
    _1w,
    _1M
}

public class KlineData
{
    public long OpenTime { get; set; }
    public decimal Open { get; set; }
    public decimal High { get; set; }
    public decimal Low { get; set; }
    public decimal Close { get; set; }
    public decimal Volume { get; set; }
    public long CloseTime { get; set; }
    public decimal QuoteAssetVolume { get; set; }
    public long NumberOfTrades { get; set; }
    public decimal TakerBuyBaseAssetVolume { get; set; }
    public decimal TakerBuyQuoteAssetVolume { get; set; }

    public KlineData(IReadOnlyList<object> rawObject)
    {
        OpenTime = (long)rawObject[0];
        Open = decimal.Parse((string)rawObject[1], new CultureInfo("en-US"));
        High = decimal.Parse((string)rawObject[2], new CultureInfo("en-US"));
        Low = decimal.Parse((string)rawObject[3], new CultureInfo("en-US"));
        Close = decimal.Parse((string)rawObject[4], new CultureInfo("en-US"));
        Volume = decimal.Parse((string)rawObject[5], new CultureInfo("en-US"));
        CloseTime = (long)rawObject[6];
        QuoteAssetVolume = decimal.Parse((string)rawObject[7], new CultureInfo("en-US"));
        NumberOfTrades = (long)rawObject[8];
        TakerBuyBaseAssetVolume = decimal.Parse((string)rawObject[9], new CultureInfo("en-US"));
        TakerBuyQuoteAssetVolume = decimal.Parse((string)rawObject[10], new CultureInfo("en-US"));
    }
}