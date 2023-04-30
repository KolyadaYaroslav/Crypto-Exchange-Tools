using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CryptoExchangeTools.Models.GateIo;

/// <summary>
/// Request body for withdrawal.
/// </summary>
[JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
internal class WithdrawalRequest
{
    public WithdrawalRequest(decimal amount, string currency, string address, string chain, string? withdraw_order_id = null, string? memo = null)
    {
        CultureInfo.DefaultThreadCurrentCulture = CultureInfo.GetCultureInfo("en-US");

        this.withdraw_order_id = withdraw_order_id;
        this.amount = amount.ToString();
        this.currency = currency.ToUpper();
        this.address = address;
        this.memo = memo;
        this.chain = chain.ToUpper();
    }

    /// <summary>
    /// Client order id, up to 32 length and can only include 0-9, A-Z, a-z, underscore(_), hyphen(-) or dot(.).
    /// </summary>
    public string? withdraw_order_id { get; set; }

    /// <summary>
    /// Currency amount.
    /// </summary>
    public string amount { get; set; }

    /// <summary>
    /// Currency name.
    /// </summary>
    public string currency { get; set; }

    /// <summary>
    /// Withdrawal address. Required for withdrawals.
    /// </summary>
    public string address { get; set; }

    /// <summary>
    /// Additional remarks with regards to the withdrawal.
    /// </summary>
    public string? memo { get; set; }

    /// <summary>
    /// Name of the chain used in withdrawals.
    /// </summary>
    public string chain { get; set; }
}


public class WithdrawalResult
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
    [JsonProperty("withdrawOrderId")]
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

    /// <summary>
    /// 
    /// </summary>
    [JsonProperty("status")]
    [JsonConverter(typeof(StringEnumConverter))]
    public WithdrawalStatus Status { get; set; }

    /// <summary>
    /// Name of the chain used in withdrawals.
    /// </summary>
    [JsonProperty("chain")]
    public required string Chain { get; set; }

    public enum WithdrawalStatus
    {
        DONE,
        CANCEL,
        REQUEST,
        MANUAL,
        BCODE,
        EXTPEND,
        FAIL,
        INVALID,
        VERIFY,
        PROCES,
        PEND,
        DMOVE,
        SPLITPEND
    }
}