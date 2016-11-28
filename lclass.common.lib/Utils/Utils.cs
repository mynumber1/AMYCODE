using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Security;


namespace lclass.common.lib.Utils
{
    /// <summary>
    ///     公用方法类
    /// </summary>
    public static class Utils
    {
        #region 生成GUID

        /// <summary>
        ///     生成GUID
        /// </summary>
        /// <returns></returns>
        public static string GetNewGuid()
        {
            return Guid.NewGuid().ToString("N");
        }

        #endregion

        #region 转换用户字输入的字符串为可正确显示的html

        /// <summary>
        ///     表情编码文字对应字典
        /// </summary>
        public static Dictionary<int, string> FaceDic = new Dictionary<int, string>
            {
                {1, "微笑"},
                {2, "嘻嘻"},
                {3, "哈哈"},
                {4, "爱你"},
                {5, "晕"},
                {6, "流泪"},
                {7, "馋嘴"},
                {8, "抓狂"},
                {9, "哼"},
                {10, "可爱"},
                {11, "怒"},
                {12, "汗"},
                {13, "困"},
                {14, "害羞"},
                {15, "睡觉"},
                {16, "钱"},
                {17, "偷笑"},
                {18, "酷"},
                {19, "衰"},
                {20, "吃惊"},
                {21, "闭嘴"},
                {22, "鄙视"},
                {23, "挖鼻屎"},
                {24, "花心"},
                {25, "坏笑"},
                {26, "鼓掌"},
                {27, "失望"},
                {28, "思考"},
                {29, "生病"},
                {30, "亲亲"},
                {31, "抱抱"},
                {32, "怒骂"},
                {33, "太开心"},
                {34, "懒得理你"},
                {35, "右哼哼"},
                {36, "左哼哼"},
                {37, "嘘"},
                {38, "委屈"},
                {39, "吐"},
                {40, "可怜"},
                {41, "打哈气"},
                {42, "顶"},
                {43, "疑问"},
                {44, "做鬼脸"},
                {45, "搞怪"},
                {46, "握手"},
                {47, "耶"},
                {48, "good"},
                {49, "弱"},
                {50, "no"},
                {51, "ok"},
                {52, "赞"},
                {53, "来"},
                {54, "蛋糕"},
                {55, "心"},
                {56, "伤心"},
                {57, "钟"},
                {58, "猪头"},
                {59, "咖啡"},
                {60, "饭"},
                {61, "浮云"},
                {62, "飘过"},
                {63, "月亮"},
                {64, "太阳"},
                {65, "下雨"},
                {66, "遛狗"},
                {67, "灰机"},
                {68, "叶子"},
                {69, "花"},
                {70, "干杯"},
                {71, "求围观"},
                {72, "又胖啦"}
            };

        public static string ConvertToHtmlString(string source)
        {
            return ConvertToHtmlString(source, false);
        }

        /// <summary>
        ///     转换&为&amp;
        ///     空格为&nbsp;
        ///     <为& lt;>为&gt;
        /// </summary>
        /// <param name="source">源字符串</param>
        /// <param name="keepBR">是否保留换行</param>
        /// <returns></returns>
        public static string ConvertToHtmlString(string source, bool keepBR)
        {
            return source.Replace("&", "&amp;")
                         .Replace(" ", "&nbsp;")
                         .Replace("<", "&lt;")
                         .Replace(">", "&gt;")
                         .Replace("\r\n", keepBR ? "<br />" : "&nbsp;")
                         .Replace("\n", keepBR ? "<br />" : "&nbsp;");
        }

        public static string ParseHtmlString(string source)
        {
            return source.Replace("&amp;", "&")
                         .Replace("&nbsp;", " ")
                         .Replace("&lt;", "<")
                         .Replace("&gt;", ">")
                         .Replace("&mdash;", "—");
        }

