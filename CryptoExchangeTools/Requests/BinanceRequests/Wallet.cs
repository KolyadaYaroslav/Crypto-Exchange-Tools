using System;
using System.Net;
using System.Xml.Linq;
using CryptoExchangeTools.Models.Binance.Wallet;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using static CryptoExchangeTools.Models.Binance.Wallet.CoinInformation;
using static CryptoExchangeTools.Models.Binance.Wallet.WithdrawHistoryRecord;
using static CryptoExchangeTools.Models.Binance.Wallet.DepositHistory;

namespace CryptoExchangeTools.BinanceRequests.Wallet;

public static class Wallet
{
    #region Original Methods

    /// <summary>
    /// Submit a withdraw request.
    /// </summary>
    /// <param name="client"></param>
    /// <param name="coin">asset name.</param>
    /// <param name="amount">amount to be withdrawn (fees not included)</param>
    /// <param name="address">destination address</param>
    /// <param name="network">spicify the network</param>
    /// <param name="addressTag">sSecondary address identifier for coins like XRP,XMR etc.</param>
    /// <param name="walletType">The wallet type for withdraw，0-spot wallet ，1-funding wallet. Default walletType is the current "selected wallet" under wallet->Fiat and Spot/Funding->Deposit</param>
    /// <returns>id of withdrawal</returns>
    /// <exception cref="RequestNotSuccessfulException"></exception>
    public static string WithdrawCurrency(
        this BinanceClient client,
        string coin,
        decimal amount,
        string address,
        string network,
        string? addressTag = null,
        int walletType = -1)
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

