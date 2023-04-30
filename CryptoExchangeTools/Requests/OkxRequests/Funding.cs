using System;
using System.Reflection;
using System.Security.Claims;
using System.Xml.Linq;
using CryptoExchangeTools.Models.Okx;
using CryptoExchangeTools.OkxRequests.Account;
using Newtonsoft.Json;
using RestSharp;

namespace CryptoExchangeTools.OkxRequests.Funding;

public static class Funding
{
    #region Original Methods

    /// <summary>
    /// Retrieve the funding account balances of all the assets and the amount that is available or on hold.
    /// </summary>
    /// <param name="client"></param>
    /// <param name="currencies">Single currency or multiple currencies (no more than 20).</param>
    /// <returns>Array of Coin Balance information.</returns>
    public static CoinBalance[] GetFundingBalance(this OkxClient client, List<string>? currencies = null)
    {
        var request = new RestRequest("api/v5/asset/balances", Method.Get);

        if (currencies is not null && currencies.Count <= 20)
            request.AddParameter("ccy", string.Join(',', currencies.Select(x => x.ToUpper())));

        return client.ExecuteRequest<CoinBalance[]>(request);
    }

    /// <summary>
    /// Retrieve the funding account balances of all the assets and the amount that is available or on hold.
    /// </summary>
    /// <param name="client"></param>
    /// <param name="currencies">Single currency or multiple currencies (no more than 20).</param>
    /// <returns>Array of Coin Balance information.</returns>
    public static async Task<CoinBalance[]> GetFundingBalanceAsync(this OkxClient client, List<string>? currencies = null)
    {
        var request = new RestRequest("api/v5/asset/balances", Method.Get);

        if (currencies is not null && currencies.Count <= 20)
            request.AddParameter("ccy", string.Join(',', currencies.Select(x => x.ToUpper())));

        return await client.ExecuteRequestAsync<CoinBalance[]>(request);
    }

    /// <summary>
    /// Retrieve a list of all currencies.
    /// </summary>
    /// <param name="client"></param>
    /// <param name="currencies">Single currency or multiple currencies (no more than 20).</param>
    /// <returns>Array of Currency information.</returns>
    public static CurrencyInformation[] GetCurrencies(this OkxClient client, List<string>? currencies = null)
    {
        var request = new RestRequest("api/v5/asset/currencies", Method.Get);

        if (currencies is not null && currencies.Count <= 20)
            request.AddParameter("ccy", string.Join(',', currencies.Select(x => x.ToUpper())));

        return client.ExecuteRequest<CurrencyInformation[]>(request);
    }

    /// <summary>
    /// Retrieve a list of all currencies.
    /// </summary>
    /// <param name="client"></param>
    /// <param name="currencies">Single currency or multiple currencies (no more than 20).</param>
    /// <returns>Array of Currency information.</returns>
    public static async Task<CurrencyInformation[]> GetCurrenciesAsync(this OkxClient client, List<string>? currencies = null)
    {
        var request = new RestRequest("api/v5/asset/currencies", Method.Get);

        if (currencies is not null && currencies.Count <= 20)
            request.AddParameter("ccy", string.Join(',', currencies.Select(x => x.ToUpper())));

        return await client.ExecuteRequestAsync<CurrencyInformation[]>(request);
    }


    /// <summary>
    /// Withdrawal of tokens. Common sub-account does not support withdrawal.
    /// </summary>
    /// <param name="client"></param>
    /// <param name="currency">Currency, e.g. USDT</param>
    /// <param name="amount">Withdrawal amount</param>
    /// <param name="address">If your dest is 4,toAddr should be a trusted crypto currency address. Some crypto currency addresses are formatted as 'address:tag', e.g. 'ARDOR-7JF3-8F2E-QUWZ-CAN7F:123456'. If your dest is 3,toAddr should be a recipient address which can be email, phone or login account name.</param>
    /// <param name="chain">Chain name. There are multiple chains under some currencies, such as USDT has USDT-ERC20, USDT-TRC20, and USDT-Omni.. If the parameter is not filled in, the default will be the main chain.. When you withdrawal the non-tradable asset, if the parameter is not filled in, the default will be the unique withdrawal chain.</param>
    /// <param name="withdrawalMethod">Withdrawal method. 3: internal . 4: on chain</param>
    /// <param name="fee">Transaction fee. By default is set to minimum for this currency and network.</param>
    /// <param name="areaCode">Area code for the phone number. If toAddr is a phone number, this parameter is required.</param>
    /// <param name="clientId">Client-supplied ID. A combination of case-sensitive alphanumerics, all numbers, or all letters of up to 32 characters.</param>
    /// <returns>Withdrawal Result</returns>
    public static WithdrawalResult Withdraw(
        this OkxClient client,
        string currency,
        decimal amount,
        string address,
        string chain,
        int withdrawalMethod = 4,
        decimal fee = -1,
        string? areaCode = null,
        string? clientId = null)
    {
        var request = new RestRequest("api/v5/asset/withdrawal", Method.Post);

        if (fee == -1)
        {
            var data = client.GetSingleCurrency(currency, chain);
            fee = data.MinFee;
        }

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

        return client.ExecuteRequest<WithdrawalResult>(request);
    }


