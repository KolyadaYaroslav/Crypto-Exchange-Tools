using System;
using System.Globalization;
using CryptoExchangeTools.Requests.OkxRequests;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CryptoExchangeTools.Models.Okx;

/// <summary>
/// Represents single asset balance on fundinag account.
/// </summary>
public class CoinBalance
{
    /// <summary>
    /// Available balance. The balance that can be withdrawn or transferred or used for spot trading
    /// </summary>
    [JsonProperty("availBal")]
    [JsonConverter(typeof(StringToDecimalConverter))]
    public required decimal AvailBal { get; set; }

    /// <summary>
    /// Balance.
    /// </summary>
    [JsonProperty("bal")]
    [JsonConverter(typeof(StringToDecimalConverter))]
    public required decimal Bal { get; set; }

    /// <summary>
    /// Currency.
    /// </summary>
    [JsonProperty("ccy")]
    public required string Ccy { get; set; }

    /// <summary>
    /// Frozen balance.
    /// </summary>
    [JsonProperty("frozenBal")]
    [JsonConverter(typeof(StringToDecimalConverter))]
    public required decimal FrozenBal { get; set; }
}



/// <summary>
/// Represents Information on a single currency.
/// </summary>
public class CurrencyInformation
{
    /// <summary>
    /// Availability to deposit from chain.
    /// </summary>
    [JsonProperty("canDep")]
    [JsonConverter(typeof(StringToBoolConverter))]
    public required bool CanDep { get; set; }

    /// <summary>
    /// Availability to internal transfer.
    /// </summary>
    [JsonProperty("canInternal")]
    [JsonConverter(typeof(StringToBoolConverter))]
    public required bool CanInternal { get; set; }

    /// <summary>
    /// Availability to withdraw to chain.
    /// </summary>
    [JsonProperty("canWd")]
    [JsonConverter(typeof(StringToBoolConverter))]
    public required bool CanWd { get; set; }

    /// <summary>
    /// Currency.
    /// </summary>
    [JsonProperty("ccy")]
    public required string Ccy { get; set; }

    /// <summary>
    /// Chain name, e.g. USDT-ERC20, USDT-TRC20, USDT-Omni.
    /// </summary>
    [JsonProperty("chain")]
    public required string Chain { get; set; }

    /// <summary>
    /// Fixed deposit limit, unit in USD.
    /// </summary>
    [JsonProperty("depQuotaFixed")]
    [JsonConverter(typeof(StringToDecimalConverter))]
    public required decimal DepQuotaFixed { get; set; }

    /// <summary>
    /// Layer2 network daily deposit limit.
    /// </summary>
    [JsonProperty("depQuoteDailyLayer2")]
    [JsonConverter(typeof(StringToDecimalConverter))]
    public required decimal DepQuoteDailyLayer2 { get; set; }

    /// <summary>
    /// Logo link of currency.
    /// </summary>
    [JsonProperty("logoLink")]
    public required string LogoLink { get; set; }

    /// <summary>
    /// If current chain is main net then return true, otherwise return false.
    /// </summary>
    [JsonProperty("mainNet")]
    [JsonConverter(typeof(StringToBoolConverter))]
    public required bool MainNet { get; set; }

    /// <summary>
    /// Maximum withdrawal fee.
    /// </summary>
    [JsonProperty("maxFee")]
    [JsonConverter(typeof(StringToDecimalConverter))]
    public required decimal MaxFee { get; set; }

    /// <summary>
    /// Maximum amount of currency withdrawal in a single transaction.
    /// </summary>
    [JsonProperty("maxWd")]
    [JsonConverter(typeof(StringToDecimalConverter))]
    public required decimal MaxWd { get; set; }

    /// <summary>
    /// Minimum deposit amount of the currency in a single transaction.
    /// </summary>
    [JsonProperty("minDep")]
    [JsonConverter(typeof(StringToDecimalConverter))]
    public required decimal MinDep { get; set; }

    /// <summary>
    /// Minimum number of blockchain confirmations to acknowledge fund deposit. Account is credited after that but the deposit cannot be withdrawn.
    /// </summary>
    [JsonProperty("minDepArrivalConfirm")]
    [JsonConverter(typeof(StringToIntConverter))]
    public required int MinDepArrivalConfirm { get; set; }

    /// <summary>
    /// Minimum withdrawal fee.
    /// </summary>
    [JsonProperty("minFee")]
    [JsonConverter(typeof(StringToDecimalConverter))]
    public required decimal MinFee { get; set; }

