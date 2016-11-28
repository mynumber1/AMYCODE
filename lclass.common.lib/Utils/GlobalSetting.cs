using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lclass.common.lib.Utils
{
    public class GlobalSetting
    {
        /// <summary>
        /// app端请求签名密钥
        /// </summary>
        public static string AppSysteSecretm = ConfigurationManager.AppSettings["AppSysteSecretm"];
        public static string GetAppSysteSecretm(double appVersion)
        {
            var appSysteSecretm = ConfigurationManager.AppSettings["AppSysteSecretm" + appVersion];
            if (!string.IsNullOrEmpty(appSysteSecretm))
            {
                return appSysteSecretm;
            }
            appSysteSecretm = ConfigurationManager.AppSettings["AppSysteSecretm" + (int)appVersion];
            if (!string.IsNullOrEmpty(appSysteSecretm))
            {
                return appSysteSecretm;
            }
            return ConfigurationManager.AppSettings["AppSysteSecretm"];
        }

    }
}
