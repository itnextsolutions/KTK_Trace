using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Dynamic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using KotakTracePortal.Shared;

namespace KotakTracePortal.Business
{
    public class UserRoleBL
    {
        string UrsAPIBaseAddress = ConfigurationManager.AppSettings["UrsAPIBaseAddress"];
        Cls_InOut objCls_InOut = new Cls_InOut();
        DataTable dt = new DataTable();
        DataSet ds = new DataSet();
        string requestUri = "";
        public static UserRoleBL _this;
        public static UserRoleBL ThisClass
        {
            get
            {
                if (_this == null)
                    _this = new UserRoleBL();
                return _this;
            }
            set { _this = value; }
        }

        public DataSet UserDetails()
        {
            requestUri = "api/UserRole/GetUserDetailList";
            ds = Cls_Common.CallAPI<string, DataSet>(UrsAPIBaseAddress, requestUri, HttpMethod.Post, string.Empty, out objCls_InOut);
            return ds;
        }

        public DataTable GetUserRoleList()
        {
            requestUri = "api/UserRole/GetUserRoleList";
            dt = Cls_Common.CallAPI<string, DataTable>(UrsAPIBaseAddress, requestUri, HttpMethod.Post, string.Empty, out objCls_InOut);
            return dt;
        }

        public DataTable AddUserRoleList(string ROLEID, string EMPNO, string CREATEDBY)
        {
            dynamic dynmodel = new ExpandoObject();
            dynmodel.ROLEID = ROLEID;
            dynmodel.EMPNO = EMPNO;
            dynmodel.CREATEDBY = CREATEDBY;

            requestUri = "api/UserRole/AddUserRoleList";
            dt = Cls_Common.CallAPI<dynamic, DataTable>(UrsAPIBaseAddress, requestUri, HttpMethod.Post, dynmodel, out objCls_InOut);
            return dt;
        }

        public DataTable DeleteUserRole(string ROLEID)
        {
            requestUri = "api/UserRole/DeleteUserRole";
            dt = Cls_Common.CallAPI<string, DataTable>(UrsAPIBaseAddress, requestUri, HttpMethod.Post, ROLEID, out objCls_InOut);
            return dt;
        }
    }
}
