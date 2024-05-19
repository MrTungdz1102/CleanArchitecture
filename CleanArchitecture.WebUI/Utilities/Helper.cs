namespace CleanArchitecture.WebUI.Utilities
{
    public static class Helper
    {
        public static long ToUnixTime(this DateTime datetime)
        {
            long epochTicks = new DateTime(1970, 1, 1).Ticks;
            return ((datetime.Ticks - epochTicks) / TimeSpan.TicksPerSecond);
        }
    }
}
