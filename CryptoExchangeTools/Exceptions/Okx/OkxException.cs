using System.Reflection.Metadata;
using System.Xml.Linq;
using CryptoExchangeTools.Utility;
using Newtonsoft.Json.Linq;

namespace CryptoExchangeTools.Exceptions.Okx;

public class OkxException : Exception
{
	public OkxException(string message) : base(message)
	{
    }

	public static void ThrowExceptionBasedOnCode(int code, string message, JToken? data)
	{
		var exceptions = ErrorCode.GetCodedErrors();

		var matchinExceptions = exceptions
			.Where(x => x
				.GetCustomAttributes(false)
				.OfType<ErrorCode>()
				.Single()?
				.Code == code)
			.ToList();

		if(!matchinExceptions.Any())
            throw new OkxException($"[{code}] {message} {data}");

        var exception = Activator.CreateInstance(matchinExceptions.Single(), message);

        throw exception switch
        {
	        null => new OkxException($"[{code}] {message} {data}"),
	        OkxException ex => ex,
	        _ => new OkxException($"[{code}] {message} {data}")
        };
	}
}