    /// <summary>
    /// Minimum withdrawal amount of the currency in a single transaction.
    /// </summary>
    [JsonProperty("minWd")]
    [JsonConverter(typeof(StringToDecimalConverter))]
    public required decimal MinWd { get; set; }

    /// <summary>
    /// Minimum number of blockchain confirmations required for withdrawal of a deposit.
    /// </summary>
    [JsonProperty("minWdUnlockConfirm")]
    [JsonConverter(typeof(StringToIntConverter))]
    public required int MinWdUnlockConfirm { get; set; }

    /// <summary>
    /// Name of currency. There is no related name when it is not shown.
    /// </summary>
    [JsonProperty("name")]
    public required string? Name { get; set; }

    /// <summary>
    /// Used amount of fixed deposit quota, unit in USD. Return empty string if there is no deposit limit
    /// </summary>
    [JsonProperty("usedDepQuotaFixed")]
    [JsonConverter(typeof(StringToDecimalConverter))]
    public required decimal UsedDepQuotaFixed { get; set; }

    /// <summary>
    /// Whether tag/memo information is required for withdrawal.
    /// </summary>
    [JsonProperty("needTag")]
    [JsonConverter(typeof(StringToDecimalConverter))]
    public required decimal NeedTag { get; set; }

    /// <summary>
    /// Amount of currency withdrawal used in the past 24 hours, unit in USD.
    /// </summary>
    [JsonProperty("usedWdQuota")]
    [JsonConverter(typeof(StringToDecimalConverter))]
    public required decimal UsedWdQuota { get; set; }

    /// <summary>
    /// Withdrawal limit in the past 24 hours, unit in USD.
    /// </summary>
    [JsonProperty("wdQuota")]
    [JsonConverter(typeof(StringToDecimalConverter))]
    public required decimal WdQuota { get; set; }

    /// <summary>
    /// Withdrawal precision, indicating the number of digits after the decimal point. The withdrawal fee precision kept the same with withdrawal precision. The accuracy of internal transfer withdrawal is 8 decimal places.
    /// </summary>
    [JsonProperty("wdTickSz")]
    [JsonConverter(typeof(StringToIntConverter))]
    public required int WdTickSz { get; set; }
}


/// <summary>
/// Result of Withdrawal operation.
/// </summary>
public class WithdrawalResult
{
    /// <summary>
    /// Withdrawal amount.
    /// </summary>
    [JsonProperty("amt")]
    [JsonConverter(typeof(StringToDecimalConverter))]
    public required decimal Amt { get; set; }

    /// <summary>
    /// Withdrawal ID.
    /// </summary>
    [JsonProperty("wdId")]
    public required string WdId { get; set; }

    /// <summary>
    /// Currency.
    /// </summary>
    [JsonProperty("ccy")]
    public required string Ccy { get; set; }

    /// <summary>
    /// Client-supplied ID. A combination of case-sensitive alphanumerics, all numbers, or all letters of up to 32 characters.
    /// </summary>
    [JsonProperty("clientId")]
    public required string ClientId { get; set; }

    /// <summary>
    /// Chain name, e.g. USDT-ERC20, USDT-TRC20, USDT-Omni.
    /// </summary>
    [JsonProperty("chain")]
    public required string Chain { get; set; }
}

