using CryptoExchangeTools.Models.Kucoin;
using Newtonsoft.Json;
using RestSharp;

namespace CryptoExchangeTools.Requests.KucoinRequests;

public class Account
{
    private KucoinClient Client;

    public Account(KucoinClient client)
    {
        Client = client;
    }

    #region Original Methods

    #region List Accounts

    /// <summary>
    /// Get a list of accounts.
    /// </summary>
    /// <param name="currency">[Optional] Currency.</param>
    /// <param name="type">[Optional] Account type: main, trade, margin.</param>
    /// <returns></returns>
    public AccountBalance[] ListAccounts(string? currency = null, string? type = null)
    {
        var request = BuildListAccounts(currency, type);

        return Client.ExecuteRequest<AccountBalance[]>(request);
    }

    /// <summary>
    /// Get a list of accounts.
    /// </summary>
    /// <param name="currency">[Optional] Currency.</param>
    /// <param name="type">[Optional] Account type: main, trade, margin.</param>
    /// <returns></returns>
    public async Task<AccountBalance[]> ListAccountsAsync(string? currency = null, string? type = null)
    {
        var request = BuildListAccounts(currency, type);

        return await Client.ExecuteRequestAsync<AccountBalance[]>(request);
    }

    private static RestRequest BuildListAccounts(string? currency, string? type)
    {
        var request = new RestRequest("api/v1/accounts");

        if (!string.IsNullOrEmpty(currency))
            request.AddParameter("currency", currency);

        if (!string.IsNullOrEmpty(type))
            request.AddParameter("type", type);

        return request;
    }

    #endregion List Accounts

    #endregion Original Methods
}

