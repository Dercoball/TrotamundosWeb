using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TrotamundosNetCore.Clases.Util
{
    public static class DateTimeExtensions
    {

        public static DateTime RoundSecond(this DateTime dt)
        {
            return dt.AddMilliseconds(-dt.Millisecond);
        }
        public static DateTime RoundMinute(this DateTime dt)
        {
            return dt.AddSeconds(-dt.Second).RoundSecond();
        }
        public static DateTime RoundHour(this DateTime dt)
        {
            return dt.AddMinutes(-dt.Minute).RoundMinute();
        }
        public static DateTime RoundDay(this DateTime dt)
        {
            return dt.AddHours(-dt.Hour).RoundHour();
        }
    }
}
