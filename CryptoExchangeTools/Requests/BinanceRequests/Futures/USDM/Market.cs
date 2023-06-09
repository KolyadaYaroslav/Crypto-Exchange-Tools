using CryptoExchangeTools.Models.Binance.Futures.USDM;
using RestSharp;

namespace CryptoExchangeTools.Requests.BinanceRequests.Futures.USDM;

public class Market
{
    private BinanceFuturesClient Client;

    public Market(BinanceFuturesClient client)
    {
        Client = client;
    }

    #region Original Methods

    #region Get Mark Price

    public MarkData GetMarkPrice(string symbol)
    {
        var request = BuildGetMarkPrice(symbol);

        return Client.ExecuteRequest<MarkData>(request);
    }

    public async Task<MarkData> GetMarkPriceAsync(string symbol)
    {
        var request = BuildGetMarkPrice(symbol);

        return await Client.ExecuteRequestAsync<MarkData>(request);
    }

    private static RestRequest BuildGetMarkPrice(string symbol)
    {
        var request = new RestRequest("fapi/v1/premiumIndex");

        request.AddParameter("symbol", symbol);

        return request;
    }

    #endregion Get Mark Price

    #region Get Exchange Information

    public ExchangeInformation GetExchangeInformation()
    {
        var request = BuildGetExchangeInformation();

        return Client.ExecuteRequest<ExchangeInformation>(request, false);
    }

    public async Task<ExchangeInformation> GetExchangeInformationAsync()
    {
        var request = BuildGetExchangeInformation();

        return await Client.ExecuteRequestAsync<ExchangeInformation>(request, false);
    }

    private static RestRequest BuildGetExchangeInformation()
    {
        var request = new RestRequest("fapi/v1/exchangeInfo", Method.Get);

        return request;
    }

    #endregion Get Exchange Information

    #endregion Original Methods

    #region Derived Methods

    public decimal GetTradeStepSize(string symbol)
    {
        var info = GetExchangeInformation();

        var filter = info.Symbols
            .Where(x => x.Symbol == symbol)
            .Single()
            .Filters?
            .Where(x => x.FilterType == "LOT_SIZE")
            .Single();

        if (filter is null)
            throw new Exception("LOT_SIZE filter not present for the current symbol.");

        return (filter.StepSize);
    }

    public async Task<decimal> GetTradeStepSizeAsync(string symbol)
    {
        var info = await GetExchangeInformationAsync();

        var filter = info.Symbols
            .Where(x => x.Symbol == symbol)
            .Single()
            .Filters?
            .Where(x => x.FilterType == "LOT_SIZE")
            .Single();

        if (filter is null)
            throw new Exception("LOT_SIZE filter not present for the current symbol.");

        return (filter.StepSize);
    }

    #endregion Derived Methods
}

