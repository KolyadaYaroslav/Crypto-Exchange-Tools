using System;
using CryptoExchangeTools.Models.Okx;
using RestSharp;

namespace CryptoExchangeTools.OkxRequests.Account;

public static class Account
{
    #region Original Methods

    /// <summary>
    /// Retrieve a list of assets (with non-zero balance), remaining balance, and available amount in the trading account.
    /// </summary>
    /// <param name="client"></param>
    /// <param name="ccy">Single currency or multiple currencies (no more than 20) separated with comma, e.g. BTC or BTC,ETH.</param>
    /// <returns>Info on non-zero assets.</returns>
    /// <exception cref="RequestNotSuccessfulException"></exception>
    public static UserAssets GetTradingBalance(this OkxClient client, string? ccy = null)
    {
        var request = new RestRequest("api/v5/account/balance", Method.Get);

        if (ccy is not null)
            request.AddParameter("ccy", ccy);

        return client.ExecuteRequest<UserAssets[]>(request).Single();
    }

    /// <summary>
    /// Retrieve a list of assets (with non-zero balance), remaining balance, and available amount in the trading account.
    /// </summary>
    /// <param name="client"></param>
    /// <param name="ccy">Single currency or multiple currencies (no more than 20) separated with comma, e.g. BTC or BTC,ETH.</param>
    /// <returns>Info on non-zero assets.</returns>
    /// <exception cref="RequestNotSuccessfulException"></exception>
    public static async Task<UserAssets> GetTradingBalanceAsync(this OkxClient client, string? ccy = null)
    {
        var request = new RestRequest("api/v5/account/balance", Method.Get);

        if (ccy is not null)
            request.AddParameter("ccy", ccy);

        return (await client.ExecuteRequestAsync<UserAssets[]>(request)).Single();
    }


    #endregion Original Methods
}

