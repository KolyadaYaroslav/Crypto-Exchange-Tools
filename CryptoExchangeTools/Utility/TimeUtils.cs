namespace CryptoExchangeTools.Utility;

public class TimeUtils
{
	public static DateTime UnixTimeStampToDateTime(long unixTimeStampSeconds, UnixTimeStampFormat unixTimeStampFormat = UnixTimeStampFormat.Seconds)
	{
		if (unixTimeStampFormat == UnixTimeStampFormat.Milliseconds)
			unixTimeStampSeconds /= 1000;
		
		var dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
		return dateTime.AddSeconds(unixTimeStampSeconds).ToLocalTime();
	}
	
	public static long DateTimeToUnixTimeStamp(DateTime dateTime, UnixTimeStampFormat unixTimeStampFormat = UnixTimeStampFormat.Seconds)
	{
		var startTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
		var diff = dateTime - startTime;

		var unixTimeStampSeconds = (long)diff.TotalSeconds;
		
		if (unixTimeStampFormat == UnixTimeStampFormat.Milliseconds)
			unixTimeStampSeconds *= 1000;

		return unixTimeStampSeconds;
	}
}

public enum UnixTimeStampFormat
{
	Seconds,
	Milliseconds
}