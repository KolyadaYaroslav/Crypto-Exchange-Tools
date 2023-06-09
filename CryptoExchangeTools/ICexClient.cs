using System;
using CryptoExchangeTools.Models.ICex;

namespace CryptoExchangeTools;

public interface ICexClient : IDisposable
{
    /// <summary>
    /// Invokes when client emits a message. E.g. Withdrawal status change.
    /// </summary>
    event EventHandler<string>? OnMessage;

    /// <summary>
    /// Request withdrawal from exchange.
    /// </summary>
    /// <param name="currency">Currency name, subject to exchange formating.</param>
    /// <param name="amount">Desired amount before fees.</param>
    /// <param name="address">Withdrawal address.</param>
    /// <param name="network">Network name, subject to exchange formating.</param>
    /// /// <param name="waitForApprove">Set to false not to wait for approval from exchange.</param>
    /// <returns></returns>
    WithdrawalRecord Withdraw(string currency, decimal amount, string address, string network, bool waitForApprove = true);

    /// <summary>
    /// Request withdrawal from exchange.
    /// </summary>
    /// <param name="currency">Currency name, subject to exchange formating.</param>
    /// <param name="amount">Desired amount before fees.</param>
    /// <param name="address">Withdrawal address.</param>
    /// <param name="network">Network name, subject to exchange formating.</param>
    /// /// <param name="waitForApprove">Set to false not to wait for approval from exchange.</param>
    Task<WithdrawalRecord> WithdrawAsync(string currency, decimal amount, string address, string network, bool waitForApprove = true);

    /// <summary>
    /// Get withdrawal fees for a specified currency on a specified network.
    /// </summary>
    /// <param name="currency">Currency name, subject to exchange formating.</param>
    /// <param name="network">Network name, subject to exchange formating.</param>
    /// <returns>Resulting fee.</returns>
    decimal GetWithdrawalFee(string currency, string network);

    /// <summary>
    /// Get withdrawal fees for a specified currency on a specified network.
    /// </summary>
    /// <param name="currency">Currency name, subject to exchange formating.</param>
    /// <param name="network">Network name, subject to exchange formating.</param>
    /// <returns>Resulting fee.</returns>
    Task<decimal> GetWithdrawalFeeAsync(string currency, string network);

    /// <summary>
    /// Get Currency withdraw precision as a power of ten.
    /// </summary>
    /// <param name="currency">Currency name, subject to exchange formating.</param>
    /// <param name="network">Network name, subject to exchange formating.</param>
    /// <returns>Resulting precision.</returns>
    int QueryWithdrawalPrecision(string currency, string network);

    /// <summary>
    /// Get Currency withdraw precision as a power of ten.
    /// </summary>
    /// <param name="currency">Currency name, subject to exchange formating.</param>
    /// <param name="network">Network name, subject to exchange formating.</param>
    /// <returns>Resulting precision.</returns>
    Task<int> QueryWithdrawalPrecisionAsync(string currency, string network);

    /// <summary>
    /// Get Currency minimum withdrawal amount for specific network.
    /// </summary>
    /// <param name="currency">Currency name, subject to exchange formating.</param>
    /// <param name="network">Network name, subject to exchange formating.</param>
    /// <returns>Resulting withdrawal minimum amount.</returns>
    decimal QueryWithdrawalMinAmount(string currency, string network);

    /// <summary>
    /// Get Currency minimum withdrawal amount for specific network.
    /// </summary>
    /// <param name="currency">Currency name, subject to exchange formating.</param>
    /// <param name="network">Network name, subject to exchange formating.</param>
    /// <returns>Resulting withdrawal minimum amount.</returns>
    Task<decimal> QueryWithdrawalMinAmountAsync(string currency, string network);

    /// <summary>
    /// Get Address for sepositing currency on the specified network.
    /// </summary>
    /// <param name="currency">Currency name, subject to exchange formating.</param>
    /// <param name="network">Network name, subject to exchange formating.</param>
    /// <returns>Deposit address.</returns>
    string GetDepositAddress(string currency, string network);

    /// <summary>
    /// Get Address for sepositing currency on the specified network.
    /// </summary>
    /// <param name="currency">Currency name, subject to exchange formating.</param>
    /// <param name="network">Network name, subject to exchange formating.</param>
    /// <returns>Deposit address.</returns>
    Task<string> GetDepositAddressAsync(string currency, string network);

    /// <summary>
    /// Wait for transaction to get deposited on cex.
    /// </summary>
    /// <param name="hash">Transaction hash.</param>
    /// <returns>Received amount.</returns>
    decimal ApproveReceiving(string hash);

    /// <summary>
    /// Wait for transaction to get deposited on cex.
    /// </summary>
    /// <param name="hash">Transaction hash.</param>
    /// <returns>Received amount.</returns>
    Task<decimal> ApproveReceivingAsync(string hash);

    /// <summary>
    /// Querry Free Balance of specified currency on the exchange.
    /// </summary>
    /// <param name="currency">Currency name, subject to exchange formating.</param>
    /// <returns></returns>
    decimal GetBalance(string currency);

    /// <summary>
    /// Querry Free Balance of specified currency on the exchange.
    /// </summary>
    /// <param name="currency">Currency name, subject to exchange formating.</param>
    /// <returns></returns>
    Task<decimal> GetBalanceAsync(string currency);


    new void Dispose();
}

