using Newtonsoft.Json;
using CryptoExchangeTools;

namespace CryptoExchangeTools.Models.Binance;

public class IsolatedMarginFeeData
{
    [JsonProperty("vipLevel")]
    public long VipLevel { get; set; }

    [JsonProperty("symbol")]
    public required string Symbol { get; set; }

    [JsonProperty("leverage")]
    [JsonConverter(typeof(StringToLongConverter))]
    public long Leverage { get; set; }

    [JsonProperty("data")]
    public required Datum[] Data { get; set; }

    public partial class Datum
    {
        [JsonProperty("coin")]
        public required string Coin { get; set; }

        [JsonProperty("dailyInterest")]
        [JsonConverter(typeof(StringToDecimalConverter))]
        public decimal DailyInterest { get; set; }

        [JsonProperty("borrowLimit")]
        [JsonConverter(typeof(StringToLongConverter))]
        public long BorrowLimit { get; set; }
    }
}

