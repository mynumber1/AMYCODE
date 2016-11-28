using System.Text;

namespace lclass.common.lib.Utils
{
    /// <summary>
    /// 
    /// </summary>
    public class ValidateMessage
    {
        /// <summary>
        /// 错误信息
        /// </summary>
        private StringBuilder _ErrorMag { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public ValidateMessage()
        {
            _ErrorMag = new StringBuilder();
        }
        /// <summary>
        /// 错误信息
        /// </summary>
        public string Messages
        {
            get
            {
                return _ErrorMag.ToString().TrimEnd(',');
            }
        }
        /// <summary>
        /// 添加错误信息
        /// </summary>
        /// <param name="msg"></param>
        public void SetError(string msg)
        {
            _ErrorMag.AppendFormat("{0},", msg);
        }
        /// <summary>
        /// 是否存在
        /// </summary>
        /// <returns></returns>
        public bool HasMag()
        {
            return _ErrorMag.Length != 0;
        }
    }
}
