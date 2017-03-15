namespace DataAccess
{
    using Oracle.DataAccess.Client;
    using NLog;
    using Spring.Infrastructure.Service.Common;
    using System;
    using System.Configuration;
    using System.Linq;
 
    using Dapper;

using System.Data;
    internal class OracleHelper
    {
        
        private string _ConnectionString { get; set; }

        private OracleConnection _Connection = new OracleConnection();

        internal static OracleHelper Instance
        {
            get { return new OracleHelper(ConfigurationManager.ConnectionStrings[Constants.HaiTaoDB].ConnectionString); }
        }
        internal OracleConnection GetConnection()
        {
            return _Connection;
        }
        internal OracleHelper(string ConnnectionString) 
        {
            _Connection.ConnectionString = ConnnectionString;
        }
    }

    public class OracleDatabase
    {
        //public static T[] Query<T>(string SQLClases, Dapper.OracleDynamicParameters Params = null)
        //{
        //    OracleConnection con = OracleHelper.Instance.GetConnection();
        //    try
        //    {
        //        if (con.State != ConnectionState.Open)
        //            con.Open();
        //        var rets = con.Query<T>(SQLClases, Params);

        //        return rets.ToArray();
        //    }
        //    catch (Exception ex)
        //    {
        //        LogManager.GetLogger(LoggerNames.DataAccessLog).Error(Errors.QueryError, ex);
        //        return default(T[]);
        //    }
        //    finally
        //    {
        //        con.Close();
        //    }
        //}

        public static T[] Query<T>(string SQLClases,dynamic dyn=null) {
            OracleConnection con = OracleHelper.Instance.GetConnection();
            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                var rets = con.Query<T>(SQLClases, (object)dyn);
             
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
        


        /// <summary>
        /// added by shuai 2015-08-13
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T"></typeparam>
        /// <param name="SQLClases"></param>
        /// <param name="Map"></param>
        /// <param name="Params"></param>
        /// <param name="SplitOn"></param>
        /// <returns></returns>
        public static T[] Query<T1, T2, T>(string SQLClases, Func<T1, T2, T> Map,dynamic Params = null, string SplitOn = "")
        {
            OracleConnection con = OracleHelper.Instance.GetConnection();
            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                var rets = con.Query<T1, T2, T>(SQLClases, Map, (object)Params, splitOn: SplitOn);
              
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
        /// <summary>
        /// added by shuai 2015-08-13
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <typeparam name="T"></typeparam>
        /// <param name="SQLClases"></param>
        /// <param name="Map"></param>
        /// <param name="Params"></param>
        /// <param name="SplitOn"></param>
        /// <returns></returns>

        public static T[] Query<T1, T2,T3, T>(string SQLClases, Func<T1, T2,T3, T> Map,dynamic Params = null, string SplitOn = "")
        {
            OracleConnection con = OracleHelper.Instance.GetConnection();
            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                var rets = con.Query<T1, T2,T3, T>(SQLClases, Map, (object)Params, splitOn: SplitOn);

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
        /// <summary>
        /// added by shuai 2015-08-13
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <typeparam name="T4"></typeparam>
        /// <typeparam name="T"></typeparam>
        /// <param name="SQLClases"></param>
        /// <param name="Map"></param>
        /// <param name="Params"></param>
        /// <param name="SplitOn"></param>
        /// <returns></returns>
        public static T[] Query<T1, T2, T3, T4, T>(string SQLClases, Func<T1, T2, T3, T4, T> Map, dynamic Params = null, string SplitOn = "")
        {
            OracleConnection con = OracleHelper.Instance.GetConnection();
            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                var rets = con.Query<T1, T2, T3, T4,T>(SQLClases, Map, (object)Params, splitOn: SplitOn);

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


        public static T[] Query<T1, T2, T3, T4, T5, T>(string SQLClases, Func<T1, T2, T3, T4, T5, T> Map, Dapper.OracleDynamicParameters Params = null, string SplitOn = "")
        {
            OracleConnection con = OracleHelper.Instance.GetConnection();
            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                var rets = con.Query<T1, T2, T3, T4, T5,T>(SQLClases, Map, Params, splitOn: SplitOn);

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


        public static bool Execute<T>(string SQLClases, T T1)
        {
            OracleConnection con = OracleHelper.Instance.GetConnection();
            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                con.Execute(SQLClases,T1);
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

        public static bool Execute(string SQLClases,Dapper.OracleDynamicParameters Params = null)
        {
            OracleConnection con = OracleHelper.Instance.GetConnection();
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
            finally {
                con.Close();
            }
        }

        public static bool Execute(string[] SQLClases, Dapper.OracleDynamicParameters[] Params = null)
        {
            OracleConnection con = OracleHelper.Instance.GetConnection();
            if (con.State != ConnectionState.Open)
                con.Open();
            var trans = con.BeginTransaction();
            try
            {
                for (int i = 0; i <= SQLClases.Length - 1; i++ )
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

        public static bool Execute(string[] SQLClases,dynamic[] Params = null)
        {
            OracleConnection con = OracleHelper.Instance.GetConnection();
            if (con.State != ConnectionState.Open)
                con.Open();
            var trans = con.BeginTransaction();
            try
            {
                for (int i = 0; i <= SQLClases.Length - 1; i++)
                {
                    con.Execute(SQLClases[i],(object)Params[i]);
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



        public static DataSet Query(string sql, OracleParameter[] Params = null)
        {
            DataSet ds = new DataSet();
            OracleConnection con = OracleHelper.Instance.GetConnection();
            try
            {
         
                OracleCommand command = new OracleCommand(sql,con);

                command.CommandType = CommandType.Text;

                command.BindByName = true;
          
                if (null != Params)
                {
                    command.Parameters.AddRange(Params);
                }
                
                OracleDataAdapter adapter = new OracleDataAdapter(command);
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
        public static T Scalar<T>(string sql, OracleParameter[] parameters = null)
        {
            T result = default(T);
            OracleConnection con = OracleHelper.Instance.GetConnection();
            try
            {
                OracleCommand command = new OracleCommand(sql, con);

                if (null != parameters)
                {
                    command.Parameters.AddRange(parameters);
                }

                if (con.State != ConnectionState.Open)
                    con.Open();

                object retValue = command.ExecuteScalar();

                if (null != retValue&&retValue!=DBNull.Value)
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
    }
}

