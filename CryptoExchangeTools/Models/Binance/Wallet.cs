using System;
using System.Globalization;

namespace CryptoExchangeTools.Models.Binance.Wallet;

public class AssetDetail
{

    public AssetDetail(string minWithdrawAmount, bool depositStatus, string withdrawFee, bool withdrawStatus, string? depositTip)
    {
        CultureInfo.DefaultThreadCurrentCulture = CultureInfo.GetCultureInfo("en-US");

        this.minWithdrawAmount = decimal.Parse(minWithdrawAmount);
        this.depositStatus = depositStatus;
        this.withdrawFee = decimal.Parse(withdrawFee);
        this.withdrawStatus = withdrawStatus;
        this.depositTip = depositTip;
    }

    /// <summary>
    /// min withdraw amount
    /// </summary>
    public decimal minWithdrawAmount { get; set; }

    /// <summary>
    /// deposit status (false if ALL of networks' are false)
    /// </summary>
    public bool depositStatus { get; set; }

    /// <summary>
    /// withdraw fee
    /// </summary>
    public decimal withdrawFee { get; set; }

    /// <summary>
    /// withdraw status (false if ALL of networks' are false)
    /// </summary>
    public bool withdrawStatus { get; set; }

    /// <summary>
    /// reason
    /// </summary>
    public string? depositTip { get; set; }
}


public class CoinInformation
{
    public CoinInformation(string coin, bool depositAllEnable, string free, string freeze, string ipoable, string ipoing, bool isLegalMoney, string locked, string name, List<Network> networkList)
    {
        CultureInfo.DefaultThreadCurrentCulture = CultureInfo.GetCultureInfo("en-US");

        this.coin = coin;
        this.depositAllEnable = depositAllEnable;
        this.free = decimal.Parse(free);
        this.freeze = decimal.Parse(freeze);
        this.ipoable = decimal.Parse(ipoable);
        this.ipoing = decimal.Parse(ipoing);
        this.isLegalMoney = isLegalMoney;
        this.locked = decimal.Parse(locked);
        this.name = name;
        this.networkList = networkList;
    }

    public string coin { get; set; }
    public bool depositAllEnable { get; set; }
    public decimal free { get; set; }
    public decimal freeze { get; set; }
    public decimal ipoable { get; set; }
    public decimal ipoing { get; set; }
    public bool isLegalMoney { get; set; }
    public decimal locked { get; set; }
    public string name { get; set; }
    public List<Network> networkList { get; set; }

    public class Network
    {
        public Network(string addressRegex, string coin, string? depositDesc, bool depositEnable, bool isDefault, string memoRegex, int minConfirm, string name, string network, bool resetAddressStatus, string specialTips, int unLockConfirm, string? withdrawDesc, bool withdrawEnable, string withdrawFee, string withdrawIntegerMultiple, string withdrawMax, string withdrawMin, bool sameAddress, string estimatedArrivalTime, bool busy)
        {
            CultureInfo.DefaultThreadCurrentCulture = CultureInfo.GetCultureInfo("en-US");

            this.addressRegex = addressRegex;
            this.coin = coin;
            this.depositDesc = depositDesc;
            this.depositEnable = depositEnable;
            this.isDefault = isDefault;
            this.memoRegex = memoRegex;
            this.minConfirm = minConfirm;
            this.name = name;
            this.network = network;
            this.resetAddressStatus = resetAddressStatus;
            this.specialTips = specialTips;
            this.unLockConfirm = unLockConfirm;
            this.withdrawDesc = withdrawDesc;
            this.withdrawEnable = withdrawEnable;
            this.withdrawFee = decimal.Parse(withdrawFee);
            this.withdrawIntegerMultiple = decimal.Parse(withdrawIntegerMultiple);
            this.withdrawMax = decimal.Parse(withdrawMax);
            this.withdrawMin = decimal.Parse(withdrawMin);
            this.sameAddress = sameAddress;
            this.estimatedArrivalTime = decimal.Parse(estimatedArrivalTime);
            this.busy = busy;
        }

        public string addressRegex { get; set; }
        public string coin { get; set; }

        /// <summary>
        /// shown only when "depositEnable" is false.
        /// </summary>
        public string? depositDesc { get; set; }
        public bool depositEnable { get; set; }
        public bool isDefault { get; set; }
        public string memoRegex { get; set; }

        /// <summary>
        /// min number for balance confirmation
        /// </summary>
        public int minConfirm { get; set; }
        public string name { get; set; }
        public string network { get; set; }
        public bool resetAddressStatus { get; set; }
        public string specialTips { get; set; }

        /// <summary>
        /// confirmation number for balance unlock 
        /// </summary>
        public int unLockConfirm { get; set; }
        public string? withdrawDesc { get; set; }
        public bool withdrawEnable { get; set; }
        public decimal withdrawFee { get; set; }
        public decimal withdrawIntegerMultiple { get; set; }
        public decimal withdrawMax { get; set; }
        public decimal withdrawMin { get; set; }

