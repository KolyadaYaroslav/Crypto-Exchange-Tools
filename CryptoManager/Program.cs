using System.Globalization;
using System.Net;
using CryptoExchangeTools;
using CryptoExchangeTools.BinanceRequests.Wallet;
using CryptoManager;


if (!args.Any())
    Console.WriteLine("No arguments were provided.");

else
    await Script.Run(args);

Console.WriteLine("Finished.");