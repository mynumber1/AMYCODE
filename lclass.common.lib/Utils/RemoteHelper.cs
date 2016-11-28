using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace lclass.common.lib.Utils
{
    /// <summary>
    /// 
    /// </summary>
    public partial class RemoteHelper
    {
        /// <summary>
        /// 
        /// </summary>
        public enum Method { GET, POST };
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="method"></param>
        /// <param name="url"></param>
        /// <param name="contentType"></param>
        /// <param name="postData"></param>
        /// <param name="cookie"></param>
        /// <returns></returns>
        public static string WebRequestPost(Method method, string url, string contentType = null, string postData = null, Cookie cookie = null)
        {
            HttpWebRequest webRequest = null;

            string responseData = "";

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
        /// 
        /// </summary>
        /// <param name="webRequest"></param>
        /// <returns></returns>
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
