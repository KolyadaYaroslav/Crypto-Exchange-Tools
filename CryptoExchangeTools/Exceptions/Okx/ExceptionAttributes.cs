using System.Reflection;
using CryptoExchangeTools.Utility;

namespace CryptoExchangeTools.Exceptions.Okx;

[AttributeUsage(AttributeTargets.Class)]
public class ErrorCode : System.Attribute
{
    public int Code;

    public ErrorCode(int code)
    {
        Code = code;
    }

    public static bool HasAttribute(Type t)
    {
        return t
            .GetCustomAttributes()
            .Where(x => x is ErrorCode)
            .Any();
    }

    public static List<Type> GetCodedErrors()
    {
        var types = ReflectionUtilities.GetTypesFromNamespace("CryptoExchangeTools.Exceptions.Okx");

        var exceptions = types
            .Where(x => x.GetCustomAttributes(false)
                .Where(x => x is ErrorCode)
                .Any());

        return exceptions.ToList();
    }
}

