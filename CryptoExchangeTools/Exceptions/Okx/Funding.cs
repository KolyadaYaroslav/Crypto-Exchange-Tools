using RestSharp;

namespace CryptoExchangeTools.Exceptions.Okx;

[ErrorCode(58207)]
public class WithdrawalAddressIsNotWhitelisted : OkxException
{
    public WithdrawalAddressIsNotWhitelisted(string message) : base(message)
	{
	}
}

