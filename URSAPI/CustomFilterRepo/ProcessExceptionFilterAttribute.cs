using KotakTracePortal.Shared;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Http.Filters;

namespace KotakTraceAPI.CustomFilterRepo
{
    /// <summary>
    /// The Custom Exception Filter
    /// </summary>
    public class ProcessExceptionFilter : ExceptionFilterAttribute, IExceptionFilter
    {
        //public override void OnException(HttpActionExecutedContext actionExecutedContext)
        //{
        //    //Check the Exception Type

        //    if (actionExecutedContext.Exception is ProcessException)
        //    {
        //        //The Response Message Set by the Action During Ececution
        //        var res = actionExecutedContext.Exception.Message;

        //        //Define the Response Message
        //        HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.InternalServerError)
        //        {
        //            Content = new StringContent(res),
        //            ReasonPhrase = res
        //        };


        //        //Create the Error Response

        //        actionExecutedContext.Response = response;
        //    }
        //}

        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            try
            {
                Cls_Common.LogToFile(Cls_Common.MessageType.App_Message, "1.0", "Start");

                Cls_InOut objCls_InOut = new Cls_InOut();

                Exception ex = actionExecutedContext.Exception;
                string ActionMethod = actionExecutedContext.ActionContext.ActionDescriptor.ActionName;
                string Controller = actionExecutedContext.ActionContext.ControllerContext.ControllerDescriptor.ControllerName;

                Cls_Common.LogToFile(Cls_Common.MessageType.App_Exception, "1.0.1", $"Controller/Action: {Controller}/{ActionMethod}", ex);

                string exceptionMessage = string.Empty;
                if (actionExecutedContext.Exception.InnerException == null)
                {
                    exceptionMessage = $"{actionExecutedContext.Exception.Message} {actionExecutedContext.Exception.StackTrace}";
                }
                else
                {
                    exceptionMessage = $"{actionExecutedContext.Exception.InnerException.Message} {actionExecutedContext.Exception.InnerException.StackTrace}";
                }

                Cls_Common.WriteToFile(exceptionMessage);

                string jsonStr = JsonConvert.SerializeObject(exceptionMessage);
                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.NotImplemented)
                {
                    Content = new StringContent(jsonStr, Encoding.UTF8, "application/json"),
                    ReasonPhrase = $"Internal Server Error.Please Contact your Administrator."
                };

                actionExecutedContext.Response = response;
            }
            catch (Exception ex)
            {
                Cls_Common.LogToFile(Cls_Common.MessageType.App_Exception, "1.2", "Exception", ex);
            }
        }
    }
}