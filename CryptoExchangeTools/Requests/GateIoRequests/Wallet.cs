using CryptoExchangeTools.Models.GateIo;
using CryptoExchangeTools.Requests.GateIoRequests;
using RestSharp;
using static CryptoExchangeTools.Models.GateIo.UserTotalBalances.TotalData;

namespace CryptoExchangeTools.Requests.GateIoRequests;

public class Wallet
{
    private GateIoClient Client;

    public Wallet(GateIoClient client)
    {
        Client = client;
    }

    #region Original Methods

    #region GetChainsSupportedForSpecifiedCurrency

    /// <summary>
    /// List chains supported for specified currency.
    /// </summary>
    /// <param name="currency">Currency name.</param>
    /// <returns>List of chains supported for currency.</returns>
    public ChainsSupportedForSpecifiedCurrency[] GetChainsSupportedForSpecifiedCurrency(string currency)
    {
        var request = BuildGetChainsSupportedForSpecifiedCurrencyAsync(currency);

        return Client.ExecuteRequest<ChainsSupportedForSpecifiedCurrency[]>(request, false);
    }

    /// <summary>
    /// List chains supported for specified currency.
    /// </summary>
    /// <param name="currency">Currency name.</param>
    /// <returns>List of chains supported for currency.</returns>
    public async Task<ChainsSupportedForSpecifiedCurrency[]> GetChainsSupportedForSpecifiedCurrencyAsync(string currency)
    {
        var request = BuildGetChainsSupportedForSpecifiedCurrencyAsync(currency);

        return await Client.ExecuteRequestAsync<ChainsSupportedForSpecifiedCurrency[]>(request, false);
    }

    private static RestRequest BuildGetChainsSupportedForSpecifiedCurrencyAsync(string currency)
    {
        var request = new RestRequest("api/v4/wallet/currency_chains", Method.Get);

        request.AddParameter("currency", currency.ToUpper());

        return request;
    }

    #endregion GetChainsSupportedForSpecifiedCurrency

    #region GenerateCurrencyDepositAddress

    /// <summary>
    /// Generate currency deposit address.
    /// </summary>
    /// <param name="currency">Currency name.</param>
    /// <returns>Info on generated address.</returns>
    public GeneratedCurrencyDepositAddress GenerateCurrencyDepositAddress(string currency)
    {
        var request = BuildGenerateCurrencyDepositAddressAsync(currency);

        return Client.ExecuteRequest<GeneratedCurrencyDepositAddress>(request);
    }

    /// <summary>
    /// Generate currency deposit address.
    /// </summary>
    /// <param name="currency">Currency name.</param>
    /// <returns>Info on generated address.</returns>
    public async Task<GeneratedCurrencyDepositAddress> GenerateCurrencyDepositAddressAsync(string currency)
    {
        var request = BuildGenerateCurrencyDepositAddressAsync(currency);

        return await Client.ExecuteRequestAsync<GeneratedCurrencyDepositAddress>(request);
    }

    private static RestRequest BuildGenerateCurrencyDepositAddressAsync(string currency)
    {
        var request = new RestRequest("api/v4/wallet/deposit_address", Method.Get);

        request.AddParameter("currency", currency.ToUpper());

        return request;
    }

    #endregion GenerateCurrencyDepositAddress

    #region RetrieveWithdrawalRecords

    /// <summary>
    /// Retrieve withdrawal records.
    /// </summary>
    /// <param name="currency">Filter by currency. Return all currency records if not specified.</param>
    /// <param name="from">Time range beginning, default to 7 days before current time.</param>
    /// <param name="to">Time range ending, default to current time.</param>
    /// <param name="limit">Maximum number of records to be returned in a single list.</param>
    /// <param name="offset">List offset, starting from 0.</param>
    /// <returns>Array of Withdrawl History.</returns>
    public WithdrawHistory[] RetrieveWithdrawalRecords(
        string? currency = null,
        string? from = null,
        string? to = null,
        int limit = -1,
        int offset = -1)
    {
        var request = BuildRetrieveWithdrawalRecords(currency, from, to, limit, offset);

        return Client.ExecuteRequest<WithdrawHistory[]>(request);
    }

