using KotakTracePortal.Shared;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Dynamic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace KotakTracePortal.Buisness
{
    public class ErrorlogBL
    {
        static string UrsAPIBaseAddress = ConfigurationManager.AppSettings["UrsAPIBaseAddress"];
        static Cls_InOut objCls_InOut = new Cls_InOut();

        public static void InsertLog(string ExceptionMsg, string Username, string EmpId, string Procedure, string Module, string MethodName)
        {
            dynamic dynModel = new ExpandoObject();
            dynModel.ExceptionMsg = ExceptionMsg;
            dynModel.Username = Username;
            dynModel.EmpId = EmpId;
            dynModel.Procedure = Procedure;
            dynModel.Module = Module;
            dynModel.MethodName = MethodName;

            string requestUri = "api/Errorlog/InsertLog";
            dynamic dynModelResponse = Cls_Common.CallAPI<dynamic, dynamic>(UrsAPIBaseAddress, requestUri, HttpMethod.Post, dynModel, out objCls_InOut);
        }
    }
}
