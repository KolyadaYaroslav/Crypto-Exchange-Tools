using Newtonsoft.Json;

namespace CryptoExchangeTools.Models.Kucoin;

public class DepositAddress
{
    /// <summary>
    /// Deposit address.
    /// </summary>
    [JsonProperty("address")]
    public required string Address { get; set; }

    /// <summary>
    /// Address remark. If there’s no remark, it is empty. When you withdraw from other platforms to the KuCoin, you need to fill in memo(tag). If you do not fill memo (tag), your deposit may not be available, please be cautious.
    /// </summary>
    [JsonProperty("memo")]
    public string? Memo { get; set; }

    /// <summary>
    /// The chain name of currency.
    /// </summary>
    [JsonProperty("chain")]
    public required string Chain { get; set; }

    /// <summary>
    /// The token contract address.
    /// </summary>
    [JsonProperty("contractAddress")]
    public string? ContractAddress { get; set; }
}

