using CryptoExchangeTools.Exceptions.Okx;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CryptoExchangeTools.Models.ByBit;

public class BaseResponse
{
	[JsonProperty("retCode")]
	public required int RetCode { get; set; }
	
	[JsonProperty("retMsg")]
	public required string RetMsg { get; set; }
	
	[JsonProperty("result")]
	public JToken? Result { get; set; }
	
	[JsonProperty("retExtInfo")]
	public object? RetExtInfo { get; set; }
	
	[JsonProperty("retExtMap")]
	public object? RetExtMap { get; set; }
	
	[JsonProperty("time")]
	public long Time { get; set; }

	public T ParseData<T>()
	{
		if (Result is null || !Result.Any())
			throw new Exception($"Content: {RetMsg}");

		var parsed = Result.ToObject<T>()
		             ?? throw new Exception($"Can't cast response data as {nameof(T)} Content: {RetMsg}");

		return parsed;
	}
}