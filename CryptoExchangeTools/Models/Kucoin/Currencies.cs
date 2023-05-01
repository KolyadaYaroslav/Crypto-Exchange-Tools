using Newtonsoft.Json;

namespace CryptoExchangeTools.Models.Kucoin;

public class CurrencyDetail
{
    /// <summary>
    /// A unique currency code that will never change.
    /// </summary>
    [JsonProperty("currency")]
    public required string Currency { get; set; }

    /// <summary>
    /// Currency name, will change after renaming.
    /// </summary>
    [JsonProperty("name")]
    public required string Name { get; set; }

    /// <summary>
    /// Full name of a currency, will change after renaming.
    /// </summary>
    [JsonProperty("fullName")]
    public required string FullName { get; set; }

    /// <summary>
    /// Currency precision.
    /// </summary>
    [JsonProperty("precision")]
    public int Precision { get; set; }

    /// <summary>
    /// Number of block confirmations.
    /// </summary>
    [JsonProperty("confirms")]
    public int? Confirms { get; set; }

    /// <summary>
    /// Contract address.
    /// </summary>
    [JsonProperty("contractAddress")]
    public string? ContractAddress { get; set; }

    /// <summary>
    /// Support margin or not.
    /// </summary>
    [JsonProperty("isMarginEnabled")]
    public bool IsMarginEnabled { get; set; }

    /// <summary>
    /// Support debit or not.
    /// </summary>
    [JsonProperty("isDebitEnabled")]
    public bool IsDebitEnabled { get; set; }

    [JsonProperty("chains")]
    public required ChainData[] Chains { get; set; }

    public class ChainData
    {
        /// <summary>
        /// Chain name of currency.
        /// </summary>
        [JsonProperty("chainName")]
        public required string ChainName { get; set; }

        /// <summary>
        /// Chain of currency.
        /// </summary>
        [JsonProperty("chain")]
        public required string Chain { get; set; }

        /// <summary>
        /// Minimum withdrawal amount.
        /// </summary>
        [JsonProperty("withdrawalMinSize")]
        [JsonConverter(typeof(StringToDecimalConverter))]
        public required decimal WithdrawalMinSize { get; set; }

        /// <summary>
        /// Minimum fees charged for withdrawal.
        /// </summary>
        [JsonProperty("withdrawalMinFee")]
        [JsonConverter(typeof(StringToDecimalConverter))]
        public required decimal WithdrawalMinFee { get; set; }

        /// <summary>
        /// Support withdrawal or not.
        /// </summary>
        [JsonProperty("isWithdrawEnabled")]
        public bool IsWithdrawEnabled { get; set; }

        /// <summary>
        /// Support deposit or not.
        /// </summary>
        [JsonProperty("isDepositEnabled")]
        public bool IsDepositEnabled { get; set; }

        /// <summary>
        /// Number of block confirmations.
        /// </summary>
        [JsonProperty("confirms")]
        public int Confirms { get; set; }

        /// <summary>
        /// Contract address.
        /// </summary>
        [JsonProperty("contractAddress")]
        public string? ContractAddress { get; set; }
    }
}

