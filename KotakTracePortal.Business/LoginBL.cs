using KotakTracePortal.Shared;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Dynamic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace KotakTracePortal.Buisness
{
    public class LoginBL
    {
        string UrsAPIBaseAddress = ConfigurationManager.AppSettings["UrsAPIBaseAddress"];
        Cls_InOut objCls_InOut = new Cls_InOut();
        DataTable dt = new DataTable();
        DataSet ds = new DataSet();
        string requestUri = "";
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
        public DataTable GetloginDetails(string Username, string Password, string clientIpaddress, string strHostName)
        {
            dynamic dynmodel = new ExpandoObject();
            dynmodel.Username = Username;
            dynmodel.Password = Password;
            dynmodel.clientIpaddress = clientIpaddress;
            dynmodel.strHostName = strHostName;
            requestUri = "api/Login/GetloginDetails";
            dt = Cls_Common.CallAPI<dynamic, DataTable>(UrsAPIBaseAddress, requestUri, HttpMethod.Post, dynmodel, out objCls_InOut);
            return dt;

        }

    }
}
