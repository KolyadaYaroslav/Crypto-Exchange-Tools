using RestSharp;
using static CryptoExchangeTools.Models.Binance.Wallet.CoinInformation;
using static CryptoExchangeTools.Models.Binance.Wallet.WithdrawHistoryRecord;
using static CryptoExchangeTools.Models.Binance.Wallet.DepositHistory;
using CryptoExchangeTools.Models.Binance.Wallet;

namespace CryptoExchangeTools.Requests.BinanceRequests;

public class Wallet
{
    private BinanceClient Client;

    public Wallet(BinanceClient client)
    {
        Client = client;
    }

    #region Original Methods

    #region WithdrawCurrency

    /// <summary>
    /// Submit a withdraw request.
    /// </summary>
    /// <param name="coin">asset name.</param>
    /// <param name="amount">amount to be withdrawn (fees not included)</param>
    /// <param name="address">destination address</param>
    /// <param name="network">spicify the network</param>
    /// <param name="addressTag">sSecondary address identifier for coins like XRP,XMR etc.</param>
    /// <param name="walletType">The wallet type for withdraw，0-spot wallet ，1-funding wallet. Default walletType is the current "selected wallet" under wallet->Fiat and Spot/Funding->Deposit</param>
    /// <returns>id of withdrawal</returns>
    /// <exception cref="RequestNotSuccessfulException"></exception>
    public string WithdrawCurrency(
        string coin,
        decimal amount,
        string address,
        string network,
        string? addressTag = null,
        int walletType = -1)
    {
        var request = BuildWithdrawCurrency(coin, amount, address, network, addressTag, walletType);

        return Client.ExecuteRequest<WithdrawResult>(request).id;
    }

    /// <summary>
    /// Submit a withdraw request.
    /// </summary>
    /// <param name="coin">asset name.</param>
    /// <param name="amount">amount to be withdrawn (fees not included)</param>
    /// <param name="address">destination address</param>
    /// <param name="network">spicify the network</param>
    /// <param name="addressTag">sSecondary address identifier for coins like XRP,XMR etc.</param>
    /// <param name="walletType">The wallet type for withdraw，0-spot wallet ，1-funding wallet. Default walletType is the current "selected wallet" under wallet->Fiat and Spot/Funding->Deposit</param>
    /// <returns>id of withdrawal</returns>
    /// <exception cref="RequestNotSuccessfulException"></exception>
    public async Task<string> WithdrawCurrencyAsync(
        string coin,
        decimal amount,
        string address,
        string network,
        string? addressTag = null,
        int walletType = -1)
    {
        var request = BuildWithdrawCurrency(coin, amount, address, network, addressTag, walletType);

        return (await Client.ExecuteRequestAsync<WithdrawResult>(request)).id;
    }

    private static RestRequest BuildWithdrawCurrency(string coin,
        decimal amount,
        string address,
        string network,
        string? addressTag,
        int walletType)
    {
        var request = new RestRequest("sapi/v1/capital/withdraw/apply", Method.Post);
        request.AddParameter("coin", coin);
        request.AddParameter("address", address);
        request.AddParameter("amount", amount);
        request.AddParameter("network", network);

        if (addressTag is not null)
            request.AddParameter("addressTag", addressTag);

        if (walletType != -1)
            request.AddParameter("walletType", walletType);

        return request;
    }

    #endregion WithdrawCurrency

    #region GetWithdrawHistory

