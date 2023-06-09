using CryptoExchangeTools.Models.Binance;
using RestSharp;

namespace CryptoExchangeTools.Requests.BinanceRequests;

public class Market
{
	private readonly BinanceClient Client;

	public Market(BinanceClient client)
	{
		Client = client;
	}

    #region Original Methods

    #region Get Exchange Information

    public ExchangeInformation GetExchangeInformation(string[]? symbols = null, string[]? permissions = null)
    {
        var request = BuildGetExchangeInformation(symbols, permissions);

        return Client.ExecuteRequest<ExchangeInformation>(request, false);
    }

    public ExchangeInformation GetExchangeInformation(string symbol, string[]? permissions = null)
    {
        var request = BuildGetExchangeInformation(new string[] { symbol }, permissions);

        return Client.ExecuteRequest<ExchangeInformation>(request, false);
    }

    public async Task<ExchangeInformation> GetExchangeInformationAsync(string[]? symbols = null, string[]? permissions = null)
    {
        var request = BuildGetExchangeInformation(symbols, permissions);

        return await Client.ExecuteRequestAsync<ExchangeInformation>(request, false);
    }

    public async Task<ExchangeInformation> GetExchangeInformationAsync(string symbol, string[]? permissions = null)
    {
        var request = BuildGetExchangeInformation(new string[] { symbol }, permissions);

        return await Client.ExecuteRequestAsync<ExchangeInformation>(request, false);
    }

    private static RestRequest BuildGetExchangeInformation(string[]? symbols = null, string[]? permissions = null)
    {
        var request = new RestRequest("api/v3/exchangeInfo", Method.Get);

        if(symbols is not null && symbols.Any())
        {
            if (symbols.Length == 1)
                request.AddParameter("symbol", symbols.Single());

            else
                request.AddParameter("symbols", $"[\"{string.Join("\",\"", symbols)}\"]");
        }

        if (permissions is not null && permissions.Any())
        {
            if (permissions.Length == 1)
                request.AddParameter("permissions", permissions.Single());

            else
                request.AddParameter("symbols", $"[\"{string.Join("\",\"", permissions)}\"]");
        }

        return request;
    }

    #endregion Get Exchange Information

    #endregion Original Methods

    #region Derived Methods

    public decimal GetTradeStepSize(string symbol)
    {
        var info = GetExchangeInformation(symbol);

        var filter = info.Symbols.First().Filters?.Where(x => x.FilterType == "LOT_SIZE").Single();

        if (filter is null)
            throw new Exception("LOT_SIZE filter not present for the current symbol.");

        return (filter.StepSize);
    }

    public async Task<decimal> GetTradeStepSizeAsync(string symbol)
    {
        var info = await GetExchangeInformationAsync(symbol);

        var filter = info.Symbols.First().Filters?.Where(x => x.FilterType == "LOT_SIZE").Single();

        if (filter is null)
            throw new Exception("LOT_SIZE filter not present for the current symbol.");

        return (filter.StepSize);
    }

    #endregion Derived Methods
}

