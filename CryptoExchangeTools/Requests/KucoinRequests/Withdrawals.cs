using CryptoExchangeTools.Models.Kucoin;
using Newtonsoft.Json;
using RestSharp;
using static System.Runtime.InteropServices.JavaScript.JSType;
using static CryptoExchangeTools.Models.Kucoin.WithdrawHistory;

namespace CryptoExchangeTools.Requests.KucoinRequests;

public class Withdrawals
{
	private KucoinClient Client;

	public Withdrawals(KucoinClient client)
	{
		Client = client;
	}

    #region Original Methods

    #region Withdraw Currency

    /// <summary>
    /// Initiate withdrawal of a currency.
    /// </summary>
    /// <param name="currency">Currency.</param>
    /// <param name="address">Withdrawal address.</param>
    /// <param name="amount">Withdrawal amount.</param>
    /// <param name="chain">[Optional] The chain of currency. For a currency with multiple chains, it is recommended to specify chain parameter instead of using the default chain.</param>
    /// <param name="memo">[Optional] Address remark. If there’s no remark, it is empty. When you withdraw from other platforms to the KuCoin, you need to fill in memo(tag). If you do not fill memo (tag), your deposit may not be available, please be cautious..</param>
    /// <param name="isInner">[Optional] Internal withdrawal or not. Default setup: false.</param>
    /// <param name="remark">[Optional] Remark.</param>
    /// <param name="feeDeductType">[Optional] Withdrawal fee deduction type: INTERNAL or EXTERNAL or not specified.</param>
    /// <returns></returns>
    public WithdrawResult WithdrawCurrency(
        string currency,
        string address,
        decimal amount,
        string chain,
        string? memo = null,
        bool isInner = false,
        string? remark = null,
        string? feeDeductType = null)
    {
        var currencyDetail = Client.Currencies.GetCurrencyDetail(currency);

        var request = BuildWithdrawCurrency(currency, address, amount, chain, memo, isInner, remark, feeDeductType, currencyDetail.Precision);

        return Client.ExecuteRequest<WithdrawResult>(request);
    }

    /// <summary>
    /// Initiate withdrawal of a currency.
    /// </summary>
    /// <param name="currency">Currency.</param>
    /// <param name="address">Withdrawal address.</param>
    /// <param name="amount">Withdrawal amount.</param>
    /// <param name="chain">[Optional] The chain of currency. For a currency with multiple chains, it is recommended to specify chain parameter instead of using the default chain.</param>
    /// <param name="memo">[Optional] Address remark. If there’s no remark, it is empty. When you withdraw from other platforms to the KuCoin, you need to fill in memo(tag). If you do not fill memo (tag), your deposit may not be available, please be cautious..</param>
    /// <param name="isInner">[Optional] Internal withdrawal or not. Default setup: false.</param>
    /// <param name="remark">[Optional] Remark.</param>
    /// <param name="feeDeductType">[Optional] Withdrawal fee deduction type: INTERNAL or EXTERNAL or not specified.</param>
    /// <returns></returns>
    public async Task<WithdrawResult> WithdrawCurrencyAsync(
        string currency,
        string address,
        decimal amount,
        string chain,
        string? memo = null,
        bool isInner = false,
        string? remark = null,
        string? feeDeductType = null)
    {
        var currencyDetail = await Client.Currencies.GetCurrencyDetailAsync(currency);

        var request = BuildWithdrawCurrency(currency, address, amount, chain, memo, isInner, remark, feeDeductType, currencyDetail.Precision);

        return await Client.ExecuteRequestAsync<WithdrawResult>(request);
    }

    private static RestRequest BuildWithdrawCurrency(
        string currency,
        string address,
		decimal amount,
		string chain,
        string? memo,
		bool isInner,
		string? remark,
		string? feeDeductType,
        int precision)
	{
		var request = new RestRequest("api/v1/withdrawals", Method.Post);

        var body = new WithdrawRequest(
            currency,
            address,
            amount,
            chain,
            memo,
            isInner,
            remark,
            feeDeductType);

        request.AddBody(JsonConvert.SerializeObject(body));

        return request;
    }

    #endregion Withdraw Currency

    #region Get Withdrawals List

    /// <summary>
    /// Get Withdrawals history.
    /// </summary>
    /// <param name="currency">[Optional] Currency.</param>
    /// <param name="status">[Optional] Status. Available value: PROCESSING, WALLET_PROCESSING, SUCCESS, and FAILURE.</param>
    /// <param name="startAt">[Optional] Start time (milisecond).</param>
    /// <param name="endAt">[Optional] End time (milisecond).</param>
    /// <returns></returns>
    public WithdrawHistory GetWithdrawalsList(string? currency = null, string? status = null, long startAt = -1, long endAt = -1)
    {
        var request = BuildGetWithdrawalsList(currency, status, startAt, endAt);

        return Client.ExecuteRequest<WithdrawHistory>(request);
    }

    /// <summary>
    /// Get Withdrawals history.
    /// </summary>
    /// <param name="currency">[Optional] Currency.</param>
    /// <param name="status">[Optional] Status. Available value: PROCESSING, WALLET_PROCESSING, SUCCESS, and FAILURE.</param>
    /// <param name="startAt">[Optional] Start time (milisecond).</param>
    /// <param name="endAt">[Optional] End time (milisecond).</param>
    /// <returns></returns>
    public async Task<WithdrawHistory> GetWithdrawalsListAsync(string? currency = null, string? status = null, long startAt = -1, long endAt = -1)
    {
        var request = BuildGetWithdrawalsList(currency, status, startAt, endAt);

        return await Client.ExecuteRequestAsync<WithdrawHistory>(request);
    }

