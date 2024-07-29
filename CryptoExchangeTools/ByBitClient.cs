using System.Net;
using System.Security.Cryptography;
using System.Text;
using CryptoExchangeTools.Models.ByBit;
using CryptoExchangeTools.Models.ICex;
using CryptoExchangeTools.Requests.BybitRequests;
using Newtonsoft.Json;
using RestSharp;

namespace CryptoExchangeTools;

public class ByBitClient : CexClient, ICexClient
{
	#region Initialize

	private const string Url = "https://api.bybit.com";

	private const int DefaultRecvWindow = 5000;
	
	public WalletBalance WalletBalance { get; }
	
	public Assets Assets { get; }

	public ByBitClient(string apiKey, string apiSecret, WebProxy? proxy) : base(apiKey, apiSecret, Url, proxy, false)
	{
		WalletBalance = new WalletBalance(this);
		Assets = new Assets(this);
	}
	
	protected sealed override T DeserializeResponse<T>(RestResponse response)
	{
		return response
			.Deserialize<BaseResponse>()
			.ParseData<T>();
	}
	
	#endregion
	
	#region Signature

	protected override void SignRequest(RestRequest request)
	{
		request.AddHeader("X-BAPI-API-KEY", ApiKey);
		request.AddHeader("X-BAPI-TIMESTAMP", DateTimeOffset.UtcNow.ToUnixTimeMilliseconds());

		var signature = Sign(request);

		request.AddHeader("X-BAPI-SIGN", signature);
	}

	private string Sign(RestRequest request)
	{
		var key = Encoding.UTF8.GetBytes(ApiSecret);

		using HMACSHA256 hmacsha256 = new(key);

		var payload = request.Method switch
		{
			Method.Get => GeneratePayloadToSignFromGetRequest(request),
			Method.Post => GeneratePayloadToSignFromPostRequest(request),
			_ => throw new ArgumentOutOfRangeException()
		};

		byte[] payloadBytes = Encoding.UTF8.GetBytes(payload);
		byte[] hash = hmacsha256.ComputeHash(payloadBytes);

		return BitConverter.ToString(hash).Replace("-", string.Empty).ToLower();
	}

	private string GeneratePayloadToSignFromGetRequest(RestRequest request)
	{
		var queryString = request.GetQueryString();

		var timestamp = request.Parameters.Single(x => x.Name == "X-BAPI-TIMESTAMP").Value ??
		                throw new Exception("Timestamp was not present in the request.");

		var recvWindowParam = request.Parameters.Where(x => x.Name == "RECV_WINDOW").ToList();

		int recvWindow;

		if (!recvWindowParam.Any())
		{
			request.AddHeader("X-BAPI-RECV-WINDOW", DefaultRecvWindow);
			recvWindow = DefaultRecvWindow;
		}
		else
		{
			var recvWindowA = recvWindowParam.Single().Value
			                  ?? throw new Exception("Recv Window param was presetn, but it's present was null.");

			recvWindow = (int)recvWindowA;
		}

		return timestamp + ApiKey + recvWindow + queryString;
	}

	private string GeneratePayloadToSignFromPostRequest(RestRequest request)
	{
		var body = request.Parameters.Single(x => x.Type == ParameterType.RequestBody)
			?? throw new Exception("Expected request body, but body was null.");

		var sb = (string)(body.Value ?? throw new Exception("Expected request body, but body was null."));
		
		var timestamp = request.Parameters.Single(x => x.Name == "X-BAPI-TIMESTAMP").Value ??
		                throw new Exception("Timestamp was not present in the request.");

		var recvWindowParam = request.Parameters.Where(x => x.Name == "RECV_WINDOW").ToList();
		
		int recvWindow;
		
		if (!recvWindowParam.Any())
		{
			request.AddHeader("X-BAPI-RECV-WINDOW", DefaultRecvWindow);
			recvWindow = DefaultRecvWindow;
		}
		else
		{
			var recvWindowA = recvWindowParam.Single().Value
			                  ?? throw new Exception("Recv Window param was presetn, but it's present was null.");

			recvWindow = (int)recvWindowA;
		}
		
		return timestamp + ApiKey + recvWindow + sb;
	}

	#endregion

	#region Global Methods

	public WithdrawalRecord Withdraw(string currency, decimal amount, string address, string network,
		bool waitForApprove = true)
	{
		if (!waitForApprove)
		{
			var id = Assets.Withdraw(currency, network, address, amount);

			return new WithdrawalRecord
			{
				TxId = id.Id,
				RequestedAmount = amount,
				WaitedForApproval = waitForApprove,
			};
		}
		else
		{
			var (id, hash) = Assets.WithdrawAndWaitForSent(currency, network, address, amount);

			return new WithdrawalRecord
			{
				TxId = id,
				RequestedAmount = amount,
				WaitedForApproval = waitForApprove,
				TxHash = hash
			};
		}
	}

	public async Task<WithdrawalRecord> WithdrawAsync(string currency, decimal amount, string address, string network,
		bool waitForApprove = true)
	{
		if (!waitForApprove)
		{
			var id = await Assets.WithdrawAsync(currency, network, address, amount);

			return new WithdrawalRecord
			{
				TxId = id.Id,
				RequestedAmount = amount,
				WaitedForApproval = waitForApprove,
			};
		}
		else
		{
			var (id, hash) = await Assets.WithdrawAndWaitForSentAsync(currency, network, address, amount);

			return new WithdrawalRecord
			{
				TxId = id,
				RequestedAmount = amount,
				WaitedForApproval = waitForApprove,
				TxHash = hash
			};
		}
	}

	public override decimal GetBalance(string currency)
	{
		return WalletBalance.GetBalance(currency);
	}
	
	public override async Task<decimal> GetBalanceAsync(string currency)
	{
		return await WalletBalance.GetBalanceAsync(currency);
	}

	public override int QueryWithdrawalPrecision(string currency, string network)
	{
		var data = Assets.GetCoinInformation(currency);

		return data
			.Rows.Single()
			.Chains.Single(x => string.Equals(x.ChainChain, network))
			.MinAccuracy;
	}

	public override async Task<int> QueryWithdrawalPrecisionAsync(string currency, string network)
	{
		var data = await Assets.GetCoinInformationAsync(currency);

		return data
			.Rows.Single()
			.Chains.Single(x => string.Equals(x.ChainChain, network))
			.MinAccuracy;
	}

	public override decimal QueryWithdrawalMinAmount(string currency, string network)
	{
		var data = Assets.GetCoinInformation(currency);

		return data
			.Rows.Single()
			.Chains.Single(x => string.Equals(x.ChainChain, network))
			.WithdrawMin;
	}

	public override async Task<decimal> QueryWithdrawalMinAmountAsync(string currency, string network)
	{
		var data = await Assets.GetCoinInformationAsync(currency);

		return data
			.Rows.Single()
			.Chains.Single(x => string.Equals(x.ChainChain, network))
			.WithdrawMin;
	}

	#endregion
}