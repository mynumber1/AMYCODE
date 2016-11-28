using System;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace lclass.common.lib.Utils
{
    public static class WebRequestHelper
    {
        public enum Method { GET, POST };

        /// <summary>
        /// 发起web请求
        /// </summary>
        /// <param name="method">请求方式：GET，POST</param>
        /// <param name="url">请求地址</param>
        /// <param name="contentType"></param>
        /// <param name="postData">请求方式为POST时，传入请求数据</param>
        /// <param name="cookie">如果需要cookie则传入，否则传入null即可</param>
        /// <returns></returns>
        public static string Request(Method method, string url, string contentType = null, string postData = null, Cookie cookie = null)
        {
            HttpWebRequest webRequest = null;

            string responseData = "";
            ServicePointManager.ServerCertificateValidationCallback = ValidateServerCertificate;  
            webRequest = System.Net.WebRequest.Create(url) as HttpWebRequest;
            webRequest.Method = method.ToString();
            webRequest.ServicePoint.Expect100Continue = false;
            webRequest.KeepAlive = true;

            if (cookie != null)
            {
                webRequest.CookieContainer = new CookieContainer();
                webRequest.CookieContainer.Add(cookie);
            }

            if (method == Method.POST)
            {
                if (string.IsNullOrEmpty(contentType))
                {
                    webRequest.ContentType = "application/x-www-form-urlencoded";
                }
                else
                {
                    webRequest.ContentType = contentType;
                }

                if (!string.IsNullOrEmpty(postData))
                {
                    StreamWriter requestWriter = null;
                    requestWriter = new StreamWriter(webRequest.GetRequestStream());
                    try
                    {
                        requestWriter.Write(postData);
                    }
                    catch (Exception e)
                    {
                        throw e;
                    }
                    finally
                    {
                        requestWriter.Close();
                        requestWriter = null;
                    }
                }
            }

            responseData = WebResponseGet(webRequest);
            webRequest = null;

            return responseData;
        }
        /// <summary>
        /// 红领接口调用专用方法
        /// </summary>
        /// <param name="url"></param>
        /// <param name="method"></param>
        /// <param name="param"></param>
        /// <param name="userName"></param>
        /// <param name="passWord"></param>
        /// <param name="language"></param>
        /// <returns></returns>
        public static string RedcollarApiRequest(string url, string method, string param, string userName, string passWord, string language)
        {
            //添加验证证书的回调方法

            ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(ValidateServerCertificate);

            if (method.ToLower() == "get")
                url += "/" + param;
            HttpWebRequest httpRequest = (HttpWebRequest)HttpWebRequest.Create(url);
            httpRequest.Timeout = 60000;
            httpRequest.Method = method;
            httpRequest.ServicePoint.Expect100Continue = false;
            httpRequest.KeepAlive = true;
            httpRequest.Headers["user"] = userName;
            httpRequest.Headers["pwd"] = passWord;
            httpRequest.Headers["lan"] = language;
            httpRequest.ContentType = "application/x-www-form-urlencoded";
            X509Certificate cerCaiShang = new X509Certificate(System.Web.HttpContext.Current.Server.MapPath("~/Content/client.p12"), "redcollar8033");

            httpRequest.ClientCertificates.Add(cerCaiShang);
            if (method.ToLower() == "post")
            {
                string postDataStr = "xml=" + System.Web.HttpUtility.UrlEncode(param);
                Encoding encoding = Encoding.UTF8;
                //根据网站的编码自定义 
                byte[] postData = encoding.GetBytes(postDataStr);
                //postDataStr即为发送的数据
                httpRequest.ContentLength = postData.Length;
                Stream requestStream = httpRequest.GetRequestStream();
                requestStream.Write(postData, 0, postData.Length);
            }
            return WebResponseGet(httpRequest);
        }
        // 回调方法

        public static bool ValidateServerCertificate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {

            //if (sslPolicyErrors == SslPolicyErrors.None)

            //    return true;

            return true;

        } 

        private static string WebResponseGet(HttpWebRequest webRequest)
        {
            StreamReader responseReader = null;
            string responseData = "";

            try
            {
                responseReader = new StreamReader(webRequest.GetResponse().GetResponseStream());
                responseData = responseReader.ReadToEnd();
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                if (responseReader != null)
                {
                    responseReader.Close();
                    responseReader = null;
                }
            }

            return responseData;
        }
    }
}