    private static RestRequest BuildGetWithdrawalsList(string? currency, string? status, long startAt, long endAt)
    {
        var request = new RestRequest("api/v1/withdrawals");

        if (!string.IsNullOrEmpty(currency))
            request.AddParameter("currency", currency);

        if (!string.IsNullOrEmpty(status))
        {
            if (!Enum.TryParse<Status>(status, out Status result))
                throw new Exception($"{status} is not a valid status.");

            request.AddParameter("status", status);
        }
            
        if (startAt != -1)
            request.AddParameter("startAt", startAt);

        if (endAt != -1)
            request.AddParameter("endAt", endAt);

        return request;
    }

    #endregion Get Withdrawals List

    #endregion Original Methods

    #region Derived Methods

    #region Withdraw And Wait For Sent

    /// <summary>
    /// Request withdrawal and wait untill assets leave Kucoin.
    /// </summary>
    /// <param name="currency">Currency.</param>
    /// <param name="address">Withdrawal address.</param>
    /// <param name="amount">Withdrawal amount.</param>
    /// <param name="chain">[Optional] The chain of currency. For a currency with multiple chains, it is recommended to specify chain parameter instead of using the default chain.</param>
    /// <param name="memo">[Optional] Address remark. If there’s no remark, it is empty. When you withdraw from other platforms to the KuCoin, you need to fill in memo(tag). If you do not fill memo (tag), your deposit may not be available, please be cautious..</param>
    /// <param name="isInner">[Optional] Internal withdrawal or not. Default setup: false.</param>
    /// <param name="remark">[Optional] Remark.</param>
    /// <param name="feeDeductType">[Optional] Withdrawal fee deduction type: INTERNAL or EXTERNAL or not specified.</param>
    /// <returns>Withdrawal record.</returns>
    /// <exception cref="Exception"></exception>
    public WithdrawHistory.Item WithdrawAndWaitForSent(
        string currency,
        string address,
        decimal amount,
        string chain,
        string? memo = null,
        bool isInner = false,
        string? remark = null,
        string? feeDeductType = null)
    {
        Client.Message($"Starting withdrawal of {amount} {currency}");

        var withdrwal = Client.Withdrawals.WithdrawCurrency(currency, address, amount, chain, memo, isInner, remark, feeDeductType);

        int limit = 500;
        while(true)
        {
            if (limit == 0)
                throw new Exception("Timeout for awaiting transaction completion was hit");

            var history = Client.Withdrawals.GetWithdrawalsList(currency);

            if(history.Items.Any())
            {
                var txData = history.Items.Where(x => x.Id == withdrwal.WithdrawalId).Single();

                if (CheckHistory(txData))
                    return txData;
            }
            Thread.Sleep(10_000);
            limit--;
        }
    }

    /// <summary>
    /// Request withdrawal and wait untill assets leave Kucoin.
    /// </summary>
    /// <param name="currency">Currency.</param>
    /// <param name="address">Withdrawal address.</param>
    /// <param name="amount">Withdrawal amount.</param>
    /// <param name="chain">[Optional] The chain of currency. For a currency with multiple chains, it is recommended to specify chain parameter instead of using the default chain.</param>
    /// <param name="memo">[Optional] Address remark. If there’s no remark, it is empty. When you withdraw from other platforms to the KuCoin, you need to fill in memo(tag). If you do not fill memo (tag), your deposit may not be available, please be cautious..</param>
    /// <param name="isInner">[Optional] Internal withdrawal or not. Default setup: false.</param>
    /// <param name="remark">[Optional] Remark.</param>
    /// <param name="feeDeductType">[Optional] Withdrawal fee deduction type: INTERNAL or EXTERNAL or not specified.</param>
    /// <returns>Withdrawal record.</returns>
    /// <exception cref="Exception"></exception>
    public async Task<WithdrawHistory.Item> WithdrawAndWaitForSentAsync(
        string currency,
        string address,
        decimal amount,
        string chain,
        string? memo = null,
        bool isInner = false,
        string? remark = null,
        string? feeDeductType = null)
    {
        Client.Message($"Starting withdrawal of {amount} {currency}");

        var withdrwal = await Client.Withdrawals.WithdrawCurrencyAsync(currency, address, amount, chain, memo, isInner, remark, feeDeductType);

        int limit = 500;
        while (true)
        {
            if (limit == 0)
                throw new Exception("Timeout for awaiting transaction completion was hit");

            var history = await Client.Withdrawals.GetWithdrawalsListAsync(currency);

            if (history.Items.Any())
            {
                var txData = history.Items.Where(x => x.Id == withdrwal.WithdrawalId).Single();

                if (CheckHistory(txData))
                    return txData;
            }
            Thread.Sleep(10_000);
            limit--;
        }
    }

    private bool CheckHistory(WithdrawHistory.Item txData)
    {
        Client.Message(txData.Status.ToString());

        if (txData.Status == WithdrawHistory.Status.SUCCESS)
            return true;

        if (txData.Status == WithdrawHistory.Status.FAILURE)
            throw new WithdrawalFailedException(txData.Id.ToString(), txData.Status.ToString());

        return false;
    }

    #endregion Withdraw And Wait For Sent

    #endregion Derived Methods
}

