using System;
using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CryptoExchangeTools.Models.Binance.Wallet;

/// <summary>
/// Represents withdrawal result.
/// </summary>
internal class WithdrawResult
{
    public required string id { get; set; }
}

public class AssetDetail
{
    /// <summary>
    /// min withdraw amount
    /// </summary>
    [JsonProperty("minWithdrawAmount")]
    [JsonConverter(typeof(StringToDecimalConverter))]
    public decimal MinWithdrawAmount { get; set; }

    /// <summary>
    /// deposit status (false if ALL of networks' are false)
    /// </summary>
    [JsonProperty("depositStatus")]
    [JsonConverter(typeof(StringToBoolConverter))]
    public bool DepositStatus { get; set; }

    /// <summary>
    /// withdraw fee
    /// </summary>
    [JsonProperty("withdrawFee")]
    [JsonConverter(typeof(StringToDecimalConverter))]
    public decimal WithdrawFee { get; set; }

    /// <summary>
    /// withdraw status (false if ALL of networks' are false)
    /// </summary>
    [JsonProperty("withdrawStatus")]
    [JsonConverter(typeof(StringToBoolConverter))]
    public bool WithdrawStatus { get; set; }

    /// <summary>
    /// reason
    /// </summary>
    [JsonProperty("depositTip")]
    public string? DepositTip { get; set; }
}


public class CoinInformation
{
    [JsonProperty("coin")]
    public required string Coin { get; set; }

    [JsonProperty("depositAllEnable")]
    [JsonConverter(typeof(StringToBoolConverter))]
    public bool DepositAllEnable { get; set; }

    [JsonProperty("free")]
    [JsonConverter(typeof(StringToDecimalConverter))]
    public decimal Free { get; set; }

    [JsonProperty("freeze")]
    [JsonConverter(typeof(StringToDecimalConverter))]
    public decimal Freeze { get; set; }

    [JsonProperty("ipoable")]
    [JsonConverter(typeof(StringToDecimalConverter))]
    public decimal Ipoable { get; set; }

    [JsonProperty("ipoing")]
    [JsonConverter(typeof(StringToDecimalConverter))]
    public decimal Ipoing { get; set; }

    [JsonProperty("isLegalMoney")]
    [JsonConverter(typeof(StringToBoolConverter))]
    public bool IsLegalMoney { get; set; }

    [JsonProperty("locked")]
    [JsonConverter(typeof(StringToDecimalConverter))]
    public decimal Locked { get; set; }

    [JsonProperty("name")]
    public required string Name { get; set; }

    [JsonProperty("networkList")]
    public required List<Network> NetworkList { get; set; }

    public class Network
    {
        [JsonProperty("addressRegex")]
        public required string AddressRegex { get; set; }

        [JsonProperty("coin")]
        public required string Coin { get; set; }

        /// <summary>
        /// shown only when "depositEnable" is false.
        /// </summary>
        [JsonProperty("depositDesc")]
        public string? DepositDesc { get; set; }

        [JsonProperty("depositEnable")]
        [JsonConverter(typeof(StringToBoolConverter))]
        public bool DepositEnable { get; set; }

        [JsonProperty("isDefault")]
        [JsonConverter(typeof(StringToBoolConverter))]
        public bool IsDefault { get; set; }

        [JsonProperty("memoRegex")]
        public required string MemoRegex { get; set; }

        /// <summary>
        /// min number for balance confirmation
        /// </summary>
        [JsonProperty("minConfirm")]
        [JsonConverter(typeof(StringToIntConverter))]
        public int MinConfirm { get; set; }

        [JsonProperty("name")]
        public required string Name { get; set; }

        [JsonProperty("network")]
        public required string NetworkName { get; set; }

        [JsonProperty("resetAddressStatus")]
        [JsonConverter(typeof(StringToBoolConverter))]
        public bool ResetAddressStatus { get; set; }

        [JsonProperty("specialTips")]
        public required string SpecialTips { get; set; }

        /// <summary>
        /// confirmation number for balance unlock 
        /// </summary>
        [JsonProperty("unLockConfirm")]
        [JsonConverter(typeof(StringToIntConverter))]
        public int UnLockConfirm { get; set; }

        [JsonProperty("withdrawDesc")]
        public string? WithdrawDesc { get; set; }

        [JsonProperty("withdrawEnable")]
        [JsonConverter(typeof(StringToBoolConverter))]
        public bool WithdrawEnable { get; set; }

        [JsonProperty("withdrawFee")]
        [JsonConverter(typeof(StringToDecimalConverter))]
        public decimal WithdrawFee { get; set; }

        [JsonProperty("withdrawIntegerMultiple")]
        [JsonConverter(typeof(StringToDecimalConverter))]
        public decimal WithdrawIntegerMultiple { get; set; }

        [JsonProperty("withdrawMax")]
        [JsonConverter(typeof(StringToDecimalConverter))]
        public decimal WithdrawMax { get; set; }

        [JsonProperty("withdrawMin")]
        [JsonConverter(typeof(StringToDecimalConverter))]
        public decimal WithdrawMin { get; set; }

        /// <summary>
        /// If the coin needs to provide memo to withdraw
        /// </summary>
        [JsonProperty("sameAddress")]
        [JsonConverter(typeof(StringToBoolConverter))]
        public bool WameAddress { get; set; }

        [JsonProperty("estimatedArrivalTime")]
        [JsonConverter(typeof(StringToDecimalConverter))]
        public decimal EstimatedArrivalTime { get; set; }

