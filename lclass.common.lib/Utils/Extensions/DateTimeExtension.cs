using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lclass.common.lib.Utils.Extensions
{
    internal static class DateTimeExtension
    {
        /// <summary>
        /// 格式化为字符串
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static string Format(this DateTime dateTime)
        {
            return dateTime.ToString("yyyy-MM-dd HH:mm:ss");
        }
    }
}