        return client.ExecuteRequest<WithdrawResult>(request).id;
    }

    /// <summary>
    /// Submit a withdraw request.
    /// </summary>
    /// <param name="client"></param>
    /// <param name="coin">asset name.</param>
    /// <param name="amount">amount to be withdrawn (fees not included)</param>
    /// <param name="address">destination address</param>
    /// <param name="network">spicify the network</param>
    /// <param name="addressTag">sSecondary address identifier for coins like XRP,XMR etc.</param>
    /// <param name="walletType">The wallet type for withdraw，0-spot wallet ，1-funding wallet. Default walletType is the current "selected wallet" under wallet->Fiat and Spot/Funding->Deposit</param>
    /// <returns>id of withdrawal</returns>
    /// <exception cref="RequestNotSuccessfulException"></exception>
    public static async Task<string> WithdrawCurrencyAsync(
        this BinanceClient client,
        string coin,
        decimal amount,
        string address,
        string network,
        string? addressTag = null,
        int walletType = -1)
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

        return (await client.ExecuteRequestAsync<WithdrawResult>(request)).id;
    }


    /// <summary>
    /// Fetch withdraw history.
    /// </summary>
    /// <param name="client"></param>
    /// <param name="coin"></param>
    /// <param name="withdrawOrderId"></param>
    /// <param name="status"></param>
    /// <param name="offset"></param>
    /// <param name="limit">Default: 1000, Max: 1000</param>
    /// <param name="startTime">Default: 90 days from current timestamp</param>
    /// <param name="endTime">Default: present timestamp</param>
    /// <param name="recvWindow"></param>
    /// <returns></returns>
    public static WithdrawHistoryRecord[] GetWithdrawHistory(
        this BinanceClient client,
        string? coin = null,
        string? withdrawOrderId = null,
        WithdrawStatus? status = null,
        int offset = -1,
        int limit = -1,
        long startTime = -1,
        long endTime = -1,
        long recvWindow = -1)
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

        return client.ExecuteRequest<WithdrawHistoryRecord[]>(request);
    }

    /// <summary>
    /// Fetch withdraw history.
    /// </summary>
    /// <param name="client"></param>
    /// <param name="coin"></param>
    /// <param name="withdrawOrderId"></param>
    /// <param name="status"></param>
    /// <param name="offset"></param>
    /// <param name="limit">Default: 1000, Max: 1000</param>
    /// <param name="startTime">Default: 90 days from current timestamp</param>
    /// <param name="endTime">Default: present timestamp</param>
    /// <param name="recvWindow"></param>
    /// <returns></returns>
    public static async Task<WithdrawHistoryRecord[]> GetWithdrawHistoryAsync(
        this BinanceClient client,
        string? coin = null,
        string? withdrawOrderId = null,
        WithdrawStatus? status = null,
        int offset = -1,
        int limit = -1,
        long startTime = -1,
        long endTime = -1,
        long recvWindow = -1)
    {
        var request = new RestRequest("sapi/v1/capital/withdraw/history", Method.Get);

        if(coin is not null)
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

        return await client.ExecuteRequestAsync<WithdrawHistoryRecord[]>(request);
    }


    /// <summary>
    /// Fetch details of assets supported on Binance.
    /// </summary>
    /// <param name="client"></param>
    /// <param name="asset">asset name.</param>
    /// <returns></returns>
    /// <exception cref="RequestNotSuccessfulException"></exception>
    /// <exception cref="ArgumentNullException"></exception>
    public static AssetDetail GetAssetDetail(this BinanceClient client, string asset)
    {
        var request = new RestRequest("sapi/v1/asset/assetDetail", Method.Get);
        request.AddParameter("asset", asset.ToUpper());

        var data = client.ExecuteRequest<Dictionary<string, AssetDetail>>(request);

        return data.Where(x => x.Key == asset).Single().Value;
    }

    /// <summary>
    /// Fetch details of assets supported on Binance.
    /// </summary>
    /// <param name="client"></param>
    /// <param name="asset">asset name.</param>
    /// <returns></returns>
    /// <exception cref="RequestNotSuccessfulException"></exception>
    /// <exception cref="ArgumentNullException"></exception>
    public static async Task<AssetDetail> GetAssetDetailAsync(this BinanceClient client, string asset)
    {
        var request = new RestRequest("sapi/v1/asset/assetDetail", Method.Get);
        request.AddParameter("asset", asset.ToUpper());

        var data = await client.ExecuteRequestAsync<Dictionary<string, AssetDetail>>(request);

        return data.Where(x => x.Key == asset).Single().Value;
    }


    /// <summary>
    /// Get information of coins (available for deposit and withdraw) for user.
    /// </summary>
    /// <param name="client"></param>
    /// <returns>List of Coin information</returns>
    public static CoinInformation[] GetAllCoinsInformation(this BinanceClient client)
    {
        var request = new RestRequest("sapi/v1/capital/config/getall", Method.Get);

        return client.ExecuteRequest<CoinInformation[]>(request);
    }

    /// <summary>
    /// Get information of coins (available for deposit and withdraw) for user.
    /// </summary>
    /// <param name="client"></param>
    /// <returns>List of Coin information</returns>
    public static async Task<CoinInformation[]> GetAllCoinsInformationAsync(this BinanceClient client)
    {
        var request = new RestRequest("sapi/v1/capital/config/getall", Method.Get);

        return await client.ExecuteRequestAsync<CoinInformation[]>(request);
    }



    /// <summary>
    /// Get user asset.
    /// </summary>
    /// <param name="client">Asset name.</param>
    /// <param name="asset">Whether need btc valuation or not, default is true.</param>
    /// <param name="needBtcValuation"></param>
    /// <returns>Asset information.</returns>
    public static UserAsset GetUserAsset(this BinanceClient client, string asset, bool needBtcValuation = true)
    {
        var request = new RestRequest("/sapi/v3/asset/getUserAsset", Method.Post);
        request.AddParameter("asset", asset);

        if (needBtcValuation)
            request.AddParameter("needBtcValuation", needBtcValuation);

        var data = client.ExecuteRequest<UserAsset[]>(request);

        if (!data.Any())
            return new UserAsset{ Asset = asset };

        return data.Single();
    }

    /// <summary>
    /// Get user asset.
    /// </summary>
    /// <param name="client">Asset name.</param>
    /// <param name="asset">Whether need btc valuation or not, default is true.</param>
    /// <param name="needBtcValuation"></param>
    /// <returns>Asset information.</returns>
    public static async Task<UserAsset> GetUserAssetAsync(this BinanceClient client, string asset, bool needBtcValuation = true)
    {
        var request = new RestRequest("/sapi/v3/asset/getUserAsset", Method.Post);
        request.AddParameter("asset", asset);

        if (needBtcValuation)
            request.AddParameter("needBtcValuation", needBtcValuation);

        var data = await client.ExecuteRequestAsync<UserAsset[]>(request);

        if (!data.Any())
            return new UserAsset { Asset = asset };

        return data.Single();
    }



    /// <summary>
    /// Get all user assets with non-zero balances.
    /// </summary>
    /// <param name="client"></param>
    /// <param name="needBtcValuation"></param>
    /// <returns>Assets information.</returns>
    public static UserAsset[] GetUserAssets(this BinanceClient client, bool needBtcValuation = true)
    {
        var request = new RestRequest("sapi/v3/asset/getUserAsset", Method.Post);

        if (needBtcValuation)
            request.AddParameter("needBtcValuation", needBtcValuation);

        return client.ExecuteRequest<UserAsset[]>(request);
    }

    /// <summary>
    /// Get all user assets with non-zero balances.
    /// </summary>
    /// <param name="client"></param>
    /// <param name="needBtcValuation"></param>
    /// <returns>Assets information.</returns>
    public static async Task<UserAsset[]> GetUserAssetsAsync(this BinanceClient client, bool needBtcValuation = true)
    {
        var request = new RestRequest("sapi/v3/asset/getUserAsset", Method.Post);

        if (needBtcValuation)
            request.AddParameter("needBtcValuation", needBtcValuation);

        return await client.ExecuteRequestAsync<UserAsset[]>(request);
    }


    /// <summary>
    /// Fetch deposit history.
    /// </summary>
    /// <param name="client"></param>
    /// <param name="coin">Coin name.</param>
    /// <param name="txId">Transaction hash.</param>
    /// <param name="status">0(0:pending,6: credited but cannot withdraw, 7=Wrong Deposit,8=Waiting User confirm, 1:success)</param>
    /// <param name="startTime">Default: 90 days from current timestamp</param>
    /// <param name="endTime">Default: present timestamp</param>
    /// <param name="offset">Default:0</param>
    /// <param name="limit">Default:1000, Max:1000</param>
    /// <param name="recvWindow"></param>
    /// <returns>Array of deposit history.</returns>
    public static DepositHistory[] GetDepositHistory(
        this BinanceClient client,
        string? coin = null,
        string? txId = null,
        DepositStatus? status = null,
        long startTime = -1,
        long endTime = -1,
        int offset = -1,
        int limit = -1,
        long recvWindow = -1)
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

        return client.ExecuteRequest<DepositHistory[]>(request);
    }


    /// <summary>
    /// Fetch deposit history.
    /// </summary>
    /// <param name="client"></param>
    /// <param name="coin">Coin name.</param>
    /// <param name="txId">Transaction hash.</param>
    /// <param name="status">0(0:pending,6: credited but cannot withdraw, 7=Wrong Deposit,8=Waiting User confirm, 1:success)</param>
    /// <param name="startTime">Default: 90 days from current timestamp</param>
    /// <param name="endTime">Default: present timestamp</param>
    /// <param name="offset">Default:0</param>
    /// <param name="limit">Default:1000, Max:1000</param>
    /// <param name="recvWindow"></param>
    /// <returns>Array of deposit history.</returns>
    public static async Task<DepositHistory[]> GetDepositHistoryAsync(
        this BinanceClient client,
        string? coin = null,
        string? txId = null,
        DepositStatus? status = null,
        long startTime = -1,
        long endTime = -1,
        int offset = -1,
        int limit = -1,
        long recvWindow = -1)
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

        return await client.ExecuteRequestAsync<DepositHistory[]>(request);
    }

    #endregion Original Methods

    #region Derived Methods

    /// <summary>
    /// Request withdrawal and wait untill assets leave Binance
    /// </summary>
    /// <param name="client"></param>
    /// <param name="coin">asset name.</param>
    /// <param name="amount">amount to be withdrawn (fees not included)</param>
    /// <param name="address">destination address</param>
    /// <param name="network">spicify the network</param>
    /// <param name="addressTag">sSecondary address identifier for coins like XRP,XMR etc.</param>
    /// <param name="walletType">The wallet type for withdraw，0-spot wallet ，1-funding wallet. Default walletType is the current "selected wallet" under wallet->Fiat and Spot/Funding->Deposit</param>
    /// <returns>Withdrawal Id and Transaction hash.</returns>
    /// <exception cref="RequestNotSuccessfulException"></exception>
    /// <exception cref="WithdrawalFailedException"></exception>
    public static (string, string) WithdrawAndWaitForSent(
        this BinanceClient client,
        string coin,
        decimal amount,
        string address,
        string network,
        string? addressTag = null,
        int walletType = -1)
    {
        var withdrawalId = client.WithdrawCurrency(coin, amount, address, network, addressTag, walletType);

        var limit = 500;
        while (true)
        {
            if (limit == 0)
                throw new Exception("Timeout for awaiting transaction completion was hit");

            var history = client.GetWithdrawHistory();

            if (history.Any())
            {
                var txData = history.Where(x => x.Id == withdrawalId).Single();

                client.Message(txData.Status.ToString());

                if (txData.Status == WithdrawStatus.Completed)
                    return (withdrawalId, txData.TxId);

                if (txData.Status == WithdrawStatus.Cancelled
                || txData.Status == WithdrawStatus.Failure
                || txData.Status == WithdrawStatus.Rejected)
                    throw new WithdrawalFailedException(txData.Id, txData.Status);
            }

            Thread.Sleep(10_000);
            limit--;
        }
    }

    /// <summary>
    /// Request withdrawal and wait untill assets leave Binance
    /// </summary>
    /// <param name="client"></param>
    /// <param name="coin">asset name.</param>
    /// <param name="amount">amount to be withdrawn (fees not included)</param>
    /// <param name="address">destination address</param>
    /// <param name="network">spicify the network</param>
    /// <param name="addressTag">sSecondary address identifier for coins like XRP,XMR etc.</param>
    /// <param name="walletType">The wallet type for withdraw，0-spot wallet ，1-funding wallet. Default walletType is the current "selected wallet" under wallet->Fiat and Spot/Funding->Deposit</param>
    /// <param name="cancellationToken"></param>
    /// <returns>Withdrawal Id and Transaction hash.</returns>
    /// <exception cref="RequestNotSuccessfulException"></exception>
    /// <exception cref="WithdrawalFailedException"></exception>
    public static async Task<(string, string)> WithdrawAndWaitForSentAsync(
        this BinanceClient client,
        string coin,
        decimal amount,
        string address,
        string network,
        string? addressTag = null,
        int walletType = -1)
    {
        var withdrawalId = await client.WithdrawCurrencyAsync(coin, amount, address, network, addressTag, walletType);

        var limit = 500;
        while (true)
        {
            if (limit == 0)
                throw new Exception("Timeout for awaiting transaction completion was hit");

            var history = await client.GetWithdrawHistoryAsync();

            if(history.Any())
            {
                var txData = history.Where(x => x.Id == withdrawalId).Single();

                client.Message(txData.Status.ToString());

                if (txData.Status == WithdrawStatus.Completed)
                    return (withdrawalId, txData.TxId);

                if (txData.Status == WithdrawStatus.Cancelled
                || txData.Status == WithdrawStatus.Failure
                || txData.Status == WithdrawStatus.Rejected)
                    throw new WithdrawalFailedException(txData.Id, txData.Status);
            }

            await Task.Delay(10_000);
            limit--;
        }
    }

    //public static async Task<decimal> GetCoinFreeAmountAsync(this BinanceClient client, string coin)
    //{

    //}



    /// <summary>
    /// Get information of coin.
    /// </summary>
    /// <param name="client"></param>
    /// <param name="name">Coin name.</param>
    /// <param name="network">Network name (optional).</param>
    /// <returns>Information on a coin.</returns>
    public static CoinInformation GetCoinInformation(this BinanceClient client, string name, string? network = null)
    {
        var infos = client.GetAllCoinsInformation();

        var info = infos.Where(x => x.Coin.ToLower() == name.ToLower()).Single();

        if (network is not null)
            info.NetworkList = info.NetworkList.Where(x => x.NetworkName.ToLower() == network.ToLower()).ToList();

        return info;
    }


    /// <summary>
    /// Get information of coin.
    /// </summary>
    /// <param name="client"></param>
    /// <param name="name">Coin name.</param>
    /// <param name="network">Network name (optional).</param>
    /// <returns>Information on a coin.</returns>
    public static async Task<CoinInformation> GetCoinInformationAsync(this BinanceClient client, string name, string? network = null)
    {
        var infos = await client.GetAllCoinsInformationAsync();

        var info = infos.Where(x => x.Coin.ToLower() == name.ToLower()).Single();

        if (network is not null)
            info.NetworkList = info.NetworkList.Where(x => x.NetworkName.ToLower() == network.ToLower()).ToList();

        return info;
    }
    


    /// <summary>
    /// Request withdrawal of all free asset balance and wait untill it leave Binance
    /// </summary>
    /// <param name="client"></param>
    /// <param name="coin">asset name.</param>
    /// <param name="address">destination address</param>
    /// <param name="network">spicify the network</param>
    /// <param name="addressTag">sSecondary address identifier for coins like XRP,XMR etc.</param>
    /// <param name="walletType">The wallet type for withdraw，0-spot wallet ，1-funding wallet. Default walletType is the current "selected wallet" under wallet->Fiat and Spot/Funding->Deposit</param>
    /// <returns>transaction hashs</returns>
    /// <exception cref="RequestNotSuccessfulException"></exception>
    /// <exception cref="WithdrawalFailedException"></exception>
    public static string WithdrawAllCoinBalanceAndWaitForSent(
        this BinanceClient client,
        string coin,
        string address,
        string network,
        string? addressTag = null,
        int walletType = -1)
    {
        var asset = client.GetUserAsset(coin);

        var info = client.GetCoinInformation(coin, network);

        var decimals = info.NetworkList.Single().WithdrawIntegerMultiple;

        var rounding = 1 / decimals;

        var amount = Math.Floor(asset.Free * rounding) / rounding;

        client.Message($"Starting withdrawal of {amount} {coin}");

        return client.WithdrawAndWaitForSent(coin, amount, address, network, addressTag, walletType);
    }


    /// <summary>
    /// Request withdrawal of all free asset balance and wait untill it leave Binance
    /// </summary>
    /// <param name="client"></param>
    /// <param name="coin">asset name.</param>
    /// <param name="address">destination address</param>
    /// <param name="network">spicify the network</param>
    /// <param name="addressTag">sSecondary address identifier for coins like XRP,XMR etc.</param>
    /// <param name="walletType">The wallet type for withdraw，0-spot wallet ，1-funding wallet. Default walletType is the current "selected wallet" under wallet->Fiat and Spot/Funding->Deposit</param>
    /// <returns>transaction hashs</returns>
    /// <exception cref="RequestNotSuccessfulException"></exception>
    /// <exception cref="WithdrawalFailedException"></exception>
    public static async Task<string> WithdrawAllCoinBalanceAndWaitForSentAsync(
        this BinanceClient client,
        string coin,
        string address,
        string network,
        string? addressTag = null,
        int walletType = -1)
    {
        var asset = await client.GetUserAssetAsync(coin);

        var info = await client.GetCoinInformationAsync(coin, network);

        var decimals = info.NetworkList.Single().WithdrawIntegerMultiple;

        var rounding = 1 / decimals;

        var amount = Math.Floor(asset.Free * rounding) / rounding;

        client.Message($"Starting withdrawal of {amount} {coin}");

        return await client.WithdrawAndWaitForSentAsync(coin, amount, address, network, addressTag, walletType);
    }

    #endregion Derived Methods
}

