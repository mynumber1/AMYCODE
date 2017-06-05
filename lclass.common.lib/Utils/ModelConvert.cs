using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Reflection;
using Newtonsoft.Json;

namespace Spring.Infrastructure.Service.Common
{
    public static class ModelConvert
    {
        public static object ConvertObject(object inputObject, object convertObject)
        {    
            if (inputObject == null)
            {
                return null;
            }
            PropertyInfo[] properties = inputObject.GetType().GetProperties(BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.Public);

            foreach (PropertyInfo property in properties)
            {
                PropertyInfo tempProperty = convertObject.GetType().GetProperty(property.Name, BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.Public);
                if (tempProperty != null)
                {
                    if (tempProperty.PropertyType.Name == "ArrayOfLong")
                    {
                        continue;
                    }
                    if (tempProperty.PropertyType.Name == "ArrayOfString")
                    {
                        continue;
                    }
                    if (tempProperty.PropertyType.Name != "List`1")
                    {
                        tempProperty.SetValue(convertObject, property.GetValue(inputObject, null), null);
                    }

                }
            }

            return convertObject;
        }



        /// <summary>
        /// 转换单个对象为另外一种类型对象
        /// </summary>
        /// <typeparam name="TSource">待转换的源对象类型</typeparam>
        /// <typeparam name="TResult">转换的目标对象类型</typeparam>
        /// <param name="source">待转换的源对象</param>
        /// <returns>转换的目标对象</returns>
        public static TResult ConvertObject<TSource, TResult>(TSource source) where TResult : new()
        {
            TResult result = new TResult();

            Type sourceType = source.GetType();
            Type resultType = result.GetType();

            PropertyInfo[] resultProperties = resultType.GetProperties(
                BindingFlags.Public | BindingFlags.Instance);

            if (resultProperties != null && resultProperties.Length > 0)
            {
                foreach (PropertyInfo resultProperty in resultProperties)
                {
                    if (resultProperty.PropertyType.IsGenericType)
                    {
                        continue;
                    }

                    PropertyInfo sourceProperty = sourceType.GetProperty(resultProperty.Name,
                        BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);

                    bool isMatched = sourceProperty != null &&
                            (!sourceProperty.PropertyType.IsGenericType) &&
                            (sourceProperty.PropertyType == resultProperty.PropertyType);

                    if (isMatched)
                    {
                        object currentValue = sourceProperty.GetValue(source, null);
                        resultProperty.SetValue(result, currentValue, null);
                    }
                    else
                    {
                        //非泛型
                        resultProperty.SetValue(result, sourceProperty.GetValue(source, null), null);

                    }

                }
            }
            return result;
        }




        /// <summary>
        /// 转换列表对象为另外一种列表对象
        /// </summary>
        /// <typeparam name="TSource">待转换的源对象类型</typeparam>
        /// <typeparam name="TResult">转换的目标对象类型</typeparam>
        /// <param name="source">待转换的源对象</param>
        /// <returns>转换的目标对象</returns>
        public static List<TResult> ConvertList<TSource, TResult>(List<TSource> source) where TResult : new()
        {
            return source.ConvertAll<TResult>(ConvertObject<TSource, TResult>);
        }


        /// <summary>
        /// 不同对象之间的深拷贝，最好属性名一样
        /// </summary>
        /// <typeparam name=”T”>源对象类型</typeparam>
        /// <typeparam name=”TF”>目的对象类型</typeparam>
        /// <param name=”original”>源对象</param>
        /// <returns>目的对象</returns>
        public static TF DeepCopy<T, TF>(T original)
        {
            string json = JsonConvert.SerializeObject(original);
            var result = JsonConvert.DeserializeObject<TF>(json);
            return result;

        }

        public static TF DeepCopy<T, TF>(T original, Formatting formatting, params JsonConverter[] converters)
        {
            string json = JsonConvert.SerializeObject(original,formatting,converters);
            var result = JsonConvert.DeserializeObject<TF>(json);
            return result;

        }

    }
}
