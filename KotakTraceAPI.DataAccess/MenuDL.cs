using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KotakTraceAPI.DataAccess
{
    public class MenuDL
    {
            DataSet dsResult = new DataSet();
            DataTable dtResult = new DataTable();
            SqlHelper obj = new SqlHelper();
            List<SqlParameter> SqlParameters = new List<SqlParameter>();

            public DataSet GetMenuList(string EmpId)
            {
                try
                {
                    SqlParameter[] param = new SqlParameter[1];
                 
                    param[0] = new SqlParameter();
                    param[0].SqlDbType = SqlDbType.Int;
                    param[0].ParameterName = "Emp_Id";
                    param[0].Value = EmpId;

                    dsResult = obj.ExecuteDataset(obj.GetConnection(), CommandType.StoredProcedure, "SP_MenuBind", param);

                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    obj.CloseConnection();
                }
                return dsResult;
            }

        public static implicit operator MenuDL(UserRoleDL v)
        {
            throw new NotImplementedException();
        }
    }
}
    

