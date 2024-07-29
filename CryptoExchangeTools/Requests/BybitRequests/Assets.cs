using CryptoExchangeTools.Models.ByBit;
using Newtonsoft.Json;
using RestSharp;

namespace CryptoExchangeTools.Requests.BybitRequests;

public class Assets
{
	private readonly ByBitClient client;

	public Assets(ByBitClient client)
	{
		this.client = client;
	}
	
	#region Original Methods

	#region Withdraw

	/// <summary>
	/// Withdraw assets from your Bybit account. You can make an off-chain transfer if the target wallet address is from Bybit. This means that no blockchain fee will be charged.
	/// </summary>
	/// <param name="coin">coin name</param>
	/// <param name="chain">chain name</param>
	/// <param name="address">Withdraw address. Please note that the address is case sensitive, so use the exact same address added in address book</param>
	/// <param name="amount">withdraw amounts. The minimum withdrawal amount can be obtained from the Coin Info Query API</param>
	/// <param name="tag">Need to fill in if there is a memo/tag when adding the wallet address. Note: please do not set a tag/memo in the address book if the chain does not support tag</param>
	/// <param name="forceChain">Force to withdraw on chain or not. 0(default)：If parsed as internal address, then internal transfer. 1：Force withdraw on chain</param>
	/// <param name="accountType">Select the wallet to be withdrawn from. SPOT(default)：spot wallet. FUND：Funding wallet</param>
	/// <param name="feeType">Handling fee option. 0(default): input amount is the actual amount received, so you have to calculate handling fee manually. 1: input amount is not the actual amount you received, the system will help to deduct the handling fee automatically</param>
	/// <param name="requestId">Customised ID, globally unique, it is used for idempotent verification. A combination of letters (case sensitive) and numbers, which can be pure letters or pure numbers and the length must be between 1 and 32 digits</param>
	/// <returns>Withdrawal ID</returns>
	public WithdrawRequestResponse Withdraw(
		string coin,
		string chain,
		string address,
		decimal amount,
		string? tag = null,
		bool? forceChain = null,
		AccountType accountType = AccountType.SPOT,
		bool? feeType = null,
		string? requestId = null)
	{
		var request = BuildWithdrawRequest(
			coin,
			chain,
			address,
			tag,
			amount,
			forceChain,
			accountType,
			feeType,
			requestId);

		return client.ExecuteRequest<WithdrawRequestResponse>(request);
	}
	
	/// <summary>
	/// Withdraw assets from your Bybit account. You can make an off-chain transfer if the target wallet address is from Bybit. This means that no blockchain fee will be charged.
	/// </summary>
	/// <param name="coin">coin name</param>
	/// <param name="chain">chain name</param>
	/// <param name="address">Withdraw address. Please note that the address is case sensitive, so use the exact same address added in address book</param>
	/// <param name="amount">withdraw amounts. The minimum withdrawal amount can be obtained from the Coin Info Query API</param>
	/// <param name="tag">Need to fill in if there is a memo/tag when adding the wallet address. Note: please do not set a tag/memo in the address book if the chain does not support tag</param>
	/// <param name="forceChain">Force to withdraw on chain or not. 0(default)：If parsed as internal address, then internal transfer. 1：Force withdraw on chain</param>
	/// <param name="accountType">Select the wallet to be withdrawn from. SPOT(default)：spot wallet. FUND：Funding wallet</param>
	/// <param name="feeType">Handling fee option. 0(default): input amount is the actual amount received, so you have to calculate handling fee manually. 1: input amount is not the actual amount you received, the system will help to deduct the handling fee automatically</param>
	/// <param name="requestId">Customised ID, globally unique, it is used for idempotent verification. A combination of letters (case sensitive) and numbers, which can be pure letters or pure numbers and the length must be between 1 and 32 digits</param>
	/// <returns>Withdrawal ID</returns>
	public async Task<WithdrawRequestResponse> WithdrawAsync(
		string coin,
		string chain,
		string address,
		decimal amount,
		string? tag = null,
		bool? forceChain = null,
		AccountType accountType = AccountType.SPOT,
		bool? feeType = null,
		string? requestId = null)
	{
		var request = BuildWithdrawRequest(
			coin,
			chain,
			address,
			tag,
			amount,
			forceChain,
			accountType,
			feeType,
			requestId);

		return await client.ExecuteRequestAsync<WithdrawRequestResponse>(request);
	}

	private static RestRequest BuildWithdrawRequest(
		string coin,
		string chain,
		string address,
		string? tag,
		decimal amount,
		bool? forceChain,
		AccountType accountType,
		bool? feeType,
		string? requestId)
	{
		var request = new RestRequest("asset/v3/private/withdraw/create", Method.Post);

		var body = new WithdrawRequestBody(
			coin.ToUpper(),
			chain.ToUpper(),
			address,
			tag,
			amount,
			forceChain,
			accountType,
			feeType,
			requestId);

		request.AddBody(JsonConvert.SerializeObject(body));

		return request;
	}

	#endregion

