
using System;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Web;
namespace lclass.common.lib.Utils
{
    /// <summary>
    /// 
    /// </summary>
    public static class ValidateHelper
    {
 
        #region  新增错误信息验证


        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="doError"></param>
        /// <param name="errorMag"></param>
        /// <param name="isAppend"></param>
        /// <returns></returns>
        public static ValidateMessage Validate(this ValidateMessage model, Func<bool> doError, string errorMag, bool isAppend = true)
        {
            var IsError = doError.Invoke();
            //正确 直接返回验证对象
            if (!IsError) return model;

            //错误，验证对象未存在错误信息，附加错误信息 返回
            if (!model.HasMag())
            {
                model.SetError(errorMag);
                return model;
            }
            //验证对象存在错误信息，但验证不附加，直接返回
            if (!isAppend) return model;

            //验证对象存在错误信息，但验证附加，附加错误信息后返回
            model.SetError(errorMag);
            return model;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="IsError"></param>
        /// <param name="errorMag"></param>
        /// <param name="isAppend"></param>
        /// <returns></returns>
        public static ValidateMessage Validate(this ValidateMessage model, bool IsError, string errorMag, bool isAppend = true)
        {
            //正确 直接返回验证对象
            if (!IsError) return model;

            //错误，验证对象未存在错误信息，附加错误信息 返回0
            if (!model.HasMag())
            {
                model.SetError(errorMag);
                return model;
            }
            //验证对象存在错误信息，但验证不附加，直接返回
            if (!isAppend) return model;

            //验证对象存在错误信息，但验证附加，附加错误信息后返回
            model.SetError(errorMag);
            return model;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="str"></param>
        /// <param name="errorMag"></param>
        /// <param name="isAppend"></param>
        /// <returns></returns>
        public static ValidateMessage ValidateStringNullOrEmpty(this ValidateMessage model, string str, string errorMag, bool isAppend = true)
        {
            var IsError = string.IsNullOrEmpty(str);
            //正确 直接返回验证对象
            if (!IsError) return model;

            //错误，验证对象未存在错误信息，附加错误信息 返回
            if (!model.HasMag())
            {
                model.SetError(errorMag);
                return model;
            }
            //验证对象存在错误信息，但验证不附加，直接返回
            if (!isAppend) return model;

            //验证对象存在错误信息，但验证附加，附加错误信息后返回
            model.SetError(errorMag);
            return model;
        }

        /// <summary>
        /// 验证时间
        /// </summary>
        /// <param name="model"></param>
        /// <param name="obj"></param>
        /// <param name="errorMag"></param>
        /// <param name="isAppend"></param>
        /// <returns></returns>
        public static ValidateMessage ValidateDataTime(this ValidateMessage model, string str, string errorMag = "时间格式不正确", bool isAppend = true)
        {
            bool IsError = false;
            if (!string.IsNullOrEmpty(str))
            { 
                try
                {
                    Convert.ToDateTime(str); 
                }
                catch (Exception ex)
                {
                    IsError = true;
                }
                 
            }
            //正确 直接返回验证对象
            if (!IsError) return model;

            //错误，验证对象未存在错误信息，附加错误信息 返回
            if (!model.HasMag())
            {
                model.SetError(errorMag);
                return model;
            }
            //验证对象存在错误信息，但验证不附加，直接返回
            if (!isAppend) return model;

            //验证对象存在错误信息，但验证附加，附加错误信息后返回
            model.SetError(errorMag);
            return model;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="obj"></param>
        /// <param name="errorMag"></param>
        /// <param name="isAppend"></param>
        /// <returns></returns>
        public static ValidateMessage ValidateNull(this ValidateMessage model, object obj, string errorMag, bool isAppend = true)
        {
            var IsError = obj == null;
            //正确 直接返回验证对象
            if (!IsError) return model;

            //错误，验证对象未存在错误信息，附加错误信息 返回
            if (!model.HasMag())
            {
                model.SetError(errorMag);
                return model;
            }
            //验证对象存在错误信息，但验证不附加，直接返回
            if (!isAppend) return model;

            //验证对象存在错误信息，但验证附加，附加错误信息后返回
            model.SetError(errorMag);
            return model;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="obj"></param>
        /// <param name="errorMag"></param>
        /// <param name="isAppend"></param>
        /// <returns></returns>
        public static ValidateMessage ValidatePhone(this ValidateMessage model, string str, string errorMag = "手机格式不正确", bool isAppend = true)
        {
            bool IsError;
            if (string.IsNullOrEmpty(str))
                IsError = true;
            else
            {
                IsError = !Regex.IsMatch(str, @"^1[3|5|7|8|][0-9]{9}$");//这个正则表达式判断手机号码明显不够 Li Yongchun 2015/4/8
            }
            //正确 直接返回验证对象
            if (!IsError) return model;

            //错误，验证对象未存在错误信息，附加错误信息 返回
            if (!model.HasMag())
            {
                model.SetError(errorMag);
                return model;
            }
            //验证对象存在错误信息，但验证不附加，直接返回
            if (!isAppend) return model;

            //验证对象存在错误信息，但验证附加，附加错误信息后返回
            model.SetError(errorMag);
            return model;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="str"></param>
        /// <param name="errorMag"></param>
        /// <param name="isAppend"></param>
        /// <returns></returns>
        public static ValidateMessage ValidateUserName(this ValidateMessage model, string str, string errorMag = "手机格式不正确", bool isAppend = true)
        {
            bool IsError;
            if (string.IsNullOrEmpty(str))
                IsError = true;
            else
                IsError = !Regex.IsMatch(str, @"^(?![0-9]+$)(?![a-zA-Z]+$)[0-9A-Za-z]{2,20}$");
            //正确 直接返回验证对象
            if (!IsError) return model;

            //错误，验证对象未存在错误信息，附加错误信息 返回
            if (!model.HasMag())
            {
                model.SetError(errorMag);
                return model;
            }
            //验证对象存在错误信息，但验证不附加，直接返回
            if (!isAppend) return model;

            //验证对象存在错误信息，但验证附加，附加错误信息后返回
            model.SetError(errorMag);
            return model;
        }

        public static ValidateMessage ValidateUserName3_1(this ValidateMessage model, string str, string errorMag = "手机格式不正确", bool isAppend = true)
        {
            bool IsError;
            if (string.IsNullOrEmpty(str))
                IsError = true;
            else
                IsError = !Regex.IsMatch(str, @"^[\D]\w{1,19}$");
            //正确 直接返回验证对象
            if (!IsError) return model;

            //错误，验证对象未存在错误信息，附加错误信息 返回
            if (!model.HasMag())
            {
                model.SetError(errorMag);
                return model;
            }
            //验证对象存在错误信息，但验证不附加，直接返回
            if (!isAppend) return model;

            //验证对象存在错误信息，但验证附加，附加错误信息后返回
            model.SetError(errorMag);
            return model;
        }
        public static ValidateMessage ValidateEmail(this ValidateMessage model, string str, string errorMag = "邮箱格式不正确", bool isAppend = true)
        {
            if (string.IsNullOrEmpty(str)) return model;
            var IsError = !Regex.IsMatch(str, @"\\w{1,}@\\w{1,}\\.\\w{1,}");
            //正确 直接返回验证对象
            if (!IsError) return model;

            //错误，验证对象未存在错误信息，附加错误信息 返回
            if (!model.HasMag())
            {
                model.SetError(errorMag);
                return model;
            }
            //验证对象存在错误信息，但验证不附加，直接返回
            if (!isAppend) return model;

            //验证对象存在错误信息，但验证附加，附加错误信息后返回
            model.SetError(errorMag);
            return model;
        }

        public static ValidateMessage ValidateNickName(this ValidateMessage model, string str, string errorMag = "昵称格式不正确", bool isAppend = true)
        {
            if (string.IsNullOrEmpty(str)) return model;
            var IsError = !Regex.IsMatch(str, @"^[\da-zA-Z_ \u4e00-\u9fa5]+$");
            //正确 直接返回验证对象
            if (!IsError) return model;

            //错误，验证对象未存在错误信息，附加错误信息 返回
            if (!model.HasMag())
            {
                model.SetError(errorMag);
                return model;
            }
            //验证对象存在错误信息，但验证不附加，直接返回
            if (!isAppend) return model;

            //验证对象存在错误信息，但验证附加，附加错误信息后返回
            model.SetError(errorMag);
            return model;
        }

        public static ValidateMessage ValidatePassword(this ValidateMessage model, string str, string errorMag = "密码格式不正确", bool isAppend = true)
        {
            if (string.IsNullOrEmpty(str)) return model;
            var IsError = !Regex.IsMatch(str, @"^[\w-`=\\\[\];',./~!@#$%^&*()_+|{}:>?]{6,}$");
            //正确 直接返回验证对象
            if (!IsError) return model;

            //错误，验证对象未存在错误信息，附加错误信息 返回
            if (!model.HasMag())
            {
                return model;
            }
            //验证对象存在错误信息，但验证不附加，直接返回
            if (!isAppend) return model;

            //验证对象存在错误信息，但验证附加，附加错误信息后返回
            model.SetError(errorMag);
            return model;
        }

        #endregion
    }
}
