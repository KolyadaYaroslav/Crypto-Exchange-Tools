using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using static CryptoExchangeTools.Models.GateIo.WithdrawalResult;

namespace CryptoExchangeTools.Models.GateIo;

public class ChainsSupportedForSpecifiedCurrency
{
    /// <summary>
    /// Chain name.
    /// </summary>
    [JsonProperty("chain")]
    public required string Chain { get; set; }

    /// <summary>
    /// Chain name in Chinese.
    /// </summary>
    [JsonProperty("name_cn")]
    public required string NameCn { get; set; }

    /// <summary>
    /// Chain name in English.
    /// </summary>
    [JsonProperty("name_en")]
    public required string NameEn { get; set; }

    /// <summary>
    /// If it is disabled.
    /// </summary>
    [JsonProperty("is_disabled")]
    [JsonConverter(typeof(IntToBoolConverter))]
    public bool IsDisabled { get; set; }

    /// <summary>
    /// Is deposit disabled.
    /// </summary>
    [JsonProperty("is_deposit_disabled")]
    [JsonConverter(typeof(IntToBoolConverter))]
    public bool IsDepositDisabled { get; set; }

    /// <summary>
    /// Is withdrawal disabled.
    /// </summary>
    [JsonProperty("is_withdraw_disabled")]
    [JsonConverter(typeof(IntToBoolConverter))]
    public bool IsWithdrawDisabled { get; set; }
}


public class GeneratedCurrencyDepositAddress
{
    /// <summary>
    /// Currency detail.
    /// </summary>
    [JsonProperty("currency")]
    public required string Currency { get; set; }

    /// <summary>
    /// Deposit address.
    /// </summary>
    [JsonProperty("address")]
    public required string Address { get; set; }

    [JsonProperty("multichain_addresses")]
    public MultichainAddress[]? MultichainAddresses { get; set; }

    public class MultichainAddress
    {
        /// <summary>
        /// Name of the chain.
        /// </summary>
        [JsonProperty("chain")]
        public required string Chain { get; set; }

        /// <summary>
        /// Deposit address.
        /// </summary>
        [JsonProperty("address")]
        public required string Address { get; set; }

        /// <summary>
        /// Notes that some currencies required(e.g., Tag, Memo) when depositing.
        /// </summary>
        [JsonProperty("payment_id")]
        public string? PaymentId { get; set; }

        /// <summary>
        /// Note type, Tag or Memo.
        /// </summary>
        [JsonProperty("payment_name")]
        public string? PaymentName { get; set; }

        /// <summary>
        /// The obtain failed status. True if failed to obtain, false if obtained successfully.
        /// </summary>
        [JsonProperty("obtain_failed")]
        [JsonConverter(typeof(IntToBoolConverter))]
        public bool ObtainFailed { get; set; }
    }
}


public partial class WithdrawHistory
{
    /// <summary>
    /// Record ID.
    /// </summary>
    [JsonProperty("id")]
    [JsonConverter(typeof(StringToLongConverter))]
    public long Id { get; set; }

    /// <summary>
    /// Operation time.
    /// </summary>
    [JsonProperty("timestamp")]
    [JsonConverter(typeof(StringToLongConverter))]
    public long Timestamp { get; set; }

    /// <summary>
    /// Client order id, up to 32 length and can only include 0-9, A-Z, a-z, underscore(_), hyphen(-) or dot(.).
    /// </summary>
    [JsonProperty("withdraw_order_id")]
    public string? WithdrawOrderId { get; set; }

    /// <summary>
    /// Currency name.
    /// </summary>
    [JsonProperty("currency")]
    public required string Currency { get; set; }

    /// <summary>
    /// Withdrawal address. Required for withdrawals.
    /// </summary>
    [JsonProperty("address")]
    public required string Address { get; set; }

    /// <summary>
    /// Hash record of the withdrawal.
    /// </summary>
    [JsonProperty("txid")]
    public required string Txid { get; set; }

    /// <summary>
    /// Currency amount.
    /// </summary>
    [JsonProperty("amount")]
    [JsonConverter(typeof(StringToDecimalConverter))]
    public decimal Amount { get; set; }

    /// <summary>
    /// Additional remarks with regards to the withdrawal.
    /// </summary>
    [JsonProperty("memo")]
    public string? Memo { get; set; }

    [JsonProperty("status")]
    [JsonConverter(typeof(StringEnumConverter))]
    public WithdrawalStatus Status { get; set; }

    /// <summary>
    /// Name of the chain used in withdrawals.
    /// </summary>
    [JsonProperty("chain")]
    public required string Chain { get; set; }

    /// <summary>
    /// Fee.
    /// </summary>
    [JsonProperty("fee")]
    [JsonConverter(typeof(StringToDecimalConverter))]
    public decimal Fee { get; set; }
}


public class AccountBalance
{
    /// <summary>
    /// Currency detail.
    /// </summary>
    [JsonProperty("currecny")]
    public required string Currecny { get; set; }

    /// <summary>
    /// Available amount.
    /// </summary>
    [JsonProperty("available")]
    [JsonConverter(typeof(StringToDecimalConverter))]
    public decimal Available { get; set; }

    /// <summary>
    /// Locked amount, used in trading.
    /// </summary>
    [JsonProperty("locked")]
    [JsonConverter(typeof(StringToDecimalConverter))]
    public decimal Locked { get; set; }
}


public class UserTotalBalances
{
    [JsonProperty("details")]
    public required DetailsData Details { get; set; }

    [JsonProperty("total")]
    public required TotalData Total { get; set; }

    public class DetailsData
    {
        [JsonProperty("cross_margin")]
        public required TotalData CrossMargin { get; set; }

        [JsonProperty("spot")]
        public required TotalData Spot { get; set; }

        [JsonProperty("finance")]
        public required TotalData Finance { get; set; }

        [JsonProperty("margin")]
        public required TotalData Margin { get; set; }

        [JsonProperty("quant")]
        public required TotalData Quant { get; set; }

        [JsonProperty("futures")]
        public required TotalData Futures { get; set; }

        [JsonProperty("delivery")]
        public required TotalData Delivery { get; set; }

        [JsonProperty("warrant")]
        public required TotalData Warrant { get; set; }

        [JsonProperty("cbbc")]
        public required TotalData Cbbc { get; set; }
    }

    public class TotalData
    {
        [JsonProperty("currency")]
        public CurrencyEnum Currency { get; set; }

        [JsonProperty("amount")]
        [JsonConverter(typeof(StringToDecimalConverter))]
        public required decimal Amount { get; set; }

        public enum CurrencyEnum
        {
            BTC,
            CNY,
            USD,
            Usdt
        };
    }
}