        /// <summary>
        ///     转换表情文字为等为表情图片
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string ConvertFaceToHtmlString(string host, string source)
        {
            string result = source;

            foreach (var pair in FaceDic)
            {
                result = result.Replace("[" + pair.Value + "]",
                                        "<img src=\"" + host + "/content/image/icons/" + pair.Key.ToString() +
                                        ".gif\" alt=\"" + pair.Value + "\" />");
            }

            return result;
        }

        #endregion

        #region 字符串操作

        /// <summary>
        ///     搜索移除特殊字符
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string RemoveSpecialCharacters(string source)
        {
            var regex = new Regex("[&|<|>|(\\\\r\\\\n)|(\\\\n)|(\\\\)|'|\\s]");
            return regex.Replace(source, "");
        }

        /// <summary>
        ///     截取字符串，中文算两个字符
        /// </summary>
        /// <param name="s">源字符串</param>
        /// <param name="bytes">截取长度</param>
        /// <returns></returns>
        public static string SubStrByByte(string s, int bytes)
        {
            if (string.IsNullOrEmpty(s))
            {
                return string.Empty;
            }
            int totalBytes = 0;
            string res = "";
            for (int i = 0; i < s.Length; i++)
            {
                if (IsChinese(s[i]))
                {
                    totalBytes = totalBytes + 2;
                }
                else
                {
                    totalBytes = totalBytes + 1;
                }

                if (totalBytes <= bytes)
                {
                    res += s[i];
                }
                else
                {
                    break;
                }
            }
            return res;
        }

        public static int EngLength(string str)
        {
            var n = new ASCIIEncoding();

            byte[] b = n.GetBytes(str);
            int length = 0;
            for (int i = 0; i <= b.Length - 1; i++)
            {
                if (b[i] == 63)
                {
                    length++;
                }
                length++;
            }
            return length;
        }

        public static string SubString(string stringToSub, int length)
        {
            var regex = new Regex("[\u4e00-\u9fa5]+", RegexOptions.Compiled);
            char[] stringChar = stringToSub.ToCharArray();
            var sb = new StringBuilder();
            int nLength = 0;

            for (int i = 0; i < stringChar.Length; i++)
            {
                if (regex.IsMatch((stringChar[i]).ToString()))
                {
                    nLength += 3;
                }
                else
                {
                    nLength = nLength + 1;
                }

                if (nLength <= length)
                {
                    sb.Append(stringChar[i]);
                }
                else
                {
                    break;
                }
            }

            return sb.ToString();
        }

        /// <summary>
        ///     判断一个字符是否为中文字符
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public static bool IsChinese(char c)
        {
            return c >= 0x4E00 && c <= 0x9FA5;
        }

        /// <summary>
        ///     获取大写的MD5签名结果
        /// </summary>
        /// <param name="encypStr"></param>
        /// <returns></returns>
        public static string GetMD5(string encypStr, string key)
        {
            encypStr += key;
            string retStr;
            var m5 = new MD5CryptoServiceProvider();

            //创建md5对象
            byte[] inputBye;
            byte[] outputBye;

            //使用GB2312编码方式把字符串转化为字节数组．
            inputBye = Encoding.GetEncoding("GB2312").GetBytes(encypStr);

            outputBye = m5.ComputeHash(inputBye);

            retStr = BitConverter.ToString(outputBye);
            retStr = retStr.Replace("-", "").ToUpper();
            return retStr;
        }

        public static string GetMD5(string encypStr)
        {
            return GetMD5(encypStr, "");
        }

        /// <summary>
        ///     SHA256函数
        /// </summary>
        /// ///
        /// <param name="str">原始字符串</param>
        /// <returns>SHA256结果</returns>
        public static string SHA256(string str)
        {
            byte[] SHA256Data = Encoding.UTF8.GetBytes(str);
            var Sha256 = new SHA256Managed();
            byte[] Result = Sha256.ComputeHash(SHA256Data);
            return Convert.ToBase64String(Result); //返回长度为44字节的字符串
        }

