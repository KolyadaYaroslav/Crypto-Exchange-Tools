using System.Collections.Generic;

namespace CryptoExchangeTools.Utility;

public class ReflectionUtilities
{
	public static List<Type> GetTypesFromNamespace(string space)
	{
        List<Type> list = new();

        foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
        {
            var types = assembly.GetTypes();

            foreach (var t in types)
            {
                if (t.FullName is not null && t.FullName.Contains(space))
                    list.Add(t);
            }
        }

        return list;
    }
}

