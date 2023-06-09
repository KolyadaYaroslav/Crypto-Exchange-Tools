using Newtonsoft.Json;

namespace CryptoExchangeTools.Models.Binance;

public enum TransferType
{
    SpotToUsdm = 1,
    UsdmToSpot = 2,
    SpotToCoinm = 3,
    CoinmToSpot = 4
}

public class TransferResult
{
    [JsonProperty("tranId")]
    [JsonConverter(typeof(StringToLongConverter))]
    public long TransactionId { get; set; }
}