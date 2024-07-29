using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CryptoExchangeTools.Models.Okx;

public class OrderResult
{
    /// <summary>
    /// Order ID
    /// </summary>
    [JsonProperty("ordId")]
    public required string OrderId { get; set; }

    /// <summary>
    /// Client Order ID as assigned by the client
    /// </summary>
    [JsonProperty("clOrdId")]
    public string? ClientOrderId { get; set; }

    /// <summary>
    /// Order tag
    /// </summary>
    [JsonProperty("tag")]
    public string? Tag { get; set; }

    /// <summary>
    /// The code of the event execution result, 0 means success.
    /// </summary>
    [JsonProperty("sCode")]
    public required string StatusCode { get; set; }

    /// <summary>
    /// Rejection or success message of event execution.
    /// </summary>
    [JsonProperty("sMsg")]
    public string? SMsg { get; set; }
}

public enum TradeMode
{
    cross,
    isolated,
    cash
}

public enum OrderSide
{
    buy,
    sell
}

/// <summary>
/// Position side. Only applicable to FUTURES/SWAP.
/// </summary>
public enum PositionSide
{
    net,
    @long,
    @short
}

public enum OrderType
{
    /// <summary>
    /// Market order
    /// </summary>
    market,

    /// <summary>
    /// Limit order
    /// </summary>
    limit,

    /// <summary>
    /// Post-only order
    /// </summary>
    post_only,

    /// <summary>
    /// Fill-or-kill order
    /// </summary>
    fok,

    /// <summary>
    /// Immediate-or-cancel order
    /// </summary>
    ioc,

    /// <summary>
    /// Market order with immediate-or-cancel order (applicable only to Futures and Perpetual swap).
    /// </summary>
    optimal_limit_ioc
}

/// <summary>
/// Whether the target currency uses the quote or base currency. Only applicable to SPOT Market Order. Default is quote_ccy for buy, base_ccy for sell
/// </summary>
public enum TargetCurrency
{
    /// <summary>
    /// Base currency
    /// </summary>
    base_ccy,

    /// <summary>
    /// Quote currency
    /// </summary>
    quote_ccy
}

/// <summary>
/// The Default is last
/// </summary>
public enum TriggerPriceType
{
    last,
    index,
    mark
}

/// <summary>
/// The default value is manual
/// </summary>
public enum MarginType
{
    manual,
    auto_borrow,
    auto_repay
}

/// <summary>
/// Default to cancel maker. Cancel both does not support FOK.
/// </summary>
public enum SelfTradePreventionMode
{
    cancel_maker,
    cancel_taker,
    cancel_both
}

[JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
internal class NewOrder
{
    public NewOrder(
        string instId,
        TradeMode tdMode,
        OrderSide side,
        OrderType ordType,
        decimal sz,
        string? ccy,
        string? clOrdId,
        string? tag,
        PositionSide? posSide,
        decimal px,
        bool? reduceOnly,
        TargetCurrency? tgtCcy,
        bool? banAmend,
        decimal tpTriggerPx,
        decimal tpOrdPx,
        decimal slTriggerPx,
        decimal slOrdPx,
        TriggerPriceType? tpTriggerPxType,
        TriggerPriceType? slTriggerPxType,
        MarginType? quickMgnType,
        string? stpId,
        SelfTradePreventionMode? stpMode)
    {
        this.instId = instId;
        this.tdMode = tdMode.ToString();
        this.side = side.ToString();
        this.ordType = ordType.ToString();
        this.sz = sz.ToString();
        this.ccy = ccy;
        this.clOrdId = clOrdId;
        this.tag = tag;
        this.posSide = posSide is null ? null : posSide.ToString();
        this.px = px == -1 ? null : px.ToString();
        this.reduceOnly = reduceOnly;
        this.tgtCcy = tgtCcy is null ? null : tgtCcy.ToString();
        this.banAmend = banAmend;
        this.tpTriggerPx = tpTriggerPx == -1 ? null : tpTriggerPx.ToString();
        this.tpOrdPx = tpOrdPx == -1 ? null : tpOrdPx.ToString();
        this.slTriggerPx = slTriggerPx == -1 ? null : slTriggerPx.ToString();
        this.slOrdPx = slOrdPx == -1 ? null : slOrdPx.ToString();
        this.tpTriggerPxType = tpTriggerPxType is null ? null : tpTriggerPxType.ToString();
        this.slTriggerPxType = slTriggerPxType is null ? null : slTriggerPxType.ToString();
        this.quickMgnType = quickMgnType is null ? null : quickMgnType.ToString();
        this.stpId = stpId;
        this.stpMode = stpId is null ? null : stpMode.ToString();
    }

    public string instId { get; set; }
    public string tdMode {get; set; }
    public string side {get; set; }
    public string ordType {get; set; }
    public string sz {get; set; }
    public string? ccy {get; set; }
    public string? clOrdId {get; set; }
    public string? tag {get; set; }
    public string? posSide {get; set; }
    public string? px {get; set; }
    public bool? reduceOnly {get; set; }
    public string? tgtCcy {get; set; }
    public bool? banAmend {get; set; }
    public string? tpTriggerPx {get; set; }
    public string? tpOrdPx {get; set; }
    public string? slTriggerPx {get; set; }
    public string? slOrdPx {get; set; }
    public string? tpTriggerPxType {get; set; }
    public string? slTriggerPxType {get; set; }
    public string? quickMgnType  {get; set; }
    public string? stpId {get; set; }
    public string? stpMode {get; set; }
}

public class OrderDetails
{
    [JsonProperty("instType")]
    [JsonConverter(typeof(StringEnumConverter))]
    public InstrumentType InstType { get; set; }

    [JsonProperty("instId")]
    public required string InstId { get; set; }

    [JsonProperty("ccy")]
    public string? Ccy { get; set; }

    [JsonProperty("ordId")]
    public required string OrdId { get; set; }

    [JsonProperty("clOrdId")]
    public string? ClOrdId { get; set; }

    [JsonProperty("tag")]
    public string? Tag { get; set; }

    [JsonProperty("px")]
    [JsonConverter(typeof(StringToDecimalConverter))]
    public decimal Px { get; set; }

    [JsonProperty("sz")]
    [JsonConverter(typeof(StringToDecimalConverter))]
    public decimal Sz { get; set; }

    [JsonProperty("pnl")]
    [JsonConverter(typeof(StringToDecimalConverter))]
    public decimal Pnl { get; set; }

    [JsonProperty("ordType")]
    [JsonConverter(typeof(StringEnumConverter))]
    public OrderType OrdType { get; set; }

    [JsonProperty("side")]
    [JsonConverter(typeof(StringEnumConverter))]
    public OrderSide Side { get; set; }

    [JsonProperty("posSide")]
    [JsonConverter(typeof(StringEnumConverter))]
    public PositionSide PosSide { get; set; }

    [JsonProperty("tdMode")]
    [JsonConverter(typeof(StringEnumConverter))]
    public TradeMode TdMode { get; set; }

    [JsonProperty("accFillSz")]
    [JsonConverter(typeof(StringToDecimalConverter))]
    public decimal AccFillSz { get; set; }

    [JsonProperty("fillPx")]
    [JsonConverter(typeof(StringToDecimalConverter))]
    public decimal FillPx { get; set; }

    [JsonProperty("tradeId")]
    public string? TradeId { get; set; }

    [JsonProperty("fillSz")]
    [JsonConverter(typeof(StringToDecimalConverter))]
    public decimal FillSz { get; set; }

    [JsonProperty("fillTime")]
    [JsonConverter(typeof(StringToLongConverter))]
    public long FillTime { get; set; }

    [JsonProperty("state")]
    [JsonConverter(typeof(StringEnumConverter))]
    public OrderState State { get; set; }

    [JsonProperty("avgPx")]
    [JsonConverter(typeof(StringToDecimalConverter))]
    public decimal AvgPx { get; set; }

    [JsonProperty("lever")]
    public string? Lever { get; set; }

    [JsonProperty("tpTriggerPx")]
    public string? TpTriggerPx { get; set; }

    [JsonProperty("tpTriggerPxType")]
    public string? TpTriggerPxType { get; set; }

    [JsonProperty("tpOrdPx")]
    public string? TpOrdPx { get; set; }

    [JsonProperty("slTriggerPx")]
    public string? SlTriggerPx { get; set; }

    [JsonProperty("slTriggerPxType")]
    public string? SlTriggerPxType { get; set; }

    [JsonProperty("slOrdPx")]
    public string? SlOrdPx { get; set; }

    [JsonProperty("stpId")]
    public string? StpId { get; set; }

    [JsonProperty("stpMode")]
    public string? StpMode { get; set; }

    [JsonProperty("feeCcy")]
    public string? FeeCcy { get; set; }

    [JsonProperty("fee")]
    public string? Fee { get; set; }

    [JsonProperty("rebateCcy")]
    public string? RebateCcy { get; set; }

    [JsonProperty("rebate")]
    public string? Rebate { get; set; }

    [JsonProperty("tgtCcy")]
    public string? TgtCcy { get; set; }

    [JsonProperty("category")]
    public string? Category { get; set; }

    [JsonProperty("reduceOnly")]
    [JsonConverter(typeof(StringToBoolConverter))]
    public bool ReduceOnly { get; set; }

    [JsonProperty("cancelSource")]
    public string? CancelSource { get; set; }

    [JsonProperty("cancelSourceReason")]
    public string? CancelSourceReason { get; set; }

    [JsonProperty("quickMgnType")]
    public string? QuickMgnType { get; set; }

    [JsonProperty("algoClOrdId")]
    public string? AlgoClOrdId { get; set; }

    [JsonProperty("algoId")]
    public string? AlgoId { get; set; }

    [JsonProperty("uTime")]
    [JsonConverter(typeof(StringToLongConverter))]
    public long UTime { get; set; }

    [JsonProperty("cTime")]
    [JsonConverter(typeof(StringToLongConverter))]
    public long CTime { get; set; }
}

public enum OrderState
{
    canceled,
    live,
    partially_filled,
    filled
}

[JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
internal class CancelOrderRequest
{
    public CancelOrderRequest(string instId, string? ordId, string? clOrdId)
    {
        this.instId = instId;
        this.ordId = ordId;
        this.clOrdId = clOrdId;
    }

    public string instId { get; }
    public string? ordId { get; }
    public string? clOrdId { get; }
}

public class CancelOrderResult
{
    [JsonProperty("clOrdId")]
    public string? ClOrdId { get; set; }

    [JsonProperty("ordId")]
    public required string OrdId { get; set; }

    [JsonProperty("sCode")]
    [JsonConverter(typeof(StringToLongConverter))]
    public long SCode { get; set; }

    [JsonProperty("sMsg")]
    public string? SMsg { get; set; }
}

public class TradeBalance
{
    [JsonProperty("adjEq")]
    [JsonConverter(typeof(StringToDecimalConverter))]
    public decimal AdjEq { get; set; }

    [JsonProperty("borrowFroz")]
    [JsonConverter(typeof(StringToDecimalConverter))]
    public decimal BorrowFroz { get; set; }

    [JsonProperty("details")]
    public required TradeBalanceDetailData[] Details { get; set; }

    [JsonProperty("imr")]
    [JsonConverter(typeof(StringToDecimalConverter))]
    public decimal Imr { get; set; }

    [JsonProperty("isoEq")]
    [JsonConverter(typeof(StringToLongConverter))]
    public long IsoEq { get; set; }

    [JsonProperty("mgnRatio")]
    [JsonConverter(typeof(StringToDecimalConverter))]
    public decimal MgnRatio { get; set; }

    [JsonProperty("mmr")]
    [JsonConverter(typeof(StringToDecimalConverter))]
    public decimal Mmr { get; set; }

    [JsonProperty("notionalUsd")]
    [JsonConverter(typeof(StringToDecimalConverter))]
    public decimal NotionalUsd { get; set; }

    [JsonProperty("ordFroz")]
    [JsonConverter(typeof(StringToDecimalConverter))]
    public decimal OrdFroz { get; set; }

    [JsonProperty("totalEq")]
    [JsonConverter(typeof(StringToDecimalConverter))]
    public decimal TotalEq { get; set; }

    [JsonProperty("uTime")]
    [JsonConverter(typeof(StringToLongConverter))]
    public long UTime { get; set; }

    [JsonProperty("upl")]
    [JsonConverter(typeof(StringToDecimalConverter))]
    public decimal Upl { get; set; }
    
    public class TradeBalanceDetailData
    {
        [JsonProperty("availBal")]
        [JsonConverter(typeof(StringToDecimalConverter))]
        public decimal AvailBal { get; set; }

        [JsonProperty("availEq")]
        [JsonConverter(typeof(StringToDecimalConverter))]
        public decimal AvailEq { get; set; }

        [JsonProperty("borrowFroz")]
        [JsonConverter(typeof(StringToDecimalConverter))]
        public decimal BorrowFroz { get; set; }

        [JsonProperty("cashBal")]
        [JsonConverter(typeof(StringToDecimalConverter))]
        public decimal CashBal { get; set; }

        [JsonProperty("ccy")]
        public required string Ccy { get; set; }

        [JsonProperty("crossLiab")]
        [JsonConverter(typeof(StringToDecimalConverter))]
        public decimal CrossLiab { get; set; }

        [JsonProperty("disEq")]
        [JsonConverter(typeof(StringToDecimalConverter))]
        public decimal DisEq { get; set; }

        [JsonProperty("eq")]
        [JsonConverter(typeof(StringToDecimalConverter))]
        public decimal Eq { get; set; }

        [JsonProperty("eqUsd")]
        [JsonConverter(typeof(StringToDecimalConverter))]
        public decimal EqUsd { get; set; }

        [JsonProperty("fixedBal")]
        [JsonConverter(typeof(StringToDecimalConverter))]
        public decimal FixedBal { get; set; }

        [JsonProperty("frozenBal")]
        [JsonConverter(typeof(StringToDecimalConverter))]
        public decimal FrozenBal { get; set; }

        [JsonProperty("imr")]
        public string? Imr { get; set; }

        [JsonProperty("interest")]
        [JsonConverter(typeof(StringToDecimalConverter))]
        public decimal Interest { get; set; }

        [JsonProperty("isoEq")]
        [JsonConverter(typeof(StringToDecimalConverter))]
        public decimal IsoEq { get; set; }

        [JsonProperty("isoLiab")]
        [JsonConverter(typeof(StringToDecimalConverter))]
        public decimal IsoLiab { get; set; }

        [JsonProperty("isoUpl")]
        [JsonConverter(typeof(StringToDecimalConverter))]
        public decimal IsoUpl { get; set; }

        [JsonProperty("liab")]
        [JsonConverter(typeof(StringToDecimalConverter))]
        public decimal Liab { get; set; }

        [JsonProperty("maxLoan")]
        [JsonConverter(typeof(StringToDecimalConverter))]
        public decimal MaxLoan { get; set; }

        [JsonProperty("mgnRatio")]
        public string? MgnRatio { get; set; }

        [JsonProperty("mmr")]
        public string? Mmr { get; set; }

        [JsonProperty("notionalLever")]
        public string? NotionalLever { get; set; }

        [JsonProperty("ordFrozen")]
        [JsonConverter(typeof(StringToDecimalConverter))]
        public decimal OrdFrozen { get; set; }

        [JsonProperty("spotInUseAmt")]
        public string? SpotInUseAmt { get; set; }

        [JsonProperty("spotIsoBal")]
        [JsonConverter(typeof(StringToDecimalConverter))]
        public decimal SpotIsoBal { get; set; }

        [JsonProperty("stgyEq")]
        [JsonConverter(typeof(StringToDecimalConverter))]
        public decimal StgyEq { get; set; }

        [JsonProperty("twap")]
        [JsonConverter(typeof(StringToDecimalConverter))]
        public decimal Twap { get; set; }

        [JsonProperty("uTime")]
        [JsonConverter(typeof(StringToLongConverter))]
        public long UTime { get; set; }

        [JsonProperty("upl")]
        [JsonConverter(typeof(StringToDecimalConverter))]
        public decimal Upl { get; set; }

        [JsonProperty("uplLiab")]
        [JsonConverter(typeof(StringToDecimalConverter))]
        public decimal UplLiab { get; set; }
    }
}