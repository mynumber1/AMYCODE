using System;
using System.Globalization;
using System.Text;
using System.Web;
namespace lclass.common.lib.Utils
{
	/// <summary>
	/// TenpayUtil 的摘要说明。
    /// 配置文件
	/// </summary>
	public class TenpayUtil
	{
        public static string Tenpay = "1";
        public static string Partner = "1218809901";//商户号
        public static string Key = "f824c19d0d692731189ae6bd11e7e332";  //密钥
        public static string Appid = "wxcebc82bedb225782";//"wxcebc82bedb225782";//appid
        public static string Appkey = @"CDFzDYB0bORHsamT65yNtqTXAhqYwK6isjFw2GzkVxBFvgD7erZKSpEqF5Fovg5KBxcDJXd1qYAOuargIZUxFNQtC2CSaLQqnNgNwC5RufnyXjVDEAWBtGGU88GA0WPU";//paysignkey(非appkey) 
        public static string TenpayNotify = "http://wx.xchong.net/PayNotifyUrl/PayNotifyUrl"; //支付完成后的回调处理页面,*替换成notify_url.asp所在路径
        
        public static string GetNoncestr()
        {
            var random = new Random();
            return Md5Util.GetMd5(random.Next(1000).ToString(CultureInfo.InvariantCulture), "utf-8");
        }


        public static string GetTimestamp()
        {
            var ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalSeconds).ToString(CultureInfo.InvariantCulture);
        }
      

		/** 对字符串进行URL编码 */
		public static string UrlEncode(string instr, string charset)
		{
			//return instr;
			if(instr == null || instr.Trim() == "")
				return "";
		    string res;
		    try
		    {
		        res = HttpUtility.UrlEncode(instr,Encoding.GetEncoding(charset));
		    }
		    catch (Exception ex)
		    {
		        res = HttpUtility.UrlEncode(instr,Encoding.GetEncoding("GB2312"));
		    }
		    return res;
		}

		/** 对字符串进行URL解码 */
		public static string UrlDecode(string instr, string charset)
		{
			if(instr == null || instr.Trim() == "")
				return "";
		    string res;
		    try
		    {
		        res = HttpUtility.UrlDecode(instr,Encoding.GetEncoding(charset));
		    }
		    catch (Exception ex)
		    {
		        res = HttpUtility.UrlDecode(instr,Encoding.GetEncoding("GB2312"));
		    }
		    return res;
		}
       

		/** 取时间戳生成随即数,替换交易单号中的后10位流水号 */
		public static UInt32 UnixStamp()
		{
			var ts = DateTime.Now - TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
			return Convert.ToUInt32(ts.TotalSeconds);
		}
		/** 取随机数 */
		public static string BuildRandomStr(int length) 
		{
			var rand = new Random();
			int num = rand.Next();
			string str = num.ToString(CultureInfo.InvariantCulture);
			if(str.Length > length)
			{
				str = str.Substring(0,length);
			}
			else if(str.Length < length)
			{
				int n = length - str.Length;
				while(n > 0)
				{
					str.Insert(0, "0");
					n--;
				}
			}
			return str;
		}
       
	}
}