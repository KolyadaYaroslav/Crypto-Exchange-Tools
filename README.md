# Crypto-Exchange-Tools
A C# library to help interact with Centralized and Desentralized Crypto Exchanges

## Usage
### Withdrawing from binance example
```C#
string apiKey = "{Your_Api_Key}";
string secret = "{Your_Api_Secret}";

// set up binance
var client = new BinanceClient(api, secret);

//request withdraw and wait untill it's executed by binance
string address = "{some wallet addreess}";
var txHash = await client.WithdrawAndWaitForSentAsync("USDT", 100m, address, "TRX");

Console.WriteLine($"Transaction completed, hash: {txHash}");
```


### Get information on a coin example
```C#
//fetch info
var info = client.GetAssetDetail("ETH");
Console.WriteLine($"{info.depositStatus}, {info.withdrawFee}");
```
