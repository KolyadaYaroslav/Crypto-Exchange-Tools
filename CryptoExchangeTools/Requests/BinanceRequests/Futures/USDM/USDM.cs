using CryptoExchangeTools.Requests.BinanceRequests;

namespace CryptoExchangeTools.Requests.BinanceRequests.Futures.USDM;

public class USDM
{
    public Market Market { get; }

    public Account Account { get; }

    public USDM(BinanceFuturesClient CLient)
	{
        Market = new(CLient);
        Account = new(CLient);
    }
}

