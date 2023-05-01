using Newtonsoft.Json;

namespace CryptoExchangeTools.Models.Kucoin;

public class AccountBalance
{
    /// <summary>
    /// The ID of the account.
    /// </summary>
    [JsonProperty("id")]
    public required string Id { get; set; }

    /// <summary>
    /// Currency.
    /// </summary>
    [JsonProperty("currency")]
    public required string Currency { get; set; }

    /// <summary>
    /// Account type: main, trade, margin.
    /// </summary>
    [JsonProperty("type")]
    public required string Type { get; set; }

    /// <summary>
    /// Total funds in the account.
    /// </summary>
    [JsonProperty("balance")]
    [JsonConverter(typeof(StringToDecimalConverter))]
    public decimal Balance { get; set; }

    /// <summary>
    /// Funds available to withdraw or trade.
    /// </summary>
    [JsonProperty("available")]
    [JsonConverter(typeof(StringToDecimalConverter))]
    public decimal Available { get; set; }

    /// <summary>
    /// Funds on hold (not available for use).
    /// </summary>
    [JsonProperty("holds")]
    [JsonConverter(typeof(StringToDecimalConverter))]
    public decimal Holds { get; set; }
}