    /// <summary>
    /// Fetch withdraw history.
    /// </summary>
    /// <param name="coin"></param>
    /// <param name="withdrawOrderId"></param>
    /// <param name="status"></param>
    /// <param name="offset"></param>
    /// <param name="limit">Default: 1000, Max: 1000</param>
    /// <param name="startTime">Default: 90 days from current timestamp</param>
    /// <param name="endTime">Default: present timestamp</param>
    /// <param name="recvWindow"></param>
    /// <returns></returns>
    public WithdrawHistoryRecord[] GetWithdrawHistory(
        string? coin = null,
        string? withdrawOrderId = null,
        WithdrawStatus? status = null,
        int offset = -1,
        int limit = -1,
        long startTime = -1,
        long endTime = -1,
        long recvWindow = -1)
    {
        var request = BuildGetWithdrawHistory(
            coin,
            withdrawOrderId,
            status,
            offset,
            limit,
            startTime,
            endTime,
            recvWindow);

        return Client.ExecuteRequest<WithdrawHistoryRecord[]>(request);
    }

    /// <summary>
    /// Fetch withdraw history.
    /// </summary>
    /// <param name="coin"></param>
    /// <param name="withdrawOrderId"></param>
    /// <param name="status"></param>
    /// <param name="offset"></param>
    /// <param name="limit">Default: 1000, Max: 1000</param>
    /// <param name="startTime">Default: 90 days from current timestamp</param>
    /// <param name="endTime">Default: present timestamp</param>
    /// <param name="recvWindow"></param>
    /// <returns></returns>
    public  async Task<WithdrawHistoryRecord[]> GetWithdrawHistoryAsync(
        string? coin = null,
        string? withdrawOrderId = null,
        WithdrawStatus? status = null,
        int offset = -1,
        int limit = -1,
        long startTime = -1,
        long endTime = -1,
        long recvWindow = -1)
    {
        var request = BuildGetWithdrawHistory(
            coin,
            withdrawOrderId,
            status,
            offset,
            limit,
            startTime,
            endTime,
            recvWindow);

        return await Client.ExecuteRequestAsync<WithdrawHistoryRecord[]>(request);
    }

    private static RestRequest BuildGetWithdrawHistory(
        string? coin,
        string? withdrawOrderId,
        WithdrawStatus? status,
        int offset,
        int limit,
        long startTime,
        long endTime,
        long recvWindow)
        {
        var request = new RestRequest("sapi/v1/capital/withdraw/history", Method.Get);

        if (coin is not null)
            request.AddParameter("coin", coin);

        if (withdrawOrderId is not null)
            request.AddParameter("withdrawOrderId", withdrawOrderId);

        if (status is not null)
            request.AddParameter("status", (int)status);

        if (offset != -1)
            request.AddParameter("offset", offset);

        if (limit != -1)
            request.AddParameter("limit", limit);

        if (startTime != -1)
            request.AddParameter("startTime", startTime);

        if (endTime != -1)
            request.AddParameter("endTime", endTime);

        if (recvWindow != -1)
            request.AddParameter("recvWindow", recvWindow);

        return request;
    }

    #endregion GetWithdrawHistory

    #region GetAssetDetail

    /// <summary>
    /// Fetch details of assets supported on Binance.
    /// </summary>
    /// <param name="asset">asset name.</param>
    /// <returns></returns>
    /// <exception cref="RequestNotSuccessfulException"></exception>
    /// <exception cref="ArgumentNullException"></exception>
    public AssetDetail GetAssetDetail(string asset)
    {
        var request = BuildGetAssetDetail(asset);

        var data = Client.ExecuteRequest<Dictionary<string, AssetDetail>>(request);

        return data.Where(x => x.Key == asset).Single().Value;
    }

    /// <summary>
    /// Fetch details of assets supported on Binance.
    /// </summary>
    /// <param name="asset">asset name.</param>
    /// <returns></returns>
    /// <exception cref="RequestNotSuccessfulException"></exception>
    /// <exception cref="ArgumentNullException"></exception>
    public  async Task<AssetDetail> GetAssetDetailAsync(string asset)
    {
        var request = BuildGetAssetDetail(asset);

        var data = await Client.ExecuteRequestAsync<Dictionary<string, AssetDetail>>(request);

        return data.Where(x => x.Key == asset).Single().Value;
    }

