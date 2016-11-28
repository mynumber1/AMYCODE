using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lclass.common.lib.Utils.Extensions
{
    public static class LinqExtension
    {
        /// <summary>
        /// 多字段Distinct
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="source"></param>
        /// <param name="KeySelector"></param>
        /// <returns></returns>
        public static IEnumerable<TSource> DistinctBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> KeySelector)
        {
            HashSet<TKey> keys = new HashSet<TKey>();
            foreach (TSource element in source)
            {
                if (keys.Add(KeySelector(element)))
                {
                    yield return element;
                }
            }
        }
    }
}