        /// <summary>
        ///     检测是否有Sql危险字符
        /// </summary>
        /// <param name="str">要判断字符串</param>
        /// <returns>判断结果</returns>
        public static bool IsSafeSqlString(string str)
        {
            return !Regex.IsMatch(str, @"[-|;|,|\/|\(|\)|\[|\]|\}|\{|%|@|\*|!|\']");
        }

        /// <summary>
        ///     检测是否有危险的可能用于链接的字符串
        /// </summary>
        /// <param name="str">要判断字符串</param>
        /// <returns>判断结果</returns>
        public static bool IsSafeUserInfoString(string str)
        {
            return !Regex.IsMatch(str, @"^\s*$|^c:\\con\\con$|[%,\*" + "\"" + @"\s\t\<\>\&]|游客|^Guest");
        }

        /// <summary>
        ///     将全角数字转换为数字
        /// </summary>
        /// <param name="SBCCase"></param>
        /// <returns></returns>
        public static string SBCCaseToNumberic(string SBCCase)
        {
            char[] c = SBCCase.ToCharArray();
            for (int i = 0; i < c.Length; i++)
            {
                byte[] b = Encoding.Unicode.GetBytes(c, i, 1);
                if (b.Length == 2)
                {
                    if (b[1] == 255)
                    {
                        b[0] = (byte)(b[0] + 32);
                        b[1] = 0;
                        c[i] = Encoding.Unicode.GetChars(b)[0];
                    }
                }
            }
            return new string(c);
        }

        /// <summary>
        ///     通过用户的生日月份和日期，返回其星座名称
        /// </summary>
        /// <param name="month"></param>
        /// <param name="day"></param>
        /// <returns></returns>
        public static string GetZodiacName(int month, int day)
        {
            return
                ("摩羯,水瓶,双鱼,白羊,金牛,双子,巨蟹,狮子,处女,天秤,天蝎,射手,摩羯").Split(',')[
                    day < (20 + int.Parse(("2101122344432")[month].ToString())) ? month - 1 : month] + "座";
        }

        public static string Concat(ICollection items, string delimiter)
        {
            bool first = true;

            var sb = new StringBuilder();
            foreach (object item in items)
            {
                if (item == null)
                    continue;

                if (!first)
                {
                    sb.Append(delimiter);
                }
                else
                {
                    first = false;
                }
                sb.Append(item);
            }
            return sb.ToString();
        }

        public static double MathRound(double dou, int num)
        {
            return Math.Round(dou, num);
        }

        public static double MathRound(double? dou)
        {
            if (dou != null)
                return MathRound(dou);
            else
                return 0;
        }

        /// <summary>
        ///     价格保留2位小数，"分"四舍五入
        /// </summary>
        /// <param name="price"></param>
        /// <returns></returns>
        public static string FormatPrice(decimal price)
        {
            return Math.Round(price, 1).ToString("#0.00");
        }

        #endregion

        #region url操作

        public static string GetCurrentHost()
        {
            return HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Host;
        }

        /// <summary>
        ///     获取短链接
        /// </summary>
        /// <param name="url">原始链接</param>
        /// <returns></returns>
        public static string GetShortUrl(string url)
        {
            //可以自定义生成MD5加密字符传前的混合KEY
            string key = "Leejor";
            //要使用生成URL的字符
            var chars = new[]
                {
                    "a", "b", "c", "d", "e", "f", "g", "h",
                    "i", "j", "k", "l", "m", "n", "o", "p",
                    "q", "r", "s", "t", "u", "v", "w", "x",
                    "y", "z", "0", "1", "2", "3", "4", "5",
                    "6", "7", "8", "9", "A", "B", "C", "D",
                    "E", "F", "G", "H", "I", "J", "K", "L",
                    "M", "N", "O", "P", "Q", "R", "S", "T",
                    "U", "V", "W", "X", "Y", "Z"
                };

            //对传入网址进行MD5加密
            string hex = FormsAuthentication.HashPasswordForStoringInConfigFile(key + url, "md5");

            var resUrl = new string[4];

            for (int i = 0; i < 4; i++)
            {
                //把加密字符按照8位一组16进制与0x3FFFFFFF进行位与运算
                int hexint = 0x3FFFFFFF & Convert.ToInt32("0x" + hex.Substring(i * 8, 8), 16);
                string outChars = string.Empty;
                for (int j = 0; j < 6; j++)
                {
                    //把得到的值与0x0000003D进行位与运算，取得字符数组chars索引
                    int index = 0x0000003D & hexint;
                    //把取得的字符相加
                    outChars += chars[index];
                    //每次循环按位右移5位
                    hexint = hexint >> 5;
                }
                //把字符串存入对应索引的输出数组
                resUrl[i] = outChars;
            }

            return resUrl[0];
        }

