using CryptoExchangeTools.Models.Okx;
using RestSharp;

namespace CryptoExchangeTools.Requests.OkxRequests;

public class PublicData
{
    private readonly OkxClient Client;

    public PublicData(OkxClient client)
    {
        Client = client;
    }

    #region Original Methods

    #endregion Original Methods

    #region Get instruments

    /// <summary>
    /// Retrieve a list of instruments with open contracts.
    /// </summary>
    /// <param name="instType">Instrument type</param>
    /// <param name="uly">Underlying. Only applicable to FUTURES/SWAP/OPTION.If instType is OPTION, either uly or instFamily is required.</param>
    /// <param name="instFamily">Instrument family. Only applicable to FUTURES/SWAP/OPTION.If instType is OPTION, either uly or instFamily is required.</param>
    /// <param name="instId">Instrument ID</param>
    /// <returns></returns>
    public InstrumentInfo[] GetInstruments(
        InstrumentType instType,
        string? uly = null,
        string? instFamily = null,
        string? instId = null)
    {
        var request = BuildGetInstruments(instType, uly, instFamily, instId);

        return Client.ExecuteRequest<InstrumentInfo[]>(request, false);
    }

    /// <summary>
    /// Retrieve a list of instruments with open contracts.
    /// </summary>
    /// <param name="instType">Instrument type</param>
    /// <param name="uly">Underlying. Only applicable to FUTURES/SWAP/OPTION.If instType is OPTION, either uly or instFamily is required.</param>
    /// <param name="instFamily">Instrument family. Only applicable to FUTURES/SWAP/OPTION.If instType is OPTION, either uly or instFamily is required.</param>
    /// <param name="instId">Instrument ID</param>
    /// <returns></returns>
    public async Task<InstrumentInfo[]> GetInstrumentsAsync(
        InstrumentType instType,
        string? uly = null,
        string? instFamily = null,
        string? instId = null)
    {
        var request = BuildGetInstruments(instType, uly, instFamily, instId);

        return await Client.ExecuteRequestAsync<InstrumentInfo[]>(request, false);
    }

    private static RestRequest BuildGetInstruments(
        InstrumentType instType,
        string? uly = null,
        string? instFamily = null,
        string? instId = null)
    {
        var request = new RestRequest("api/v5/public/instruments", Method.Get);

        request.AddParameter("instType", instType.ToString());

        if(!string.IsNullOrEmpty(uly))
            request.AddParameter("uly", uly);

        if (!string.IsNullOrEmpty(instFamily))
            request.AddParameter("instFamily", instFamily);

        if (!string.IsNullOrEmpty(instId))
            request.AddParameter("instId", instId);

        return request;
    }

    #endregion Get instruments

    #region Derived Methods

    #region Get Instrument Name

    public string GetInstrumentName(string baseCurrency, string quoteCurrency, InstrumentType instType)
    {
        var info = GetInstruments(instType);

        var match = info.Where(x => x.BaseCcy.ToUpper() == baseCurrency.ToUpper() && x.QuoteCcy.ToUpper() == quoteCurrency.ToUpper());

        if (!match.Any())
            throw new Exception("Can't find instrument!");

        return match.Single().InstId;
    }

    public async Task<string> GetInstrumentNameAsync(string baseCurrency, string quoteCurrency, InstrumentType instType)
    {
        var info = await GetInstrumentsAsync(instType);

        var match = info.Where(x => x.BaseCcy.ToUpper() == baseCurrency.ToUpper() && x.QuoteCcy.ToUpper() == quoteCurrency.ToUpper());

        if (!match.Any())
            throw new Exception("Can't find instrument!");

        return match.Single().InstId;
    }

    #endregion Get Instrument Name

    #region Get Single Instrument

    public InstrumentInfo GetSingleInstrument(string symbol, InstrumentType instType)
    {
        return GetInstruments(instType, instId: symbol).Single();
    }

    public async Task<InstrumentInfo> GetSingleInstrumentAsync(string symbol, InstrumentType instType)
    {
        return (await GetInstrumentsAsync(instType, instId: symbol)).Single();
    }

    #endregion Get Single Instrument

    #endregion Derived Methods
}

