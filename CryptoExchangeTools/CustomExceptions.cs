using System;
using System.Net;
using System.Security.Cryptography;
using static CryptoExchangeTools.Models.Binance.Wallet.CoinInformation.WithdrawHistoryRecord;

namespace CryptoExchangeTools;

public class ConnectionNotSetException : Exception
{
	public HttpStatusCode StatusCode;
	public string? Response;

    public ConnectionNotSetException(string message, HttpStatusCode statusCode, string? response) :base(message)
    {
        StatusCode = statusCode;
        Response = response;
    }
}

public class BadAccountStatusException : Exception
{
    public HttpStatusCode StatusCode;
    public string? Response;

    public BadAccountStatusException(string message, HttpStatusCode statusCode, string? response) : base(message)
    {
        StatusCode = statusCode;
        Response = response;
    }
}

public class RequestNotSuccessfulException : Exception
{
    public string Endpoint;
    public HttpStatusCode StatusCode;
    public string? Response;

    public RequestNotSuccessfulException(string endpoint, HttpStatusCode statusCode, string? response) : base("Request was not successful.")
    {
        Endpoint = endpoint;
        StatusCode = statusCode;
        Response = response;
    }
}

public class WithdrawalFailedException : Exception
{
    public string id { get; set; }
    public WithdrawStatus Status { get; set; }

    public WithdrawalFailedException(string id, WithdrawStatus status) : base("Withdraw has failed.")
    {
        this.id = id;
        Status = status;
    }
}

public class AssetIsNullException : Exception
{
    public string asset { get; set; }

    public AssetIsNullException(string asset) : base("Asset balance is zero.")
    {
        this.asset = asset;
    }
}