using CryptoExchangeTools.Models.Kucoin;
using RestSharp;

namespace CryptoExchangeTools.Requests.KucoinRequests;

public class Currencies
{
	private KucoinClient Client;

    public Currencies(KucoinClient client)
    {
        Client = client;
    }

    #region Original Methods

    #region Get Currency Detail

    /// <summary>
    /// Get the currency details of a specified currency.
    /// </summary>
    /// <param name="currency">Currency.</param>
    /// <param name="chain">[Optional] Support for querying the chain of currency, return the currency details of all chains by default.</param>
    /// <returns>Currency Detail.</returns>
    public CurrencyDetail GetCurrencyDetail(string currency, string? chain = null)
    {
        var request = BuildGetCurrencyDetail(currency, chain);

        return Client.ExecuteRequest<CurrencyDetail>(request, false);
    }

    /// <summary>
    /// Get the currency details of a specified currency.
    /// </summary>
    /// <param name="currency">Currency.</param>
    /// <param name="chain">[Optional] Support for querying the chain of currency, return the currency details of all chains by default.</param>
    /// <returns>Currency Detail.</returns>
    public async Task<CurrencyDetail> GetCurrencyDetailAsync(string currency, string? chain = null)
    {
        var request = BuildGetCurrencyDetail(currency, chain);

        return await Client.ExecuteRequestAsync<CurrencyDetail>(request, false);
    }

    private static RestRequest BuildGetCurrencyDetail(string currency, string? chain)
    {
        var request = new RestRequest($"api/v2/currencies/{currency.ToUpper()}");

        if (!string.IsNullOrEmpty(chain))
            request.AddParameter("chain", chain);

        return request;
    }

    #endregion Get Currency Detail

    #endregion Original Methods
}

