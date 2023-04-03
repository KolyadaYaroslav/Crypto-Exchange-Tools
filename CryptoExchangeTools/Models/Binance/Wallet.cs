using System;
namespace CryptoExchangeTools.Models.Binance.Wallet;

public class AssetDetail
{
    public AssetDetail(string minWithdrawAmount, bool depositStatus, string withdrawFee, bool withdrawStatus, string? depositTip)
    {
        this.minWithdrawAmount = decimal.Parse(minWithdrawAmount);
        this.depositStatus = depositStatus;
        this.withdrawFee = decimal.Parse(withdrawFee);
        this.withdrawStatus = withdrawStatus;
        this.depositTip = depositTip;
    }

    /// <summary>
    /// min withdraw amount
    /// </summary>
    public required decimal minWithdrawAmount { get; set; }

    /// <summary>
    /// deposit status (false if ALL of networks' are false)
    /// </summary>
    public required bool depositStatus { get; set; }

    /// <summary>
    /// withdraw fee
    /// </summary>
    public required decimal withdrawFee { get; set; }

    /// <summary>
    /// withdraw status (false if ALL of networks' are false)
    /// </summary>
    public required bool withdrawStatus { get; set; }

    /// <summary>
    /// reason
    /// </summary>
    public string? depositTip { get; set; }
}


public class CoinInformation
{
    public CoinInformation(string coin, bool depositAllEnable, string free, string freeze, string ipoable, string ipoing, bool isLegalMoney, string locked, string name, List<Network> networkList)
    {
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

    public required string coin { get; set; }
    public required bool depositAllEnable { get; set; }
    public required decimal free { get; set; }
    public required decimal freeze { get; set; }
    public required decimal ipoable { get; set; }
    public required decimal ipoing { get; set; }
    public required bool isLegalMoney { get; set; }
    public required decimal locked { get; set; }
    public required string name { get; set; }
    public required List<Network> networkList { get; set; }

    public class Network
    {
        public Network(string addressRegex, string coin, string? depositDesc, bool depositEnable, bool isDefault, string memoRegex, int minConfirm, string name, string network, bool resetAddressStatus, string specialTips, int unLockConfirm, string? withdrawDesc, bool withdrawEnable, string withdrawFee, string withdrawIntegerMultiple, string withdrawMax, string withdrawMin, bool sameAddress, string estimatedArrivalTime, bool busy)
        {
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

        public required string addressRegex { get; set; }
        public required string coin { get; set; }

        /// <summary>
        /// shown only when "depositEnable" is false.
        /// </summary>
        public string? depositDesc { get; set; }
        public required bool depositEnable { get; set; }
        public required bool isDefault { get; set; }
        public required string memoRegex { get; set; }

        /// <summary>
        /// min number for balance confirmation
        /// </summary>
        public required int minConfirm { get; set; }
        public required string name { get; set; }
        public required string network { get; set; }
        public required bool resetAddressStatus { get; set; }
        public required string specialTips { get; set; }

        /// <summary>
        /// confirmation number for balance unlock 
        /// </summary>
        public required int unLockConfirm { get; set; }
        public string? withdrawDesc { get; set; }
        public required bool withdrawEnable { get; set; }
        public required decimal withdrawFee { get; set; }
        public required decimal withdrawIntegerMultiple { get; set; }
        public required decimal withdrawMax { get; set; }
        public required decimal withdrawMin { get; set; }

        /// <summary>
        /// If the coin needs to provide memo to withdraw
        /// </summary>
        public required bool sameAddress { get; set; }
        public required decimal estimatedArrivalTime { get; set; }
        public required bool busy { get; set; }
    }

    public class WithdrawHistoryRecord
    {
        public WithdrawHistoryRecord(string id, string amount, string transactionFee, string coin, int status, string address, string txId, string applyTime, string network, int transferType, string? withdrawOrderId, string? info, int confirmNo, int walletType, string? txKey, string? completeTime)
        {
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
        public required string id { get; set; }

        /// <summary>
        /// withdrawal amount
        /// </summary>
        public required decimal amount { get; set; }

        /// <summary>
        /// transaction fee
        /// </summary>
        public required decimal transactionFee { get; set; }
        public required string coin { get; set; }
        public required WithdrawStatus status { get; set; }
        public required string address { get; set; }

        /// <summary>
        /// withdrawal transaction id
        /// </summary>
        public required string txId { get; set; }

        /// <summary>
        /// UTC time
        /// </summary>
        public required string applyTime { get; set; }
        public required string network { get; set; }

        /// <summary>
        /// 1 for internal transfer, 0 for external transfer
        /// </summary>
        public required int transferType { get; set; }

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
        public required int confirmNo { get; set; }

        /// <summary>
        /// 1: Funding Wallet 0:Spot Wallet
        /// </summary>
        public required int walletType { get; set; }
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