        [JsonProperty("busy")]
        [JsonConverter(typeof(StringToBoolConverter))]
        public bool Busy { get; set; }
    }
}
public class WithdrawHistoryRecord
{
    /// <summary>
    /// Withdrawal id in Binance
    /// </summary>
    [JsonProperty("id")]
    public required string Id { get; set; }

    /// <summary>
    /// withdrawal amount
    /// </summary>
    [JsonProperty("amount")]
    [JsonConverter(typeof(StringToDecimalConverter))]
    public decimal Amount { get; set; }

    /// <summary>
    /// transaction fee
    /// </summary>
    [JsonProperty("transactionFee")]
    [JsonConverter(typeof(StringToDecimalConverter))]
    public decimal TransactionFee { get; set; }

    [JsonProperty("coin")]
    public required string Coin { get; set; }

    [JsonProperty("status")]
    [JsonConverter(typeof(StringEnumConverter))]
    public WithdrawStatus Status { get; set; }

    [JsonProperty("address")]
    public required string Address { get; set; }

    /// <summary>
    /// withdrawal transaction id
    /// </summary>
    [JsonProperty("txId")]
    public required string TxId { get; set; }

    /// <summary>
    /// UTC time
    /// </summary>
    [JsonProperty("applyTime")]
    public required string ApplyTime { get; set; }

    [JsonProperty("network")]
    public required string Network { get; set; }

    /// <summary>
    /// 1 for internal transfer, 0 for external transfer
    /// </summary>
    [JsonProperty("transferType")]
    [JsonConverter(typeof(StringToIntConverter))]
    public int TransferType { get; set; }

    /// <summary>
    /// will not be returned if there's no withdrawOrderId for this withdraw.
    /// </summary>
    [JsonProperty("withdrawOrderId")]
    public string? WithdrawOrderId { get; set; }

    /// <summary>
    /// reason for withdrawal failure
    /// </summary>
    [JsonProperty("info")]
    public string? Info { get; set; }

    /// <summary>
    /// confirm times for withdraw
    /// </summary>
    [JsonProperty("confirmNo")]
    [JsonConverter(typeof(StringToIntConverter))]
    public int ConfirmNo { get; set; }

    /// <summary>
    /// 1: Funding Wallet 0:Spot Wallet
    /// </summary>
    [JsonProperty("walletType")]
    [JsonConverter(typeof(StringToIntConverter))]
    public int WalletType { get; set; }

    [JsonProperty("txKey")]
    public string? TxKey { get; set; }

    /// <summary>
    /// complete UTC time when user's asset is deduct from withdrawing, only if status =  6(success)
    /// </summary>
    [JsonProperty("completeTime")]
    public string? CompleteTime { get; set; }

    public enum WithdrawStatus
    {
        Email_Sent,
        Cancelled,
        Awaiting_Approval,
        Rejected,
        Processing,
        Failure,
        Completed
    }
}

public class UserAsset
{
    [JsonProperty("asset")]
    public required string Asset { get; set; }

    [JsonProperty("free")]
    [JsonConverter(typeof(StringToDecimalConverter))]
    public decimal Free { get; set; }

    [JsonProperty("freeze")]
    [JsonConverter(typeof(StringToDecimalConverter))]
    public decimal Freeze { get; set; }

    [JsonProperty("withdrawing")]
    [JsonConverter(typeof(StringToDecimalConverter))]
    public decimal Withdrawing { get; set; }

    [JsonProperty("ipoable")]
    [JsonConverter(typeof(StringToDecimalConverter))]
    public decimal Ipoable { get; set; }

    [JsonProperty("btcValuation")]
    [JsonConverter(typeof(StringToDecimalConverter))]
    public decimal BtcValuation { get; set; }
}


public class DepositHistory
{
    [JsonProperty("id")]
    [JsonConverter(typeof(StringToLongConverter))]
    public long Id { get; set; }

    [JsonProperty("amount")]
    [JsonConverter(typeof(StringToDecimalConverter))]
    public decimal Amount { get; set; }

    [JsonProperty("coin")]
    public required string Coin { get; set; }

    [JsonProperty("network")]
    public required string Network { get; set; }

    [JsonProperty("status")]
    [JsonConverter(typeof(StringEnumConverter))]
    public DepositStatus Status { get; set; }

    [JsonProperty("address")]
    public required string Address { get; set; }

    [JsonProperty("addressTag")]
    public required string AddressTag { get; set; }

    [JsonProperty("txId")]
    public required string TxId { get; set; }

    [JsonProperty("insertTime")]
    [JsonConverter(typeof(StringToLongConverter))]
    public long InsertTime { get; set; }

    [JsonProperty("transferType")]
    [JsonConverter(typeof(StringToIntConverter))]
    public int TransferType { get; set; }

    [JsonProperty("confirmTimes")]
    public required string ConfirmTimes { get; set; }

    [JsonProperty("unlockConfirm")]
    [JsonConverter(typeof(StringToIntConverter))]
    public int UnlockConfirm { get; set; }

    [JsonProperty("walletType")]
    [JsonConverter(typeof(StringToIntConverter))]
    public int WalletType { get; set; }

    public enum DepositStatus
    {
        pending = 0,
        success = 1,
        creditedButCannotWithdraw = 6,
        WrongDeposit = 7,
        WaitingUserConfirm = 8
    }
}

public class DepositAddress
{
    [JsonProperty("address")]
    public required string Address { get; set; }

    [JsonProperty("coin")]
    public required string Coin { get; set; }

    [JsonProperty("tag")]
    public string? Tag { get; set; }

    [JsonProperty("url")]
    public string? Url { get; set; }
}