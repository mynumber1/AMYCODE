
namespace lclass.common.lib.Utils
{
    /// <summary>
    /// api日志接口 
    /// </summary>
    public interface IApiStats
    {
        /// <summary>
        /// 统计调用次数
        /// </summary>
        void IncrApi();

        /// <summary>
        /// 日志记录开始
        /// </summary>
        void LogApiRequestBegin();

        /// <summary>
        /// 日志记录结束
        /// </summary>
        /// <param name="request"></param>
        void LogApiRequestEnd(string request);
    }
}
