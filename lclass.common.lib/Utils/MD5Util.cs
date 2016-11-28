using System;
using System.Security.Cryptography;
using System.Text;

namespace lclass.common.lib.Utils
{
	/// <summary>
	/// MD5Util 的摘要说明。
	/// </summary>
	public class Md5Util
	{
		public Md5Util()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}
        public static string GetMd5(string encypStr)
        {
            var m5 = new MD5CryptoServiceProvider();

            //创建md5对象
            byte[] inputBye;

            //使用GB2312编码方式把字符串转化为字节数组．
            try
            {
                inputBye = Encoding.UTF8.GetBytes(encypStr);
            }
            catch (Exception ex)
            {
                inputBye = Encoding.GetEncoding("GB2312").GetBytes(encypStr);
            }
            byte[] outputBye = m5.ComputeHash(inputBye);
            var retStr = BitConverter.ToString(outputBye);
            retStr = retStr.Replace("-", "").ToUpper();
            return retStr;
        }
		/** 获取大写的MD5签名结果 */
		public static string GetMd5(string encypStr, string charset)
		{
		    var m5 = new MD5CryptoServiceProvider();

			//创建md5对象
			byte[] inputBye;

		    //使用GB2312编码方式把字符串转化为字节数组．
			try
			{
				inputBye = Encoding.GetEncoding(charset).GetBytes(encypStr);
			}
			catch (Exception ex)
			{
                inputBye = Encoding.UTF8.GetBytes(encypStr);
			}
			byte[] outputBye = m5.ComputeHash(inputBye);
			var retStr = BitConverter.ToString(outputBye);
			retStr = retStr.Replace("-", "").ToUpper();
			return retStr;
		}
	}
}
