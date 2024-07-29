using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

// ReSharper disable InconsistentNaming

namespace CryptoExchangeTools.Models.ByBit;

public class WithdrawRequestBody
{
	public WithdrawRequestBody(string coin, string chain, string address, string? tag, decimal amount, bool? forceChain, AccountType accountType, bool? feeType, string? requestId)
	{
		Coin = coin;
		Chain = chain;
		Address = address;
		Tag = tag;
		Amount = amount.ToString(CultureInfo.GetCultureInfo("en-US"));
		Timestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
		ForceChain = forceChain is null ? null : (bool)forceChain ? 1 : 0;
		AccountType = accountType.ToString();
		FeeType = feeType is null ? null : (bool)feeType ? 1 : 0;
		RequestId = requestId;
	}

	[JsonProperty("coin")]
	public string Coin { get; set; }
	
	[JsonProperty("chain")]
	public string Chain { get; set; }
	
	[JsonProperty("address")]
	public string Address { get; set; }
	
	[JsonProperty("tag", NullValueHandling = NullValueHandling.Ignore)]
	public string? Tag { get; set; }
	
	[JsonProperty("amount")]
	public string Amount { get; set; }
	
	[JsonProperty("timestamp")]
	public long Timestamp { get; set; }
	
	[JsonProperty("forceChain", NullValueHandling = NullValueHandling.Ignore)]
	public int? ForceChain { get; set; }
	
	[JsonProperty("accountType")]
	public string AccountType { get; set; }
	
	[JsonProperty("feeType", NullValueHandling = NullValueHandling.Ignore)]
	public int? FeeType { get; set; }
	
	[JsonProperty("requestId", NullValueHandling = NullValueHandling.Ignore)]
	public string? RequestId { get; set; }
}

public enum AccountType
{
	/// <summary>
	/// Derivatives Account
	/// </summary>
	CONTRACT,
	
	/// <summary>
	/// Spot Account
	/// </summary>
	SPOT,
	
	/// <summary>
	/// ByFi Account (The service has been offline)
	/// </summary>
	INVESTMENT,
	
	/// <summary>
	/// USDC Account
	/// </summary>
	OPTION,
	
	/// <summary>
	/// UTA or UMA
	/// </summary>
	UNIFIED,
	
	/// <summary>
	/// Funding Account
	/// </summary>
	FUND
}

public class WithdrawRequestResponse
{
	/// <summary>
	/// Withdrawal ID
	/// </summary>
	[JsonProperty("id")]
	public required string Id { get; set; }
}

public class WithdrawRecordResponse
{
	[JsonProperty("rows")]
	public required WithdrawRecord[] Rows { get; set; }
	
	[JsonProperty("nextPageCursor")]
	public required string NextPageCursor { get; set; }
}

public class WithdrawRecord
{
	/// <summary>
	/// withdrawal coin
	/// </summary>
	[JsonProperty("coin")]
	public required string Coin { get; set; }
	
	/// <summary>
	/// chain name
	/// </summary>
	[JsonProperty("chain")]
	public required string Chain { get; set; }
	
	/// <summary>
	/// withdrawal quantity
	/// </summary>
	[JsonProperty("amount")]
	[JsonConverter(typeof(StringToDecimalConverter))]
	public required decimal Amount { get; set; }
	
	/// <summary>
	/// transaction ID. It returns "" when withdrawal failed, withdrawal cancelled
	/// </summary>
	[JsonProperty("txID")]
	public required string? TxId { get; set; }
	
	/// <summary>
	/// status
	/// </summary>
	[JsonProperty("status")]
	[JsonConverter(typeof(StringEnumConverter))]
	public required WithdrawStatus Status { get; set; }
	
	/// <summary>
	/// withdrawal target address. It shows email or mobile number for internal transfer
	/// </summary>
	[JsonProperty("toAddress")]
	public required string ToAddress { get; set; }
	
	/// <summary>
	/// tag of withdrawal target address
	/// </summary>
	[JsonProperty("tag")]
	public required string Tag { get; set; }
	
	/// <summary>
	/// withdrawal fee
	/// </summary>
	[JsonProperty("withdrawFee")]
	[JsonConverter(typeof(StringToDecimalConverter))]
	public required decimal WithdrawFee { get; set; }
	
