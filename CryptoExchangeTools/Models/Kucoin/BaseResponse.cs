using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CryptoExchangeTools.Models.Kucoin;

public class BaseResponse
{
    [JsonProperty("code")]
    [JsonConverter(typeof(StringToIntConverter))]
    public required int Code { get; set; }

    [JsonProperty("data")]
    public required JToken Data { get; set; }

    public T ParseData<T>()
    {
        if (Data is null)
            throw new Exception("Response Data is null");

        var parsed = Data.ToObject<T>()
            ?? throw new Exception($"Can't cast response data as {nameof(T)}");

        return parsed;
    }
}