        #endregion

        #region ip相关

        /// <summary>
        ///     是否为ip
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        public static bool IsIP(string ip)
        {
            return Regex.IsMatch(ip, @"^((2[0-4]\d|25[0-5]|[01]?\d\d?)\.){3}(2[0-4]\d|25[0-5]|[01]?\d\d?)$");
        }


        private const string HttpContextStr = "MS_HttpContext";
        private const string RemoteEndpointMessageStr =
            "System.ServiceModel.Channels.RemoteEndpointMessageProperty";
        private const string OwinContextStr = "MS_OwinContext";

        public static string GetClientIpAddress(System.Net.Http.HttpRequestMessage request)
        {
            // Web-hosting. Needs reference to System.Web.dll
            if (request.Properties.ContainsKey(HttpContextStr))
            {
                dynamic ctx = request.Properties[HttpContextStr];
                if (ctx != null)
                {
                    return ctx.Request.UserHostAddress;
                }
            }

            // Self-hosting. Needs reference to System.ServiceModel.dll. 
            if (request.Properties.ContainsKey(RemoteEndpointMessageStr))
            {
                dynamic remoteEndpoint = request.Properties[RemoteEndpointMessageStr];
                if (remoteEndpoint != null)
                {
                    return remoteEndpoint.Address;
                }
            }

            // Self-hosting using Owin. Needs reference to Microsoft.Owin.dll. 
            if (request.Properties.ContainsKey(OwinContextStr))
            {
                dynamic owinContext = request.Properties[OwinContextStr];
                if (owinContext != null)
                {
                    return owinContext.Request.RemoteIpAddress;
                }
            }
            return null;
        }
        public static string GetClientIpAddress()
        {
            var current = HttpContext.Current;

            if (current == null) return "127.0.0.1";

            var request = current.Request;

            //获取代理的服务器Ip地址
            string ip = request.ServerVariables["HTTP_X_FORWARDED_FOR"];

            if (!string.IsNullOrEmpty(ip)) return ip;
            ip = request.ServerVariables["X-Forwarded-For"];
            if (!string.IsNullOrEmpty(ip)) return ip;
            //获取发出请求的远程主机的Ip地址 
            ip = request.ServerVariables["REMOTE_ADDR"];

            if (string.IsNullOrEmpty(ip))
            {
                ip = request.UserHostAddress;
            }

            return ip;
        }

        #endregion

        #region email相关

        /// <summary>
        ///     检测是否符合email格式
        /// </summary>
        /// <param name="strEmail">要判断的email字符串</param>
        /// <returns>判断结果</returns>
        public static bool IsValidEmail(string strEmail)
        {
            //return Regex.IsMatch(strEmail, @"^[A-Za-z0-9-_]+@[A-Za-z0-9-_]+[\.][A-Za-z0-9-_]");
            return Regex.IsMatch(strEmail, @"^[\w\.]+@[A-Za-z0-9-_]+[\.][A-Za-z0-9-_]");
        }