    /// <summary>
    /// Withdrawal of tokens. Common sub-account does not support withdrawal.
    /// </summary>
    /// <param name="client"></param>
    /// <param name="currency">Currency, e.g. USDT</param>
    /// <param name="amount">Withdrawal amount</param>
    /// <param name="address">If your dest is 4,toAddr should be a trusted crypto currency address. Some crypto currency addresses are formatted as 'address:tag', e.g. 'ARDOR-7JF3-8F2E-QUWZ-CAN7F:123456'. If your dest is 3,toAddr should be a recipient address which can be email, phone or login account name.</param>
    /// <param name="chain">Chain name. There are multiple chains under some currencies, such as USDT has USDT-ERC20, USDT-TRC20, and USDT-Omni.. If the parameter is not filled in, the default will be the main chain.. When you withdrawal the non-tradable asset, if the parameter is not filled in, the default will be the unique withdrawal chain.</param>
    /// <param name="withdrawalMethod">Withdrawal method. 3: internal . 4: on chain</param>
    /// <param name="fee">Transaction fee. By default is set to minimum for this currency and network.</param>
    /// <param name="areaCode">Area code for the phone number. If toAddr is a phone number, this parameter is required.</param>
    /// <param name="clientId">Client-supplied ID. A combination of case-sensitive alphanumerics, all numbers, or all letters of up to 32 characters.</param>
    /// <returns>Withdrawal Result</returns>
    public static async Task<WithdrawalResult> WithdrawAsync(
        this OkxClient client,
        string currency,
        decimal amount,
        string address,
        string chain,
        int withdrawalMethod = 4,
        decimal fee = -1,
        string? areaCode = null,
        string? clientId = null)
    {
        var request = new RestRequest("api/v5/asset/withdrawal", Method.Post);

        if (fee == -1)
        {
            var data = await client.GetSingleCurrencyAsync(currency, chain);
            fee = data.MinFee;
        }

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

        return await client.ExecuteRequestAsync<WithdrawalResult>(request);
    }

    #endregion Original Methods

    #region Derived Methods

    /// <summary>
    /// Retrieve a list of all currencies.
    /// </summary>
    /// <param name="client"></param>
    /// <param name="currency">Single currency.</param>
    /// <param name="network">Chain name, e.g. USDT-ERC20, USDT-TRC20, USDT-Omni.</param>
    /// <returns>Currency Information of a single currency on a particular network.</returns>
    public static CurrencyInformation GetSingleCurrency(this OkxClient client, string currency, string network)
    {
        var data = client.GetCurrencies(new List<string> { currency });

        var neededCurrencyInfo = data.Where(x => x.Chain.ToUpper() == network.ToUpper());

        if (!neededCurrencyInfo.Any())
            throw new Exception("Currency info not found. Check currency and network spelling.");

        return neededCurrencyInfo.Single();
    }

    /// <summary>
    /// Retrieve a list of all currencies.
    /// </summary>
    /// <param name="client"></param>
    /// <param name="currency">Single currency.</param>
    /// <param name="network">Chain name, e.g. USDT-ERC20, USDT-TRC20, USDT-Omni.</param>
    /// <returns>Currency Information of a single currency on a particular network.</returns>
    public static async Task<CurrencyInformation> GetSingleCurrencyAsync(this OkxClient client, string currency, string network)
    {
        var data = await client.GetCurrenciesAsync(new List<string> { currency });

        var neededCurrencyInfo = data.Where(x => x.Chain.ToUpper() == network.ToUpper());

        if (!neededCurrencyInfo.Any())
            throw new Exception("Currency info not found. Check currency and network spelling.");

        return neededCurrencyInfo.Single();
    }

    #endregion Derived Methods
}

