# Crypto-Exchange-Tools
A C# library to help interact with Centralized and Desentralized Crypto Exchanges

# Usage
## Withdrawing from binance example
// set up binance
var client = new BinanceClient(api, secret);

//request withdraw and wait untill it's executed by binance
string address = "{some wallet addreess}";
var txHash = await client.WithdrawAndWaitForSentAsync("USDT", 100m, "", "TRX");

Console.WriteLine($"Transaction completed, hash: {txHash}");
