using System;
using System.Runtime.InteropServices;

namespace CryptoExchangeTools;

internal class PathUtils
{
    internal static string BuildPath(string endPath)
    {
        var result = "";

        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            var appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            result = Path.Combine(appDataPath, "Crypto Exchange Tools", endPath);
        }
        else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
        {
            throw new PlatformNotSupportedException();
        }
        else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
        {
            var appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            result = Path.Combine(appDataPath, "Crypto Exchange Tools", endPath);
        }
        else
        {
            throw new Exception("Can't define platform.");
        }

        var dir = Directory.GetParent(result) ?? throw new Exception("dir is null");
        Directory.CreateDirectory(dir.FullName);

        return result;
    }
}

