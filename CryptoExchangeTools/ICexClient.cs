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

    /// <summary>
    /// Get Latest market price of a ticker on exchange.
    /// </summary>
    /// <param name="symbol">ticker</param>
    /// <returns></returns>
    decimal GetMarketPrice(string symbol);

    /// <summary>
    /// Get Latest market price of a ticker on exchange.
    /// </summary>
    /// <param name="symbol">ticker</param>
    /// <returns></returns>
    Task<decimal> GetMarketPriceAsync(string symbol);

    /// <summary>
    /// Determine how much of first currency you need to trade to get desired amount of second currency on SPOT market.
    /// </summary>
    /// <param name="currencyIn">Currency you want to trade.</param>
    /// <param name="currencyOut">Currency you want to trade for.</param>
    /// /// <param name="amountOut">Desired amount out to receive.</param>
    /// /// <param name="slippage">Fees and price effect tollerance, defaults to 0.2%</param>
    /// <returns></returns>
    decimal GetAmountIn(string currencyIn, string currencyOut, decimal amountOut, decimal slippage = 0.998m);

    /// <summary>
    /// Determine how much of first currency you need to trade to get desired amount of second currency on SPOT market.
    /// </summary>
    /// <param name="currencyIn">Currency you want to trade.</param>
    /// <param name="currencyOut">Currency you want to trade for.</param>
    /// /// <param name="amountOut">Desired amount out to receive.</param>
    /// /// <param name="slippage">Fees and price effect tollerance, defaults to 0.2%</param>
    /// <returns></returns>
    Task<decimal> GetAmountInAsync(string currencyIn, string currencyOut, decimal amountOut, decimal slippage = 0.998m);

    /// <summary>
    /// Flatten order mount according to exchange trading filters.
    /// </summary>
    /// <param name="symbol">Trading ticekr</param>
    /// <param name="amount">Initial amount</param>
    /// <param name="stepSizeDown">Determines by how many trading steps the resulting amount will be decremented.</param>
    /// <returns>Flattened amount.</returns>
    decimal FlattenOrderAmount(string symbol, decimal amount, int stepSizeDown = 0);

    /// <summary>
    /// Flatten order mount according to exchange trading filters.
    /// </summary>
    /// <param name="symbol">Trading ticekr</param>
    /// <param name="amount">Initial amount</param>
    /// <param name="stepSizeDown">Determines by how many trading steps the resulting amount will be decremented.</param>
    /// <returns>Flattened amount.</returns>
    Task<decimal> FlattenOrderAmountAsync(string symbol, decimal amount, int stepSizeDown = 0);

    /// <summary>
    /// Place an order that will return only after it is fully filled. If it is not filled for long time - it will create a new order to fill the remaining amount.
    /// </summary>
    /// <param name="baseCurrency"></param>
    /// <param name="quoteCurrency"></param>
    /// <param name="direction">Buy or sell.</param>
    /// <param name="amount">Order quantity.</param>
    /// <returns>First: Executed resulting quantity. Second: Consumed currency Quantity.</returns>
    (decimal, decimal) ForcedMarketOrder(string baseCurrency, string quoteCurrency, OrderDirection direction, decimal amount, CalculationBase calculationBase = CalculationBase.Base);

    /// <summary>
    /// Place an order that will return only after it is fully filled. If it is not filled for long time - it will create a new order to fill the remaining amount.
    /// </summary>
    /// <param name="baseCurrency"></param>
    /// <param name="quoteCurrency"></param>
    /// <param name="direction">Buy or sell.</param>
    /// <param name="amount">Order quantity.</param>
    /// <returns>First: Executed resulting quantity. Second: Consumed currency Quantity.</returns>
    Task<(decimal, decimal)> ForcedMarketOrderAsync(string baseCurrency, string quoteCurrency, OrderDirection direction, decimal amount, CalculationBase calculationBase = CalculationBase.Base);

    /// <summary>
    /// Calculates the minimum order amount.
    /// </summary>
    /// <param name="baseCurrency"></param>
    /// <param name="quoteCurrency"></param>
    /// <param name="calculationBase">In what currency to return the minimum order amount.</param>
    /// <returns></returns>
    decimal GetMinOrderSizeForPair(string baseCurrency, string quoteCurrency, CalculationBase calculationBase = CalculationBase.Base);

    /// <summary>
    /// Calculates the minimum order amount.
    /// </summary>
    /// <param name="baseCurrency"></param>
    /// <param name="quoteCurrency"></param>
    /// <param name="calculationBase">In what currency to return the minimum order amount.</param>
    /// <returns></returns>
    Task<decimal> GetMinOrderSizeForPairAsync(string baseCurrency, string quoteCurrency, CalculationBase calculationBase = CalculationBase.Base);

    new void Dispose();
}

