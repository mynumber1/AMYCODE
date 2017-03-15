using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Configuration;

namespace DataAccess
{
    public static class MongoHelper
    {
        #region mongodb 实例
        /// <summary>
        /// mongodb 链接字符串
        /// </summary>
        public static string MongodbConnectionStringName = ConfigurationManager.ConnectionStrings["mongodb"].ConnectionString;
        /// <summary>
        /// 获取 MongoCollection 集合
        /// </summary>
        /// <returns></returns>
        private static MongoCollection GetCollection(string tbName, string dataBaseName)
        {
            if (string.IsNullOrWhiteSpace(MongodbConnectionStringName)) throw new ArgumentException(" MongodbConnectionStringName 无效");

            MongoServer ms = new MongoClient(MongodbConnectionStringName).GetServer();

            return ms.GetDatabase(dataBaseName, new MongoDatabaseSettings
            {
                GuidRepresentation = GuidRepresentation.Standard,
                WriteConcern = WriteConcern.Acknowledged
            }).GetCollection(tbName, new MongoCollectionSettings
            {
                WriteConcern = WriteConcern.Acknowledged,
                GuidRepresentation = GuidRepresentation.Standard,
                AssignIdOnInsert = true,
            });
        }
        #endregion

        #region 插入数据
        /// <summary>
        /// 插入数据
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="obj">要存储的数据</param>
        /// <param name="tbName">集合名称</param>
        /// <param name="dataBaseName">数据库名</param>
        /// <returns>成功：string.Empty；失败：错误信息</returns>
        public static string Insert<T>(T obj, string tbName, string dataBaseName)
        {
            var wcResult = GetCollection(tbName, dataBaseName).Insert(obj);
            return wcResult.Ok ? string.Empty : wcResult.ErrorMessage;
        }
        #endregion

        #region 批量插入数据
        /// <summary>
        /// 批量插入数据
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="obj">要存储的数据</param>
        /// <param name="tbName">集合名称</param>
        /// <param name="dataBaseName">数据库名</param>
        /// <returns>成功：string.Empty；失败：错误信息</returns>
        public static string InsertBatch<T>(IEnumerable<T> obj, string tbName, string dataBaseName)
        {
            var wcResult = GetCollection(tbName, dataBaseName).InsertBatch(obj).ToArray();
            return wcResult.All(n => n.Ok) ? string.Empty : wcResult.First(n => !n.Ok).ErrorMessage;
        }
        #endregion

        #region 获取全部数据
        /// <summary>
        /// 获取数据
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="tbName">集合名称</param>
        /// <param name="dataBaseName">数据库名</param>
        /// <returns><![CDATA[List<T>]]></returns>
        public static T[] FindAllAs<T>(string tbName, string dataBaseName)
        {
            var cursor = GetCollection(tbName, dataBaseName).FindAllAs<T>();
            return cursor.ToArray();
        }
        #endregion

        #region 按照条件获取单个数据
        /// <summary>
        /// 获取数据(按照条件)
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="query">条件字典</param>
        /// <param name="tbName">集合名称</param>
        /// <param name="dataBaseName">数据库名</param>
        /// <returns>返回查询结果</returns>
        public static T FindOneAs<T>(IMongoQuery query, string tbName, string dataBaseName)
        {
            return GetCollection(tbName, dataBaseName).FindOneAs<T>(query);
        }
        #endregion

        #region 按照条件获取数据
        /// <summary>
        /// 按照条件获取数据
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="query">条件字典</param>
        /// <param name="tbName">集合名称</param>
        /// <param name="dataBaseName">数据库名</param>
        /// <returns>返回查询结果</returns>
        public static T[] FindAs<T>(IMongoQuery query, string tbName, string dataBaseName)
        {
            var cursor = GetCollection(tbName, dataBaseName).FindAs<T>(query);
            return cursor.ToArray();
        }
        #endregion

        #region 按照条件获取并更新数据
        /// <summary>
        /// 按照条件获取并更新数据
        /// </summary>
        /// <param name="args">条件字典</param>
        /// <param name="tbName">集合名称</param>
        /// <param name="dataBaseName">数据库名</param>
        /// <returns>返回查询结果</returns>
        public static T FindAndModify<T>(FindAndModifyArgs args, string tbName, string dataBaseName)
        {
            var result = GetCollection(tbName, dataBaseName).FindAndModify(args);
            return result.GetModifiedDocumentAs<T>();
        }
        #endregion

        #region 按照条件获取并删除数据
        /// <summary>
        /// 按照条件获取并删除数据
        /// </summary>
        /// <param name="args">条件字典</param>
        /// <param name="tbName">集合名称</param>
        /// <param name="dataBaseName">数据库名</param>
        /// <returns>返回查询结果</returns>
        public static T FindAndRemove<T>(FindAndRemoveArgs args, string tbName, string dataBaseName)
        {
            var result = GetCollection(tbName, dataBaseName).FindAndRemove(args);
            return result.GetModifiedDocumentAs<T>();
        }
        #endregion

        #region 按照条件更新数据
        /// <summary>
        /// 按照条件更新数据
        /// </summary>
        /// <param name="query">条件字典</param>
        /// <param name="update">更新数据</param>
        /// <param name="tbName">集合名称</param>
        /// <param name="dataBaseName">数据库名</param>
        /// <returns>返回查询结果</returns>
        public static string Update(IMongoQuery query, IMongoUpdate update, string tbName, string dataBaseName)
        {
            var wcResult = GetCollection(tbName, dataBaseName).Update(query, update);
            return wcResult.Ok ? string.Empty : wcResult.ErrorMessage;
        }
        #endregion

        #region 删除所有数据
        /// <summary>
        /// 删除所有数据（慎用）
        /// </summary>
        /// <param name="tbName">集合名称</param>
        /// <param name="dataBaseName">数据库名</param>
        /// <returns>成功：string.Empty；失败：错误信息</returns>
        public static string RemoveAll(string tbName, string dataBaseName)
        {
            var wcResult = GetCollection(tbName, dataBaseName).RemoveAll(WriteConcern.Acknowledged);
            return wcResult.Ok ? string.Empty : wcResult.ErrorMessage;
        }
        #endregion

        #region 删除数据(按照条件)
        /// <summary>
        /// 删除数据(按照条件)
        /// </summary>
        /// <param name="query">条件字典（Key[字段名]-Value[值]）</param>
        /// <param name="tbName">集合名称</param>
        /// <param name="dataBaseName">数据库名</param>
        /// <returns>成功：string.Empty；失败：错误信息</returns>
        public static string Remove(IMongoQuery query, string tbName, string dataBaseName)
        {
            var wcResult = GetCollection(tbName, dataBaseName).Remove(query, RemoveFlags.None, WriteConcern.Acknowledged);
            return wcResult.Ok ? string.Empty : wcResult.ErrorMessage;
        }
        #endregion

        #region 删除指定集合
        /// <summary>
        /// 删除指定集合
        /// </summary>
        /// <param name="tbName">集合名称</param>
        /// <param name="dataBaseName">数据库名</param>
        /// <returns>成功：string.Empty；失败：错误信息</returns>
        public static string Drop(string tbName, string dataBaseName)
        {
            var wcResult = GetCollection(tbName, dataBaseName).Drop();
            return wcResult.Ok ? string.Empty : wcResult.ErrorMessage;
        }

        

        #endregion
    }
}