	/// <summary>
	/// withdrawal create time
	/// </summary>
	[JsonProperty("createTime")]
	public required string CreateTime { get; set; }
	
	/// <summary>
	/// withdrawal status updated time
	/// </summary>
	[JsonProperty("updateTime")]
	public required string UpdateTime { get; set; }
	
	/// <summary>
	/// withdrawal id. (withdraw request ID in Response Parameters of Withdrawal endpoint)
	/// </summary>
	[JsonProperty("withdrawId")]
	public required string WithdrawId { get; set; }
	
	/// <summary>
	/// Withdraw type. 0：on chain; 1：internal address transfer
	/// </summary>
	[JsonProperty("withdrawType")]
	public required int WithdrawType { get; set; }
}

public enum WithdrawStatus
{
	SecurityCheck,
	Pending,
	success,
	CancelByUser,
	Reject,
	Fail,
	BlockchainConfirmed,
	MoreInformationRequired
}

public class GetAllCoinsBalanceResponse
{
	/// <summary>
	/// Account type
	/// </summary>
	[JsonProperty("accountType")]
	[JsonConverter(typeof(StringEnumConverter))]
	public required AccountType AccountType { get; set; }
	
	/// <summary>
	/// UserID
	/// </summary>
	[JsonProperty("memberId")]
	public string? MemberId { get; set; }

	[JsonProperty("Balance")]
	public required CoinBalance[] balance { get; set; }
}

public class CoinBalance
{
	/// <summary>
	/// Currency type
	/// </summary>
	[JsonProperty("coin")]
	public required string Coin { get; set; }
	
	/// <summary>
	/// Wallet Balance
	/// </summary>
	[JsonProperty("walletBalance")]
	[JsonConverter(typeof(StringToDecimalConverter))]
	public required decimal WalletBalance { get; set; }
	
	/// <summary>
	/// Transferable Balance
	/// </summary>
	[JsonProperty("TransferBalance")]
	[JsonConverter(typeof(StringToDecimalConverter))]
	public required decimal transferBalance { get; set; }
	
	/// <summary>
	/// The Bonus
	/// </summary>
	[JsonProperty("bonus")]
	public required string Bonus { get; set; }
}

public class GetCoinInfromationResponse
{
	[JsonProperty("rows")]
	public required CoinInformation[] Rows { get; set; }
}

public class CoinInformation
{
	[JsonProperty("name")]
	public required string Name { get; set; }

	[JsonProperty("coin")]
	public required string Coin { get; set; }

	[JsonProperty("remainAmount")]
	[JsonConverter(typeof(StringToLongConverter))]
	public long RemainAmount { get; set; }

	[JsonProperty("chains")]
	public required ChainInfo[] Chains { get; set; }
}

public class ChainInfo
{
	[JsonProperty("chainType")]
	public required string ChainType { get; set; }

	[JsonProperty("confirmation")]
	[JsonConverter(typeof(StringToLongConverter))]
	public long Confirmation { get; set; }

	[JsonProperty("withdrawFee")]
	[JsonConverter(typeof(StringToDecimalConverter))]
	public decimal WithdrawFee { get; set; }

	[JsonProperty("depositMin")]
	[JsonConverter(typeof(StringToDecimalConverter))]
	public decimal DepositMin { get; set; }

	[JsonProperty("withdrawMin")]
	[JsonConverter(typeof(StringToDecimalConverter))]
	public decimal WithdrawMin { get; set; }

	[JsonProperty("chain")]
	public required string ChainChain { get; set; }

	[JsonProperty("chainDeposit")]
	[JsonConverter(typeof(StringToIntConverter))]
	public int ChainDeposit { get; set; }

	[JsonProperty("chainWithdraw")]
	[JsonConverter(typeof(StringToIntConverter))]
	public int ChainWithdraw { get; set; }

	[JsonProperty("minAccuracy")]
	[JsonConverter(typeof(StringToIntConverter))]
	public int MinAccuracy { get; set; }

	[JsonProperty("withdrawPercentageFee")]
	[JsonConverter(typeof(StringToDecimalConverter))]
	public decimal WithdrawPercentageFee { get; set; }
}