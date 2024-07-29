using CryptoExchangeTools.Models.ByBit;
using RestSharp;

namespace CryptoExchangeTools.Requests.BybitRequests;

public class WalletBalance
{
	private readonly ByBitClient client;

	public WalletBalance(ByBitClient client)
	{
		this.client = client;
	}
	
	#region Original Methods
	
	#region Get Wallet Balance

	public WalletBalanceResponse GetWalletBalance()
	{
		return client.ExecuteRequest<WalletBalanceResponse>(BuildGetWalletBalanceRequest());
	}
	
	public async Task<WalletBalanceResponse> GetWalletBalanceAsync()
	{
		return await client.ExecuteRequestAsync<WalletBalanceResponse>(BuildGetWalletBalanceRequest());
	}

	private static RestRequest BuildGetWalletBalanceRequest()
	{
		return new RestRequest("spot/v3/private/account");
	}

	#endregion
	
	#endregion

	#region DerivedMethods
	
	#region GetBalance

	public decimal GetBalance(string currency)
	{
		var data = GetWalletBalance();

		return GetFreeBalanceFromBalanceData(currency, data);
	}
	
	public async Task<decimal> GetBalanceAsync(string currency)
	{
		var data = await GetWalletBalanceAsync();

		return GetFreeBalanceFromBalanceData(currency, data);
	}

	private static decimal GetFreeBalanceFromBalanceData(string currency, WalletBalanceResponse data)
	{
		var currencyBalance = data.Balances
			.Where(x => string.Equals(currency, x.Coin, StringComparison.InvariantCultureIgnoreCase))
			.ToList();

		if (!currencyBalance.Any())
			return 0;

		if (currencyBalance.Count > 1)
			throw new Exception($"Ambigous currency reference! Found {currencyBalance.Count} records for currency {currency}.");

		return currencyBalance.Single().Free;
	}
	
	#endregion

	#endregion
}