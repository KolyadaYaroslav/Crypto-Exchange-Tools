using System;
using System.Globalization;
using System.Numerics;
using Newtonsoft.Json;

namespace CryptoExchangeTools.Models.Okx;

/// <summary>
/// Represents all Trading account asset balances as well as total balance.
/// </summary>
public class UserAssets
{
    /// <summary>
    /// Adjusted / Effective equity in USD. The net fiat value of the assets in the account that can provide margins for spot, futures, perpetual swap and options under the cross margin mode. 
    /// </summary>
    [JsonProperty("adjEq")]
    [JsonConverter(typeof(StringToDecimalConverter))]
    public required decimal AdjEq { get; set; }

    /// <summary>
    /// List of non-zero assets.
    /// </summary>
    [JsonProperty("details")]
    public required List<Detail> Details { get; set; }

    /// <summary>
    /// Initial margin requirement in USD.
    /// </summary>
    [JsonProperty("imr")]
    [JsonConverter(typeof(StringToDecimalConverter))]
    public required decimal Imr { get; set; }

    /// <summary>
    /// Isolated margin equity in USD.
    /// </summary>
    [JsonProperty("isoEq")]
    [JsonConverter(typeof(StringToDecimalConverter))]
    public required decimal IsoEq { get; set; }

    /// <summary>
    /// Margin ratio in USD.
    /// </summary>
    [JsonProperty("mgnRatio")]
    [JsonConverter(typeof(StringToDecimalConverter))]
    public required decimal MgnRatio { get; set; }

    /// <summary>
    /// Maintenance margin requirement in USD.
    /// </summary>
    [JsonProperty("mmr")]
    [JsonConverter(typeof(StringToDecimalConverter))]
    public required decimal Mmr { get; set; }

    /// <summary>
    /// Notional value of positions in USD.
    /// </summary>
    [JsonProperty("notionalUsd")]
    [JsonConverter(typeof(StringToDecimalConverter))]
    public required decimal NotionalUsd { get; set; }

    /// <summary>
    /// Margin frozen for open orders
    /// </summary>
    [JsonProperty("ordFroz")]
    [JsonConverter(typeof(StringToDecimalConverter))]
    public required decimal OrdFroz { get; set; }

    /// <summary>
    /// The total amount of equity in USD.
    /// </summary>
    [JsonProperty("totalEq")]
    [JsonConverter(typeof(StringToDecimalConverter))]
    public required decimal TotalEq { get; set; }


    /// <summary>
    /// Update time of account information, millisecond format of Unix timestamp.
    /// </summary>
    [JsonProperty("uTime")]
    [JsonConverter(typeof(StringToLongConverter))]
    public required long UTime { get; set; }

    public class Detail
    {
        /// <summary>
        /// Available balance of the currency.
        /// </summary>
        [JsonProperty("availBal")]
        [JsonConverter(typeof(StringToDecimalConverter))]
        public required decimal AvailBal { get; set; }

        /// <summary>
        /// Available equity of the currency.
        /// </summary>
        [JsonProperty("availEq")]
        [JsonConverter(typeof(StringToDecimalConverter))]
        public required decimal AvailEq { get; set; }

        /// <summary>
        /// Cash balance
        /// </summary>
        [JsonProperty("cashBal")]
        [JsonConverter(typeof(StringToDecimalConverter))]
        public required decimal CashBal { get; set; }

        /// <summary>
        /// Currency.
        /// </summary>
        [JsonProperty("ccy")]
        public required string Ccy { get; set; }

        /// <summary>
        /// Cross liabilities of the currency.
        /// </summary>
        [JsonProperty("crossLiab")]
        [JsonConverter(typeof(StringToDecimalConverter))]
        public required decimal CrossLiab { get; set; }

        /// <summary>
        /// Discount equity of the currency in USD.
        /// </summary>
        [JsonProperty("disEq")]
        [JsonConverter(typeof(StringToDecimalConverter))]
        public required decimal DisEq { get; set; }

        /// <summary>
        /// Equity of the currency.
        /// </summary>
        [JsonProperty("eq")]
        [JsonConverter(typeof(StringToDecimalConverter))]
        public required decimal Eq { get; set; }

