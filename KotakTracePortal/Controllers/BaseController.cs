using KotakTracePortal.Buisness;
using KotakTracePortal.Business;
using KotakTracePortal.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace KotakTracePortal.Controllers
{

    public class BaseController : Controller
    {
        public int _supplierId = 0;

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            try
            {
                LoggedInSupplierId();
                #region To check User Session
                HttpSessionStateBase session = filterContext.HttpContext.Session;
                Controller controller = filterContext.Controller as Controller;
                var descriptor = filterContext.ActionDescriptor;
                var actionName = descriptor.ActionName;
                var controllerName = descriptor.ControllerDescriptor.ControllerName;
                if (controller != null)
                {
                    
                    if (session != null && session["EmpID"] == null)
                    {
                       
                            Cls_Common.LogToFile(Cls_Common.MessageType.App_Validation, "1.0", "Before Login redirection");

                            filterContext.Result =
                                                    new RedirectToRouteResult(
                                                        new RouteValueDictionary { { "controller","Login"},
                                                                        {"Default","Login" }
                                                                                    });

                            Cls_Common.LogToFile(Cls_Common.MessageType.App_Validation, "1.1", "After Login redirection");
                            //filterContext.Result = new RedirectResult("~/Login/Index");
                            //return;
                       
                    }
                }

                base.OnActionExecuting(filterContext);

            }
            catch (Exception ex)
            {
                Cls_Common.LogToFile(Cls_Common.MessageType.App_Exception, "1.2", "Exception", ex);
            }

            #endregion
        }

        protected override void OnException(ExceptionContext filterContext)
        {
            try
            {
                Cls_Common.LogToFile(Cls_Common.MessageType.App_Message, "1.0", "Start");

                HttpSessionStateBase session = filterContext.HttpContext.Session;
                Exception ex = filterContext.Exception;
                string ActionMethod = Convert.ToString(filterContext.RouteData.Values["action"]);
                string Controller = Convert.ToString(filterContext.RouteData.Values["controller"]);

                ErrorlogBL.InsertLog(ex.Message.Replace("/n", ""), Convert.ToString(session["EMPNAME"]), Convert.ToString(session["EmpID"]), "", "URS", ActionMethod);


                //filterContext.Result = View(new RouteValueDictionary (new { area = "", controller = "Home", action = "Error" }));

                Cls_Common.LogToFile(Cls_Common.MessageType.App_Exception, "1.0.1", $"Controller/Action: {Controller}/{ActionMethod}", ex);

                Cls_Common.LogToFile(Cls_Common.MessageType.App_Exception, "1.0.1 - Inner Exception :", $"Controller/Action: {Controller}/{ActionMethod}", ex.InnerException);

                Cls_Common.LogToFile(Cls_Common.MessageType.App_Message, "1.1", "Before Error redirection assignment for 'Sorry, an err...'");

                filterContext.Result = new ViewResult { ViewName = "~/Views/Shared/Error.cshtml" };

                Cls_Common.LogToFile(Cls_Common.MessageType.App_Message, "1.2", "After Error redirection assignment for 'Sorry, an err...'");

                filterContext.ExceptionHandled = true;

                //erase any output already generated
                Cls_Common.LogToFile(Cls_Common.MessageType.App_Message, "1.3", "Before Response clear");

                filterContext.HttpContext.Response.Clear();
                //base.OnException(filterContext);

                Cls_Common.LogToFile(Cls_Common.MessageType.App_Message, "1.4", "End");

            }
            catch (Exception ex)
            {
                Cls_Common.LogToFile(Cls_Common.MessageType.App_Exception, "1.5", "Exception", ex);
            }
        }

        public int LoggedInSupplierId()
        {

            if (Session["EmpID"] != null)
            {
                _supplierId = Convert.ToInt32(Session["EmpID"]);
            }
            else
            {
               // Cls_Common.WriteToFile("LoggedInSupplierId() : Auto Logout : Session Time Out - Employee Id : " + Convert.ToString(Session["EmpID"]) + " , Date_Time : " + Convert.ToString(DateTime.Now));
               // LoginBL objBAL = new LoginBL();
               // string Mailbody = "<html ><head></head><body>Dear Team,<br/><br/> Auto session time out detected.<br/><br/> Regards,<br/> Procure Buddy </body></html> ";
               // objBAL.SendMailToUser("Auto Session Time Out", "kiran.jadhav@hdfcergo.co.in", "", Mailbody);
                RedirectToAction("Login", "Login");
            }

            return _supplierId;
        }

        [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
        public class EncryptActionParameterAttribute : ActionFilterAttribute
        {
            public override void OnActionExecuting(ActionExecutingContext filterContext)
            {
                Dictionary<string, object> decryptParameters = new Dictionary<string, object>();
                if (System.Web.HttpContext.Current.Request.QueryString.Get("ErgoParam") != null)
                {
                    string EncryptedQueryString = HttpUtility.HtmlDecode(System.Web.HttpContext.Current.Request.QueryString.Get("ErgoParam"));
                    string decryptstring = CommonControlsBL.Decrypt(Convert.ToString(EncryptedQueryString));
                    string[] parameters = decryptstring.Split('?');
                    for (int i = 0; i < parameters.Length; i++)
                    {
                        string[] paramArr = parameters[i].Split('=');
                        decryptParameters.Add(paramArr[0], Convert.ToInt32(paramArr[1]));
                    }

                    for (int i = 0; i < decryptParameters.Count; i++)
                    {
                        filterContext.ActionParameters[decryptParameters.Keys.ElementAt(i)] = decryptParameters.Values.ElementAt(i);
                    }
                }
                base.OnActionExecuting(filterContext);
            }


        }
        [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
        public class NoDirectAccessAttribute : ActionFilterAttribute
        {
            public override void OnActionExecuting(ActionExecutingContext filterContext)
            {
                try
                {
                    //Cls_Common.LogToFile(Cls_Common.MessageType.App_Message, "1.0", "Start");
                    //if (filterContext.HttpContext.Request.Url.Host != filterContext.HttpContext.Request.UrlReferrer.Host)
                    //{ bool y = true; }
              
                    if (filterContext.HttpContext.Request.UrlReferrer == null &&
                                filterContext.HttpContext.Request.Url.Host != filterContext.HttpContext.Request.UrlReferrer.Host)
                    {
                        string ActionMethod = Convert.ToString(filterContext.RouteData.Values["action"]);
                        string Controller = Convert.ToString(filterContext.RouteData.Values["controller"]);

                        Cls_Common.LogToFile(Cls_Common.MessageType.App_Message, "1.0.1", $"Controller/Action: {Controller}/{ActionMethod}");

                        Cls_Common.LogToFile(Cls_Common.MessageType.App_Message, "1.1", "Before Error redirection assignment for 'Sorry, an err...'");

                        filterContext.Result = new ViewResult { ViewName = "~/Views/Shared/Error.cshtml" };

                        Cls_Common.LogToFile(Cls_Common.MessageType.App_Message, "1.2", "After Error redirection assignment for 'Sorry, an err...'");

                    }

                    //Cls_Common.LogToFile(Cls_Common.MessageType.App_Message, "1.3", "End");
                }
                catch (Exception ex)
                {
                    Cls_Common.LogToFile(Cls_Common.MessageType.App_Exception, "1.4", "Exception", ex);
                }
            }
        }
    }
}