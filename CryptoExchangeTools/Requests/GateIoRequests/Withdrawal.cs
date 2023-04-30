using System;
using CryptoExchangeTools.Models.GateIo;
using Newtonsoft.Json;
using RestSharp;

namespace CryptoExchangeTools.GateIoRequests.Withdrawal;

public static class Withdrawal
{
    #region Original Methods

    /// <summary>
    /// Request a withdrawal operation.
    /// </summary>
    /// <param name="client"></param>
    /// <param name="currency">Currency name.</param>
    /// <param name="amount">Currency amount.</param>
    /// <param name="address">Withdrawal address. Required for withdrawals.</param>
    /// <param name="chain">Name of the chain used in withdrawals.</param>
    /// <param name="withdraw_order_id"></param>
    /// <param name="memo">Additional remarks with regards to the withdrawal.</param>
    /// <returns>Withdrawal result.</returns>
    public static WithdrawalResult WithdrawCurrency(
        this GateIoClient client,
        string currency,
        decimal amount,
        string address,
        string chain,
        string? withdraw_order_id = null,
        string? memo = null)
    {
        var request = new RestRequest("api/v4/withdrawals", Method.Post);

        var body = new WithdrawalRequest(
            amount,
            currency,
            address,
            chain,
            withdraw_order_id,
            memo);

        request.AddBody(JsonConvert.SerializeObject(body));

        return client.ExecuteRequest<WithdrawalResult>(request);
    }

    /// <summary>
    /// Request a withdrawal operation.
    /// </summary>
    /// <param name="client"></param>
    /// <param name="currency">Currency name.</param>
    /// <param name="amount">Currency amount.</param>
    /// <param name="address">Withdrawal address. Required for withdrawals.</param>
    /// <param name="chain">Name of the chain used in withdrawals.</param>
    /// <param name="withdraw_order_id"></param>
    /// <param name="memo">Additional remarks with regards to the withdrawal.</param>
    /// <returns>Withdrawal result.</returns>
    public static async Task<WithdrawalResult> WithdrawCurrencyAsync(
        this GateIoClient client,
        string currency,
        decimal amount,
        string address,
        string chain,
        string? withdraw_order_id = null,
        string? memo = null)
    {
        var request = new RestRequest("api/v4/withdrawals", Method.Post);

        var body = new WithdrawalRequest(
            amount,
            currency,
            address,
            chain,
            withdraw_order_id,
            memo);

        request.AddBody(JsonConvert.SerializeObject(body));

        return await client.ExecuteRequestAsync<WithdrawalResult>(request);
    }

    #endregion Original Methods

    #region Derived Methods

    public static WithdrawalResult Withdraw

    #endregion Derived Methods
}

