using Newtonsoft.Json;

namespace CryptoExchangeTools.Models.ByBit;

public class WalletBalanceResponse
{
	[JsonProperty("balances")]
	public required WalletCurrencyBalance[] Balances { get; set; }
}

public class WalletCurrencyBalance
{
	/// <summary>
	/// Coin
	/// </summary>
	[JsonProperty("coin")]
	public required string Coin { get; set; }
	
	/// <summary>
	/// Coin ID
	/// </summary>
	[JsonProperty("coinId")]
	public required string CoinId { get; set; }
	
	/// <summary>
	/// Total equity
	/// </summary>
	[JsonProperty("total")]
	[JsonConverter(typeof(StringToDecimalConverter))]
	public required decimal Total { get; set; }
	
	/// <summary>
	/// Available balance
	/// </summary>
	[JsonProperty("free")]
	[JsonConverter(typeof(StringToDecimalConverter))]
	public required decimal Free { get; set; }
	
	/// <summary>
	/// Reserved for orders
	/// </summary>
	[JsonProperty("locked")]
	[JsonConverter(typeof(StringToDecimalConverter))]
	public required decimal Locked { get; set; }
}