        /// <summary>
        /// If the coin needs to provide memo to withdraw
        /// </summary>
        public bool sameAddress { get; set; }
        public decimal estimatedArrivalTime { get; set; }
        public bool busy { get; set; }
    }

    public class WithdrawHistoryRecord
    {
        public WithdrawHistoryRecord(string id, string amount, string transactionFee, string coin, int status, string address, string txId, string applyTime, string network, int transferType, string? withdrawOrderId, string? info, int confirmNo, int walletType, string? txKey, string? completeTime)
        {
            CultureInfo.DefaultThreadCurrentCulture = CultureInfo.GetCultureInfo("en-US");

            this.id = id;
            this.amount = decimal.Parse(amount);
            this.transactionFee = decimal.Parse(transactionFee);
            this.coin = coin;
            this.status = (WithdrawStatus)status;
            this.address = address;
            this.txId = txId;
            this.applyTime = applyTime;
            this.network = network;
            this.transferType = transferType;
            this.withdrawOrderId = withdrawOrderId;
            this.info = info;
            this.confirmNo = confirmNo;
            this.walletType = walletType;
            this.txKey = txKey;
            this.completeTime = completeTime;
        }

        /// <summary>
        /// Withdrawal id in Binance
        /// </summary>
        public string id { get; set; }

        /// <summary>
        /// withdrawal amount
        /// </summary>
        public decimal amount { get; set; }

        /// <summary>
        /// transaction fee
        /// </summary>
        public decimal transactionFee { get; set; }
        public string coin { get; set; }
        public WithdrawStatus status { get; set; }
        public string address { get; set; }

        /// <summary>
        /// withdrawal transaction id
        /// </summary>
        public string txId { get; set; }

        /// <summary>
        /// UTC time
        /// </summary>
        public string applyTime { get; set; }
        public string network { get; set; }

        /// <summary>
        /// 1 for internal transfer, 0 for external transfer
        /// </summary>
        public int transferType { get; set; }

        /// <summary>
        /// will not be returned if there's no withdrawOrderId for this withdraw.
        /// </summary>
        public string? withdrawOrderId { get; set; }

        /// <summary>
        /// reason for withdrawal failure
        /// </summary>
        public string? info { get; set; }

        /// <summary>
        /// confirm times for withdraw
        /// </summary>
        public int confirmNo { get; set; }

        /// <summary>
        /// 1: Funding Wallet 0:Spot Wallet
        /// </summary>
        public int walletType { get; set; }
        public string? txKey { get; set; }

        /// <summary>
        /// complete UTC time when user's asset is deduct from withdrawing, only if status =  6(success)
        /// </summary>
        public string? completeTime { get; set; }

        public enum WithdrawStatus
        {
            Email_Sent,
            Cancelled,
            Awaiting_Approval,
            Rejected,
            Processing,
            Failure,
            Completed
        }
    }
}

public class UserAsset
{
    public UserAsset(string asset, string free, string freeze, string withdrawing, string ipoable, string btcValuation)
    {
        CultureInfo.DefaultThreadCurrentCulture = CultureInfo.GetCultureInfo("en-US");

        this.asset = asset;
        this.free = decimal.Parse(free);
        this.freeze = decimal.Parse(freeze);
        this.withdrawing = decimal.Parse(withdrawing);
        this.ipoable = decimal.Parse(ipoable);
        this.btcValuation = decimal.Parse(btcValuation);
    }

    public string asset { get; set; }
    public decimal free { get; set; }
    public decimal freeze { get; set; }
    public decimal withdrawing { get; set; }
    public decimal ipoable { get; set; }
    public decimal btcValuation { get; set; }
}


public class DepositHistory
{
    public DepositHistory(string id, string amount, string coin, string network, int status, string address, string addressTag, string txId, long insertTime, int transferType, string confirmTimes, int unlockConfirm, int walletType)
    {
        CultureInfo.DefaultThreadCurrentCulture = CultureInfo.GetCultureInfo("en-US");

        this.id = long.Parse(id);
        this.amount = decimal.Parse(amount);
        this.coin = coin;
        this.network = network;
        this.status = (DepositStatus)status;
        this.address = address;
        this.addressTag = addressTag;
        this.txId = txId;
        this.insertTime = insertTime;
        this.transferType = transferType;
        this.confirmTimes = confirmTimes;
        this.unlockConfirm = unlockConfirm;
        this.walletType = walletType;
    }

    public long id { get; set; }
    public decimal amount { get; set; }
    public string coin { get; set; }
    public string network { get; set; }
    public DepositStatus status { get; set; }
    public string address { get; set; }
    public string addressTag { get; set; }
    public string txId { get; set; }
    public long insertTime { get; set; }
    public int transferType { get; set; }
    public string confirmTimes { get; set; }
    public int unlockConfirm { get; set; }
    public int walletType { get; set; }

    public enum DepositStatus
    {
        pending = 0,
        success = 1,
        creditedButCannotWithdraw = 6,
        WrongDeposit = 7,
        WaitingUserConfirm = 8
    }
}