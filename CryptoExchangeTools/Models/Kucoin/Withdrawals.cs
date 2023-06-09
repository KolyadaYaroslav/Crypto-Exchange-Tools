﻿using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CryptoExchangeTools.Models.Kucoin;

public class WithdrawResult
{
    /// <summary>
    /// Withdrawal id
    /// </summary>
    [JsonProperty("withdrawalId")]
    public required string WithdrawalId { get; set; }
}

[JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
internal class WithdrawRequest
{
    public WithdrawRequest(string currency, string address, decimal amount, string chain, string? memo, bool isInner, string? remark, string? feeDeductType)
    {
        this.currency = currency.ToUpper();
        this.address = address;
        this.amount = amount;
        this.chain = chain.ToLower();
        this.memo = memo;
        this.isInner = isInner;
        this.remark = remark;
        this.feeDeductType = feeDeductType;
    }

    public string currency { get; set; }
    public string address { get; set; }
    public decimal amount { get; set; }
    public string chain { get; set; }
    public string? memo { get; set; }
    public bool isInner { get; set; }
    public string? remark { get; set; }
    public string? feeDeductType { get; set; }
}

public partial class WithdrawHistory
{
    [JsonProperty("currentPage")]
    public int CurrentPage { get; set; }

    [JsonProperty("pageSize")]
    public int PageSize { get; set; }

    [JsonProperty("totalNum")]
    public int TotalNum { get; set; }

    [JsonProperty("totalPage")]
    public int TotalPage { get; set; }

    [JsonProperty("items")]
    public required Item[] Items { get; set; }

    public partial class Item
    {
        /// <summary>
        /// Unique identity.
        /// </summary>
        [JsonProperty("id")]
        public required string Id { get; set; }

        /// <summary>
        /// Currency.
        /// </summary>
        [JsonProperty("currency")]
        public required string Currency { get; set; }

        /// <summary>
        /// The chain of currency.
        /// </summary>
        [JsonProperty("chain")]
        public required string Chain { get; set; }

        /// <summary>
        /// Status.
        /// </summary>
        [JsonProperty("status")]
        [JsonConverter(typeof(StringEnumConverter))]
        public Status Status { get; set; }

        /// <summary>
        /// Withdrawal address.
        /// </summary>
        [JsonProperty("address")]
        public required string Address { get; set; }

        /// <summary>
        /// Address remark. If there’s no remark, it is empty. When you withdraw from other platforms to the KuCoin, you need to fill in memo(tag). If you do not fill memo (tag), your deposit may not be available, please be cautious.
        /// </summary>
        [JsonProperty("memo")]
        public string? Memo { get; set; }

        /// <summary>
        /// Internal withdrawal or not.
        /// </summary>
        [JsonProperty("isInner")]
        public bool IsInner { get; set; }

        /// <summary>
        /// Withdrawal amount.
        /// </summary>
        [JsonProperty("amount")]
        [JsonConverter(typeof(StringToDecimalConverter))]
        public decimal Amount { get; set; }

        /// <summary>
        /// Withdrawal fee.
        /// </summary>
        [JsonProperty("fee")]
        [JsonConverter(typeof(StringToDecimalConverter))]
        public decimal Fee { get; set; }

        /// <summary>
        /// Wallet Txid.
        /// </summary>
        [JsonProperty("walletTxId")]
        public required string WalletTxId { get; set; }

        /// <summary>
        /// Creation time.
        /// </summary>
        [JsonProperty("createdAt")]
        public long CreatedAt { get; set; }

        /// <summary>
        /// Creation time.
        /// </summary>
        [JsonProperty("updatedAt")]
        public long UpdatedAt { get; set; }

        /// <summary>
        /// Remark.
        /// </summary>
        [JsonProperty("remark")]
        public string? Remark { get; set; }
    }

    public enum Status
    {
        PROCESSING,
        WALLET_PROCESSING,
        SUCCESS,
        FAILURE
    }
}


public partial class WithdrawalQuota
{
    /// <summary>
    /// Currency.
    /// </summary>
    [JsonProperty("currency")]
    public required string Currency { get; set; }

    /// <summary>
    /// Current available withdrawal amount.
    /// </summary>
    [JsonProperty("limitBTCAmount")]
    [JsonConverter(typeof(StringToDecimalConverter))]
    public decimal LimitBtcAmount { get; set; }

    /// <summary>
    /// Remaining amount available to withdraw the current day.
    /// </summary>
    [JsonProperty("usedBTCAmount")]
    [JsonConverter(typeof(StringToDecimalConverter))]
    public decimal UsedBtcAmount { get; set; }

    /// <summary>
    /// Minimum withdrawal amount.
    /// </summary>
    [JsonProperty("remainAmount")]
    [JsonConverter(typeof(StringToDecimalConverter))]
    public decimal RemainAmount { get; set; }

    /// <summary>
    /// Total BTC amount available to withdraw the current day.
    /// </summary>
    [JsonProperty("availableAmount")]
    [JsonConverter(typeof(StringToDecimalConverter))]
    public decimal AvailableAmount { get; set; }

    /// <summary>
    /// Fees for internal withdrawal.
    /// </summary>
    [JsonProperty("withdrawMinFee")]
    [JsonConverter(typeof(StringToDecimalConverter))]
    public decimal WithdrawMinFee { get; set; }

    /// <summary>
    /// The estimated BTC amount (based on the daily fiat limit) that can be withdrawn within the current day.
    /// </summary>
    [JsonProperty("innerWithdrawMinFee")]
    [JsonConverter(typeof(StringToDecimalConverter))]
    public decimal InnerWithdrawMinFee { get; set; }

    /// <summary>
    /// Is the withdraw function enabled or not.
    /// </summary>
    [JsonProperty("withdrawMinSize")]
    [JsonConverter(typeof(StringToDecimalConverter))]
    public decimal WithdrawMinSize { get; set; }

    /// <summary>
    /// Minimum withdrawal fee.
    /// </summary>
    [JsonProperty("isWithdrawEnabled")]
    public bool IsWithdrawEnabled { get; set; }

    /// <summary>
    /// Floating point precision.
    /// </summary>
    [JsonProperty("precision")]
    public int Precision { get; set; }

    /// <summary>
    /// The chain name of currency, e.g. The available value for USDT are OMNI, ERC20, TRC20, default is ERC20.
    /// </summary>
    [JsonProperty("chain")]
    public required string Chain { get; set; }
}