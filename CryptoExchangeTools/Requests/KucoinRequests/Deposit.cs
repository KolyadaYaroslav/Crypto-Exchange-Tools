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

    #region Derived Methods

    #region Get Deposit Address

    /// <summary>
    /// Retrieve the deposit addresses foe currency on a single network, including previously-used addresses.
    /// </summary>
    /// <param name="ccy">Currency</param>
    /// <param name="network">Network</param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public DepositAddress GetDepositAddress(string ccy, string network)
    {
        var addresses = GetDepositAddresses(ccy);

        var matching = addresses.Where(x => x.Chain == network.ToUpper());

        if (!matching.Any())
            throw new Exception($"There is no deposit address for {ccy} on {network}. Check if Currency and network are spelled correctly.");

        return matching.Single();
    }

    /// <summary>
    /// Retrieve the deposit addresses foe currency on a single network, including previously-used addresses.
    /// </summary>
    /// <param name="ccy">Currency</param>
    /// <param name="network">Network</param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public async Task<DepositAddress> GetDepositAddressAsync(string ccy, string network)
    {
        var addresses = await GetDepositAddressesAsync(ccy);

        var matching = addresses.Where(x => x.Chain == network.ToUpper());

        if (!matching.Any())
            throw new Exception($"There is no deposit address for {ccy} on {network}. Check if Currency and network are spelled correctly.");

        return matching.Single();
    }

    #endregion Get Deposit Address

    #endregion Derived Methods
}