        /// <summary>
        /// Equity in USD of the currency.
        /// </summary>
        [JsonProperty("eqUsd")]
        [JsonConverter(typeof(StringToDecimalConverter))]
        public required decimal EqUsd { get; set; }

        [JsonProperty("fixedBal")]
        [JsonConverter(typeof(StringToDecimalConverter))]
        public required decimal FixedBal { get; set; }

        /// <summary>
        /// Frozen balance of the currency.
        /// </summary>
        [JsonProperty("frozenBal")]
        [JsonConverter(typeof(StringToDecimalConverter))]
        public required decimal FrozenBal { get; set; }

        /// <summary>
        /// Accrued interest of the currency.
        /// </summary>
        [JsonProperty("interest")]
        [JsonConverter(typeof(StringToDecimalConverter))]
        public required decimal Interest { get; set; }

        /// <summary>
        /// Isolated margin equity in USD.
        /// </summary>
        [JsonProperty("isoEq")]
        [JsonConverter(typeof(StringToDecimalConverter))]
        public required decimal IsoEq { get; set; }

        /// <summary>
        /// Isolated liabilities of the currency.
        /// </summary>
        [JsonProperty("isoLiab")]
        [JsonConverter(typeof(StringToDecimalConverter))]
        public required decimal IsoLiab { get; set; }

        /// <summary>
        /// Isolated unrealized profit and loss of the currency.
        /// </summary>
        [JsonProperty("isoUpl")]
        [JsonConverter(typeof(StringToDecimalConverter))]
        public required decimal IsoUpl { get; set; }

        /// <summary>
        /// Liabilities of the currency.
        /// </summary>
        [JsonProperty("liab")]
        [JsonConverter(typeof(StringToDecimalConverter))]
        public required decimal Liab { get; set; }

        /// <summary>
        /// Max loan of the currency.
        /// </summary>
        [JsonProperty("maxLoan")]
        [JsonConverter(typeof(StringToDecimalConverter))]
        public required decimal MaxLoan { get; set; }

        /// <summary>
        /// Margin ratio in USD.
        /// </summary>
        [JsonProperty("mgnRatio")]
        [JsonConverter(typeof(StringToDecimalConverter))]
        public required decimal MgnRatio { get; set; }

        /// <summary>
        /// Leverage of the currency.
        /// </summary>
        [JsonProperty("notionalLever")]
        [JsonConverter(typeof(StringToDecimalConverter))]
        public required decimal NotionalLever { get; set; }

        /// <summary>
        /// Cross margin frozen for pending orders in USD.
        /// </summary>
        [JsonProperty("uTordFrozenime")]
        [JsonConverter(typeof(StringToDecimalConverter))]
        public required decimal OrdFrozen { get; set; }

        /// <summary>
        /// Risk indicator of auto liability repayment. Divided into 5 levels, from 1 to 5, the larger the number, the more likely the auto repayment will be triggered.
        /// </summary>
        [JsonProperty("twap")]
        [JsonConverter(typeof(StringToDecimalConverter))]
        public required decimal Twap { get; set; }

        /// <summary>
        /// Update time of account information, millisecond format of Unix timestamp.
        /// </summary>
        [JsonProperty("uTime")]
        [JsonConverter(typeof(StringToLongConverter))]
        public required long UTime { get; set; }

        /// <summary>
        /// The sum of the unrealized profit & loss of all margin and derivatives positions of the currency.
        /// </summary>
        [JsonProperty("upl")]
        [JsonConverter(typeof(StringToDecimalConverter))]
        public required decimal Upl { get; set; }

        /// <summary>
        /// Liabilities due to Unrealized loss of the currency.
        /// </summary>
        [JsonProperty("uplLiab")]
        [JsonConverter(typeof(StringToDecimalConverter))]
        public required decimal UplLiab { get; set; }

        /// <summary>
        /// Strategy equity.
        /// </summary>
        [JsonProperty("stgyEq")]
        [JsonConverter(typeof(StringToDecimalConverter))]
        public required decimal StgyEq { get; set; }

        /// <summary>
        /// Spot in use amount.
        /// </summary>
        [JsonProperty("spotInUseAmt")]
        [JsonConverter(typeof(StringToDecimalConverter))]
        public required decimal SpotInUseAmt { get; set; }
    }
}



