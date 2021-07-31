using System.Security.Cryptography;

namespace System
{
    public static class DateTimeCustomExtension
    {
        public static DateTime RandomTimeMorning(this DateTime date)
        {
            var hours = RandomNumberGenerator.GetInt32(7, 12);
            var minutes = RandomNumberGenerator.GetInt32(0, 59);
            
            var ts = new TimeSpan(hours, minutes, seconds: 0);

            return date.Date + ts;
        }
    }
}
