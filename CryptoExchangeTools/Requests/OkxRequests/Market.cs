using CryptoExchangeTools.Models.Okx;
using RestSharp;

namespace CryptoExchangeTools.Requests.OkxRequests;

public class Market
{
	private readonly OkxClient Client;

	public Market(OkxClient client)
	{
		Client = client;
	}

    #region Original Methods

    #region Get Ticker

    /// <summary>
    /// Retrieve the latest price snapshot, best bid/ask price, and trading volume in the last 24 hours.
    /// </summary>
    /// <param name="instId">Instrument ID, e.g. BTC-USD-SWAP./param>
    /// <returns></returns>
    public TickerInfo[] GetTicker(string instId)
	{
		var request = BuildGetTicker(instId);

		return Client.ExecuteRequest<TickerInfo[]>(request, false);
	}

    /// <summary>
    /// Retrieve the latest price snapshot, best bid/ask price, and trading volume in the last 24 hours.
    /// </summary>
    /// <param name="instId">Instrument ID, e.g. BTC-USD-SWAP./param>
    /// <returns></returns>
    public async Task<TickerInfo[]> GetTickerAsync(string instId)
    {
        var request = BuildGetTicker(instId);

        return await Client.ExecuteRequestAsync<TickerInfo[]>(request);
    }

    private static RestRequest BuildGetTicker(string instId)
	{
		var request = new RestRequest("api/v5/market/ticker", Method.Get);

		request.AddParameter("instId", instId, false);

		return request;
	}

    #endregion Get Ticker

    #endregion Original Methods

    #region Derived Methods

    #region Get Market Price

    public decimal GetMarketPrice(string instId)
    {
        var info = GetTicker(instId);

        return info.Single().Last;
    }

    public async Task<decimal> GetMarketPriceAsync(string instId)
    {
        var info = await GetTickerAsync(instId);

        return info.Single().Last;
    }

    #endregion Get Market Price

    #endregion Derived Methods
}

