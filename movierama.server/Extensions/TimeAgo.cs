using System;

namespace ExtensionMethods
{
    public static class MyExtensions
    {
        public static (int, string) TimeAgo(this DateTime dateTime)
        {
            int duration = 0;
            string unit = string.Empty;

            var timeSpan = DateTime.Now.Subtract(dateTime);
            if (timeSpan.TotalSeconds < 0)
                timeSpan = dateTime.Subtract(DateTime.Now);

            if (timeSpan <= TimeSpan.FromSeconds(60))
            {
                duration = timeSpan.Seconds;
                unit = "seconds";
            }
            else if (timeSpan <= TimeSpan.FromMinutes(60))
            {
                duration = timeSpan.Minutes;
                unit = "minutes";
            }
            else if (timeSpan <= TimeSpan.FromHours(24))
            {
                duration = timeSpan.Hours;
                unit = "hours";
            }
            else if (timeSpan <= TimeSpan.FromDays(30))
            {
                duration = timeSpan.Days;
                unit = "days";
            }
            else if (timeSpan <= TimeSpan.FromDays(365))
            {
                duration = timeSpan.Days / 30;
                unit = "months";
            }
            else
            {
                duration = timeSpan.Days / 365;
                unit = "years";
            }

            return (duration, unit);
        }
    }
}