    private static RestRequest BuildGetAssetDetail(string asset)
    {
        var request = new RestRequest("sapi/v1/asset/assetDetail", Method.Get);

        request.AddParameter("asset", asset.ToUpper());

        return request;
    }

    #endregion GetAssetDetail

    #region GetAllCoinsInformation

    /// <summary>
    /// Get information of coins (available for deposit and withdraw) for user.
    /// </summary>
    /// <returns>List of Coin information</returns>
    public CoinInformation[] GetAllCoinsInformation()
    {
        var request = BuildGetAllCoinsInformation();

        return Client.ExecuteRequest<CoinInformation[]>(request);
    }

    /// <summary>
    /// Get information of coins (available for deposit and withdraw) for user.
    /// </summary>
    /// <returns>List of Coin information</returns>
    public  async Task<CoinInformation[]> GetAllCoinsInformationAsync()
    {
        var request = BuildGetAllCoinsInformation();

        return await Client.ExecuteRequestAsync<CoinInformation[]>(request);
    }

    private static RestRequest BuildGetAllCoinsInformation()
    {
        var request = new RestRequest("sapi/v1/capital/config/getall", Method.Get);

        return request;
    }

    #endregion GetAllCoinsInformation

    #region GetUserAsset

    /// <summary>
    /// Get user asset.
    /// </summary>
    /// <param name="asset">Whether need btc valuation or not, default is true.</param>
    /// <param name="needBtcValuation"></param>
    /// <returns>Asset information.</returns>
    public UserAsset GetUserAsset(string asset, bool needBtcValuation = true)
    {
        var request = BuildGetUserAsset(asset, needBtcValuation);

        var data = Client.ExecuteRequest<UserAsset[]>(request);

        if (!data.Any())
            return new UserAsset{ Asset = asset };

        return data.Single();
    }

    /// <summary>
    /// Get user asset.
    /// </summary>
    /// <param name="asset">Whether need btc valuation or not, default is true.</param>
    /// <param name="needBtcValuation"></param>
    /// <returns>Asset information.</returns>
    public  async Task<UserAsset> GetUserAssetAsync(string asset, bool needBtcValuation = true)
    {
        var request = BuildGetUserAsset(asset, needBtcValuation);

        var data = await Client.ExecuteRequestAsync<UserAsset[]>(request);

        if (!data.Any())
            return new UserAsset { Asset = asset };

        return data.Single();
    }

    private static RestRequest BuildGetUserAsset(string asset, bool needBtcValuation)
    {
        var request = new RestRequest("/sapi/v3/asset/getUserAsset", Method.Post);
        request.AddParameter("asset", asset);

        if (needBtcValuation)
            request.AddParameter("needBtcValuation", needBtcValuation);

        return request;
    }

    #endregion GetUserAsset

    #region GetUserAssets

    /// <summary>
    /// Get all user assets with non-zero balances.
    /// </summary>
    /// <param name="needBtcValuation"></param>
    /// <returns>Assets information.</returns>
    public UserAsset[] GetUserAssets(bool needBtcValuation = true)
    {
        var request = BuildGetUserAssets(needBtcValuation);

        return Client.ExecuteRequest<UserAsset[]>(request);
    }

    /// <summary>
    /// Get all user assets with non-zero balances.
    /// </summary>
    /// <param name="needBtcValuation"></param>
    /// <returns>Assets information.</returns>
    public  async Task<UserAsset[]> GetUserAssetsAsync(bool needBtcValuation = true)
    {
        var request = BuildGetUserAssets(needBtcValuation);

        return await Client.ExecuteRequestAsync<UserAsset[]>(request);
    }

    private static RestRequest BuildGetUserAssets(bool needBtcValuation)
    {
        var request = new RestRequest("sapi/v3/asset/getUserAsset", Method.Post);

        if (needBtcValuation)
            request.AddParameter("needBtcValuation", needBtcValuation);

        return request;
    }

