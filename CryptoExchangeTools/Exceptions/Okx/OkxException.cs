using System.Reflection.Metadata;
using System.Xml.Linq;
using CryptoExchangeTools.Utility;

namespace CryptoExchangeTools.Exceptions.Okx;

public class OkxException : Exception
{
	public OkxException(string message) : base(message)
	{
    }

	public static void ThrowExceptionBasedOnCode(int code, string message)
	{
		var exceptions = ErrorCode.GetCodedErrors();

		var matchinExceptions = exceptions
			.Where(x => x.GetCustomAttributes(false)
				.Where(x => x is ErrorCode)
				.Select(x => x as ErrorCode)
				.Single()?
				.Code == code);

		if(!matchinExceptions.Any())
            throw new Exception($"[{code}] {message}");

        var exception = Activator.CreateInstance(matchinExceptions.Single(), message);

		if (exception is null)
			throw new Exception($"[{code}] {message}");

		if (exception is OkxException ex)
			throw ex;

		else
            throw new Exception($"[{code}] {message}");
    }
}

