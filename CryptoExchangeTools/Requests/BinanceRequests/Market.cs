using System.Linq;
using CryptoExchangeTools.Models.Binance;
using CryptoExchangeTools.Models.ICex;
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

    #region Symbol Price Ticker

    /// <summary>
    /// Latest price for a symbol or symbols.
    /// </summary>
    /// <param name="symbol"></param>
    /// <returns></returns>
    public SymbolPriceTicker GetPriceTicker(string symbol)
    {
        var request = BuildGetTicker(new List<string> { symbol });

        return Client.ExecuteRequest<SymbolPriceTicker>(request, false);
    }

    /// <summary>
    /// Latest price for a symbol or symbols.
    /// </summary>
    /// <param name="symbols"></param>
    /// <returns></returns>
    public SymbolPriceTicker GetPriceTicker(IEnumerable<string> symbols)
    {
        var request = BuildGetTicker(symbols.ToList());

        return Client.ExecuteRequest<SymbolPriceTicker>(request, false);
    }

    /// <summary>
    /// Latest price for a symbol or symbols.
    /// </summary>
    /// <param name="symbol"></param>
    /// <returns></returns>
    public async Task<SymbolPriceTicker> GetPriceTickerAsync(string symbol)
    {
        var request = BuildGetTicker(new List<string> { symbol });

        return await Client.ExecuteRequestAsync<SymbolPriceTicker>(request, false);
    }

    /// <summary>
    /// Latest price for a symbol or symbols.
    /// </summary>
    /// <param name="symbols"></param>
    /// <returns></returns>
    public async Task<SymbolPriceTicker> GetPriceTickerAsync(IEnumerable<string> symbols)
    {
        var request = BuildGetTicker(symbols.ToList());

        return await Client.ExecuteRequestAsync<SymbolPriceTicker>(request, false);
    }

    private static RestRequest BuildGetTicker(List<string>? symbols = null)
    {
        var request = new RestRequest("api/v3/ticker/price", Method.Get);

        if (symbols is not null && symbols.Count == 1)
            request.AddParameter("symbol", symbols.Single().ToUpper());

        else if(symbols is not null && symbols.Any())
        {
            symbols.ForEach(x => x = x.ToUpper());
            request.AddParameter("symbols", $"[\"{string.Join("\",\"", symbols)}\"]");
        }

        return request;
    }

    #endregion Symbol Price Ticker

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

    #region Get Min Order Size

    public decimal GetMinOrderSize(string symbol)
    {
        var data = GetExchangeInformation(symbol);

        var filter = data
            .Symbols
            .Single()
            .Filters?
            .Where(x => x.FilterType == "NOTIONAL");

        if (filter is null || !filter.Any())
            throw new Exception("Can't find NOTIONAL filter. Maybe symbol is incorrect?");

        return filter.Single().MinNotional;
    }

    public async Task<decimal> GetMinOrderSizeAsync(string symbol)
    {
        var data = await GetExchangeInformationAsync(symbol);

        var filter = data
            .Symbols
            .Single()
            .Filters?
            .Where(x => x.FilterType == "NOTIONAL");

        if (filter is null || !filter.Any())
            throw new Exception("Can't find NOTIONAL filter. Maybe symbol is incorrect?");

        return filter.Single().MinNotional;
    }

    #endregion Get Min Order Size

    #region Get Min Order Size For Pair

    public decimal GetMinOrderSizeForPair(string baseCurrency, string quoteCurrency, CalculationBase calculationBase = CalculationBase.Base)
    {
        string symbol = baseCurrency.ToUpper() + quoteCurrency.ToUpper();

        try
        {
            Client.Market.GetPriceTicker(symbol);
        }
        catch (RequestNotSuccessfulException ex) when (ex.Response == "{\"code\":-1121,\"msg\":\"Invalid symbol.\"}")
        {
            symbol = quoteCurrency.ToUpper() + baseCurrency.ToUpper();

            (quoteCurrency, baseCurrency) = (baseCurrency, quoteCurrency);

            calculationBase = HelperMethods.ReverseCalculationBase(calculationBase);
        }

        var amount = GetMinOrderSize(symbol);

        if (calculationBase == CalculationBase.Base)
            return Client.GetAmountIn(baseCurrency, quoteCurrency, amount, 1);

        return amount;
    }

    public async Task<decimal> GetMinOrderSizeForPairAsync(string baseCurrency, string quoteCurrency, CalculationBase calculationBase = CalculationBase.Base)
    {
        string symbol = baseCurrency.ToUpper() + quoteCurrency.ToUpper();

        try
        {
            await Client.Market.GetPriceTickerAsync(symbol);
        }
        catch (RequestNotSuccessfulException ex) when (ex.Response == "{\"code\":-1121,\"msg\":\"Invalid symbol.\"}")
        {
            symbol = quoteCurrency.ToUpper() + baseCurrency.ToUpper();

            (quoteCurrency, baseCurrency) = (baseCurrency, quoteCurrency);

            calculationBase = HelperMethods.ReverseCalculationBase(calculationBase);
        }

        var amount = await GetMinOrderSizeAsync(symbol);

        if (calculationBase == CalculationBase.Base)
            return await Client.GetAmountInAsync(baseCurrency, quoteCurrency, amount, 1);

        return amount;
    }

    #endregion Get Min Order Size For Pair

    #endregion Derived Methods
}

