using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using KotakTracePortal.Buisness;
using KotakTracePortal.Business;
using KotakTracePortal.Shared;

namespace KotakTracePortal.Controllers
{
    public class HomeController : BaseController
    {
        MenuBL objMenuBL = new MenuBL();
        DataSet ds = new DataSet();
        UserRole objUserRole = new UserRole();
        UserRoleBL objUserRoleBL = new UserRoleBL();
        UserRoleList objUserRoleList = new UserRoleList();
        DataTable dt = new DataTable();
 
        public ActionResult Home()
        {
            return View();
        }
        
        public ActionResult GetMenu()
        {
            DataSet ds = objMenuBL.GetMenuList(Convert.ToString(Session["EmpID"]));
            string strMenu = BindMenu(ds);
            ViewBag.MenuDetails = strMenu;
            return PartialView("~/Views/Shared/PartialViews/_Menu.cshtml");
        }

        private string BindMenu(DataSet ds)
        {
            StringBuilder strMenu = new StringBuilder();
            try
            {

                strMenu.Append("<ul class='nav nav-main'>");
                DataTable dt = ds.Tables[0];
                foreach (DataRow dr in dt.Rows)
                {
                    if (Convert.ToString(dr["IsParent"]) == "1")
                    {
                        strMenu.Append("<li class='nav-parent'>");
                        strMenu.Append("<a>");
                        strMenu.Append("<i class='" + Convert.ToString(dr["Icon"]) + "' aria-hidden='true'></i><span>" + Convert.ToString(dr["Menu_Name"]) + "</span>");
                        strMenu.Append("</a>");

                        //strMenu.Append("<ul class='nav nav-children'>");

                        //strMenu.Append("<li><a href='/URS/SearchURSS'><i class='la la-book lnr-xs lnr-apartment' aria-hidden='true'></i><span>URS Form</span> </a></li>");

                        foreach (DataRow drchild in ds.Tables[0].Select("ParentId=" + Convert.ToString(dr["MenuOrder"])))
                        {
                            strMenu.Append("<li><a href='" + Convert.ToString(drchild["URL"]) + "'><i class='" + Convert.ToString(drchild["Icon"]) + "' aria-hidden='true'></i><span>" + Convert.ToString(drchild["Menu_Name"]) + "</a></li>");
                        }
                        strMenu.Append("</ul>");
                        strMenu.Append("</li>");

                    }
                    else if (Convert.ToString(dr["ParentId"]) == "0" && Convert.ToString(dr["IsParent"]) == "0")
                    {
                        strMenu.Append("<li class='nav-active'>");
                        strMenu.Append(" <a href='" + Convert.ToString(dr["URL"]) + "'><i class='" + Convert.ToString(dr["Icon"]) + "'></i><span>" + Convert.ToString(dr["Menu_Name"]) + "</span></a>");
                        strMenu.Append("</li>");
                    }

                }

                strMenu.Append("</ul>");

            }
            catch
            {

            }
            return Convert.ToString(strMenu);
        }

        public ActionResult UserRole()
        {
            ds = objUserRoleBL.UserDetails();
            
            if(ds.Tables.Count > 0)
            { 
                 if (ds.Tables[0].Rows.Count > 0)
                {
                    objUserRole.USERLIST = CommonControlsBL.ConvertDataTableToList<CommonControls.DropDownModel>(ds.Tables[0]);
                }
                if (ds.Tables[1].Rows.Count > 0)
                {
                    objUserRole.ROLELIST = CommonControlsBL.ConvertDataTableToList<CommonControls.DropDownModel>(ds.Tables[1]);
                }
            }
            return View("UserRole", objUserRole);
        }

        public ActionResult SearchUserRole()
        {
            UserRole objUserRole = new UserRole();
            return View(objUserRole);
        }

        [HttpPost]
        public ActionResult SearchUserRole(UserRole objUserRole)
        {
            dt = objUserRoleBL.AddUserRoleList(objUserRole.RoleId, objUserRole.UserId, "1001");

            if (TempData["Message"] != null)
             {
              //  if (TempData["Message"].ToString() == "Success")
                    
             }

            //dt = objUserRoleBL.GetUserRoleList();

            if (dt.Rows.Count > 0)
            {
                ViewBag.Message = "Success";
                objUserRoleList.getUserRolelist = CommonControlsBL.ConvertDataTableToList<UserRoleList>(dt);
            }

            return View("SearchUserRole", objUserRoleList);
        }

        [HttpGet]
        public ActionResult DeleteRight(string RoleId)
        {
            dt = objUserRoleBL.DeleteUserRole(RoleId);

            if (dt.Rows.Count > 0)
            {
                objUserRoleList.getUserRolelist = CommonControlsBL.ConvertDataTableToList<UserRoleList>(dt);
            }

            return Content("{ \"data\":\"Delete\"}","application/json"); //View("SearchUserRole", objUserRoleList);

        }

    }
}