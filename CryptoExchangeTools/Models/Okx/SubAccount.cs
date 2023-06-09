using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CryptoExchangeTools.Models.Okx;

public enum SubAccountStatus
{
    Normal,
    Frozen
}

public enum SubAccountType
{
    StandardSubAccount = 1,
    ManagedTradingSubAccount = 2,
    CustodySubAccountCopper = 5
}

public enum AccountType
{
    Funding = 6,
    Trading = 18
}

public class SubAccountInfo
{
    /// <summary>
    /// Sub-account type.
    /// </summary>
    [JsonProperty("enable")]
    public bool Enable { get; set; }

    /// <summary>
    /// Sub-account name.
    /// </summary>
    [JsonProperty("subAcct")]
    public required string SubAcct { get; set; }

    /// <summary>
    /// Sub-account status.
    /// </summary>
    [JsonProperty("type")]
    [JsonConverter(typeof(StringEnumConverter))]
    public SubAccountType Type { get; set; }

    /// <summary>
    /// Sub-account note
    /// </summary>
    [JsonProperty("label")]
    public string? Label { get; set; }

    /// <summary>
    /// Mobile number that linked with the sub-account.
    /// </summary>
    [JsonProperty("mobile")]
    public string? Mobile { get; set; }

    /// <summary>
    /// If the sub-account switches on the Google Authenticator for login authentication.
    /// </summary>
    [JsonProperty("gAuth")]
    public bool GAuth { get; set; }

    /// <summary>
    /// Whether the sub-account has the right to transfer out.
    /// </summary>
    [JsonProperty("canTransOut")]
    public bool CanTransOut { get; set; }

    /// <summary>
    /// ub-account creation time, Unix timestamp in millisecond format.
    /// </summary>
    [JsonProperty("ts")]
    [JsonConverter(typeof(StringToLongConverter))]
    public long Ts { get; set; }
}

public class SubAccountTransferResult
{
    /// <summary>
    /// Transfer ID
    /// </summary>
    [JsonProperty("transId")]
    public string? TransId { get; set; }
}

[JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
internal class SubAccountTransferRequest
{
    public SubAccountTransferRequest(string ccy, decimal amt, AccountType from, AccountType to, string fromSubAccount, string toSubAccount, bool? loanTrans, bool? omitPosRisk)
    {
        this.ccy = ccy;
        this.amt = amt.ToString();
        this.from = ((int)from).ToString();
        this.to = ((int)to).ToString();
        this.fromSubAccount = fromSubAccount;
        this.toSubAccount = toSubAccount;
        this.loanTrans = loanTrans;
        this.omitPosRisk = omitPosRisk is not null ? ((bool)omitPosRisk).ToString() : null;
    }

    public string ccy { get; set; }
    public string amt { get; set; }
    public string from { get; set; }
    public string to { get; set; }
    public string fromSubAccount { get; set; }
    public string toSubAccount { get; set; }
    public bool? loanTrans { get; set; }
    public string? omitPosRisk { get; set; }

    public string Serialize()
    {
        return JsonConvert.SerializeObject(this);
    }
}