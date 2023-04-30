using Newtonsoft.Json.Linq;

namespace CryptoExchangeTools.Models.Okx;

public class BaseResponse
{
    public BaseResponse(string code, JArray? data, string msg)
    {
        this.code = int.Parse(code);
        this.data = data;
        this.msg = msg;
    }

    public required int code { get; set; }
	public JArray? data { get; set; }
	public required string msg { get; set; }

    public T ParseData<T>()
    {
        if (data is null)
            throw new Exception("Response Data is null");

        var parsed = data.ToObject<T>()
            ?? throw new Exception($"Can't cast response data as {nameof(T)}");

        return parsed;
    }
}

