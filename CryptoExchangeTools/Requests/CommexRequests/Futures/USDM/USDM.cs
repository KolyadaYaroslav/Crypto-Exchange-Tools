namespace CryptoExchangeTools.Requests.CommexRequests.Futures.USDM;

public class USDM
{
    public BinanceRequests.Futures.USDM.Market Market { get; }

    public BinanceRequests.Futures.USDM.Account Account { get; }

    public USDM(BinanceFuturesClient CLient)
	{
        Market = new(CLient);
        Account = new(CLient);
    }
}

