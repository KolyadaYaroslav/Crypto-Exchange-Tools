using System.Xml.Linq;
using CryptoExchangeTools.Models.GateIo;
using CryptoExchangeTools.Requests.GateIoRequests;
using Newtonsoft.Json;
using RestSharp;
using static CryptoExchangeTools.Models.GateIo.WithdrawalResult;

namespace CryptoExchangeTools.Requests.GateIoRequests;

public class Withdrawal
{
    private GateIoClient Client;

    public Withdrawal(GateIoClient client)
    {
        Client = client;
    }

    #region Original Methods

    #region WithdrawCurrency

    /// <summary>
    /// Request a withdrawal operation.
    /// </summary>
    /// <param name="Client"></param>
    /// <param name="currency">Currency name.</param>
    /// <param name="amount">Currency amount.</param>
    /// <param name="address">Withdrawal address. Required for withdrawals.</param>
    /// <param name="chain">Name of the chain used in withdrawals.</param>
    /// <param name="withdraw_order_id"></param>
    /// <param name="memo">Additional remarks with regards to the withdrawal.</param>
    /// <returns>Withdrawal result.</returns>
    public WithdrawalResult WithdrawCurrency(
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

        return Client.ExecuteRequest<WithdrawalResult>(request);
    }

    /// <summary>
    /// Request a withdrawal operation.
    /// </summary>
    /// <param name="Client"></param>
    /// <param name="currency">Currency name.</param>
    /// <param name="amount">Currency amount.</param>
    /// <param name="address">Withdrawal address. Required for withdrawals.</param>
    /// <param name="chain">Name of the chain used in withdrawals.</param>
    /// <param name="withdraw_order_id"></param>
    /// <param name="memo">Additional remarks with regards to the withdrawal.</param>
    /// <returns>Withdrawal result.</returns>
    public async Task<WithdrawalResult> WithdrawCurrencyAsync(
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

        return await Client.ExecuteRequestAsync<WithdrawalResult>(request);
    }

    #endregion WithdrawCurrency

    #endregion Original Methods

    #region Derived Methods

    #region WithdrawAndWaitForSent

    /// <summary>
    /// Request withdrawal and wait untill assets leave GateIo.
    /// </summary>
    /// <param name="currency">Currency name.</param>
    /// <param name="amount">Currency amount.</param>
    /// <param name="address">Withdrawal address. Required for withdrawals.</param>
    /// <param name="chain">Name of the chain used in withdrawals.</param>
    /// <param name="withdraw_order_id"></param>
    /// <param name="memo">Additional remarks with regards to the withdrawal.</param>
    /// <returns>Withdraw record.</returns>
    /// <exception cref="Exception"></exception>
    public WithdrawHistory WithdrawAndWaitForSent(
        string currency,
        decimal amount,
        string address,
        string chain,
        string? withdraw_order_id = null,
        string? memo = null)
    {
        var withdrawalResult = Client.Withdrawal.WithdrawCurrency(currency, amount, address, chain, withdraw_order_id, memo);

        var limit = 500;
        while (true)
        {
            if (limit == 0)
                throw new Exception("Timeout for awaiting transaction completion was hit");

            var history = Client.Wallet.RetrieveWithdrawalRecords(currency);

            if (history.Any())
            {
                var txData = history.Where(x => x.Id == withdrawalResult.Id).Single();

                if (CheckHistory(txData))
                    return txData;
            }

            Thread.Sleep(10_000);
            limit--;
        }
    }

    /// <summary>
    /// Request withdrawal and wait untill assets leave GateIo.
    /// </summary>
    /// <param name="currency">Currency name.</param>
    /// <param name="amount">Currency amount.</param>
    /// <param name="address">Withdrawal address. Required for withdrawals.</param>
    /// <param name="chain">Name of the chain used in withdrawals.</param>
    /// <param name="withdraw_order_id"></param>
    /// <param name="memo">Additional remarks with regards to the withdrawal.</param>
    /// <returns>Withdraw record.</returns>
    /// <exception cref="Exception"></exception>
    public async Task<WithdrawHistory> WithdrawAndWaitForSentAsync(
        string currency,
        decimal amount,
        string address,
        string chain,
        string? withdraw_order_id = null,
        string? memo = null)
    {
        var withdrawalResult = await Client.Withdrawal.WithdrawCurrencyAsync(currency, amount, address, chain, withdraw_order_id, memo);

        var limit = 500;
        while (true)
        {
            if (limit == 0)
                throw new Exception("Timeout for awaiting transaction completion was hit");

            var history = await Client.Wallet.RetrieveWithdrawalRecordsAsync(currency);

            if (history.Any())
            {
                var txData = history.Where(x => x.Id == withdrawalResult.Id).Single();

                if (CheckHistory(txData))
                    return txData;
            }

            Thread.Sleep(10_000);
            limit--;
        }
    }

    private bool CheckHistory(WithdrawHistory txData)
    {
        Client.Message(txData.Status.ToString());

        if (txData.Status == WithdrawalStatus.DONE)
            return true;

        if (txData.Status == WithdrawalStatus.CANCEL
            || txData.Status == WithdrawalStatus.DMOVE
            || txData.Status == WithdrawalStatus.MANUAL
            || txData.Status == WithdrawalStatus.BCODE
            || txData.Status == WithdrawalStatus.FAIL
            || txData.Status == WithdrawalStatus.INVALID)
            throw new WithdrawalFailedException(txData.Id.ToString(), txData.Status.ToString(), null);

        return false;
    }

    #endregion WithdrawAndWaitForSent

    #endregion Derived Methods
}