    #endregion GetUserAssets

    #region GetDepositHistory

    /// <summary>
    /// Fetch deposit history.
    /// </summary>
    /// <param name="coin">Coin name.</param>
    /// <param name="txId">Transaction hash.</param>
    /// <param name="status">0(0:pending,6: credited but cannot withdraw, 7=Wrong Deposit,8=Waiting User confirm, 1:success)</param>
    /// <param name="startTime">Default: 90 days from current timestamp</param>
    /// <param name="endTime">Default: present timestamp</param>
    /// <param name="offset">Default:0</param>
    /// <param name="limit">Default:1000, Max:1000</param>
    /// <param name="recvWindow"></param>
    /// <returns>Array of deposit history.</returns>
    public DepositHistory[] GetDepositHistory(
        string? coin = null,
        string? txId = null,
        DepositStatus? status = null,
        long startTime = -1,
        long endTime = -1,
        int offset = -1,
        int limit = -1,
        long recvWindow = -1)
    {
        var request = BuildGetDepositHistory(
            coin,
            txId,
            status,
            startTime,
            endTime,
            offset,
            limit,
            recvWindow);

        return Client.ExecuteRequest<DepositHistory[]>(request);
    }


    /// <summary>
    /// Fetch deposit history.
    /// </summary>
    /// <param name="coin">Coin name.</param>
    /// <param name="txId">Transaction hash.</param>
    /// <param name="status">0(0:pending,6: credited but cannot withdraw, 7=Wrong Deposit,8=Waiting User confirm, 1:success)</param>
    /// <param name="startTime">Default: 90 days from current timestamp</param>
    /// <param name="endTime">Default: present timestamp</param>
    /// <param name="offset">Default:0</param>
    /// <param name="limit">Default:1000, Max:1000</param>
    /// <param name="recvWindow"></param>
    /// <returns>Array of deposit history.</returns>
    public  async Task<DepositHistory[]> GetDepositHistoryAsync(
        string? coin = null,
        string? txId = null,
        DepositStatus? status = null,
        long startTime = -1,
        long endTime = -1,
        int offset = -1,
        int limit = -1,
        long recvWindow = -1)
    {
        var request = BuildGetDepositHistory(
            coin,
            txId,
            status,
            startTime,
            endTime,
            offset,
            limit,
            recvWindow);

        return await Client.ExecuteRequestAsync<DepositHistory[]>(request);
    }

    private static RestRequest BuildGetDepositHistory(
        string? coin,
        string? txId,
        DepositStatus? status,
        long startTime,
        long endTime,
        int offset,
        int limit,
        long recvWindow)
    {
        var request = new RestRequest("sapi/v1/capital/deposit/hisrec", Method.Get);

        if (coin is not null)
            request.AddParameter("coin", coin);

        if (txId is not null)
            request.AddParameter("txId", txId);

        if (status is not null)
            request.AddParameter("status", (int)status);

        if (startTime != -1)
            request.AddParameter("startTime", startTime);

        if (endTime != -1)
            request.AddParameter("endTime", endTime);

        if (offset != -1)
            request.AddParameter("offset", offset);

        if (limit != -1)
            request.AddParameter("limit", limit);

        if (recvWindow != -1)
            request.AddParameter("recvWindow", recvWindow);

        return request;
    }

    #endregion GetDepositHistory

    #endregion Original Methods

    #region Derived Methods

    #region WithdrawAndWaitForSent

