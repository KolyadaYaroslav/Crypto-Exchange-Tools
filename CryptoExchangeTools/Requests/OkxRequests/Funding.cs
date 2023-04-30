using CryptoExchangeTools.Models.Okx;
using Newtonsoft.Json;
using RestSharp;

namespace CryptoExchangeTools.Requests.OkxRequests;

public class Funding
{
    private OkxClient Client;

    public Funding(OkxClient client)
    {
        Client = client;
    }

    #region Original Methods

    #region GetFundingBalance

    /// <summary>
    /// Retrieve the funding account balances of all the assets and the amount that is available or on hold.
    /// </summary>
    /// <param name="currencies">Single currency or multiple currencies (no more than 20).</param>
    /// <returns>Array of Coin Balance information.</returns>
    public CoinBalance[] GetFundingBalance(List<string>? currencies = null)
    {
        var request = BuildGetFundingBalance(currencies);

        return Client.ExecuteRequest<CoinBalance[]>(request);
    }

    /// <summary>
    /// Retrieve the funding account balances of all the assets and the amount that is available or on hold.
    /// </summary>
    /// <param name="currencies">Single currency or multiple currencies (no more than 20).</param>
    /// <returns>Array of Coin Balance information.</returns>
    public async Task<CoinBalance[]> GetFundingBalanceAsync(List<string>? currencies = null)
    {
        var request = BuildGetFundingBalance(currencies);

        return await Client.ExecuteRequestAsync<CoinBalance[]>(request);
    }

    private static RestRequest BuildGetFundingBalance(List<string>? currencies)
    {
        var request = new RestRequest("api/v5/asset/balances", Method.Get);

        if (currencies is not null && currencies.Count <= 20)
            request.AddParameter("ccy", string.Join(',', currencies.Select(x => x.ToUpper())));

        return request;
    }

    #endregion GetFundingBalance

    #region GetCurrencies

    /// <summary>
    /// Retrieve a list of all currencies.
    /// </summary>
    /// <param name="currencies">Single currency or multiple currencies (no more than 20).</param>
    /// <returns>Array of Currency information.</returns>
    public CurrencyInformation[] GetCurrencies(List<string>? currencies = null)
    {
        var request = BuildGetCurrencies(currencies);

        return Client.ExecuteRequest<CurrencyInformation[]>(request);
    }

    /// <summary>
    /// Retrieve a list of all currencies.
    /// </summary>
    /// <param name="currencies">Single currency or multiple currencies (no more than 20).</param>
    /// <returns>Array of Currency information.</returns>
    public async Task<CurrencyInformation[]> GetCurrenciesAsync(List<string>? currencies = null)
    {
        var request = BuildGetCurrencies(currencies);

        return await Client.ExecuteRequestAsync<CurrencyInformation[]>(request);
    }

    private static RestRequest BuildGetCurrencies(List<string>? currencies)
    {
        var request = new RestRequest("api/v5/asset/currencies", Method.Get);

        if (currencies is not null && currencies.Count <= 20)
            request.AddParameter("ccy", string.Join(',', currencies.Select(x => x.ToUpper())));

        return request;
    }

    #endregion GetCurrencies

    #region WithdrawCurrency

    /// <summary>
    /// Withdrawal of tokens. Common sub-account does not support withdrawal.
    /// </summary>
    /// <param name="currency">Currency, e.g. USDT</param>
    /// <param name="amount">Withdrawal amount</param>
    /// <param name="address">If your dest is 4,toAddr should be a trusted crypto currency address. Some crypto currency addresses are formatted as 'address:tag', e.g. 'ARDOR-7JF3-8F2E-QUWZ-CAN7F:123456'. If your dest is 3,toAddr should be a recipient address which can be email, phone or login account name.</param>
    /// <param name="chain">Chain name. There are multiple chains under some currencies, such as USDT has USDT-ERC20, USDT-TRC20, and USDT-Omni.. If the parameter is not filled in, the default will be the main chain.. When you withdrawal the non-tradable asset, if the parameter is not filled in, the default will be the unique withdrawal chain.</param>
    /// <param name="withdrawalMethod">Withdrawal method. 3: internal . 4: on chain</param>
    /// <param name="fee">Transaction fee. By default is set to minimum for this currency and network.</param>
    /// <param name="areaCode">Area code for the phone number. If toAddr is a phone number, this parameter is required.</param>
    /// <param name="clientId">Client-supplied ID. A combination of case-sensitive alphanumerics, all numbers, or all letters of up to 32 characters.</param>
    /// <returns>Withdrawal Result</returns>
    public WithdrawalResult WithdrawCurrency(
        string currency,
        decimal amount,
        string address,
        string chain,
        int withdrawalMethod = 4,
        decimal fee = -1,
        string? areaCode = null,
        string? clientId = null)
    {
        if (fee == -1)
        {
            var data = Client.Funding.GetSingleCurrency(currency, chain);
            fee = data.MinFee;
        }

        var request = BuildWithdrawCurrency(
            currency,
            amount,
            address,
            chain,
            withdrawalMethod,
            fee,
            areaCode,
            clientId);

        return Client.ExecuteRequest<WithdrawalResult>(request);
    }


