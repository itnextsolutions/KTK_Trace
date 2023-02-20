using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KotakTraceAPI.DataAccess;

namespace KotakTraceAPI.Business
{

    public class UserRoleBL
    {
        UserRoleDL objUserRoleDL = new UserRoleDL();
        DataTable dt = new DataTable();
        DataSet ds = new DataSet();
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

        public DataSet GetUserDetailList()
        {
            return objUserRoleDL.GetUserDetailList();
        }

        public DataTable GetUserRoleList()
        {
            return objUserRoleDL.GetUserRoleList();
        }

        public DataTable AddUserRoleList(string ROLEID, string EMPNO, string CREATEDBY)
        {
            return objUserRoleDL.AddUserRoleList(ROLEID, EMPNO, CREATEDBY);
        }

        public DataTable DeleteUserRole(string ROLEID)
        {
            return objUserRoleDL.DeleteUserRole(ROLEID);
        }
    }
}