    /// <summary>
    /// Request withdrawal and wait untill assets leave Binance
    /// </summary>
    /// <param name="coin">asset name.</param>
    /// <param name="amount">amount to be withdrawn (fees not included)</param>
    /// <param name="address">destination address</param>
    /// <param name="network">spicify the network</param>
    /// <param name="addressTag">sSecondary address identifier for coins like XRP,XMR etc.</param>
    /// <param name="walletType">The wallet type for withdraw，0-spot wallet ，1-funding wallet. Default walletType is the current "selected wallet" under wallet->Fiat and Spot/Funding->Deposit</param>
    /// <returns>Withdrawal Id and Transaction hash.</returns>
    /// <exception cref="RequestNotSuccessfulException"></exception>
    /// <exception cref="WithdrawalFailedException"></exception>
    public (string, string) WithdrawAndWaitForSent(
        string coin,
        decimal amount,
        string address,
        string network,
        string? addressTag = null,
        int walletType = -1)
    {
        var withdrawalId = Client.Wallet.WithdrawCurrency(coin, amount, address, network, addressTag, walletType);

        var limit = 500;
        while (true)
        {
            Thread.Sleep(10_000);

            if (limit == 0)
                throw new Exception("Timeout for awaiting transaction completion was hit");

            var history = Client.Wallet.GetWithdrawHistory();

            if (history.Any())
            {
                var txData = history.Where(x => x.Id == withdrawalId).Single();

                Client.Message(txData.Status.ToString());

                if (txData.Status == WithdrawStatus.Completed)
                    return (withdrawalId, txData.TxId);

                if (txData.Status == WithdrawStatus.Cancelled
                || txData.Status == WithdrawStatus.Failure
                || txData.Status == WithdrawStatus.Rejected)
                    throw new WithdrawalFailedException(withdrawalId.ToString(), txData.Status.ToString());
            }
            limit--;
        }
    }

    /// <summary>
    /// Request withdrawal and wait untill assets leave Binance
    /// </summary>
    /// <param name="coin">asset name.</param>
    /// <param name="amount">amount to be withdrawn (fees not included)</param>
    /// <param name="address">destination address</param>
    /// <param name="network">spicify the network</param>
    /// <param name="addressTag">sSecondary address identifier for coins like XRP,XMR etc.</param>
    /// <param name="walletType">The wallet type for withdraw，0-spot wallet ，1-funding wallet. Default walletType is the current "selected wallet" under wallet->Fiat and Spot/Funding->Deposit</param>
    /// <returns>Withdrawal Id and Transaction hash.</returns>
    /// <exception cref="RequestNotSuccessfulException"></exception>
    /// <exception cref="WithdrawalFailedException"></exception>
    public  async Task<(string, string)> WithdrawAndWaitForSentAsync(
        string coin,
        decimal amount,
        string address,
        string network,
        string? addressTag = null,
        int walletType = -1)
    {
        var withdrawalId = await Client.Wallet.WithdrawCurrencyAsync(coin, amount, address, network, addressTag, walletType);

        var limit = 500;
        while (true)
        {
            await Task.Delay(10_000);

            if (limit == 0)
                throw new Exception("Timeout for awaiting transaction completion was hit");

            var history = await Client.Wallet.GetWithdrawHistoryAsync();

            if(history.Any())
            {
                var txData = history.Where(x => x.Id == withdrawalId).Single();

                Client.Message(txData.Status.ToString());

                if (txData.Status == WithdrawStatus.Completed)
                    return (withdrawalId, txData.TxId);

                if (txData.Status == WithdrawStatus.Cancelled
                || txData.Status == WithdrawStatus.Failure
                || txData.Status == WithdrawStatus.Rejected)
                    throw new WithdrawalFailedException(txData.Id, txData.Status.ToString());
            }
            limit--;
        }
    }

    #endregion WithdrawAndWaitForSent

    #region GetCoinInformation

    /// <summary>
    /// Get information of coin.
    /// </summary>
    /// <param name="name">Coin name.</param>
    /// <param name="network">Network name (optional).</param>
    /// <returns>Information on a coin.</returns>
    public CoinInformation GetCoinInformation(string name, string? network = null)
    {
        var infos = Client.Wallet.GetAllCoinsInformation();

        var info = infos.Where(x => x.Coin.ToLower() == name.ToLower()).Single();

        if (network is not null)
            info.NetworkList = info.NetworkList.Where(x => x.NetworkName.ToLower() == network.ToLower()).ToList();

        return info;
    }


