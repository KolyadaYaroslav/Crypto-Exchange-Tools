using System;
using System.Diagnostics.Metrics;
using CryptoExchangeTools.Models.Okx;
using CryptoExchangeTools.Requests.GateIoRequests;
using CryptoExchangeTools.Requests.KucoinRequests;
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
    public WithdrawalResult[] WithdrawCurrency(
        string currency,
        decimal amount,
        string address,
        string chain,
        int withdrawalMethod = 4,
        decimal fee = -1,
        string? areaCode = null,
        string? clientId = null)
    {
        chain = OkxClient.FormatNetworkName(currency, chain);

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

        return Client.ExecuteRequest<WithdrawalResult[]>(request);
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
    public async Task<WithdrawalResult[]> WithdrawCurrencyAsync(
        string currency,
        decimal amount,
        string address,
        string chain,
        int withdrawalMethod = 4,
        decimal fee = -1,
        string? areaCode = null,
        string? clientId = null)
    {
        chain = OkxClient.FormatNetworkName(currency, chain);

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

        return await Client.ExecuteRequestAsync<WithdrawalResult[]>(request);
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

    #region Get withdrawal history

    /// <summary>
    /// Retrieve the withdrawal records according to the currency, withdrawal status, and time range in reverse chronological order. The 100 most recent records are returned by default.
    /// </summary>
    /// <param name="ccy">Currency, e.g. BTC</param>
    /// <param name="wdId">Withdrawal ID</param>
    /// <param name="clientId">Client-supplied ID. A combination of case-sensitive alphanumerics, all numbers, or all letters of up to 32 characters.</param>
    /// <param name="txId">Hash record of the deposit</param>
    /// <param name="type">Withdrawal type</param>
    /// <param name="state">Status of withdrawal</param>
    /// <param name="after">Pagination of data to return records earlier than the requested ts, Unix timestamp format in milliseconds, e.g. 1654041600000</param>
    /// <param name="before">Pagination of data to return records newer than the requested ts, Unix timestamp format in milliseconds, e.g. 1656633600000</param>
    /// <param name="limit">Number of results per request. The maximum is 100; The default is 100</param>
    /// <returns></returns>
    public WithdrawalHistory[] GetWithdrawalHistory(
        string? ccy = null,
        string? wdId = null,
        string? clientId = null,
        string? txId = null,
        WithdrawalType? type = null,
        WithdrawalStatus? state = null,
        long after = -1,
        long before = -1,
        int limit = -1)
    {
        var request = BuildGetWithdrawalHistory(ccy, wdId, clientId, txId, type, state, after, before, limit);

        return Client.ExecuteRequest<WithdrawalHistory[]>(request);
    }

    /// <summary>
    /// Retrieve the withdrawal records according to the currency, withdrawal status, and time range in reverse chronological order. The 100 most recent records are returned by default.
    /// </summary>
    /// <param name="ccy">Currency, e.g. BTC</param>
    /// <param name="wdId">Withdrawal ID</param>
    /// <param name="clientId">Client-supplied ID. A combination of case-sensitive alphanumerics, all numbers, or all letters of up to 32 characters.</param>
    /// <param name="txId">Hash record of the deposit</param>
    /// <param name="type">Withdrawal type</param>
    /// <param name="state">Status of withdrawal</param>
    /// <param name="after">Pagination of data to return records earlier than the requested ts, Unix timestamp format in milliseconds, e.g. 1654041600000</param>
    /// <param name="before">Pagination of data to return records newer than the requested ts, Unix timestamp format in milliseconds, e.g. 1656633600000</param>
    /// <param name="limit">Number of results per request. The maximum is 100; The default is 100</param>
    /// <returns></returns>
    public async Task<WithdrawalHistory[]> GetWithdrawalHistoryAsync(
        string? ccy = null,
        string? wdId = null,
        string? clientId = null,
        string? txId = null,
        WithdrawalType? type = null,
        WithdrawalStatus? state = null,
        long after = -1,
        long before = -1,
        int limit = -1)
    {
        var request = BuildGetWithdrawalHistory(ccy, wdId, clientId, txId, type, state, after, before, limit);

        return await Client.ExecuteRequestAsync<WithdrawalHistory[]>(request);
    }

    private static RestRequest BuildGetWithdrawalHistory(
        string? ccy = null,
        string? wdId = null,
        string? clientId = null,
        string? txId = null,
        WithdrawalType? type = null,
        WithdrawalStatus? state = null,
        long after = -1,
        long before = -1,
        int limit = -1)
    {
        var request = new RestRequest("api/v5/asset/withdrawal-history", Method.Get);

        if (!string.IsNullOrEmpty(ccy))
            request.AddParameter("ccy", ccy);

        if (!string.IsNullOrEmpty(wdId))
            request.AddParameter("wdId", wdId);

        if (!string.IsNullOrEmpty(clientId))
            request.AddParameter("clientId", clientId);

        if (!string.IsNullOrEmpty(txId))
            request.AddParameter("txId", txId);

        if (type is not null)
            request.AddParameter("type", ((int)type).ToString());

        if (state is not null)
            request.AddParameter("state", ((int)state).ToString());

        if (after != -1)
            request.AddParameter("after", after.ToString());

        if (before != -1)
            request.AddParameter("before", before.ToString());

        if (limit != -1)
            request.AddParameter("limit", limit.ToString());

        return request;
    }

    #endregion Get withdrawal history

    #region Get Deposit Address

    /// <summary>
    /// Retrieve the deposit addresses of currencies, including previously-used addresses.
    /// </summary>
    /// <param name="ccy">Currency, e.g. BTC.</param>
    /// <returns></returns>
    public DepositAddress[] GetDepositAddresses(string ccy)
    {
        var request = BuildGetDepositAddresses(ccy);

        return Client.ExecuteRequest<DepositAddress[]>(request);
    }

    /// <summary>
    /// Retrieve the deposit addresses of currencies, including previously-used addresses.
    /// </summary>
    /// <param name="ccy">Currency, e.g. BTC.</param>
    /// <returns></returns>
    public async Task<DepositAddress[]> GetDepositAddressesAsync(string ccy)
    {
        var request = BuildGetDepositAddresses(ccy);

        return await Client.ExecuteRequestAsync<DepositAddress[]>(request);
    }

    private static RestRequest BuildGetDepositAddresses(string ccy)
    {
        var request = new RestRequest("api/v5/asset/deposit-address");

        request.AddParameter("ccy", ccy.ToUpper());

        return request;
    }

    #endregion Get Deposit Address

    #region Funds transfer

    /// <summary>
    /// Only API keys with Trade privilege can call this endpoint. This endpoint supports the transfer of funds between your funding account and trading account, and from the master account to sub-accounts.
    /// </summary>
    /// <param name="ccy">Currency, e.g. USDT</param>
    /// <param name="amt">Amount to be transferred</param>
    /// <param name="from">The remitting account</param>
    /// <param name="to">The beneficiary account</param>
    /// <param name="subAcct">Name of the sub-account</param>
    /// <param name="type">Transfer type</param>
    /// <param name="loanTrans">Whether or not borrowed coins can be transferred out under Multi-currency margin and Portfolio margin. Default is false.</param>
    /// <param name="clientId">Client-supplied ID. A combination of case-sensitive alphanumerics, all numbers, or all letters of up to 32 characters.</param>
    /// <param name="omitPosRisk">Ignore position risk. Default is false. Applicable to Portfolio margin</param>
    /// <returns></returns>
    public FundsTransferResult[] FundsTransfer
        (string ccy,
        decimal amt,
        AccountType from,
        AccountType to,
        string? subAcct = null,
        FundsTransferType? type = null,
        bool? loanTrans = null,
        string? clientId = null,
        bool? omitPosRisk = null)
    {
        var request = BuildFundsTransfer(ccy, amt, from, to, subAcct, type, loanTrans, clientId, omitPosRisk);

        return Client.ExecuteRequest<FundsTransferResult[]>(request);
    }

    /// <summary>
    /// Only API keys with Trade privilege can call this endpoint. This endpoint supports the transfer of funds between your funding account and trading account, and from the master account to sub-accounts.
    /// </summary>
    /// <param name="ccy">Currency, e.g. USDT</param>
    /// <param name="amt">Amount to be transferred</param>
    /// <param name="from">The remitting account</param>
    /// <param name="to">The beneficiary account</param>
    /// <param name="subAcct">Name of the sub-account</param>
    /// <param name="type">Transfer type</param>
    /// <param name="loanTrans">Whether or not borrowed coins can be transferred out under Multi-currency margin and Portfolio margin. Default is false.</param>
    /// <param name="clientId">Client-supplied ID. A combination of case-sensitive alphanumerics, all numbers, or all letters of up to 32 characters.</param>
    /// <param name="omitPosRisk">Ignore position risk. Default is false. Applicable to Portfolio margin</param>
    /// <returns></returns>
    public async Task<FundsTransferResult[]> FundsTransferAsync
        (string ccy,
        decimal amt,
        AccountType from,
        AccountType to,
        string? subAcct = null,
        FundsTransferType? type = null,
        bool? loanTrans = null,
        string? clientId = null,
        bool? omitPosRisk = null)
    {
        var request = BuildFundsTransfer(ccy, amt, from, to, subAcct, type, loanTrans, clientId, omitPosRisk);

        return await Client.ExecuteRequestAsync<FundsTransferResult[]>(request);
    }

    private static RestRequest BuildFundsTransfer(
        string ccy,
        decimal amt,
        AccountType from,
        AccountType to,
        string? subAcct,
        FundsTransferType? type,
        bool? loanTrans,
        string? clientId,
        bool? omitPosRisk)
    {
        var request = new RestRequest("api/v5/asset/transfer", Method.Post);

        var body = new FundsTransferRequest(ccy, amt, from, to, subAcct, type, loanTrans, clientId, omitPosRisk);

        request.AddBody(body.Serialize());

        return request;
    }

    #endregion Funds transfer

    #region Get deposit history

    /// <summary>
    /// Fetch deposit history.
    /// </summary>
    /// <param name="ccy">Currency, e.g. BTC</param>
    /// <param name="depId">Deposit ID</param>
    /// <param name="fromWdId">Internal transfer initiator's withdrawal ID. If the deposit comes from internal transfer, this field displays the withdrawal ID of the internal transfer initiator</param>
    /// <param name="txId">Hash record of the deposit</param>
    /// <param name="type">Deposit Type</param>
    /// <param name="state">Status of deposit</param>
    /// <param name="after">Pagination of data to return records earlier than the requested ts, Unix timestamp format in milliseconds, e.g. 1654041600000/param>
    /// <param name="before">Pagination of data to return records newer than the requested ts, Unix timestamp format in milliseconds, e.g. 1656633600000</param>
    /// <param name="limit"></param>
    /// <returns>Number of results per request. The maximum is 100; The default is 100</returns>
    public DepositHistory[] GetDepositHistory(
        string? ccy = null,
        string? depId = null,
        string? fromWdId = null,
        string? txId = null,
        DepositType? type = null,
        DepositStatus? state = null,
        long after = -1,
        long before = -1,
        int limit = -1)
    {
        var request = BuildGetDepositHistory(ccy, depId, fromWdId, txId, type, state, after, before, limit);

        return Client.ExecuteRequest<DepositHistory[]>(request);
    }

    /// <summary>
    /// Fetch deposit history.
    /// </summary>
    /// <param name="ccy">Currency, e.g. BTC</param>
    /// <param name="depId">Deposit ID</param>
    /// <param name="fromWdId">Internal transfer initiator's withdrawal ID. If the deposit comes from internal transfer, this field displays the withdrawal ID of the internal transfer initiator</param>
    /// <param name="txId">Hash record of the deposit</param>
    /// <param name="type">Deposit Type</param>
    /// <param name="state">Status of deposit</param>
    /// <param name="after">Pagination of data to return records earlier than the requested ts, Unix timestamp format in milliseconds, e.g. 1654041600000/param>
    /// <param name="before">Pagination of data to return records newer than the requested ts, Unix timestamp format in milliseconds, e.g. 1656633600000</param>
    /// <param name="limit"></param>
    /// <returns>Number of results per request. The maximum is 100; The default is 100</returns>
    public async Task<DepositHistory[]> GetDepositHistoryAsync(
        string? ccy = null,
        string? depId = null,
        string? fromWdId = null,
        string? txId = null,
        DepositType? type = null,
        DepositStatus? state = null,
        long after = -1,
        long before = -1,
        int limit = -1)
    {
        var request = BuildGetDepositHistory(ccy, depId, fromWdId, txId, type, state, after, before, limit);

        return await Client.ExecuteRequestAsync<DepositHistory[]>(request);
    }

    private static RestRequest BuildGetDepositHistory(
        string? ccy = null,
        string? depId = null,
        string? fromWdId = null,
        string? txId = null,
        DepositType? type = null,
        DepositStatus? state = null,
        long after = -1,
        long before = -1,
        int limit = -1)
    {
        var request = new RestRequest("api/v5/asset/deposit-history", Method.Get);

        if (!string.IsNullOrEmpty(ccy))
            request.AddParameter("ccy", ccy);

        if (!string.IsNullOrEmpty(depId))
            request.AddParameter("depId", depId);

        if (!string.IsNullOrEmpty(fromWdId))
            request.AddParameter("fromWdId", fromWdId);

        if (!string.IsNullOrEmpty(txId))
            request.AddParameter("txId", txId);

        if (type is not null)
            request.AddParameter("type", ((int)type).ToString());

        if (state is not null)
            request.AddParameter("state", ((int)state).ToString());

        if (after != -1)
            request.AddParameter("after", after.ToString());

        if (before != -1)
            request.AddParameter("before", before.ToString());

        if (limit != -1)
            request.AddParameter("limit", limit.ToString());

        return request;
    }

    #endregion Get deposit history

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
        network = OkxClient.FormatNetworkName(currency, network);

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
        network = OkxClient.FormatNetworkName(currency, network);

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

    #region Get Deposit Address

    /// <summary>
    /// Retrieve the deposit addresses foe currency on a single network, including previously-used addresses.
    /// </summary>
    /// <param name="ccy">Currency</param>
    /// <param name="network">Network</param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public DepositAddress GetDepositAddress(string ccy, string network)
    {
        network = OkxClient.FormatNetworkName(ccy, network);

        var addresses = GetDepositAddresses(ccy);

        var matching = addresses.Where(x => x.Chain.ToUpper() == network.ToUpper());

        if (!matching.Any())
            throw new Exception($"There is no deposit address for {ccy} on {network}. Check if Currency and network are spelled correctly.");

        return matching.First();
    }

    /// <summary>
    /// Retrieve the deposit addresses foe currency on a single network, including previously-used addresses.
    /// </summary>
    /// <param name="ccy">Currency</param>
    /// <param name="network">Network</param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public async Task<DepositAddress> GetDepositAddressAsync(string ccy, string network)
    {
        network = OkxClient.FormatNetworkName(ccy, network);

        var addresses = await GetDepositAddressesAsync(ccy);

        var matching = addresses.Where(x => x.Chain.ToUpper() == network.ToUpper());

        if (!matching.Any())
            throw new Exception($"There is no deposit address for {ccy} on {network}. Check if Currency and network are spelled correctly.");

        return matching.First();
    }

    #endregion Get Deposit Address

    #region Withdraw And Wait For Sent

    public WithdrawalHistory WithdrawAndWaitForSent(string currency,
        decimal amount,
        string address,
        string chain,
        int withdrawalMethod = 4,
        decimal fee = -1,
        string? areaCode = null,
        string? clientId = null)
    {
        var result = WithdrawCurrency(currency, amount, address, chain, withdrawalMethod, fee, areaCode, clientId);

        for (int i = 0; i < 500; i++)
        {
            Task.Delay(10_000).Wait();

            var history = GetWithdrawalHistory(wdId: result.Single().WdId);

            if (history.Any())
            {
                if (CheckHistory(history.Single()))
                    return history.Single();
            }
        }

        throw new Exception("Timeout for awaiting transaction completion was hit");
    }

    public async Task<WithdrawalHistory> WithdrawAndWaitForSentAsync(string currency,
        decimal amount,
        string address,
        string chain,
        int withdrawalMethod = 4,
        decimal fee = -1,
        string? areaCode = null,
        string? clientId = null)
    {
        var result = await WithdrawCurrencyAsync(currency, amount, address, chain, withdrawalMethod, fee, areaCode, clientId);

        for (int i = 0; i < 500; i++)
        {
            Task.Delay(10_000).Wait();

            var history = await GetWithdrawalHistoryAsync(wdId: result.Single().WdId);

            if (history.Any())
            {
                if (CheckHistory(history.Single()))
                    return history.Single();
            }
        }

        throw new Exception("Timeout for awaiting transaction completion was hit");
    }

    private bool CheckHistory(WithdrawalHistory txData)
    {
        Client.Message(txData.State.ToString());

        if (txData.State == WithdrawalStatus.canceled
            || txData.State == WithdrawalStatus.canceling
            || txData.State == WithdrawalStatus.failed
            || txData.State == WithdrawalStatus.waitingMannualReview1
            || txData.State == WithdrawalStatus.waitingMannualReview2
            || txData.State == WithdrawalStatus.waitingMannualReview3
            || txData.State == WithdrawalStatus.waitingMannualReview4
            || txData.State == WithdrawalStatus.waitingMannualReview5
            || txData.State == WithdrawalStatus.waitingMannualReview6)
            throw new WithdrawalFailedException(txData.WdId.ToString(), txData.State.ToString(), null);

        if (txData.State == WithdrawalStatus.withdrawSuccess)
            return true;

        return false;
    }

    #endregion Withdraw And Wait For Sent

    #region Wait For Receive

    public decimal WaitForReceive(string hash)
    {
        for (int i = 0; i < 1000; i++)
        {
            var history = GetDepositHistory(txId: hash);

            if (history.Any())
                break;

            Task.Delay(5000).Wait();
        }

        return CheckDepositstatus(hash).Result;
    }

    public async Task<decimal> WaitForReceiveAsync(string hash)
    {
        for (int i = 0; i < 1000; i++)
        {
            var history = await GetDepositHistoryAsync(txId: hash);

            if (history.Any())
                break;

            await Task.Delay(5000);
        }

        return await CheckDepositstatus(hash);
    }

    private async Task<decimal> CheckDepositstatus(string hash)
    {
        for (int i = 0; i < 1000; i++)
        {
            var result = await GetDepositHistoryAsync(txId: hash);

            var tx = result.Single();

            Client.Message(tx.State.ToString());

            if (tx.State == DepositStatus.depositCredited
                || tx.State == DepositStatus.depositSuccessful)
                return tx.Amt;

            if (tx.State == DepositStatus.pendingDueToTemporaryDepositSuspensionOnThisCryptocurrency)
            {
                Client.Message("Waiting 15 minutes.");
                await Task.Delay(15 * 60 * 1000);
            }

            if(tx.State == DepositStatus.matchTheAddressBlacklist
                || tx.State == DepositStatus.accountOrDepositIsFrozen
                || tx.State == DepositStatus.subAccountDepositInterception
                || tx.State == DepositStatus.KYClimit)
                throw new Exception($"tx status - {tx.State.ToString()}");

            await Task.Delay(5_000);
        }

        throw new Exception($"Didn't receive positive deposit status after 1000 attempts.");
    }

    #endregion Wait For Receive

    #endregion Derived Methods
}

