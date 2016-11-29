using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lclass.common.lib.Utils
{
    public class DateHelper
    {
        /// <summary>
        /// 返回日期
        /// </summary>
        /// <param name="date">日期字符传</param>
        /// <param name="type">类型month或day</param>
        /// <param name="isEnd">是否截至日</param>
        /// <returns></returns>
        public static DateTime GetDate(string date, string type, bool isEnd = false)
        {
            if (type.ToLower() == "month")
            {
                date += "-01";
                if (isEnd)
                {
                    return Convert.ToDateTime(date).AddMonths(1);
                }
            }
            if (isEnd)
            {
                return Convert.ToDateTime(date).AddDays(1);
            }
            return Convert.ToDateTime(date);
        }

        /// <summary>
        /// 时间戳转为C#格式时间
        /// </summary>
        public static DateTime? StampToDateTime(int? timeStamp)
        {
            if (timeStamp == null)
            {
                return null;
            }
            return StampToDateTime(timeStamp.Value);
        }
        /// <summary>
        /// / 时间戳转为C#格式时间
        /// </summary>
        public static DateTime? StampToDateTime(int timeStamp)
        {
            if (timeStamp == 0)
            {
                return null;
            }
            DateTime dateTimeStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            long lTime = long.Parse(timeStamp + "0000000");
            TimeSpan toNow = new TimeSpan(lTime);

            return dateTimeStart.Add(toNow);
        }

        /// <summary>
        /// DateTime时间格式转换为Unix时间戳格式
        /// </summary>
        public static int? DateTimeToStamp(System.DateTime? time)
        {
            if (time == null)
            {
                return null;
            }
            return DateTimeToStamp(time.Value);
        }
        /// <summary>
        ///  DateTime时间格式转换为Unix时间戳格式
        /// </summary>
        public static int DateTimeToStamp(System.DateTime time)
        {
            TimeSpan span = (time - new DateTime(1970, 1, 1, 0, 0, 0, 0).ToLocalTime());
            return (int)span.TotalSeconds;
        }
        /// <summary>
        /// timeSpan转DateTime
        /// </summary>
        /// <param name="timestamp"></param>
        /// <returns></returns>
        public static DateTime ConvertTimestamp(double timestamp)
        {
            DateTime converted = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            DateTime newDateTime = converted.AddSeconds(timestamp);
            return newDateTime.ToLocalTime();
        }


        public static string  ConvertTimestampToFormat(string formats)
        {
            return DateTime.Now.ToString(formats);

        }
    }
}
