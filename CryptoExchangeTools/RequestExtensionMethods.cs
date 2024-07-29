using System;
using RestSharp;
using System.Text;
using System.Web;

namespace CryptoExchangeTools;

internal static class RequestExtensionMethods
{
    internal static string? GetQueryString(this RestRequest request)
    {
        var queryParams = request.Parameters.Where(x => x.Type == ParameterType.GetOrPost).ToList();

        if (!queryParams.Any())
            return null;

        var queryString = new StringBuilder();

        foreach (var param in queryParams.Where(param => param.Value is not null))
        {
            queryString.Append(param.Name);
            queryString.Append('=');
            queryString.Append(HttpUtility.UrlEncode(param.Value?.ToString()));

            if (param != queryParams.Last())
                queryString.Append('&');
        }

        return queryString.ToString();
    }
}

