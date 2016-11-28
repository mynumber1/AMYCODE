using System;
using System.Security.Cryptography;
using System.Text;

namespace lclass.common.lib.Utils
{
	/// <summary>
	/// MD5Util ��ժҪ˵����
	/// </summary>
	public class Md5Util
	{
		public Md5Util()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}
        public static string GetMd5(string encypStr)
        {
            var m5 = new MD5CryptoServiceProvider();

            //����md5����
            byte[] inputBye;

            //ʹ��GB2312���뷽ʽ���ַ���ת��Ϊ�ֽ����飮
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
		/** ��ȡ��д��MD5ǩ����� */
		public static string GetMd5(string encypStr, string charset)
		{
		    var m5 = new MD5CryptoServiceProvider();

			//����md5����
			byte[] inputBye;

		    //ʹ��GB2312���뷽ʽ���ַ���ת��Ϊ�ֽ����飮
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