/// <summary>
/// Request body for withdrawal.
/// </summary>
[JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
internal class WithdrawalRequest
{
    public WithdrawalRequest(string ccy, decimal amt, string toAddr, string chain, int dest, decimal fee, string? clientId, string? areaCode)
    {
        this.ccy = ccy.ToUpper();
        this.amt = amt.ToString();
        this.toAddr = toAddr;
        this.chain = chain;
        this.dest = dest.ToString();
        this.fee = fee.ToString();
        this.clientId = clientId;
        this.areaCode = areaCode;
    }

    public string ccy { get; set; }
    public string amt { get; set; }
    public string toAddr { get; set; }
    public string chain { get; set; }
    public string dest { get; set; }
    public string fee { get; set; }
    public string? clientId { get; set; }
    public string? areaCode { get; set; }
}

public class DepositAddress
{
    /// <summary>
    /// Chain name, e.g. USDT-ERC20, USDT-TRC20, USDT-Omni.
    /// </summary>
    [JsonProperty("chain")]
    public required string Chain { get; set; }

    /// <summary>
    /// Last 6 digits of contract address.
    /// </summary>
    [JsonProperty("ctAddr")]
    public required string CtAddr { get; set; }

    /// <summary>
    /// Currency, e.g. BTC.
    /// </summary>
    [JsonProperty("ccy")]
    public required string Ccy { get; set; }

    /// <summary>
    /// The beneficiary account. 6: Funding account 18: Trading account
    /// </summary>
    [JsonProperty("to")]
    [JsonConverter(typeof(StringEnumConverter))]
    public AccountTo To { get; set; }

    /// <summary>
    /// Deposit address
    /// </summary>
    [JsonProperty("addr")]
    public required string Addr { get; set; }

    /// <summary>
    /// Return true if the current deposit address is selected by the website page.
    /// </summary>
    [JsonProperty("selected")]
    public bool Selected { get; set; }

    /// <summary>
    /// Deposit tag (This will not be returned if the currency does not require a tag for deposit)
    /// </summary>
    [JsonProperty("tag")]
    public string? Tag { get; set; }

    /// <summary>
    /// Deposit memo (This will not be returned if the currency does not require a payment_id for deposit)
    /// </summary>
    [JsonProperty("memo")]
    public string? Memo { get; set; }

    /// <summary>
    /// Deposit payment ID (This will not be returned if the currency does not require a payment_id for deposit)
    /// </summary>
    [JsonProperty("pmtId")]
    public string? PmtId { get; set; }

    /// <summary>
    /// Deposit address attachment (This will not be returned if the currency does not require this). e.g.TONCOIN attached tag name is comment, the return will be {'comment':'123456'}
    /// </summary>
    [JsonProperty("addrEx")]
    public object? AddrEx { get; set; }
}

public enum AccountTo
{
    FundingAccount = 6,
    TradingAccount = 18
}

public enum DepositType
{
    InternalTransfer = 1,
    WithdrawalToChain = 2
}

public enum WithdrawalType
{
    InternalTransfer = 1,
    WithdrawalToChain = 2
}

public enum WithdrawalStatus
{
    canceling = -3,
    canceled = -2,
    failed = -1,
    waitingWithdrawal = 0,
    withdrawing = 1,
    withdrawSuccess = 2,
    approved = 7,
    waitingTransfer = 10,
    waitingMannualReview1 = 4,
    waitingMannualReview2 = 5,
    waitingMannualReview3 = 6,
    waitingMannualReview4 = 8,
    waitingMannualReview5 = 9,
    waitingMannualReview6 = 12
}

public enum DepositStatus
{
    waitingForConfirmation = 0,
    depositCredited = 1,
    depositSuccessful = 2,
    pendingDueToTemporaryDepositSuspensionOnThisCryptocurrency = 8,
    matchTheAddressBlacklist = 11,
    accountOrDepositIsFrozen = 12,
    subAccountDepositInterception = 13,
    KYClimit = 14
}

public class WithdrawalHistory
{
    /// <summary>
    /// Chain name, e.g. USDT-ERC20, USDT-TRC20, USDT-Omni
    /// </summary>
    [JsonProperty("chain")]
    public required string Chain { get; set; }

    /// <summary>
    /// Withdrawal fee amount
    /// </summary>
    [JsonProperty("fee")]
    [JsonConverter(typeof(StringToDecimalConverter))]
    public decimal Fee { get; set; }

    /// <summary>
    /// Currency
    /// </summary>
    [JsonProperty("ccy")]
    public required string Ccy { get; set; }

    /// <summary>
    /// Client-supplied ID.
    /// </summary>
    [JsonProperty("clientId")]
    public string? ClientId { get; set; }

    /// <summary>
    /// Withdrawal amount
    /// </summary>
    [JsonProperty("amt")]
    [JsonConverter(typeof(StringToDecimalConverter))]
    public decimal Amt { get; set; }

    /// <summary>
    /// Hash record of the withdrawal. This parameter will returned "" for internal transfers.
    /// </summary>
    [JsonProperty("txId")]
    public required string TxId { get; set; }

    /// <summary>
    /// Withdrawal account. It can be email/phone
    /// </summary>
    [JsonProperty("from")]
    public required string From { get; set; }

    /// <summary>
    /// Area code for the phone number. If from is a phone number, this parameter returns the area code for the phone number
    /// </summary>
    [JsonProperty("areaCodeFrom")]
    public string? AreaCodeFrom { get; set; }

    /// <summary>
    /// Receiving address
    /// </summary>
    [JsonProperty("to")]
    public required string To { get; set; }

    /// <summary>
    /// Area code for the phone number. If to is a phone number, this parameter returns the area code for the phone number
    /// </summary>
    [JsonProperty("areaCodeTo")]
    public string? AreaCodeTo { get; set; }

    /// <summary>
    /// Status of withdrawal.
    /// </summary>
    [JsonProperty("state")]
    [JsonConverter(typeof(StringEnumConverter))]
    public WithdrawalStatus State { get; set; }

    /// <summary>
    /// Time the withdrawal request was submitted, Unix timestamp format in milliseconds, e.g. 1655251200000
    /// </summary>
    [JsonProperty("ts")]
    [JsonConverter(typeof(StringToLongConverter))]
    public long Ts { get; set; }

    /// <summary>
    /// Withdrawal ID.
    /// </summary>
    [JsonProperty("wdId")]
    public required string WdId { get; set; }
}

public enum FundsTransferType
{
    TransferWithinAccount = 0,

    /// <summary>
    /// Only applicable to API Key from master account.
    /// </summary>
    MasterAccountToSubAccount = 1,

    /// <summary>
    /// Only applicable to API Key from master account.
    /// </summary>
    SubAccountToMasterAccountFromMaster = 2,

    /// <summary>
    /// Only applicable to APIKey from sub-account.
    /// </summary>
    SubAccountToMasterAccountFromSub = 3,

    /// <summary>
    /// Only applicable to APIKey from sub-account, and target account needs to be another sub-account which belongs to same master account.
    /// </summary>
    SubAccountToSubAccount = 4
}

[JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
internal class FundsTransferRequest
{
    public FundsTransferRequest(string ccy, decimal amt, AccountType from, AccountType to, string? subAcct, FundsTransferType? type, bool? loanTrans, string? clientId, bool? omitPosRisk)
    {
        Ccy = ccy;
        Amt = amt.ToString();
        this.From = ((int)from).ToString();
        this.To = ((int)to).ToString();
        SubAcct = subAcct;
        Type = type is not null ? ((int)type).ToString() : null;
        LoanTrans = loanTrans;
        ClientId = clientId;
        OmitPosRisk = omitPosRisk is not null ? ((bool)omitPosRisk).ToString() : null;
    }

    [JsonProperty("ccy")]
    public string Ccy { get; set; }

    [JsonProperty("amt")]
    public string Amt { get; set; }

    [JsonProperty("from")]
    public string From { get; set; }

    [JsonProperty("to")]
    public string To { get; set; }

    [JsonProperty("subAcct")]
    public string? SubAcct { get; set; }

    [JsonProperty("type")]
    public string? Type { get; set; }

    [JsonProperty("loanTrans")]
    public bool? LoanTrans { get; set; }

    [JsonProperty("clientId")]
    public string? ClientId { get; set; }

    [JsonProperty("omitPosRisk")]
    public string? OmitPosRisk { get; set; }

    public string Serialize()
    {
        return JsonConvert.SerializeObject(this);
    }
}

public class FundsTransferResult
{
    [JsonProperty("transId")]
    public required string TransId { get; set; }

    [JsonProperty("ccy")]
    public required string Ccy { get; set; }

    [JsonProperty("clientId")]
    public string? ClientId { get; set; }

    [JsonProperty("from")]
    [JsonConverter(typeof(StringEnumConverter))]
    public AccountType From { get; set; }

    [JsonProperty("amt")]
    [JsonConverter(typeof(StringToDecimalConverter))]
    public decimal Amt { get; set; }

    [JsonProperty("to")]
    [JsonConverter(typeof(StringEnumConverter))]
    public AccountType To { get; set; }
}

public class DepositHistory
{
    [JsonProperty("actualDepBlkConfirm")]
    [JsonConverter(typeof(StringToLongConverter))]
    public long ActualDepBlkConfirm { get; set; }

    [JsonProperty("amt")]
    [JsonConverter(typeof(StringToDecimalConverter))]
    public decimal Amt { get; set; }

    [JsonProperty("areaCodeFrom")]
    public string? AreaCodeFrom { get; set; }

    [JsonProperty("ccy")]
    public required string Ccy { get; set; }

    [JsonProperty("chain")]
    public required string Chain { get; set; }

    [JsonProperty("depId")]
    public string? DepId { get; set; }

    [JsonProperty("from")]
    public string? From { get; set; }

    [JsonProperty("fromWdId")]
    public string? FromWdId { get; set; }

    [JsonProperty("state")]
    [JsonConverter(typeof(StringEnumConverter))]
    public DepositStatus State { get; set; }

    [JsonProperty("to")]
    public string? To { get; set; }

    [JsonProperty("ts")]
    [JsonConverter(typeof(StringToLongConverter))]
    public long Ts { get; set; }

    [JsonProperty("txId")]
    public string? TxId { get; set; }
}