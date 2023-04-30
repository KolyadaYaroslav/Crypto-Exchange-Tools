using CryptoExchangeTools.Models.Okx;
using RestSharp;

namespace CryptoExchangeTools.Requests.OkxRequests;

public class Account
{
    private OkxClient Client;

    public Account(OkxClient client)
    {
        Client = client;
    }

    #region Original Methods

    #region GetTradingBalance

    /// <summary>
    /// Retrieve a list of assets (with non-zero balance), remaining balance, and available amount in the trading account.
    /// </summary>
    /// <param name="ccy">Single currency or multiple currencies (no more than 20) separated with comma, e.g. BTC or BTC,ETH.</param>
    /// <returns>Info on non-zero assets.</returns>
    /// <exception cref="RequestNotSuccessfulException"></exception>
    public UserAssets GetTradingBalance(string? ccy = null)
    {
        var request = BuildGetTradingBalance(ccy);

        return Client.ExecuteRequest<UserAssets[]>(request).Single();
    }

    /// <summary>
    /// Retrieve a list of assets (with non-zero balance), remaining balance, and available amount in the trading account.
    /// </summary>
    /// <param name="ccy">Single currency or multiple currencies (no more than 20) separated with comma, e.g. BTC or BTC,ETH.</param>
    /// <returns>Info on non-zero assets.</returns>
    /// <exception cref="RequestNotSuccessfulException"></exception>
    public async Task<UserAssets> GetTradingBalanceAsync(string? ccy = null)
    {
        var request = BuildGetTradingBalance(ccy);

        return (await Client.ExecuteRequestAsync<UserAssets[]>(request)).Single();
    }

    private static RestRequest BuildGetTradingBalance(string? ccy)
    {
        var request = new RestRequest("api/v5/account/balance", Method.Get);

        if (ccy is not null)
            request.AddParameter("ccy", ccy);

        return request;
    }

    #endregion GetTradingBalance

    #endregion Original Methods
}