	#region Get Withdraw Records

	/// <summary>
	/// Query Withdraw Records
	/// </summary>
	/// <param name="withdrawId">withdrawal id. (withdraw request ID in Response Parameters of Withdrawal endpoint)</param>
	/// <param name="txId">Transaction hash ID</param>
	/// <param name="startTime">The start time (ms). Default value: 30 days before the current time</param>
	/// <param name="endTime">The end time (ms). Default value: current time</param>
	/// <param name="coin">coin name: for example, BTC. Default value: all</param>
	/// <param name="withdrawType">0 (default)：on chain. 1：off chain. 2：on and off chain</param>
	/// <param name="cursor">Cursor, used for pagination</param>
	/// <param name="limit">Number of items per page. [1, 50] Default value: 50</param>
	/// <returns></returns>
	public WithdrawRecordResponse GetWithdrawRecords(
		long withdrawId = -1,
		string? txId = null,
		long startTime = -1,
		long endTime = -1,
		string? coin = null,
		bool? withdrawType = null,
		string? cursor = null,
		int limit = -1)
	{
		var request = BuildGetWithdrawRecordsRequest(
			withdrawId,
			txId,
			startTime,
			endTime,
			coin,
			withdrawType,
			cursor,
			limit);

		return client.ExecuteRequest<WithdrawRecordResponse>(request);
	}
	
	/// <summary>
	/// Query Withdraw Records
	/// </summary>
	/// <param name="withdrawId">withdrawal id. (withdraw request ID in Response Parameters of Withdrawal endpoint)</param>
	/// <param name="txId">Transaction hash ID</param>
	/// <param name="startTime">The start time (ms). Default value: 30 days before the current time</param>
	/// <param name="endTime">The end time (ms). Default value: current time</param>
	/// <param name="coin">coin name: for example, BTC. Default value: all</param>
	/// <param name="withdrawType">0 (default)：on chain. 1：off chain. 2：on and off chain</param>
	/// <param name="cursor">Cursor, used for pagination</param>
	/// <param name="limit">Number of items per page. [1, 50] Default value: 50</param>
	/// <returns></returns>
	public async Task<WithdrawRecordResponse> GetWithdrawRecordsAsync(
		long withdrawId = -1,
		string? txId = null,
		long startTime = -1,
		long endTime = -1,
		string? coin = null,
		bool? withdrawType = null,
		string? cursor = null,
		int limit = -1)
	{
		var request = BuildGetWithdrawRecordsRequest(
			withdrawId,
			txId,
			startTime,
			endTime,
			coin,
			withdrawType,
			cursor,
			limit);

		return await client.ExecuteRequestAsync<WithdrawRecordResponse>(request);
	}

	private static RestRequest BuildGetWithdrawRecordsRequest(
		long withdrawId = -1,
		string? txId = null,
		long startTime = -1,
		long endTime = -1,
		string? coin = null,
		bool? withdrawType = null,
		string? cursor = null,
		int limit = -1)
	{
		var request = new RestRequest("asset/v3/private/withdraw/record/query");

		if (withdrawId != -1)
			request.AddParameter("withdrawID", withdrawId);
		
		if (!string.IsNullOrEmpty(txId))
			request.AddParameter("txID", txId);
		
		if (withdrawId != -1)
			request.AddParameter("startTime", startTime);
		
		if (withdrawId != -1)
			request.AddParameter("endTime", endTime);
		
		if (!string.IsNullOrEmpty(coin))
			request.AddParameter("coin", coin);
		
		if (withdrawType is not null)
			request.AddParameter("withdrawType", (bool)withdrawType ? 1 : 0);
		
		if (!string.IsNullOrEmpty(cursor))
			request.AddParameter("cursor", cursor);
		
		if (limit != -1)
			request.AddParameter("limit", limit);

		return request;
	}

	#endregion

	#region Get All Coins Balance
	
	/// <summary>
	/// You could get all coins balance of all account types under the master account, and sub account.
	/// </summary>
	/// <param name="memberId">User Id. It is required when you use master api key to check sub account coin balance</param>
	/// <param name="accountType">Account type</param>
	/// <param name="withBonus">Whether query bonus or not.</param>
	/// <param name="coins">Query all coins if not passed</param>

	public GetAllCoinsBalanceResponse GetAllCoinsBalance(
		string? memberId = null,
		AccountType accountType = AccountType.FUND,
		bool? withBonus = null,
		params string[] coins)
	{
		var request = BuildGetAllCoinsBalance(memberId, accountType, withBonus, coins);
		
		return client.ExecuteRequest<GetAllCoinsBalanceResponse>(request);
	}
	
	/// <summary>
	/// You could get all coins balance of all account types under the master account, and sub account.
	/// </summary>
	/// <param name="memberId">User Id. It is required when you use master api key to check sub account coin balance</param>
	/// <param name="accountType">Account type</param>
	/// <param name="withBonus">Whether query bonus or not.</param>
	/// <param name="coins">Query all coins if not passed</param>

