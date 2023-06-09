using System.Globalization;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using CryptoExchangeTools.Models.ICex;
using CryptoExchangeTools.Models.Okx;
using CryptoExchangeTools.Requests.OkxRequests;
using RestSharp;

namespace CryptoExchangeTools;

public class OkxClient : CexClient, ICexClient
{
    private const string Url = "https://www.okx.com";

    public Account Account { get; }

    public Funding Funding { get; }

    public SubAccount SubAccount { get; }

    public OkxClient(string apiKey, string apiSecret, string passPhrase, WebProxy? proxy = null) : base(apiKey, apiSecret, passPhrase, Url, proxy)
    {
        Account = new(this);
        Funding = new(this);
        SubAccount = new(this);
    }

    protected sealed override void TryLogin()
    {
        var request = new RestRequest("api/v5/system/status");
        var response = ExecuteRequestWithoutResponse(request, false);

        if (!response.IsSuccessful)
            throw new ConnectionNotSetException("Couldn't connect to Okx Server.", response.StatusCode, response.Content);
    }

    internal sealed override void SignRequest(RestRequest request)
    {
        if (string.IsNullOrEmpty(PassPhrase))
            throw new Exception("PassPhrase is not set for Okx client.");

        var ts = DateTimeOffset.UtcNow.ToString("yyyy-MM-ddTHH:mm:ssZ", CultureInfo.InvariantCulture);

        request.AddHeader("OK-ACCESS-KEY", ApiKey);
        request.AddHeader("OK-ACCESS-SIGN", Sign(request, ts));
        request.AddHeader("OK-ACCESS-TIMESTAMP", ts);
        request.AddHeader("OK-ACCESS-PASSPHRASE", PassPhrase);
    }

    private string Sign(RestRequest request, string ts)
    {
        var bodyParams = request.Parameters.Where(x => x.Type == ParameterType.RequestBody);
        var body = bodyParams.Any() ? bodyParams.Single().Value?.ToString() : null;

        var queryString = request.GetQueryString();
        var path = request.Resource + (!string.IsNullOrEmpty(queryString) ? $"?{queryString}" : null);

        var method = request.Method.ToString().ToUpper();

        var preHashString = ts + method + "/" + path + body;

        var key = Encoding.UTF8.GetBytes(ApiSecret);

        using (HMACSHA256 hmacsha256 = new HMACSHA256(key))
        {
            byte[] payloadBytes = Encoding.UTF8.GetBytes(preHashString);
            byte[] hash = hmacsha256.ComputeHash(payloadBytes);

            return System.Convert.ToBase64String(hash);
        }
    }

    protected sealed override T DeserializeResponse<T>(RestResponse response)
    {
        return response
            .Deserialize<BaseResponse>()
            .ParseData<T>();
    }

    public WithdrawalRecord Withdraw(string currency, decimal amount, string address, string network, bool waitForApprove = true)
    {
        if(waitForApprove)
        {
            var result = Funding.WithdrawAndWaitForSent(currency, amount, address, network);

            return new WithdrawalRecord
            {
                TxId = result.WdId,
                RequestedAmount = amount,
                WaitedForApproval = waitForApprove,
                TxHash = result.TxId
            };
        }
        else
        {
            var result = Funding.WithdrawCurrency(currency, amount, address, network);

            return new WithdrawalRecord
            {
                TxId = result.Single().WdId,
                RequestedAmount = amount,
                WaitedForApproval = waitForApprove
            };
        }
    }

    public async Task<WithdrawalRecord> WithdrawAsync(string currency, decimal amount, string address, string network, bool waitForApprove = true)
    {
        if (waitForApprove)
        {
            var result = await Funding.WithdrawAndWaitForSentAsync(currency, amount, address, network);

            return new WithdrawalRecord
            {
                TxId = result.WdId,
                RequestedAmount = amount,
                WaitedForApproval = waitForApprove,
                TxHash = result.TxId
            };
        }
        else
        {
            var result = await Funding.WithdrawCurrencyAsync(currency, amount, address, network);

            return new WithdrawalRecord
            {
                TxId = result.Single().WdId,
                RequestedAmount = amount,
                WaitedForApproval = waitForApprove
            };
        }
    }

    public sealed override decimal GetWithdrawalFee(string currency, string network)
    {
        var data = Funding.GetSingleCurrency(currency, network);

        return data.MinFee;
    }

    public sealed override async Task<decimal> GetWithdrawalFeeAsync(string currency, string network)
    {
        var data = await Funding.GetSingleCurrencyAsync(currency, network);

        return data.MinFee;
    }

    public sealed override int QueryWithdrawalPrecision(string currency, string network)
    {
        var data = Funding.GetSingleCurrency(currency, network);

        return data.WdTickSz;
    }

    public sealed override async Task<int> QueryWithdrawalPrecisionAsync(string currency, string network)
    {
        var data = await Funding.GetSingleCurrencyAsync(currency, network);

        return data.WdTickSz;
    }

    public sealed override decimal QueryWithdrawalMinAmount(string currency, string network)
    {
        var data = Funding.GetSingleCurrency(currency, network);

        return data.MinWd;
    }

    public sealed override async Task<decimal> QueryWithdrawalMinAmountAsync(string currency, string network)
    {
        var data = await Funding.GetSingleCurrencyAsync(currency, network);

        return data.MinWd;
    }

    public sealed override string GetDepositAddress(string currency, string network)
    {
        var address = Funding.GetDepositAddress(currency, network);

        return address.Addr;
    }

    public sealed override async Task<string> GetDepositAddressAsync(string currency, string network)
    {
        var address = await Funding.GetDepositAddressAsync(currency, network);

        return address.Addr;
    }

    public sealed override decimal GetBalance(string currency)
    {
        var data = Funding.GetFundingBalance(new List<string> { currency });

        if (!data.Any())
            return 0;

        return data.Single().AvailBal;
    }

    public sealed override async Task<decimal> GetBalanceAsync(string currency)
    {
        var data = await Funding.GetFundingBalanceAsync(new List<string> { currency });

        if (!data.Any())
            return 0;

        return data.Single().AvailBal;
    }

    public sealed override decimal ApproveReceiving(string hash)
    {
        return Funding.WaitForReceive(hash);
    }

    public sealed override async Task<decimal> ApproveReceivingAsync(string hash)
    {
        return await Funding.WaitForReceiveAsync(hash);
    }
}

