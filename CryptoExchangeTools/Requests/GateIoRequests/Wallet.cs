using System;
using CryptoExchangeTools.Models.GateIo;
using RestSharp;

namespace CryptoExchangeTools.GateIoRequests.Wallet;

public static class Wallet
{
    #region Original Methods

    /// <summary>
    /// List chains supported for specified currency.
    /// </summary>
    /// <param name="client"></param>
    /// <param name="currency">Currency name.</param>
    /// <returns>List of chains supported for currency.</returns>
    public static ChainsSupportedForSpecifiedCurrency[] GetChainsSupportedForSpecifiedCurrency(this GateIoClient client, string currency)
	{
		var request = new RestRequest("api/v4/wallet/currency_chains", Method.Get);

		request.AddParameter("currency", currency.ToUpper());

		return client.ExecuteRequest<ChainsSupportedForSpecifiedCurrency[]>(request, false);
	}

    /// <summary>
    /// List chains supported for specified currency.
    /// </summary>
    /// <param name="client"></param>
    /// <param name="currency">Currency name.</param>
    /// <returns>List of chains supported for currency.</returns>
    public static async Task<ChainsSupportedForSpecifiedCurrency[]> GetChainsSupportedForSpecifiedCurrencyAsync(this GateIoClient client, string currency)
    {
        var request = new RestRequest("api/v4/wallet/currency_chains", Method.Get);

        request.AddParameter("currency", currency.ToUpper());

        return await client.ExecuteRequestAsync<ChainsSupportedForSpecifiedCurrency[]>(request, false);
    }

    /// <summary>
    /// Generate currency deposit address.
    /// </summary>
    /// <param name="client"></param>
    /// <param name="currency">Currency name.</param>
    /// <returns>Info on generated address.</returns>
    public static GeneratedCurrencyDepositAddress GenerateCurrencyDepositAddress(this GateIoClient client, string currency)
    {
        var request = new RestRequest("api/v4/wallet/deposit_address", Method.Get);

        request.AddParameter("currency", currency.ToUpper());

        return client.ExecuteRequest<GeneratedCurrencyDepositAddress>(request);
    }

    /// <summary>
    /// Generate currency deposit address.
    /// </summary>
    /// <param name="client"></param>
    /// <param name="currency">Currency name.</param>
    /// <returns>Info on generated address.</returns>
    public static async Task<GeneratedCurrencyDepositAddress> GenerateCurrencyDepositAddressAsync(this GateIoClient client, string currency)
    {
        var request = new RestRequest("api/v4/wallet/deposit_address", Method.Get);

        request.AddParameter("currency", currency.ToUpper());

        return await client.ExecuteRequestAsync<GeneratedCurrencyDepositAddress>(request);
    }

    #endregion Original Methods
}

