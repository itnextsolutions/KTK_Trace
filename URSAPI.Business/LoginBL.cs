using KotakTraceAPI.DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KotakTraceAPI.Business
{
    public class LoginBL
    {
        LoginDL objLoginDL = new LoginDL();

        public static LoginBL _this;

        public static LoginBL ThisClass
        {
            get
            {
                if (_this == null)
                    _this = new LoginBL();
                return _this;
            }
            set { _this = value; }
        }

        public DataTable GetloginDetails(string Username, string Password)
        {
            return objLoginDL.GetloginDetails(Username, Password);
        }

        public DataTable UpdateFlag(string Username)
        {
            return objLoginDL.UpdateFlag(Username);
        }
        public DataSet QuerySubmit(string Query)

        {
            return objLoginDL.QuerySubmit(Query);
        }
        public DataTable DMLQuerySubmit(string Query)
        {
            return objLoginDL.DMLQuerySubmit(Query);
        }
        public DataTable CheckIslogin(string Username)
        {
            return objLoginDL.CheckIslogin(Username);
        }

        public DataTable GetUserlogedin(string Username)
        {
            return objLoginDL.GetUserlogedin(Username);
        }

    }
}
