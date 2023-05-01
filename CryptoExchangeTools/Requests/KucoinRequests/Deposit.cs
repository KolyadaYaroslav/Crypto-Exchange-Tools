using CryptoExchangeTools.Models.Kucoin;
using RestSharp;

namespace CryptoExchangeTools.Requests.KucoinRequests;

public class Deposit
{
	private KucoinClient Client;

    public Deposit(KucoinClient client)
    {
        Client = client;
    }

    #region Original Methods

    #region Get Deposit Addresses

    /// <summary>
    /// Get all deposit addresses for the currency you intend to deposit. If the returned data is empty, you may need to create a deposit address first.
    /// </summary>
    /// <param name="currency">The currency./param>
    /// <returns>Array of deposit addresses info.</returns>
    public DepositAddress[] GetDepositAddresses(string currency)
    {
        var request = BuildGetDepositAddresses(currency);

        return Client.ExecuteRequest<DepositAddress[]>(request);
    }

    /// <summary>
    /// Get all deposit addresses for the currency you intend to deposit. If the returned data is empty, you may need to create a deposit address first.
    /// </summary>
    /// <param name="currency">The currency./param>
    /// <returns>Array of deposit addresses info.</returns>
    public async Task<DepositAddress[]> GetDepositAddressesAsync(string currency)
    {
        var request = BuildGetDepositAddresses(currency);

        return await Client.ExecuteRequestAsync<DepositAddress[]>(request);
    }

    private static RestRequest BuildGetDepositAddresses(string currency)
    {
        var request = new RestRequest("api/v2/deposit-addresses");

        request.AddParameter("currency", currency);

        return request;
    }

    #endregion Get Deposit Addresses

    #endregion Original Methods
}