    /// <summary>
    /// Get information of coin.
    /// </summary>
    /// <param name="name">Coin name.</param>
    /// <param name="network">Network name (optional).</param>
    /// <returns>Information on a coin.</returns>
    public  async Task<CoinInformation> GetCoinInformationAsync(string name, string? network = null)
    {
        var infos = await Client.Wallet.GetAllCoinsInformationAsync();

        var info = infos.Where(x => x.Coin.ToLower() == name.ToLower()).Single();

        if (network is not null)
            info.NetworkList = info.NetworkList.Where(x => x.NetworkName.ToLower() == network.ToLower()).ToList();

        return info;
    }

    #endregion GetCoinInformation

    #region WithdrawAllCoinBalanceAndWaitForSent

    /// <summary>
    /// Request withdrawal of all free asset balance and wait untill it leave Binance
    /// </summary>
    /// <param name="coin">asset name.</param>
    /// <param name="address">destination address</param>
    /// <param name="network">spicify the network</param>
    /// <param name="addressTag">sSecondary address identifier for coins like XRP,XMR etc.</param>
    /// <param name="walletType">The wallet type for withdraw，0-spot wallet ，1-funding wallet. Default walletType is the current "selected wallet" under wallet->Fiat and Spot/Funding->Deposit</param>
    /// <returns>Withdrawal id and transaction hash.</returns>
    /// <exception cref="RequestNotSuccessfulException"></exception>
    /// <exception cref="WithdrawalFailedException"></exception>
    public (string, string) WithdrawAllCoinBalanceAndWaitForSent(
        string coin,
        string address,
        string network,
        string? addressTag = null,
        int walletType = -1)
    {
        var asset = Client.Wallet.GetUserAsset(coin);

        var info = Client.Wallet.GetCoinInformation(coin, network);

        var decimals = info.NetworkList.Single().WithdrawIntegerMultiple;

        var rounding = 1 / decimals;

        var amount = Math.Floor(asset.Free * rounding) / rounding;

        Client.Message($"Starting withdrawal of {amount} {coin}");

        return Client.Wallet.WithdrawAndWaitForSent(coin, amount, address, network, addressTag, walletType);
    }


    /// <summary>
    /// Request withdrawal of all free asset balance and wait untill it leave Binance
    /// </summary>
    /// <param name="coin">asset name.</param>
    /// <param name="address">destination address</param>
    /// <param name="network">spicify the network</param>
    /// <param name="addressTag">sSecondary address identifier for coins like XRP,XMR etc.</param>
    /// <param name="walletType">The wallet type for withdraw，0-spot wallet ，1-funding wallet. Default walletType is the current "selected wallet" under wallet->Fiat and Spot/Funding->Deposit</param>
    /// <returns>Withdrawal id and transaction hash.</returns>
    /// <exception cref="RequestNotSuccessfulException"></exception>
    /// <exception cref="WithdrawalFailedException"></exception>
    public  async Task<(string, string)> WithdrawAllCoinBalanceAndWaitForSentAsync(
        string coin,
        string address,
        string network,
        string? addressTag = null,
        int walletType = -1)
    {
        var asset = await Client.Wallet.GetUserAssetAsync(coin);

        var info = await Client.Wallet.GetCoinInformationAsync(coin, network);

        var decimals = info.NetworkList.Single().WithdrawIntegerMultiple;

        var rounding = 1 / decimals;

        var amount = Math.Floor(asset.Free * rounding) / rounding;

        Client.Message($"Starting withdrawal of {amount} {coin}");

        return await Client.Wallet.WithdrawAndWaitForSentAsync(coin, amount, address, network, addressTag, walletType);
    }

    #endregion WithdrawAllCoinBalanceAndWaitForSent

    #endregion Derived Methods
}

