using CryptoExchangeTools.Models.Binance;
using RestSharp;

namespace CryptoExchangeTools.Requests.CommexRequests;

public class FuturesTransfer
{
	private CommexClient Client;

	public FuturesTransfer(CommexClient client)
	{
		Client = client;
	}

    #region Original Methods

    #region New Future Account Transfer

    /// <summary>
    /// Execute transfer between spot account and futures account.
    /// </summary>
    /// <param name="asset">The asset being transferred, e.g., USDT</param>
    /// <param name="amount">The amount to be transferred</param>
    /// <param name="type"></param>
    /// <param name="recvWindow"></param>
    /// <returns></returns>
    public long NewFutureAccountTransfer(string asset, decimal amount, TransferType type, long recvWindow = -1)
	{
		var request = BuilNewFutureAccountTransfer(asset, amount, type, recvWindow);

		return Client.ExecuteRequest<TransferResult>(request).TransactionId;
    }

    /// <summary>
    /// Execute transfer between spot account and futures account.
    /// </summary>
    /// <param name="asset">The asset being transferred, e.g., USDT</param>
    /// <param name="amount">The amount to be transferred</param>
    /// <param name="type"></param>
    /// <param name="recvWindow"></param>
    /// <returns></returns>
    public async Task<long> NewFutureAccountTransferAsync(string asset, decimal amount, TransferType type, long recvWindow = -1)
	{
		var request = BuilNewFutureAccountTransfer(asset, amount, type, recvWindow);

		return (await Client.ExecuteRequestAsync<TransferResult>(request)).TransactionId;
    }

    private static RestRequest BuilNewFutureAccountTransfer(string asset, decimal amount, TransferType type, long recvWindow = -1)
	{
		var request = new RestRequest("api/v1/futures/transfer", Method.Post);

		request.AddParameter("asset", asset);
        request.AddParameter("amount", amount);
        request.AddParameter("type", (int)type);

		if(recvWindow != -1)
            request.AddParameter("recvWindow", recvWindow);

		return request;
    }

    #endregion New Future Account Transfer

    #endregion Original Methods
}