    /// <summary>
    /// Retrieve withdrawal records.
    /// </summary>
    /// <param name="currency">Filter by currency. Return all currency records if not specified.</param>
    /// <param name="from">Time range beginning, default to 7 days before current time.</param>
    /// <param name="to">Time range ending, default to current time.</param>
    /// <param name="limit">Maximum number of records to be returned in a single list.</param>
    /// <param name="offset">List offset, starting from 0.</param>
    /// <returns>Array of Withdrawl History.</returns>
    public async Task<WithdrawHistory[]> RetrieveWithdrawalRecordsAsync(
        string? currency = null,
        string? from = null,
        string? to = null,
        int limit = -1,
        int offset = -1)
    {
        var request = BuildRetrieveWithdrawalRecords(currency, from, to, limit, offset);

        return await Client.ExecuteRequestAsync<WithdrawHistory[]>(request);
    }

    private static RestRequest BuildRetrieveWithdrawalRecords(
        string? currency,
        string? from,
        string? to,
        int limit,
        int offset)
    {
        var request = new RestRequest("api/v4/wallet/withdrawals", Method.Get);

        if (!string.IsNullOrEmpty(currency))
            request.AddParameter("currency", currency.ToUpper());

        if (!string.IsNullOrEmpty(from))
            request.AddParameter("from", from);

        if (!string.IsNullOrEmpty(to))
            request.AddParameter("to", to);

        if (limit != -1)
            request.AddParameter("limit", limit);

        if (offset != -1)
            request.AddParameter("offset", offset);

        return request;
    }

    #endregion RetrieveWithdrawalRecords

    #region GetUserTotalBalances

    /// <summary>
    /// This endpoint returns an approximate sum of exchanged amount from all currencies to input currency for each account.The exchange rate and account balance could have been cached for at most 1 minute. It is not recommended to use its result for any trading calculation.
    /// </summary>
    /// <param name="currency">Currency unit used to calculate the balance amount. BTC, CNY, USD and USDT are allowed. USDT is the default.</param>
    /// <returns></returns>
    public UserTotalBalances GetUserTotalBalances(string? currency = null)
    {
        var request = BuildGetUserTotalBalances(currency);

        return Client.ExecuteRequest<UserTotalBalances>(request);
    }

    /// <summary>
    /// This endpoint returns an approximate sum of exchanged amount from all currencies to input currency for each account.The exchange rate and account balance could have been cached for at most 1 minute. It is not recommended to use its result for any trading calculation.
    /// </summary>
    /// <param name="currency">Currency unit used to calculate the balance amount. BTC, CNY, USD and USDT are allowed. USDT is the default.</param>
    /// <returns></returns>
    public async Task<UserTotalBalances> GetUserTotalBalancesAsync(string? currency = null)
    {
        var request = BuildGetUserTotalBalances(currency);

        return await Client.ExecuteRequestAsync<UserTotalBalances>(request);
    }

    private static RestRequest BuildGetUserTotalBalances(string? currency)
    {
        var request = new RestRequest("api/v4/wallet/total_balance", Method.Get);

        if (!string.IsNullOrEmpty(currency))
        {
            if (!Enum.TryParse<CurrencyEnum>(currency, true, out CurrencyEnum result))
                throw new ArgumentOutOfRangeException($"currency {currency} is not supported for GetUserTotalBalances method.");

            request.AddParameter("currency", result.ToString());
        }

        return request;
    }

    #endregion GetUserTotalBalances

    #region GetSpotBalances

    /// <summary>
    /// List spot accounts.
    /// </summary>
    /// <param name="currency">Retrieve data of the specified currency.</param>
    /// <returns>List of accounts.</returns>
    public AccountBalance[] GetSpotBalances(string? currency = null)
    {
        var request = BuildGetSpotBalances(currency);

        return Client.ExecuteRequest<AccountBalance[]>(request);
    }

    /// <summary>
    /// List spot accounts.
    /// </summary>
    /// <param name="currency">Retrieve data of the specified currency.</param>
    /// <returns>List of accounts.</returns>
    public async Task<AccountBalance[]> GetSpotBalancesAsync(string? currency = null)
    {
        var request = BuildGetSpotBalances(currency);

        return await Client.ExecuteRequestAsync<AccountBalance[]>(request);
    }

    private static RestRequest BuildGetSpotBalances(string? currency)
    {
        var request = new RestRequest("api/v4/spot/accounts", Method.Get);

        if (!string.IsNullOrEmpty(currency))
            request.AddParameter("currency", currency);

        return request;
    }

    #endregion GetSpotBalances

    #endregion Original Methods
}

