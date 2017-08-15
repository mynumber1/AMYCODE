namespace Spring.Admin.Database
{
    using Spring.Admin.Common.Utility;
    using Data.Dapper;
    using MySql.Data.MySqlClient;
    using Spring.Admin.Common;
    using System;
    using System.Configuration;
    using System.Linq;
    using Spring.Admin.Models.Common.Enum;
    using Spring.Admin.Models.Common;
    using System.Data;
    internal class MySQLHelper
    {
        private string _ConnectionString { get; set; }

        private MySqlConnection _Connection = new MySqlConnection();

        private static MySQLHelper _Instance = new MySQLHelper(ConfigurationManager.ConnectionStrings["mysqldb"].ConnectionString);


        internal static MySQLHelper Instance
        {
            get { return _Instance; }
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
        public static T[] Query<T>(string SQLClases, MySQLDynamicParameter Params = null)
        {
            //MySqlConnection con = MySQLHelper.Instance.GetConnection();
            MySqlConnection con = new MySqlConnection(ConfigurationManager.ConnectionStrings["mysqldb"].ConnectionString);
            try
            {
                con.Open();
                var rets = con.Query<T>(SQLClases, Params);
                return rets.ToArray();
            }
            catch (Exception ex)
            {
                LogUtility.Instance.LogError("CMS-MySqlHelper-Query", "", ex);
                return null;
            }
            finally
            {
                con.Close();
            }
        }
        public static DataSet Query(string sql, MySqlParameter[] Params = null)
        {
            DataSet ds = new DataSet();
            //MySqlConnection con = MySQLHelper.Instance.GetConnection();
            MySqlConnection con = new MySqlConnection(ConfigurationManager.ConnectionStrings["mysqldb"].ConnectionString);
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
                LogUtility.Instance.LogError("CMS-MySqlHelper-Query(DataSet)", "", ex);
                return ds;
            }
            finally
            {
                con.Close();
            }
        }
        public static T ExecuteScalar<T>(string SqlClass, MySqlParameter[] Params = null)
        {
            //MySqlConnection con = MySQLHelper.Instance.GetConnection();
            MySqlConnection con = new MySqlConnection(ConfigurationManager.ConnectionStrings["mysqldb"].ConnectionString);
            try
            {
                MySqlCommand command = new MySqlCommand(SqlClass, con);
                con.Open();
                if (null != Params)
                {
                    command.Parameters.AddRange(Params);
                }

                object o = command.ExecuteScalar();

                return (T)o;
            }
            catch (Exception ex)
            {
                LogUtility.Instance.LogError("CMS-MySqlHelper-Query(Scalar)", "", ex);
                return default(T);
            }
            finally
            {
                con.Close();
            }
        }
        public static BaseReturnObj Execute(string SQLClases, MySQLDynamicParameter Params = null)
        {
            BaseReturnObj ret = new BaseReturnObj();
            //MySqlConnection con = MySQLHelper.Instance.GetConnection();
            MySqlConnection con = new MySqlConnection(ConfigurationManager.ConnectionStrings["mysqldb"].ConnectionString);
            try
            {
                con.Open();
                var Lines = con.Execute(SQLClases, Params);
                if (Lines >= 0)
                {
                    ret.Code = ReturnState.Success;
                }
                else
                {
                    ret.Code = "Data Error";
                }
            }
            catch (Exception ex)
            {
                LogUtility.Instance.LogError("CMS-MySqlHelper-Execute", "", ex);
                ret.Code = ex.Message;
            }
            finally
            {
                con.Close();
            }
            return ret;
        }
        public static bool Execute<T>(string SQLClases, T T1)
        {
            MySqlConnection con = new MySqlConnection(ConfigurationManager.ConnectionStrings["mysqldb"].ConnectionString);
            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                con.Execute(SQLClases, T1);
                return true;
            }
            catch (Exception ex)
            {
                LogUtility.Instance.LogError("CMS-OracleDatabase-Execute<T>", "", ex);
                return false;
            }
            finally
            {
                con.Close();
            }
        }
        public static int ExecuteAffectLines(string SQLClases, MySQLDynamicParameter Params = null)
        {
            //-1为SQL语句有问题
            int AffectLines = -1;
            BaseReturnObj ret = new BaseReturnObj();
            //MySqlConnection con = MySQLHelper.Instance.GetConnection();
            MySqlConnection con = new MySqlConnection(ConfigurationManager.ConnectionStrings["mysqldb"].ConnectionString);
            try
            {
                con.Open();
                AffectLines = con.Execute(SQLClases, Params);
            }
            catch (Exception ex)
            {
                LogUtility.Instance.LogError("CMS-MySqlHelper-Execute", "", ex);
            }
            finally
            {
                con.Close();
            }
            return AffectLines;
        }

        public static BaseReturnObj Execute(string[] SQLClases, MySQLDynamicParameter[] Params = null)
        {
            BaseReturnObj ret = new BaseReturnObj();
            //MySqlConnection con = MySQLHelper.Instance.GetConnection();
            MySqlConnection con = new MySqlConnection(ConfigurationManager.ConnectionStrings["mysqldb"].ConnectionString);
            con.Open();
            var trans = con.BeginTransaction();
            try
            {
                for (int i = 0; i <= SQLClases.Length - 1; i++)
                {
                    var Lines = con.Execute(SQLClases[i], Params[i]);
                    if (Lines >= 0)
                    {
                        ret.Code = ReturnState.Success;
                    }
                    else
                    {
                        ret.Code = "Data Error";
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Instance.LogError("CMS-MySqlHelper-Execute", "", ex);
                ret.Code = ex.Message;
            }
            finally
            {
                if (ret.Code != ReturnState.Success)
                {
                    trans.Rollback();
                }
                else
                {
                    trans.Commit();
                    ret.Code = ReturnState.Success;
                }
                trans.Dispose();
                con.Close();
            }
            return ret;
        }


        public static DataSet Query(string ConnetionStr, string SQLClases, MySqlParameter[] Params = null)
        {
            DataSet ds = new DataSet();
            MySqlConnection con = new MySqlConnection();
            con.ConnectionString = ConnetionStr;
            try
            {
                MySqlCommand command = new MySqlCommand(SQLClases, con);

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
                LogUtility.Instance.LogError("CMS-MySqlHelper-Query(DataSet)", "", ex);
                return ds;
            }
            finally
            {
                con.Close();
            }

        }
    }
}

