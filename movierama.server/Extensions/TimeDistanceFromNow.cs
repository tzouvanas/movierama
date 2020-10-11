using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;

namespace ExtensionMethods
{
    public static class MyExtensions
    {
        public static (int, string) TimeDistanceFromNow(this DateTime dt)
        {
            var ts = new TimeSpan(DateTime.Now.Ticks - dt.Ticks);
            double delta = Math.Abs(ts.TotalSeconds);

            int value = 0;
            string unit = string.Empty;

            if (delta < 60)
            {
                if (ts.Seconds == 1)
                {
                    value = 1;
                    unit = "second";
                }
                else 
                {
                    value = ts.Seconds;
                    unit = "seconds";
                }

                return (value, unit);

            }
            
            if (delta < 60 * 2)
            {
                value = 1;
                unit = "minute";
                return (value, unit);
            }

            if (delta < 45 * 60)
            {
                value = ts.Minutes;
                unit = "minutes";
                return (value, unit);
            }
            
            if (delta < 90 * 60)
            {
                value = 1;
                unit = "hour";
                return (value, unit);
            }

            if (delta < 24 * 60 * 60)
            {
                value = ts.Hours;
                unit = "hours";
                return (value, unit);
            }
           
            if (delta < 48 * 60 * 60)
            {
                value = 1;
                unit = "yesterday";
                return (value, unit);
            }
           
            if (delta < 30 * 24 * 60 * 60)
            {
                value = ts.Days;
                unit = "days";
                return (value, unit);
            }

            if (delta < 12 * 30 * 24 * 60 * 60)
            {
                int months = Convert.ToInt32(Math.Floor((double)ts.Days / 30));
                if (months <= 1)
                {
                    value = 1;
                    unit = "month";
                }
                else {
                    value = months;
                    unit = "months";
                }
                return (value, unit);
            }

            int years = Convert.ToInt32(Math.Floor((double)ts.Days / 365));

            if (years == 1)
            {
                value = 1;
                unit = "year";
            }
            else
            {
                value = years;
                unit = "years";
            }

            return (value, unit);
        }
    }
}