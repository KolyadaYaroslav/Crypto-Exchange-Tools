using System;
using CryptoExchangeTools.Models.ICex;

namespace CryptoExchangeTools;

public interface ICexClient : IDisposable
{
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
    /// Invokes when client emits a message. E.g. Withdrawal status change.
    /// </summary>
    event EventHandler<string>? OnMessage;

    new void Dispose();
}

