using CryptoExchangeTools.Models.Okx;
using CryptoExchangeTools.Requests.BinanceRequests;
using RestSharp;

namespace CryptoExchangeTools.Requests.OkxRequests;

public class SubAccount
{
	private readonly OkxClient Client;

	public SubAccount(OkxClient client)
	{
		Client = client;
	}

    #region Original Methods

    #region Get Sub Account List

    /// <summary>
    /// Applies to master accounts only.
    /// </summary>
    /// <param name="enable">Sub-account status.</param>
    /// <param name="subAcct">Sub-account name.</param>
    /// <param name="after">If you query the data prior to the requested creation time ID, the value will be a Unix timestamp in millisecond format.</param>
    /// <param name="before">If you query the data after the requested creation time ID, the value will be a Unix timestamp in millisecond format.</param>
    /// <param name="limit">Number of results per request. The maximum is 100. The default is 100.</param>
    /// <returns></returns>
    public SubAccountInfo[] GetSubAccountList(
		SubAccountStatus? enable = null,
        string? subAcct = null,
        long after = -1,
        long before = -1,
        int limit = -1)
	{
		var request = BuildGetSubAccountList(enable, subAcct, after, before, limit);

		return Client.ExecuteRequest<SubAccountInfo[]>(request);
	}

    /// <summary>
    /// Applies to master accounts only.
    /// </summary>
    /// <param name="enable">Sub-account status.</param>
    /// <param name="subAcct">Sub-account name.</param>
    /// <param name="after">If you query the data prior to the requested creation time ID, the value will be a Unix timestamp in millisecond format.</param>
    /// <param name="before">If you query the data after the requested creation time ID, the value will be a Unix timestamp in millisecond format.</param>
    /// <param name="limit">Number of results per request. The maximum is 100. The default is 100.</param>
    /// <returns></returns>
    public async Task<SubAccountInfo[]> GetSubAccountListAsync(
        SubAccountStatus? enable = null,
        string? subAcct = null,
        long after = -1,
        long before = -1,
        int limit = -1)
    {
        var request = BuildGetSubAccountList(enable, subAcct, after, before, limit);

        return await Client.ExecuteRequestAsync<SubAccountInfo[]>(request);
    }

    private static RestRequest BuildGetSubAccountList(
		SubAccountStatus? enable = null,
		string? subAcct = null,
		long after = -1,
		long before = -1,
		int limit = -1)
	{
		var request = new RestRequest("api/v5/users/subaccount/list", Method.Get);

		if (enable is not null)
			request.AddParameter("enable", enable == SubAccountStatus.Normal);

		if(!string.IsNullOrEmpty(subAcct))
            request.AddParameter("subAcct", subAcct);

		if(after != -1)
            request.AddParameter("after", after);

        if (before != -1)
            request.AddParameter("before", before);

        if (limit != -1)
            request.AddParameter("limit", limit);

        return request;
	}

    #endregion Get Sub Account List

    #region Master accounts manage the transfers between sub-accounts

    /// <summary>
    /// Applies to master accounts only. Only API keys with Trade privilege can call this endpoint.
    /// </summary>
    /// <param name="ccy">Currency</param>
    /// <param name="amt">Transfer amount</param>
    /// <param name="from"></param>
    /// <param name="to"></param>
    /// <param name="fromSubAccount">Sub-account name of the account that transfers funds out.</param>
    /// <param name="toSubAccount">Sub-account name of the account that transfers funds in.</param>
    /// <param name="loanTrans">Whether or not borrowed coins can be transferred out under Multi-currency margin and Portfolio margin. the default is false</param>
    /// <param name="omitPosRisk">Ignore position risk</param>
    /// <returns></returns>
    public SubAccountTransferResult Transfer(
        string ccy,
        decimal amt,
        AccountType from,
        AccountType to,
        string fromSubAccount,
        string toSubAccount,
        bool? loanTrans = null,
        bool? omitPosRisk = null)
    {
        var request = BuildTransfer(ccy, amt, from, to, fromSubAccount, toSubAccount, loanTrans, omitPosRisk);

        return Client.ExecuteRequest<SubAccountTransferResult>(request);
    }

    /// <summary>
    /// Applies to master accounts only. Only API keys with Trade privilege can call this endpoint.
    /// </summary>
    /// <param name="ccy">Currency</param>
    /// <param name="amt">Transfer amount</param>
    /// <param name="from"></param>
    /// <param name="to"></param>
    /// <param name="fromSubAccount">Sub-account name of the account that transfers funds out.</param>
    /// <param name="toSubAccount">Sub-account name of the account that transfers funds in.</param>
    /// <param name="loanTrans">Whether or not borrowed coins can be transferred out under Multi-currency margin and Portfolio margin. the default is false</param>
    /// <param name="omitPosRisk">Ignore position risk</param>
    /// <returns></returns>
    public async Task<SubAccountTransferResult> TransferAsync(
        string ccy,
        decimal amt,
        AccountType from,
        AccountType to,
        string fromSubAccount,
        string toSubAccount,
        bool? loanTrans = null,
        bool? omitPosRisk = null)
    {
        var request = BuildTransfer(ccy, amt, from, to, fromSubAccount, toSubAccount, loanTrans, omitPosRisk);

        return await Client.ExecuteRequestAsync<SubAccountTransferResult>(request);
    }

    private static RestRequest BuildTransfer(
        string ccy,
        decimal amt,
        AccountType from,
        AccountType to,
        string fromSubAccount,
        string toSubAccount,
        bool? loanTrans = null,
        bool? omitPosRisk = null)
    {
        var request = new RestRequest("api/v5/asset/subaccount/transfer", Method.Post);

        var body = new SubAccountTransferRequest(ccy, amt, from, to, fromSubAccount, toSubAccount, loanTrans, omitPosRisk);

        request.AddBody(body.Serialize());

        return request;
    }

    #endregion Master accounts manage the transfers between sub-accounts

    #endregion Original Methods
}

