using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KotakTraceAPI.DataAccess
{
    public class ErrorLogDL
    {
        public static DataTable InsertLog(string ExceptionMsg, string Username, string EmpId, string Procedure, string Module, string MethodName)
        {

            DataTable dtResult = new DataTable();
            SqlHelper obj = new SqlHelper();
            List<SqlParameter> SqlParameters = new List<SqlParameter>();
            try
            {
                SqlParameter[] param = new SqlParameter[7];
                param[0] = new SqlParameter();
                param[0].SqlDbType = SqlDbType.VarChar;
                param[0].ParameterName = "p_exception_msg";
                param[0].Value = ExceptionMsg;
                param[0].Direction = ParameterDirection.Input;

                param[1] = new SqlParameter();
                param[1].SqlDbType = SqlDbType.VarChar;
                param[1].ParameterName = "p_username";
                param[1].Value = Username;
                param[1].Direction = ParameterDirection.Input;

                param[2] = new SqlParameter();
                param[2].SqlDbType = SqlDbType.VarChar;
                param[2].ParameterName = "p_empid";
                param[2].Value = EmpId;
                param[2].Direction = ParameterDirection.Input;

                param[3] = new SqlParameter();
                param[3].SqlDbType = SqlDbType.VarChar;
                param[3].ParameterName = "p_methodname";
                param[3].Value = MethodName;
                param[3].Direction = ParameterDirection.Input;

                param[4] = new SqlParameter();
                param[4].SqlDbType = SqlDbType.VarChar;
                param[4].ParameterName = "p_procedure";
                param[4].Value = Procedure;
                param[4].Direction = ParameterDirection.Input;

                param[5] = new SqlParameter();
                param[5].SqlDbType = SqlDbType.VarChar;
                param[5].ParameterName = "p_MODULENAME";
                param[5].Value = Module;
                param[5].Direction = ParameterDirection.Input;

                param[6] = new SqlParameter();
                param[6].SqlDbType = SqlDbType.VarChar;
                param[6].ParameterName = "ocur";
                param[6].Direction = ParameterDirection.Output;

                dtResult = obj.ExecuteDataTable(obj.GetConnection(), CommandType.StoredProcedure, "ISUPP_INSERTERRORLOG", param);


            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                obj.CloseConnection();
            }
            return dtResult;
        }

    }
}
    