    /// <summary>
    /// Withdrawal of tokens. Common sub-account does not support withdrawal.
    /// </summary>
    /// <param name="currency">Currency, e.g. USDT</param>
    /// <param name="amount">Withdrawal amount</param>
    /// <param name="address">If your dest is 4,toAddr should be a trusted crypto currency address. Some crypto currency addresses are formatted as 'address:tag', e.g. 'ARDOR-7JF3-8F2E-QUWZ-CAN7F:123456'. If your dest is 3,toAddr should be a recipient address which can be email, phone or login account name.</param>
    /// <param name="chain">Chain name. There are multiple chains under some currencies, such as USDT has USDT-ERC20, USDT-TRC20, and USDT-Omni.. If the parameter is not filled in, the default will be the main chain.. When you withdrawal the non-tradable asset, if the parameter is not filled in, the default will be the unique withdrawal chain.</param>
    /// <param name="withdrawalMethod">Withdrawal method. 3: internal . 4: on chain</param>
    /// <param name="fee">Transaction fee. By default is set to minimum for this currency and network.</param>
    /// <param name="areaCode">Area code for the phone number. If toAddr is a phone number, this parameter is required.</param>
    /// <param name="clientId">Client-supplied ID. A combination of case-sensitive alphanumerics, all numbers, or all letters of up to 32 characters.</param>
    /// <returns>Withdrawal Result</returns>
    public async Task<WithdrawalResult> WithdrawCurrencyAsync(
        string currency,
        decimal amount,
        string address,
        string chain,
        int withdrawalMethod = 4,
        decimal fee = -1,
        string? areaCode = null,
        string? clientId = null)
    {
        if (fee == -1)
        {
            var data = await Client.Funding.GetSingleCurrencyAsync(currency, chain);
            fee = data.MinFee;
        }

        var request = BuildWithdrawCurrency(
            currency,
            amount,
            address,
            chain,
            withdrawalMethod,
            fee,
            areaCode,
            clientId);

        return await Client.ExecuteRequestAsync<WithdrawalResult>(request);
    }

    private static RestRequest BuildWithdrawCurrency(
        string currency,
        decimal amount,
        string address,
        string chain,
        int withdrawalMethod,
        decimal fee,
        string? areaCode,
        string? clientId)
    {
        var request = new RestRequest("api/v5/asset/withdrawal", Method.Post);

        var body = new WithdrawalRequest(
            currency,
            amount,
            address,
            chain,
            withdrawalMethod,
            fee,
            clientId,
            areaCode);

        request.AddBody(JsonConvert.SerializeObject(body));

        return request;
    }

    #endregion WithdrawCurrency

    #endregion Original Methods

    #region Derived Methods

    #region GetSingleCurrency

    /// <summary>
    /// Retrieve a list of all currencies.
    /// </summary>
    /// <param name="currency">Single currency.</param>
    /// <param name="network">Chain name, e.g. USDT-ERC20, USDT-TRC20, USDT-Omni.</param>
    /// <returns>Currency Information of a single currency on a particular network.</returns>
    public CurrencyInformation GetSingleCurrency(string currency, string network)
    {
        var data = Client.Funding.GetCurrencies(new List<string> { currency });

        return FindNeededCurrencyInfo(data, currency, network);
    }

    /// <summary>
    /// Retrieve a list of all currencies.
    /// </summary>
    /// <param name="currency">Single currency.</param>
    /// <param name="network">Chain name, e.g. USDT-ERC20, USDT-TRC20, USDT-Omni.</param>
    /// <returns>Currency Information of a single currency on a particular network.</returns>
    public async Task<CurrencyInformation> GetSingleCurrencyAsync(string currency, string network)
    {
        var data = await Client.Funding.GetCurrenciesAsync(new List<string> { currency });

        return FindNeededCurrencyInfo(data, currency, network);
    }

    private static CurrencyInformation FindNeededCurrencyInfo(CurrencyInformation[] data, string currency, string network)
    {
        var neededCurrencyInfo = data.Where(x => x.Chain.ToUpper() == network.ToUpper());

        if (!neededCurrencyInfo.Any())
            throw new Exception("Currency info not found. Check currency and network spelling.");

        return neededCurrencyInfo.Single();
    }

    #endregion GetSingleCurrency

    #endregion Derived Methods
}

