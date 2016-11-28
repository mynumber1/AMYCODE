using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace lclass.common.lib.Utils
{
    /// <summary>
    /// 
    /// </summary>
    public static class LambdaResolver
    {
        /// <summary>
        /// 纵深0级
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="TR"></typeparam>
        /// <param name="source"></param>
        /// <param name="selectorTR"></param>
        /// <returns></returns>
        public static LambdaFields<T1> SelectFields<T1, TR>(this LambdaFields<T1> source,
            Expression<Func<T1, TR>> selectorTR)
            where T1 : class,new()
        {
            if (selectorTR == null)
                throw new Exception("selectorTR is null");
            var body = selectorTR.Body as NewExpression;
            if (body == null)
                throw new Exception("selectorTR is not NewExpression");
            body.Arguments.ToList().ForEach(x =>
            {
                var lastMeEx = x as MemberExpression;
                if (lastMeEx == null) return;
                var beforeMeEx = lastMeEx.Expression as MemberExpression;
                if (beforeMeEx != null)
                    throw new Exception("selectorTR is not NewExpression");
                source.Add(lastMeEx.Member.Name);
            });
            return source;
        }

        #region List对象纵深
        public static LambdaFields<T1> SelectFields<T1, T2, TR>(this LambdaFields<T1> source,
            Expression<Func<T1, IEnumerable<T2>>> selectorLast,
            Expression<Func<T2, TR>> selectorTR)
            where T1 : class,new()
            where T2 : class,new()
        {
            if (selectorLast == null || selectorTR == null)
                throw new Exception("selector1 or selector is null");
            var lastBody = selectorLast.Body as MemberExpression;
            var body = selectorTR.Body as NewExpression;
            if (lastBody == null || body == null)
                throw new Exception("selector1 or selectorTR is not MemberExpression");
            body.Arguments.ToList().ForEach(x =>
            {
                var lastMeEx = x as MemberExpression;
                if (lastMeEx == null) return;
                var beforeMeEx = lastMeEx.Expression as MemberExpression;
                if (beforeMeEx != null)
                    throw new Exception("selectorTR is not NewExpression");
                source.Add(lastBody.Member.Name + "." + lastMeEx.Member.Name);
            });

            return source;
        }

        public static LambdaFields<T1> SelectFields<T1, T2, T3, TR>(this LambdaFields<T1> source,
            Expression<Func<T1, IEnumerable<T2>>> selector1,
            Expression<Func<T2, IEnumerable<T3>>> selectorLast,
            Expression<Func<T3, TR>> selectorTR)
            where T1 : class,new()
            where T2 : class,new()
            where T3 : class,new()
        {
            if (selectorLast == null || selectorTR == null)
                throw new Exception("selectorLast or selectorTR is null");
            var lastBody = selectorLast.Body as MemberExpression;
            var body = selectorTR.Body as NewExpression;
            if (lastBody == null || body == null)
                throw new Exception("selectorLast or selectorTR is not MemberExpression");
            body.Arguments.ToList().ForEach(x =>
            {
                var lastMeEx = x as MemberExpression;
                if (lastMeEx == null) return;
                var beforeMeEx = lastMeEx.Expression as MemberExpression;
                if (beforeMeEx != null)
                    throw new Exception("selectorTR is not NewExpression");
                source.Add(lastBody.Member.Name + "." + lastMeEx.Member.Name);
            });
            return source;
        }
        #endregion

        #region Single对象纵深
        public static LambdaFields<T1> SelectFields<T1, T2, TR>(this LambdaFields<T1> source,
            Expression<Func<T1, T2>> selectorLast,
            Expression<Func<T2, TR>> selectorTR)
            where T1 : class,new()
            where T2 : class,new()
        {
            if (selectorLast == null || selectorTR == null)
                throw new Exception("selectorLast or selectorTR is null");
            var lastBody = selectorLast.Body as MemberExpression;
            var body = selectorTR.Body as NewExpression;
            if (lastBody == null || body == null)
                throw new Exception("selectorLast or selectorTR is not MemberExpression");
            body.Arguments.ToList().ForEach(x =>
            {
                var lastMeEx = x as MemberExpression;
                if (lastMeEx == null) return;
                var beforeMeEx = lastMeEx.Expression as MemberExpression;
                if (beforeMeEx != null)
                    throw new Exception("selectorTR is not NewExpression");
                source.Add(lastBody.Member.Name + "." + lastMeEx.Member.Name);
            });
            return source;
        }

        public static LambdaFields<T1> SelectFields<T1, T2, T3, TR>(this LambdaFields<T1> source,
            Expression<Func<T1, T2>> selector1,
            Expression<Func<T2, T3>> selectorLast,
            Expression<Func<T3, TR>> selectorTR)
            where T1 : class,new()
            where T2 : class,new()
            where T3 : class,new()
        {
            if (selectorLast == null || selectorTR == null)
                throw new Exception("selectorLast or selectorTR is null");
            var lastBody = selector1.Body as MemberExpression;
            var body = selectorTR.Body as NewExpression;
            if (lastBody == null || body == null)
                throw new Exception("selectorLast or selectorTR is not MemberExpression");
            body.Arguments.ToList().ForEach(x =>
            {
                var lastMeEx = x as MemberExpression;
                if (lastMeEx == null) return;
                var beforeMeEx = lastMeEx.Expression as MemberExpression;
                if (beforeMeEx != null)
                    throw new Exception("selectorTR is not NewExpression");
                source.Add(lastBody.Member.Name + "." + lastMeEx.Member.Name);
            });
            return source;
        }
        #endregion


        #region Single List 交叉纵深
        public static LambdaFields<T1> SelectFields<T1, T2, T3, TR>(this LambdaFields<T1> source,
            Expression<Func<T1, IEnumerable<T2>>> selector1,
            Expression<Func<T2, T3>> selectorLast,
            Expression<Func<T3, TR>> selectorTR)
            where T1 : class,new()
            where T2 : class,new()
            where T3 : class,new()
        {
            if (selectorLast == null || selectorTR == null)
                throw new Exception("selectorLast or selectorTR is null");
            var lastBody = selectorLast.Body as MemberExpression;
            var body = selectorTR.Body as NewExpression;
            if (lastBody == null || body == null)
                throw new Exception("selectorLast or selectorTR is not MemberExpression");
            body.Arguments.ToList().ForEach(x =>
            {
                var lastMeEx = x as MemberExpression;
                if (lastMeEx == null) return;
                var beforeMeEx = lastMeEx.Expression as MemberExpression;
                if (beforeMeEx != null)
                    throw new Exception("selectorTR is not NewExpression");
                source.Add(lastBody.Member.Name + "." + lastMeEx.Member.Name);
            });
            return source;
        }


        public static LambdaFields<T1> SelectFields<T1, T2, T3, TR>(this LambdaFields<T1> source,
            Expression<Func<T1, T2>> selector1,
            Expression<Func<T2, IEnumerable<T3>>> selectorLast,
            Expression<Func<T3, TR>> selectorTR)
            where T1 : class,new()
            where T2 : class,new()
            where T3 : class,new()
        {
            if (selectorLast == null || selectorTR == null)
                throw new Exception("selectorLast or selectorTR is null");
            var lastBody = selector1.Body as MemberExpression;
            var body = selectorTR.Body as NewExpression;
            if (lastBody == null || body == null)
                throw new Exception("selectorLast or selectorTR is not MemberExpression");
            body.Arguments.ToList().ForEach(x =>
            {
                var lastMeEx = x as MemberExpression;
                if (lastMeEx == null) return;
                var beforeMeEx = lastMeEx.Expression as MemberExpression;
                if (beforeMeEx != null)
                    throw new Exception("selectorTR is not NewExpression");
                source.Add(lastBody.Member.Name + "." + lastMeEx.Member.Name);
            });
            return source;
        }
        #endregion
    }
}