	public async Task<GetAllCoinsBalanceResponse> GetAllCoinsBalanceAsync(
		string? memberId = null,
		AccountType accountType = AccountType.FUND,
		bool? withBonus = null,
		params string[] coins)
	{
		var request = BuildGetAllCoinsBalance(memberId, accountType, withBonus, coins);

		return await client.ExecuteRequestAsync<GetAllCoinsBalanceResponse>(request);
	}

	private static RestRequest BuildGetAllCoinsBalance(
		string? memberId,
		AccountType accountType,
		bool? withBonus,
		params string[] coins)
	{
		var request = new RestRequest("asset/v3/private/transfer/account-coins/balance/query");
		
		request.AddParameter("accountType", accountType.ToString());
		
		if (!string.IsNullOrEmpty(memberId))
			request.AddParameter("memberId", memberId);
		
		if (withBonus is not null)
			request.AddParameter("withBonus", (bool)withBonus ? 1 : 0);

		if (coins.Any())
		{
			var parameterValue = coins.Length == 1
				? coins.Single().ToUpper()
				: string.Join(',', coins.Select(x => x.ToUpper()));
			
			request.AddParameter("coin", parameterValue);
		}

		return request;
	}

	#endregion
	
	#region Get Coin Information

	public GetCoinInfromationResponse GetCoinInformation(params string[] coins)
	{
		var request = BuildGetCoinInformationRequest(coins);

		return client.ExecuteRequest<GetCoinInfromationResponse>(request);
	}
	
	public async Task<GetCoinInfromationResponse> GetCoinInformationAsync(params string[] coins)
	{
		var request = BuildGetCoinInformationRequest(coins);

		return await client.ExecuteRequestAsync<GetCoinInfromationResponse>(request);
	}

	private RestRequest BuildGetCoinInformationRequest(params string[] coins)
	{
		var request = new RestRequest("asset/v3/private/coin-info/query");
		
		if (coins.Any())
		{
			var parameterValue = coins.Length == 1
				? coins.Single().ToUpper()
				: string.Join(',', coins.Select(x => x.ToUpper()));
			
			request.AddParameter("coin", parameterValue);
		}

		return request;
	}
	
	#endregion
	
	#endregion
	
	#region Derived Methods
	
	#region Withdraw And Wait For Sent
	
	public (string, string) WithdrawAndWaitForSent(string coin,
		string chain,
		string address,
		decimal amount,
		string? tag = null,
		bool? forceChain = null,
		AccountType accountType = AccountType.SPOT,
		bool? feeType = null,
		string? requestId = null)
	{
		var withdrawalId = Withdraw(coin,
			chain,
			address,
			amount,
			tag,
			forceChain,
			accountType,
			feeType,
			requestId);
		
		var limit = 500;
		while (true)
		{
			Thread.Sleep(10_000);

			if (limit == 0)
				throw new Exception("Timeout for awaiting transaction completion was hit");

			var history = GetWithdrawRecords();

			if (history.Rows.Any())
			{
				var txData = history.Rows.Single(x => x.WithdrawId == withdrawalId.Id);

				client.Message(txData.Status.ToString());

				switch (txData.Status)
				{
					case WithdrawStatus.success 
						or WithdrawStatus.BlockchainConfirmed:
						return (withdrawalId.Id, txData.TxId);
					
					case WithdrawStatus.CancelByUser 
						or WithdrawStatus.Reject
						or WithdrawStatus.Fail
						or WithdrawStatus.MoreInformationRequired:
						throw new WithdrawalFailedException(withdrawalId.Id, txData.Status.ToString(), null);
				}
			}
			limit--;
		}
	}

	public async Task<(string, string)> WithdrawAndWaitForSentAsync(string coin,
		string chain,
		string address,
		decimal amount,
		string? tag = null,
		bool? forceChain = null,
		AccountType accountType = AccountType.SPOT,
		bool? feeType = null,
		string? requestId = null)
	{
		var withdrawalId = await WithdrawAsync(coin,
			chain,
			address,
			amount,
			tag,
			forceChain,
			accountType,
			feeType,
			requestId);
		
		var limit = 500;
		while (true)
		{
			await Task.Delay(10_000);

			if (limit == 0)
				throw new Exception("Timeout for awaiting transaction completion was hit");

			var history = await GetWithdrawRecordsAsync();

			if (history.Rows.Any())
			{
				var txData = history.Rows.Single(x => x.WithdrawId == withdrawalId.Id);

				client.Message(txData.Status.ToString());

				switch (txData.Status)
				{
					case WithdrawStatus.success 
						or WithdrawStatus.BlockchainConfirmed:
						return (withdrawalId.Id, txData.TxId);
					
					case WithdrawStatus.CancelByUser 
						or WithdrawStatus.Reject
						or WithdrawStatus.Fail
						or WithdrawStatus.MoreInformationRequired:
						throw new WithdrawalFailedException(withdrawalId.Id, txData.Status.ToString(), null);
				}
			}
			limit--;
		}
	}
	
	#endregion
	
	#endregion
}