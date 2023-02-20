using KotakTracePortal.Buisness;
using KotakTracePortal.Shared;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;

namespace KotakTracePortal.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login

        DataTable dt = new DataTable();
        CommonController objCommonController = new CommonController();
        LoginBL objLoginBL = new LoginBL();


        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            Login objModel = new Login();
            return View(objModel);
        }

        [HttpPost]
        public ActionResult Login(Login objModel)
        {
            objCommonController = new CommonController();
            Uri CompleteUri = objCommonController.GetUri(objModel.RedirectURLEncoded, "RedirectURLEncoded");
            string URLParameter = CompleteUri.Query;
            string ControllerName = objCommonController.GetUriParameterValue(CompleteUri, "Controller");
            string ActionName = objCommonController.GetUriParameterValue(CompleteUri, "Action");
            string strIPAddress = GetLocalIPAddress();
            string strHostname = Dns.GetHostName();
            if (!string.IsNullOrEmpty(objModel.UserId) && !string.IsNullOrEmpty(objModel.Password))
            {
                //Session["EmpID"] = Convert.ToString("1012");
                //Session["EMPNAME"] = Convert.ToString("David Smith");
                //Session["LASTLOGINDATE"] = Convert.ToString("18/01/2023 12:12:12");
                dt = objLoginBL.GetloginDetails(objModel.UserId, objModel.Password, strIPAddress, strHostname);
                if(dt.Rows.Count > 0)
                {
                    if (!string.IsNullOrEmpty(Convert.ToString(dt.Rows[0][0])))
                    {
                        Session["EmpId"] = Convert.ToString(dt.Rows[0]["Emp_Id"]);
                        Session["EmpName"] = Convert.ToString(dt.Rows[0]["Emp_Name"]);
                        Session["EmailId"] = Convert.ToString(dt.Rows[0]["Email_Id"]);
                        return RedirectToAction("Home", "Home");
                    }
                    else
                    {
                        return View("Login", objModel);
                    }
                }
                else
                {
                    ModelState.AddModelError("ErrorMsg", "Invalid UserName & Password");
                    return View("Login", objModel);

                }


                // dt = objLoginBL.GetloginDetails(objModel.UserId, objModel.Password, strIPAddress, strHostname);
                //if (dt.Rows.Count > 0)
                //{
                //    if (!string.IsNullOrEmpty(Convert.ToString(dt.Rows[0][0])))
                //    {
                //        Session["EmpID"] = Convert.ToString(dt.Rows[0]["EMPLOYEENO"]);
                //        Session["EMPNAME"] = Convert.ToString(dt.Rows[0]["EMPNAME"]);
                //        Session["LASTLOGINDATE"] = Convert.ToString(dt.Rows[0]["LASTLOGINDATE"]);
                //        return RedirectToAction("Home", "Home");
                //    }
                //    else
                //    {
                //        ModelState.AddModelError("ErrorMsg", "Invalid UserName & Password");
                //        return View("Login", objModel);
                //    }
                //}
                //else
                //{
                //    ModelState.AddModelError("ErrorMsg", "You are not Authorised User");
                //    return View("Login", objModel);
                //}
            }
            else
            {
                ModelState.AddModelError("ErrorMsg", "Invalid UserName & Password");
                return View("Login", objModel);
            }
        }


        public string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            throw new Exception("No network adapters with an IPv4 address in the system!");
        }

        public ActionResult Logout()
        {
            if (Session.Count > 0)
            {
                Session.Abandon();
                this.Response.Cache.SetExpires(DateTime.UtcNow.AddDays(-1));
                this.Response.Cache.SetValidUntilExpires(false);
                this.Response.Cache.SetRevalidation(HttpCacheRevalidation.AllCaches);
                this.Response.Cache.SetCacheability(HttpCacheability.NoCache);
                this.Response.Cache.SetNoStore();
            }
            return View("Login");
        }

        [HttpPost]
        public ActionResult AutoLogout(Login ObjModel)
        {
            if (Session.Count > 0)
            {
                Session.Abandon();
                this.Response.Cache.SetExpires(DateTime.UtcNow.AddDays(-1));
                this.Response.Cache.SetValidUntilExpires(false);
                this.Response.Cache.SetRevalidation(HttpCacheRevalidation.AllCaches);
                this.Response.Cache.SetCacheability(HttpCacheability.NoCache);
                this.Response.Cache.SetNoStore();
            }
            return View("Login");
        }

        public JsonResult AppicationExist()
        {
            return Json("1", JsonRequestBehavior.AllowGet);
        }

        public JsonResult ResetLogin(string User)
        {
            return Json("1", JsonRequestBehavior.AllowGet);
        }

        public JsonResult ResetSession()
        {
            int timeout = GetSessionTimeout();
            return Json(timeout, JsonRequestBehavior.AllowGet);
        }

        public static int GetSessionTimeout()
        {
            Configuration config = WebConfigurationManager.OpenWebConfiguration("~/Web.Config");
            SessionStateSection section = (SessionStateSection)config.GetSection("system.web/sessionState");
            return Convert.ToInt32(section.Timeout.TotalMinutes * 60);
        }
    }
}