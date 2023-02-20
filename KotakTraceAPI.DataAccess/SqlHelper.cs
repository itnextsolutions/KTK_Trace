using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KotakTraceAPI.DataAccess
{
    internal class SqlHelper
    {

        private Hashtable parmCache = Hashtable.Synchronized(new Hashtable());
        public SqlParameter[] cmdParameter;

        private SqlConnection conn;
        public readonly string CON_STRING = Convert.ToString(ConfigurationManager.ConnectionStrings["ConnectionString"]);
        public static string GetConnectionString()
        {
            return Convert.ToString(ConfigurationManager.ConnectionStrings["ConnectionString"]);
        }
        public SqlConnection GetConnection()
        {
            try
            {
                if (conn == null)
                {
                    conn = new SqlConnection();
                    conn.ConnectionString = CON_STRING;
                    // conn.ConnectionString = DLLStringEncrypt.DecryptString(CON_STRING, "ART");
                    conn.Open();
                    return conn;
                }
                else
                {
                    if (conn.State == 0)
                    {
                        conn.Open();
                        return conn;
                    }
                    else
                    {
                        return conn;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void CloseConnection()
        {
            try
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int ExecuteNonQuery(string connString, CommandType cmdType, string cmdText)
        {
            try
            {

                SqlCommand cmd = new SqlCommand();
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    PrepareCommand(cmd, conn, null, cmdType, cmdText, null);
                    int val = cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();
                    return val;
                }
            }
            catch
            {
                throw;
            }
        }
        public DataSet ExecuteDataset(SqlConnection conn, CommandType cmdType, string cmdText, SqlParameter[] cmdParms, bool AssignDataTableName = true)
        {
            DataSet ds;

            try
            {
                ds = ExecuteDataset(conn, cmdType, cmdText, cmdParms);

                if (AssignDataTableName == true)
                {
                    if (ds != null && ds.Tables != null && ds.Tables.Count > 0)
                    {
                        int ds_i = 0;
                        foreach (var parms in cmdParms)
                        {
                            if (parms.SqlDbType == SqlDbType.VarChar)
                            {
                                ds.Tables[ds_i].TableName = parms.ParameterName;
                                ds_i++;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return ds;
        }
        public int ExecuteNonQuery(string connString, CommandType cmdType, string cmdText, params SqlParameter[] cmdParms)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    PrepareCommand(cmd, conn, null, cmdType, cmdText, cmdParms);
                    int val = cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();
                    return val;
                }
            }
            catch
            {
                throw;
            }
        }
        public int ExecuteNonQuery(SqlConnection conn, CommandType cmdType, string cmdText, params SqlParameter[] cmdParms)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                PrepareCommand(cmd, conn, null, cmdType, cmdText, cmdParms);
                int val = cmd.ExecuteNonQuery();
                cmdParameter = cmdParms;
                cmd.Parameters.Clear();
                return val;
            }
            catch
            {
                throw;
            }
        }
        public int ExecuteNonQueryOut(SqlConnection conn, CommandType cmdType, string cmdText, string ParamName, params SqlParameter[] cmdParms)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                PrepareCommand(cmd, conn, null, cmdType, cmdText, cmdParms);

                int val = cmd.ExecuteNonQuery();
                return Convert.ToInt32(cmd.Parameters[ParamName].Value);
            }
            catch
            {
                throw;
            }
        }
        //added by priya on 1 dec 2012
        public object[] ExecuteNonQueryOutMultiple(SqlConnection conn, CommandType cmdType, string cmdText, string ParamName, string ParamName1, string ParamName2, params SqlParameter[] cmdParms)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                PrepareCommand(cmd, conn, null, cmdType, cmdText, cmdParms);
                cmd.ExecuteNonQuery();
                return (new object[] { cmd.Parameters[ParamName].Value, cmd.Parameters[ParamName1].Value, cmd.Parameters[ParamName2].Value });
            }
            catch
            {
                throw;
            }
        }

        public long ExecuteNonQueryOutLong(SqlConnection conn, CommandType cmdType, string cmdText, string ParamName, params SqlParameter[] cmdParms)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                PrepareCommand(cmd, conn, null, cmdType, cmdText, cmdParms);
                int val = cmd.ExecuteNonQuery();
                return Convert.ToInt64(cmd.Parameters[ParamName].Value);
            }
            catch
            {
                throw;
            }
        }

        public string ExecuteNonQueryOutString(SqlConnection conn, CommandType cmdType, string cmdText, string ParamName, params SqlParameter[] cmdParms)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                PrepareCommand(cmd, conn, null, cmdType, cmdText, cmdParms);
                int val = cmd.ExecuteNonQuery();
                return cmd.Parameters[ParamName].Value.ToString();
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Execute a SqlCommand (that returns no resultset) against an existing database connection 
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  int result = ExecuteNonQuery(connString, CommandType.StoredProcedure, "PublishOrders", new SqlParameter("@prodid", 24));
        /// </remarks>
        /// <param name="conn">an existing database connection</param>
        /// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">the stored procedure name or T-Oracle command</param>
        /// <param name="commandParameters">an array of OracleParamters used to execute the command</param>
        /// <returns>an int representing the number of rows affected by the command</returns>


        /// <summary>
        /// Execute a SqlCommand (that returns no resultset) using an existing Oracle Transaction 
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  int result = ExecuteNonQuery(connString, CommandType.StoredProcedure, "PublishOrders", new SqlParameter("@prodid", 24));
        /// </remarks>
        /// <param name="trans">an existing Oracle transaction</param>
        /// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">the stored procedure name or T-Oracle command</param>
        /// <param name="commandParameters">an array of OracleParamters used to execute the command</param>
        /// <returns>an int representing the number of rows affected by the command</returns>
        public int ExecuteNonQuery(SqlTransaction trans, CommandType cmdType, string cmdText, params SqlParameter[] cmdParms)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                PrepareCommand(cmd, trans.Connection, trans, cmdType, cmdText, cmdParms);
                int val = cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
                return val;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Execute a SqlCommand that returns a resultset against the database specified in the connection string 
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  SqlDataReader r = ExecuteReader(connString, CommandType.StoredProcedure, "PublishOrders", new SqlParameter("@prodid", 24));
        /// </remarks>
        /// <param name="connectionString">a valid connection string for a SqlConnection</param>
        /// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">the stored procedure name or T-Oracle command</param>
        /// <param name="commandParameters">an array of OracleParamters used to execute the command</param>
        /// <returns>A SqlDataReader containing the results</returns>
        /*public static SqlDataReader ExecuteReader(string connString, CommandType cmdType, string cmdText, params SqlParameter[] cmdParms) {
            SqlCommand cmd = new SqlCommand();
            SqlConnection conn = new SqlConnection(connString);

            // we use a try/catch here because if the method throws an exception we want to 
            // close the connection throw code, because no datareader will exist, hence the 
            // commandBehaviour.CloseConnection will not work
            try {
                PrepareCommand(cmd, conn, null, cmdType, cmdText, cmdParms);
                SqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                cmd.Parameters.Clear();
                return rdr;
            }catch {
                conn.Close();
                throw;
            }
        }*/
        public SqlDataReader ExecuteReader(SqlConnection conn, CommandType cmdType, string cmdText, params SqlParameter[] cmdParms)
        {

            SqlCommand cmd = new SqlCommand();

            // we use a try/catch here because if the method throws an exception we want to 
            // close the connection throw code, because no datareader will exist, hence the 
            // commandBehaviour.CloseConnection will not work
            try
            {
                PrepareCommand(cmd, conn, null, cmdType, cmdText, cmdParms);
                SqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                cmd.Parameters.Clear();
                return rdr;
            }
            catch
            {
                conn.Close();
                throw;
            }

        }
        /// <summary>
        /// Execute a SqlCommand that returns the first column of the first record against the database specified in the connection string 
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  Object obj = ExecuteScalar(connString, CommandType.StoredProcedure, "PublishOrders", new SqlParameter("@prodid", 24));
        /// </remarks>
        /// <param name="connectionString">a valid connection string for a SqlConnection</param>
        /// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">the stored procedure name or T-Oracle command</param>
        /// <param name="commandParameters">an array of OracleParamters used to execute the command</param>
        /// <returns>An object that should be converted to the expected type using Convert.To{Type}</returns>
        public object ExecuteScalar(string connString, CommandType cmdType, string cmdText, params SqlParameter[] cmdParms)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();

                using (SqlConnection conn = new SqlConnection(connString))
                {
                    PrepareCommand(cmd, conn, null, cmdType, cmdText, cmdParms);
                    object val = cmd.ExecuteScalar();
                    cmd.Parameters.Clear();
                    return val;
                }
            }
            catch
            {
                throw;
            }
        }


        /// <summary>
        /// Execute a SqlCommand that returns the first column of the first record against an existing database connection 
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  Object obj = ExecuteScalar(connString, CommandType.StoredProcedure, "PublishOrders", new SqlParameter("@prodid", 24));
        /// </remarks>
        /// <param name="conn">an existing database connection</param>
        /// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">the stored procedure name or T-Oracle command</param>
        /// <param name="commandParameters">an array of OracleParamters used to execute the command</param>
        /// <returns>An object that should be converted to the expected type using Convert.To{Type}</returns>
        public object ExecuteScalar(SqlConnection conn, CommandType cmdType, string cmdText, params SqlParameter[] cmdParms)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();

                PrepareCommand(cmd, conn, null, cmdType, cmdText, cmdParms);
                object val = cmd.ExecuteScalar();

                cmd.Parameters.Clear();
                return val;
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="cmdType"></param>
        /// <param name="cmdText"></param>
        /// <param name="cmdParms"></param>
        /// <returns></returns>



        public DataTable ExecuteDataTable(SqlConnection conn, CommandType cmdType, string cmdText, SqlParameter[] cmdParms)
        {

            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter da;

            DataTable dt;

            try
            {

                //PrepareCommand(cmd, conn, null, cmdType, cmdText, cmdParms);

                dt = new DataTable();
                cmd.Connection = conn;
                cmd.CommandText = cmdText;
                cmd.CommandType = cmdType;

                if (cmdParms != null)
                {
                    foreach (SqlParameter parm in cmdParms)
                        cmd.Parameters.Add(parm);
                }

                //create the DataAdapter & DataSet
                da = new SqlDataAdapter(cmd);

                //fill the DataSet using default values for DataTable names, etc.
                try
                {
                    da.Fill(dt);
                }

                catch (Exception ex)
                {
                    throw;
                }
                cmd.Parameters.Clear();

                //return the dataset
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
            }
        }
        public DataTable ExecuteDataTable(SqlConnection conn, CommandType cmdType, string cmdText)
        {
            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter da;

            DataTable dt;

            try
            {

                //PrepareCommand(cmd, conn, null, cmdType, cmdText, cmdParms);

                dt = new DataTable();
                cmd.Connection = conn;
                cmd.CommandText = cmdText;
                cmd.CommandType = cmdType;
                //create the DataAdapter & DataSet
                da = new SqlDataAdapter(cmd);
                //fill the DataSet using default values for DataTable names, etc.
                try
                {
                    da.Fill(dt);
                }

                catch (Exception ex)
                {
                    throw ex;
                }
                cmd.Parameters.Clear();

                //return the dataset
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="cmdType"></param>
        /// <param name="cmdText"></param>
        /// <param name="cmdParms"></param>
        /// <returns></returns>
        public DataSet ExecuteDataset(SqlConnection conn, CommandType cmdType, string cmdText, SqlParameter[] cmdParms)
        {
            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter da;
            //cmd.CommandTimeout = 600;
            DataSet ds;

            try
            {

                PrepareCommand(cmd, conn, null, cmdType, cmdText, cmdParms);

                ds = new DataSet();
                cmd.Connection = conn;
                cmd.CommandText = cmdText;
                cmd.CommandType = cmdType;

                //create the DataAdapter & DataSet
                da = new SqlDataAdapter(cmd);
                //fill the DataSet using default values for DataTable names, etc.
                da.Fill(ds);

                cmd.Parameters.Clear();

                //return the dataset
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }

        }
        public DataTable ExecuteDataTable(SqlConnection conn, CommandType cmdType, string cmdText, SqlParameter[] cmdParms, bool AssignDataTableName = true)
        {
            DataTable dt;

            try
            {

                dt = ExecuteDataTable(conn, cmdType, cmdText, cmdParms);

                if (AssignDataTableName == true)
                {
                    if (dt != null)
                    {
                        int ds_i = 0;
                        foreach (var parms in cmdParms)
                        {
                            if (parms.SqlDbType == SqlDbType.VarChar)
                            {
                                dt.TableName = parms.ParameterName;
                                ds_i++;
                                continue;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return dt;
        }
        /// <summary>
        /// add parameter array to the cache
        /// </summary>
        /// <param name="cacheKey">Key to the parameter cache</param>
        /// <param name="cmdParms">an array of OracleParamters to be cached</param>
        public void CacheParameters(string cacheKey, params SqlParameter[] cmdParms)
        {
            parmCache[cacheKey] = cmdParms;
        }

        /// <summary>
        /// Retrieve cached parameters
        /// </summary>
        /// <param name="cacheKey">key used to lookup parameters</param>
        /// <returns>Cached OracleParamters array</returns>
        public SqlParameter[] GetCachedParameters(string cacheKey)
        {
            SqlParameter[] cachedParms = (SqlParameter[])parmCache[cacheKey];

            if (cachedParms == null)
                return null;

            SqlParameter[] clonedParms = new SqlParameter[cachedParms.Length];

            for (int i = 0, j = cachedParms.Length; i < j; i++)
                clonedParms[i] = (SqlParameter)((ICloneable)cachedParms[i]).Clone();

            return clonedParms;
        }

        /// <summary>
        /// Prepare a command for execution
        /// </summary>
        /// <param name="cmd">SqlCommand object</param>
        /// <param name="conn">SqlConnection object</param>
        /// <param name="trans">OracleTransaction object</param>
        /// <param name="cmdType">Cmd type e.g. stored procedure or text</param>
        /// <param name="cmdText">Command text, e.g. Select * from Products</param>
        /// <param name="cmdParms">SqlParameters to use in the command</param>
        private void PrepareCommand(SqlCommand cmd, SqlConnection conn, SqlTransaction trans, CommandType cmdType, string cmdText, SqlParameter[] cmdParms)
        {
            try
            {


                if (conn.State != ConnectionState.Open)
                    conn.Open();

                cmd.Connection = conn;
                cmd.CommandText = cmdText;

                if (trans != null)
                    cmd.Transaction = trans;

                cmd.CommandType = cmdType;

                if (cmdParms != null)
                {
                    foreach (SqlParameter parm in cmdParms)
                        cmd.Parameters.Add(parm);
                }
            }
            catch
            {
                throw;
            }

        }


    }
}

