namespace CleanArchitecture.ApplicationCore.Utilities
{
    public static class Helper
    {
        public static DateTime ToDateTime(this long timeStamp)
        {
            return DateTimeOffset.FromUnixTimeSeconds(timeStamp).UtcDateTime;
        }
    }
}
