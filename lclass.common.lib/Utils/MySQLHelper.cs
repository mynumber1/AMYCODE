using MySql.Data.MySqlClient;
    using NLog;
    using Spring.Infrastructure.Service.Common;
    using System;
    using System.Configuration;
    using System.Linq;
    using Data.Dapper;
    using System.Data;
    internal class MySQLHelper
    {
        private string _ConnectionString { get; set; }

        private MySqlConnection _Connection = new MySqlConnection();

        internal static MySQLHelper Instance
        {
            get { return new MySQLHelper(ConfigurationManager.ConnectionStrings[Constants.RemarkDB].ConnectionString); }
        }
        internal MySqlConnection GetConnection()
        {
            return _Connection;
        }
        internal MySQLHelper(string ConnnectionString)
        {
            _Connection.ConnectionString = ConnnectionString;
        }
    }

    public class MySQLDatabase
    {
        public static T[] Query<T>(string SQLClases, Data.Dapper.MySQLDynamicParameter Params = null)
        {
            MySqlConnection con = MySQLHelper.Instance.GetConnection();
            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                var rets = con.Query<T>(SQLClases, Params);
                return rets.ToArray();
            }
            catch (Exception ex)
            {
                LogManager.GetLogger(LoggerNames.DataAccessLog).Error(Errors.QueryError, ex);
                return default(T[]);
            }
            finally
            {
                con.Close();
            }
        }

        public static bool Execute(string SQLClases, MySQLDynamicParameter Params = null)
        {
            MySqlConnection con = MySQLHelper.Instance.GetConnection();
            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                con.Execute(SQLClases, Params);
                return true;
            }
            catch (Exception ex)
            {
                LogManager.GetLogger(LoggerNames.DataAccessLog).Error(Errors.ExecuteError, ex);
                return false;
            }
            finally
            {
                con.Close();
            }
        }

        public static bool Execute(string[] SQLClases, MySQLDynamicParameter[] Params = null)
        {
            MySqlConnection con = MySQLHelper.Instance.GetConnection();
            if (con.State != ConnectionState.Open)
                con.Open();
            var trans = con.BeginTransaction();
            try
            {
                for (int i = 0; i <= SQLClases.Length - 1; i++)
                {
                    con.Execute(SQLClases[i], Params[i]);
                }

                trans.Commit();
                return true;
            }
            catch (Exception ex)
            {
                LogManager.GetLogger(LoggerNames.DataAccessLog).Error(Errors.ExecuteError, ex);
                trans.Rollback();
                return false;
            }
            finally
            {
                trans.Dispose();
                con.Close();
            }
        }
        public static DataSet Query(string sql, MySqlParameter[] Params = null)
        {
            DataSet ds = new DataSet();
            MySqlConnection con = MySQLHelper.Instance.GetConnection();
            try
            {
                MySqlCommand command = new MySqlCommand(sql, con);

                if (null != Params)
                {
                    command.Parameters.AddRange(Params);
                }

                MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                if (con.State != ConnectionState.Open)
                    con.Open();
                adapter.Fill(ds);

                return ds;
            }
            catch (Exception ex)
            {
                LogManager.GetLogger(LoggerNames.DataAccessLog).Error(Errors.QueryError, ex);
                return ds;
            }
            finally
            {
                con.Close();
            }
        }
        public static T Scalar<T>(string sql, MySqlParameter[] parameters = null)
        {
            T result = default(T);
            MySqlConnection con = MySQLHelper.Instance.GetConnection();
            try
            {
                MySqlCommand command = new MySqlCommand(sql, con);

                if (null != parameters)
                {
                    command.Parameters.AddRange(parameters);
                }

                if (con.State != ConnectionState.Open)
                    con.Open();

                object retValue = command.ExecuteScalar();

                if (null != retValue && retValue != DBNull.Value)
                {
                    result = (T)Convert.ChangeType(retValue, typeof(T));
                }
            }
            catch (Exception ex)
            {
                LogManager.GetLogger(LoggerNames.DataAccessLog).Error(Errors.QueryError, ex);
            }
            finally
            {
                con.Close();
            }

            return result;
        }
        /// <summary>
        /// 执行sql
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="SQLClases"></param>
        /// <param name="T1"></param>
        /// <returns></returns>
        public static bool Execute<T>(string SQLClases, T T1)
        {
            MySqlConnection con = MySQLHelper.Instance.GetConnection();
            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                con.Execute(SQLClases, T1);
                return true;
            }
            catch (Exception ex)
            {
                LogManager.GetLogger(LoggerNames.DataAccessLog).Error(Errors.ExecuteError, ex);
                return false;
            }
            finally
            {
                con.Close();
            }
        }

    }