        public static bool IsValidDoEmail(string strEmail)
        {
            return Regex.IsMatch(strEmail,
                                 @"^@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
        }

        #endregion

        #region 留言板数据安全性格式化

        public static string SecurityFormat(string htm, HttpContext context)
        {
            var reg = new Regex(@"<(.*?)>");
            string htm2 = reg.Replace(htm, "").Replace("&nbsp;", "");

            if (htm2.Trim().Length == 0)
            {
                return "";
            }

            var reg2 = new Regex(@"\[img\](?<Url>.+?)\[\/img\]", RegexOptions.IgnoreCase);
            var reg3 = new Regex(@"EditorImg/(.*?)\.gif", RegexOptions.IgnoreCase);
            var reg4 = new Regex(@"<br(.*?)>", RegexOptions.IgnoreCase);
            var reg5 = new Regex(@"</p>", RegexOptions.IgnoreCase);
            htm = reg4.Replace(htm, "[br]");
            htm = reg5.Replace(htm, "[br]");
            htm = reg.Replace(htm, "");

            MatchCollection matches = reg2.Matches(htm);
            var reg7 = new Regex(@"http://(.*?)/", RegexOptions.IgnoreCase);
            Match mt = reg7.Match(context.Request.Url.ToString());

            string currentDomainUrl = mt.Value.TrimEnd('/');

            foreach (Match match in matches)
            {
                string url = match.Groups["Url"].Value;
                string url2 = string.Empty;
                if (!url.StartsWith(currentDomainUrl))
                {
                    url2 = currentDomainUrl + url;
                }
                else
                {
                    url2 = url;
                }
                string imgUrl = reg3.Replace(url2, "EditorImg");
                if (imgUrl.ToUpper() == (currentDomainUrl + "/CONTENT/PLUGINS/MSGEDITOR/EDITORIMG").ToUpper())
                {
                    htm = htm.Replace("[img]" + url + "[/img]", "<img src=\"" + url + "\" />");
                }
                else
                {
                    htm = htm.Replace("[img]" + url + "[/img]", "");
                }
            }
            htm = htm.Replace("[br]", "<br/>");
            return htm;
        }

        public static string ImageRegex(Match mac)
        {
            string val = "";
            if (mac.Success && mac.Groups.Count > 1)
            {
                string url = mac.Groups[1].Value;
                if (url.StartsWith(".."))
                {
                    url = url.Replace("../..", "").Replace("..", "");
                }
                val = "[img src=\"" + url + "\"/]";
            }

            return val;
        }

        public static string SecurityFormat(string htm, bool keepimg, HttpContext context)
        {
            var reg = new Regex(@"<(.*?)>");

            var reg2 = new Regex(@"\<img[^>]*src=['|""]([^'|""]*)['|""][^>]*>", RegexOptions.IgnoreCase);
            var regimg = new Regex(@"\[img[^]]*src=['|""]([^'|""]*)['|""][^]]*]", RegexOptions.IgnoreCase);
            var reg3 = new Regex(@"EditorImg/(.*?)\.gif", RegexOptions.IgnoreCase);
            var reg4 = new Regex(@"<br(.*?)>", RegexOptions.IgnoreCase);
            var reg5 = new Regex(@"</p>", RegexOptions.IgnoreCase);
            var reg7 = new Regex(@"http://(.*?)/", RegexOptions.IgnoreCase);
            var regdiv = new Regex(@"<div[^>]*align=['|""]([^'|""]*)['|""][^>]*>", RegexOptions.IgnoreCase);
            var regdivend = new Regex(@"</[^>]*div[^>]*>", RegexOptions.IgnoreCase);

            //htm = regdiv.Replace(htm, "[div$1]");
            htm = regdivend.Replace(htm, "[br]");

            htm = reg4.Replace(htm, "[br]");
            htm = reg5.Replace(htm, "[br]");
            htm = reg2.Replace(htm, ImageRegex);
            htm = reg.Replace(htm, "");
            //htm = htm.Replace("[divleft]", "<div align=\"left\">");
            //htm = htm.Replace("[divcenter]", "<div align=\"center\">");
            //htm = htm.Replace("[divright]", "<div align=\"right\">");
            //htm = htm.Replace("[/div]", "</div>");
            htm = regimg.Replace(htm, "<img src=\"$1\" />");

            htm = htm.Replace("[br]", "<br/>");
            return htm;
        }

        /// <summary>
        ///     过滤HTML
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        public static string checkStr(string html)
        {
            var regex1 = new Regex(@"<script[\s\S]+</script *>", RegexOptions.IgnoreCase);
            var regex2 = new Regex(@" href *= *[\s\S]*script *:", RegexOptions.IgnoreCase);
            var regex3 = new Regex(@" on[\s\S]*=", RegexOptions.IgnoreCase);
            var regex4 = new Regex(@"<iframe[\s\S]+</iframe *>", RegexOptions.IgnoreCase);
            var regex5 = new Regex(@"<frameset[\s\S]+</frameset *>", RegexOptions.IgnoreCase);
            var regex6 = new Regex(@"\<img[^\>]+\>", RegexOptions.IgnoreCase);
            var regex7 = new Regex(@"</p>", RegexOptions.IgnoreCase);
            var regex8 = new Regex(@"<p>", RegexOptions.IgnoreCase);

            html = regex1.Replace(html, ""); //过滤<script></script>标记
            html = regex2.Replace(html, ""); //过滤href=javascript: (<A>) 属性
            html = regex3.Replace(html, " _disibledevent="); //过滤其它控件的on...事件
            html = regex4.Replace(html, ""); //过滤iframe
            html = regex5.Replace(html, ""); //过滤frameset
            html = regex6.Replace(html, ""); //过滤frameset
            html = regex7.Replace(html, ""); //过滤frameset
            html = regex8.Replace(html, ""); //过滤frameset
            html = html.Replace("</strong>", "");
            html = html.Replace("<strong>", "");
            return html;
        }

        #endregion

        #region 类型转换

        /// <summary>
        ///     将泛类型集合List类转换成DataTable
        /// </summary>
        /// <param name="list">泛类型集合</param>
        /// <returns>转换后的DataTable对象</returns>
        public static DataTable ListToDataTable<T>(IList<T> entitys)
        {
            //检查实体集合不能为空
            if (entitys == null || entitys.Count < 1)
            {
                throw new Exception("需转换的集合为空");
            }

            //取出第一个实体的所有Propertie
            Type entityType = entitys[0].GetType();
            PropertyInfo[] entityProperties = entityType.GetProperties();

            //生成DataTable的structure
            //生产代码中，应将生成的DataTable结构Cache起来，此处略
            var dt = new DataTable();
            for (int i = 0; i < entityProperties.Length; i++)
            {
                //dt.Columns.Add(entityProperties[i].Name, entityProperties[i].PropertyType);
                dt.Columns.Add(entityProperties[i].Name);
            }

            //将所有entity添加到DataTable中
            foreach (object entity in entitys)
            {
                //检查所有的的实体都为同一类型
                if (entity.GetType() != entityType)
                {
                    throw new Exception("要转换的集合元素类型不一致");
                }
                var entityValues = new object[entityProperties.Length];
                for (int i = 0; i < entityProperties.Length; i++)
                {
                    entityValues[i] = entityProperties[i].GetValue(entity, null);
                }
                dt.Rows.Add(entityValues);
            }
            return dt;
        }

        #endregion

        #region 字母转换
        public static string ConvertChar(string str)
        {
            if (string.IsNullOrEmpty(str))
                return "0";
            if (str.CompareTo("吖") < 0)
            {
                string s = str.Substring(0, 1).ToUpper();
                if (char.IsNumber(s, 0))
                {
                    return "0";
                }
                else
                {
                    return s;
                }
            }
            else if (str.CompareTo("八") < 0)
            {
                return "A";
            }
            else if (str.CompareTo("嚓") < 0)
            {
                return "B";
            }
            else if (str.CompareTo("咑") < 0)
            {
                return "C";
            }
            else if (str.CompareTo("妸") < 0)
            {
                return "D";
            }
            else if (str.CompareTo("发") < 0)
            {
                return "E";
            }
            else if (str.CompareTo("旮") < 0)
            {
                return "F";
            }
            else if (str.CompareTo("铪") < 0)
            {
                return "G";
            }
            else if (str.CompareTo("讥") < 0)
            {
                return "H";
            }
            else if (str.CompareTo("咔") < 0)
            {
                return "J";
            }
            else if (str.CompareTo("垃") < 0)
            {
                return "K";
            }
            else if (str.CompareTo("嘸") < 0)
            {
                return "L";
            }
            else if (str.CompareTo("拏") < 0)
            {
                return "M";
            }
            else if (str.CompareTo("噢") < 0)
            {
                return "N";
            }
            else if (str.CompareTo("妑") < 0)
            {
                return "O";
            }
            else if (str.CompareTo("七") < 0)
            {
                return "P";
            }
            else if (str.CompareTo("亽") < 0)
            {
                return "Q";
            }
            else if (str.CompareTo("仨") < 0)
            {
                return "R";
            }
            else if (str.CompareTo("他") < 0)
            {
                return "S";
            }
            else if (str.CompareTo("哇") < 0)
            {
                return "T";
            }
            else if (str.CompareTo("夕") < 0)
            {
                return "W";
            }
            else if (str.CompareTo("丫") < 0)
            {
                return "X";
            }
            else if (str.CompareTo("帀") < 0)
            {
                return "Y";
            }
            else if (str.CompareTo("咗") < 0)
            {
                return "Z";
            }
            else
            {
                return "0";
            }
        }
        #endregion

        #region 解析字符串：DecodeParams

        /// <summary>
        /// 解析字符串
        /// </summary>
        /// <param name="str">字符串,格式如：name1=value1&name2=value2</param>
        /// <returns>解析后的字典</returns>
        /// <exception cref="ArgumentNullException">解析的字符串为空或Null时触发该异常</exception>
        /// <exception cref="FormatException">分割后的字符串的键值对不匹配时触发该异常</exception>
        public static Dictionary<string, string> DecodeParams(string str)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            if (!string.IsNullOrEmpty(str))
            {
                //分割字符串
                string[] arr = str.Split(new string[] { "&", "=" }, StringSplitOptions.None);
                if (arr.Length % 2 == 0)
                {
                    //拼装 key，value 到字典里
                    string key = string.Empty, value = string.Empty;
                    for (int i = 0; i < arr.Length; i++)
                    {
                        if (i % 2 == 0)
                            key = arr[i];
                        else if (i % 2 == 1)
                        {
                            value = arr[i];
                            dict.Add(key, Uri.UnescapeDataString(value == null ? "" : value));
                        }
                    }
                }
                else
                    throw new FormatException("分割后的字符串的键值对不匹配，字符串数组长度为" + arr.Length + "！");
            }
            else
            {
                throw new ArgumentNullException("要解析的字符串为空或Null！");
            }
            return dict;
        }

        #endregion

        #region 生成请求字符串：BuildQueryString

        /// <summary>
        /// 生成请求字符串
        /// </summary>
        /// <param name="parameters">Key-Value形式请求参数字典</param>
        /// <returns>URL编码后的请求数据</returns>
        public static string BuildQueryString(IDictionary<string, string> parameters)
        {
            StringBuilder builder = new StringBuilder();
            if (parameters != null)
            {
                bool hasParam = false;

                IEnumerator<KeyValuePair<string, string>> dem = parameters.GetEnumerator();
                //拼装字符串
                while (dem.MoveNext())
                {
                    string name = dem.Current.Key;
                    string value = dem.Current.Value;
                    if (hasParam)
                    {
                        builder.Append("&");
                    }
                    builder.Append(name);
                    builder.Append("=");
                    builder.Append((value == null ? "" : value));
                    hasParam = true;
                }
            }
            return builder.ToString();
        }

        #endregion


        public static object GetValueByProperty(object obj, string name)
        {
            if (obj == null)
            {
                return null;
            }
            Type type = obj.GetType(); //获取类型
            System.Reflection.PropertyInfo propertyInfo = type.GetProperty(name);
            return propertyInfo.GetValue(obj, null);
        }

    }
}