using CryptoExchangeTools.Models.Binance;
using RestSharp;

namespace CryptoExchangeTools.Requests.CommexRequests;

public class Margin
{
    private CommexClient Client;

    public Margin(CommexClient client)
    {
        Client = client;
    }

    #region Original Methods

    #region Query Isolated Margin Fee Data

    public IsolatedMarginFeeData[] QueryIsolatedMarginFeeData(string? symbol = null, int vipLevel = -1, long recvWindow =-1)
    {
        var request = BuildQueryIsolatedMarginFeeData(vipLevel, symbol, recvWindow);

        return Client.ExecuteRequest<IsolatedMarginFeeData[]>(request);
    }

    public async Task<IsolatedMarginFeeData[]> QueryIsolatedMarginFeeDataAsync(string? symbol = null, int vipLevel = -1, long recvWindow = -1)
    {
        var request = BuildQueryIsolatedMarginFeeData(vipLevel, symbol, recvWindow);

        return await Client.ExecuteRequestAsync<IsolatedMarginFeeData[]>(request);
    }

    private static RestRequest BuildQueryIsolatedMarginFeeData(int vipLevel, string? symbol, long recvWindow)
    {
        var request = new RestRequest("api/v1/margin/isolatedMarginData");

        if (vipLevel != -1)
            request.AddParameter("vipLevel", vipLevel);

        if (recvWindow != -1)
            request.AddParameter("recvWindow", recvWindow);

        if(!string.IsNullOrEmpty(symbol))
            request.AddParameter("symbol", symbol.ToUpper());

        return request;
    }

    #endregion Query Isolated Margin Fee Data

    #endregion Original Methods
}

