using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using KotakTracePortal.Shared;

namespace KotakTracePortal.Business
{
    public class MenuBL
    {
        string UrsAPIBaseAddress = ConfigurationManager.AppSettings["UrsAPIBaseAddress"];
        Cls_InOut objCls_InOut = new Cls_InOut();
        DataTable dt = new DataTable();
        DataSet ds = new DataSet();
        string requestUri = "";
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
            requestUri = "api/Menu/GetMenuList";
            ds = Cls_Common.CallAPI<string, DataSet>(UrsAPIBaseAddress, requestUri, HttpMethod.Post, EmpId, out objCls_InOut);
            return ds;
        }
    }
}
