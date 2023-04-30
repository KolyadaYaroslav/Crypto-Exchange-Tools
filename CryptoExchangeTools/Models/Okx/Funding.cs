using System;
using System.Globalization;
using Newtonsoft.Json;

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
    [JsonConverter(typeof(StringToDecimalConverter))]
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
    /// NName of currency. There is no related name when it is not shown.
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
        this.chain = chain.ToUpper();
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