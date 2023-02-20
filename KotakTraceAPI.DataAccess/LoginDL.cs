using KotakTracePortal.Shared;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KotakTraceAPI.DataAccess
{
    public class LoginDL
    {
        public DataTable GetloginDetails(string Username, string Password)
        {
            DataTable dtResult = new DataTable();
            SqlHelper obj = new SqlHelper();
            List<SqlParameter> SqlParameters = new List<SqlParameter>();
            try
            {
                SqlParameter[] param = new SqlParameter[2];

                param[0] = new SqlParameter();
                param[0].ParameterName = "@Username";
                param[0].SqlDbType = SqlDbType.VarChar;
                param[0].Value = Username;
                param[0].Direction = ParameterDirection.Input;

                param[1] = new SqlParameter();
                param[1].ParameterName = "@Password";
                param[1].SqlDbType = SqlDbType.VarChar;
                param[1].Value = Password;
                param[1].Direction = ParameterDirection.Input;

                dtResult = obj.ExecuteDataTable(obj.GetConnection(), CommandType.StoredProcedure, "SP_VerifyLogin", param);
                return dtResult;

            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                obj.CloseConnection();
            }
            return dtResult;
        }

        public DataTable UpdateFlag(string Username)
        {

            DataTable dtResult = new DataTable();
            SqlHelper obj = new SqlHelper();
            List<SqlParameter> SqlParameters = new List<SqlParameter>();
            try
            {
                SqlParameter[] param = new SqlParameter[2];
                param[0] = new SqlParameter();
                param[0].SqlDbType = SqlDbType.VarChar;
                param[0].ParameterName = "ocur";
                param[0].Direction = ParameterDirection.Output;
                param[1] = new SqlParameter();
                param[1].SqlDbType = SqlDbType.VarChar;
                param[1].ParameterName = "EmpNTID";
                param[1].Value = Username;
                param[1].Direction = ParameterDirection.Input;
                dtResult = obj.ExecuteDataTable(obj.GetConnection(), CommandType.StoredProcedure, "Ezeepay_COMMON.GETUSERLOGout", param);
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                obj.CloseConnection();
            }
            return dtResult;
        }
        public DataTable DMLQuerySubmit(string Query)
        {

            DataTable dtResult = new DataTable();
            dtResult.Columns.Add("Column1");
            DataRow row = dtResult.NewRow();
            SqlHelper obj = new SqlHelper();

            try
            {
                SqlConnection con = obj.GetConnection();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = Query;
                cmd.ExecuteNonQuery();

                con.Close();


                row["Column1"] = "1";
                dtResult.Rows.Add(row);


            }
            catch (Exception ex)
            {
                ErrorLogDL.InsertLog(ex.Message, "system", "system", "system", "Commands", "DMLQuerySubmit");
                throw;
                row["Column1"] = "0";
                dtResult.Rows.Add(row);
                ErrorLogDL.InsertLog(ex.Message, "system", "system", "system", "Commands", "DMLQuerySubmit");
                return dtResult;
            }
            finally
            {
                obj.CloseConnection();
            }
            return dtResult;
        }
        public DataSet QuerySubmit(string Query)
        {

            DataSet dtResult = new DataSet();
            SqlHelper obj = new SqlHelper();

            try
            {
                SqlConnection con = obj.GetConnection();
                SqlCommand cmd = new SqlCommand(Query, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);

                da.Fill(dtResult);
                con.Close();



            }
            catch (Exception ex)
            {
                Cls_Common.LogToFile(Cls_Common.MessageType.App_Exception, "1.0", "Exception", ex);
                ErrorLogDL.InsertLog(ex.Message, "system", "system", "system", "Commands", "QuerySubmit");

                throw;
                ErrorLogDL.InsertLog(ex.Message, "system", "system", "system", "Commands", "QuerySubmit");
            }
            finally
            {
                obj.CloseConnection();
            }
            return dtResult;
        }
        public DataTable CheckIslogin(string Username)
        {

            DataTable dtResult = new DataTable();
            SqlHelper obj = new SqlHelper();
            List<SqlParameter> SqlParameters = new List<SqlParameter>();
            try
            {
                SqlParameter[] param = new SqlParameter[2];
                param[0] = new SqlParameter();
                param[0].SqlDbType = SqlDbType.VarChar;
                param[0].ParameterName = "ocur";
                param[0].Direction = ParameterDirection.Output;
                param[1] = new SqlParameter();
                param[1].SqlDbType = SqlDbType.VarChar;
                param[1].ParameterName = "EmpNTID";
                param[1].Value = Username;
                param[1].Direction = ParameterDirection.Input;
                dtResult = obj.ExecuteDataTable(obj.GetConnection(), CommandType.StoredProcedure, "Ezeepay_COMMON.GETUSERLOGDETAILS", param);


            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                obj.CloseConnection();
            }
            return dtResult;
        }
        public DataTable GetUserlogedin(string Username)
        {

            DataTable dtResult = new DataTable();
            SqlHelper obj = new SqlHelper();
            List<SqlParameter> SqlParameters = new List<SqlParameter>();
            try
            {
                SqlParameter[] param = new SqlParameter[2];
                param[0] = new SqlParameter();
                param[0].SqlDbType = SqlDbType.VarChar;
                param[0].ParameterName = "ocur";
                param[0].Direction = ParameterDirection.Output;
                param[1] = new SqlParameter();
                param[1].SqlDbType = SqlDbType.VarChar;
                param[1].ParameterName = "EmpNTID";
                param[1].Value = Username;
                param[1].Direction = ParameterDirection.Input;
                dtResult = obj.ExecuteDataTable(obj.GetConnection(), CommandType.StoredProcedure, "Ezeepay_COMMON.GETUSERLOGEDIN", param);


            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                obj.CloseConnection();
            }
            return dtResult;
        }

    }
}

    

