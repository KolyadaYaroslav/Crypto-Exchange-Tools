using System;
using Newtonsoft.Json;
using RestSharp;

namespace CryptoExchangeTools;

internal static class RestResponseExtension
{
	internal static T Deserialize<T>(this RestResponse response)
	{
        if (!response.IsSuccessful || response.Content is null)
            throw new RequestNotSuccessfulException(response.Request.Resource, response.StatusCode, response.Content);

        T data = JsonConvert.DeserializeObject<T>(response.Content)
            ?? throw new ArgumentNullException("assetInfo", "parameter was null after deserialization");

        return data;
    }
}

