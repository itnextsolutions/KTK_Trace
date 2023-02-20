using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KotakTraceAPI.DataAccess;

namespace KotakTraceAPI.Business
{

    public class MenuBL
    {
        MenuDL objMenuDL = new MenuDL();
        DataTable dt = new DataTable();
        DataSet ds = new DataSet();
        public static MenuBL _this;

        public static MenuBL ThisClass
        {
            get
            {
                if (_this == null)
                    _this = new MenuBL();
                return _this;
            }
            set { _this = value; }
        }

        public DataSet GetMenuList(string EmpId)
        {
            return objMenuDL.GetMenuList(EmpId);
        }

    }
}
