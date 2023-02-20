using KotakTraceAPI.DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Routing;

namespace KotakTraceAPI.Business
{
   public static class ErrorlogBL
    {
       
        public static void InsertLog(string ExceptionMsg, string Username, string EmpId, string Procedure, string Module, string MethodName)
        {
            ErrorLogDL.InsertLog(ExceptionMsg, Username, EmpId, Procedure, Module, MethodName);
        }
       
    }
}
