using System;
using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CryptoExchangeTools;

internal class StringToLongConverter : JsonConverter
{
    public override bool CanConvert(Type t) => t == typeof(long) || t == typeof(long?);

    public override object? ReadJson(JsonReader reader, Type t, object? existingValue, JsonSerializer serializer)
    {
        CultureInfo.DefaultThreadCurrentCulture = CultureInfo.GetCultureInfo("en-US");

        if (reader.TokenType == JsonToken.Null) return null;
        var value = serializer.Deserialize<string>(reader);
        long l;

        if (Int64.TryParse(value, out l))
        {
            return l;
        }

        throw new Exception("Cannot unmarshal type long");
    }

    public override void WriteJson(JsonWriter writer, object? untypedValue, JsonSerializer serializer)
    {
        CultureInfo.DefaultThreadCurrentCulture = CultureInfo.GetCultureInfo("en-US");

        if (untypedValue == null)
        {
            serializer.Serialize(writer, null);
            return;
        }

        var value = (long)untypedValue;
        serializer.Serialize(writer, value.ToString());

        return;
    }

    public static readonly StringToLongConverter Singleton = new StringToLongConverter();
}

internal class StringToDecimalConverter : JsonConverter
{
    public override bool CanConvert(Type t) => t == typeof(decimal) || t == typeof(decimal?);

    public override object? ReadJson(JsonReader reader, Type t, object? existingValue, JsonSerializer serializer)
    {
        CultureInfo.DefaultThreadCurrentCulture = CultureInfo.GetCultureInfo("en-US");

        if (reader.TokenType == JsonToken.Null) return null;
        var value = serializer.Deserialize<string>(reader);
        decimal d;

        if (decimal.TryParse(value, out d))
        {
            return d;
        }
        else
        {
            return 0m;
        }

        throw new Exception("Cannot unmarshal type decimal");
    }

    public override void WriteJson(JsonWriter writer, object? untypedValue, JsonSerializer serializer)
    {
        CultureInfo.DefaultThreadCurrentCulture = CultureInfo.GetCultureInfo("en-US");

        if (untypedValue == null)
        {
            serializer.Serialize(writer, null);
            return;
        }

        var value = (decimal)untypedValue;
        serializer.Serialize(writer, value.ToString());

        return;
    }

    public static readonly StringToDecimalConverter Singleton = new StringToDecimalConverter();
}

internal class StringToIntConverter : JsonConverter
{
    public override bool CanConvert(Type t) => t == typeof(int) || t == typeof(int?);

    public override object? ReadJson(JsonReader reader, Type t, object? existingValue, JsonSerializer serializer)
    {
        CultureInfo.DefaultThreadCurrentCulture = CultureInfo.GetCultureInfo("en-US");

        if (reader.TokenType == JsonToken.Null) return null;
        var value = serializer.Deserialize<string>(reader);
        int i;

        if (int.TryParse(value, out i))
        {
            return i;
        }

        throw new Exception("Cannot unmarshal type int");
    }

    public override void WriteJson(JsonWriter writer, object? untypedValue, JsonSerializer serializer)
    {
        CultureInfo.DefaultThreadCurrentCulture = CultureInfo.GetCultureInfo("en-US");

        if (untypedValue == null)
        {
            serializer.Serialize(writer, null);
            return;
        }

        var value = (int)untypedValue;
        serializer.Serialize(writer, value.ToString());

        return;
    }

    public static readonly StringToIntConverter Singleton = new StringToIntConverter();
}

internal class StringToBoolConverter : JsonConverter
{
    public override bool CanConvert(Type t) => t == typeof(bool) || t == typeof(bool?);

    public override object? ReadJson(JsonReader reader, Type t, object? existingValue, JsonSerializer serializer)
    {
        CultureInfo.DefaultThreadCurrentCulture = CultureInfo.GetCultureInfo("en-US");

        if (reader.TokenType == JsonToken.Null) return null;
        var value = serializer.Deserialize<string>(reader);
        bool b;

        if (bool.TryParse(value, out b))
        {
            return b;
        }

        throw new Exception("Cannot unmarshal type bool");
    }

    public override void WriteJson(JsonWriter writer, object? untypedValue, JsonSerializer serializer)
    {
        CultureInfo.DefaultThreadCurrentCulture = CultureInfo.GetCultureInfo("en-US");

        if (untypedValue == null)
        {
            serializer.Serialize(writer, null);
            return;
        }

        var value = (bool)untypedValue;
        serializer.Serialize(writer, value.ToString());

        return;
    }

    public static readonly StringToBoolConverter Singleton = new StringToBoolConverter();
}

internal class IntToBoolConverter : JsonConverter
{
    public override bool CanConvert(Type t) => t == typeof(bool) || t == typeof(bool?);

    public override object? ReadJson(JsonReader reader, Type t, object? existingValue, JsonSerializer serializer)
    {
        CultureInfo.DefaultThreadCurrentCulture = CultureInfo.GetCultureInfo("en-US");

        if (reader.TokenType == JsonToken.Null) return null;
        var value = serializer.Deserialize<int>(reader);

        if (value == 1)
        {
            return true;
        }
        else if (value == 0)
        {
            return false;
        }
        else
        {
            throw new Exception("Cannot unmarshal type bool");
        }
    }

    public override void WriteJson(JsonWriter writer, object? untypedValue, JsonSerializer serializer)
    {
        CultureInfo.DefaultThreadCurrentCulture = CultureInfo.GetCultureInfo("en-US");

        if (untypedValue == null)
        {
            serializer.Serialize(writer, null);
            return;
        }

        var value = (bool)untypedValue;
        serializer.Serialize(writer, value.ToString());

        return;
    }

    public static readonly IntToBoolConverter Singleton = new IntToBoolConverter();
}

