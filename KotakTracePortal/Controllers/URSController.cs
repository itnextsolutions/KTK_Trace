using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KotakTracePortal.Models;

namespace KotakTracePortal.Controllers
{
    public class URSController : Controller
    {
        // GET: URS
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult URSCreate(string Id, string flag)
        {
            URSModel objURS = new URSModel();
            if (!string.IsNullOrEmpty(Id))
            { 
                objURS.Id= Id;
                objURS.RequesterName = "David Smith";
                objURS.Department = "IT";
                objURS.ProjectName = "New Lounch";
                objURS.ProductName = "3.0";
                objURS.Status = "Pending with vikas khande";
                objURS.AgencyName = "INS Air Condition";
                objURS.CreatedBy = "Kiran Jadhav";
                objURS.EDDdate="19/1/2023";
                objURS.TypeOfCollateral = "N";
                objURS.ChannelName = "Y";
                objURS.Language = "English";
                objURS.Reason = "Business Equipment Collateral";

            }
            else
            {
                //objURS.RequesterName = Convert.ToString(Session["EMPNAME"]);
                objURS.RequesterName = "Kiran Jadhav";
                objURS.Department = "IT";
            }
            ViewBag.flag = flag;
            return View(objURS);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult URSCreate(URSModel objURS,string SubmitBtn)
        {

            objURS.RequesterName = Convert.ToString(Session["EMPNAME"]);
            objURS.Department = "IT";
            return View(objURS);
        }


        [HttpGet]
        public ActionResult SearchURS()
        {
            URSModel objURS = new URSModel();
            objURS.ListURSModel = GetURSLIST();
            return View(objURS);
        }

        [HttpGet]
        public ActionResult AcceptURS()
        {
            URSModel objURS = new URSModel();
            objURS.ListURSModel = GetURSLIST();
            return View(objURS);
        }

        public List<URSModel> GetURSLIST()
        {
            List<URSModel> listurs = new List<URSModel>();

            listurs.Add(new URSModel
            {
            
                Id = "URS0001",
                Department = "IT",
                ProjectName = "New Lounch",
                ProductName = "3.0",
                Status = "Pending With Vikas Khande",
                AgencyName = "INS Air Condition",
                CreatedBy = "Kiran Jadhav",
                CreatedDate = DateTime.Now,
            });

            listurs.Add(new URSModel
            {
            
                Id = "URS0002",
                Department = "IT",
                ProjectName = "New Lounch",
                ProductName = "3.0",
                Status = "Pending With Vikas Khande",
                AgencyName = "INS Air Condition",
                CreatedBy = "Kiran Jadhav",
                CreatedDate = DateTime.Now,
            });
            listurs.Add(new URSModel
            {
                
                Id = "URS0003",
                Department = "IT",
                ProjectName = "New Lounch",
                ProductName = "3.0",
                Status = "Pending With Vikas Khande",
                AgencyName = "INS Air Condition",
                CreatedBy = "Kiran Jadhav",
                CreatedDate = DateTime.Now,
            });
            listurs.Add(new URSModel
            {
              
                Id = "URS0004",
                Department = "IT",
                ProjectName = "New Lounch",
                ProductName = "3.0",
                Status = "Pending With Vikas Khande",
                AgencyName = "INS Air Condition",
                CreatedBy = "Kiran Jadhav",
                CreatedDate = DateTime.Now,
            });
            listurs.Add(new URSModel
            {
               
                Id = "URS0005",
                Department = "IT",
                ProjectName = "New Lounch",
                ProductName = "3.0",
                Status = "Pending With Vikas Khande",
                AgencyName = "INS Air Condition",
                CreatedBy = "Kiran Jadhav",
                CreatedDate = DateTime.Now,
            });
            listurs.Add(new URSModel
            {
               
                Id = "URS0006",
                Department = "IT",
                ProjectName = "New Lounch",
                ProductName = "3.0",
                Status = "Pending With Vikas Khande",
                AgencyName = "INS Air Condition",
                CreatedBy = "Kiran Jadhav",
                CreatedDate = DateTime.Now,
            });
            listurs.Add(new URSModel
            {
                
                Id = "URS0007",
                Department = "IT",
                ProjectName = "New Lounch",
                ProductName = "3.0",
                Status = "Pending With Vikas Khande",
                AgencyName = "INS Air Condition",
                CreatedBy = "Kiran Jadhav",
                CreatedDate = DateTime.Now,
            });
            listurs.Add(new URSModel
            {
                
                Id = "URS0008",
                Department = "IT",
                ProjectName = "New Lounch",
                ProductName = "3.0",
                Status = "Pending With Vikas Khande",
                AgencyName = "INS Air Condition",
                CreatedBy = "Kiran Jadhav",
                CreatedDate = DateTime.Now,
            });
            listurs.Add(new URSModel
            {
               
                Id = "URS0009",
                Department = "IT",
                ProjectName = "New Lounch",
                ProductName = "3.0",
                Status = "Pending With Vikas Khande",
                AgencyName = "INS Air Condition",
                CreatedBy = "Kiran Jadhav",
                CreatedDate = DateTime.Now,
            });
            listurs.Add(new URSModel
            {
               
                Id = "URS0010",
                Department = "IT",
                ProjectName = "New Lounch",
                ProductName = "3.0",
                Status = "Pending With Vikas Khande",
                AgencyName = "INS Air Condition",
                CreatedBy = "Kiran Jadhav",
                CreatedDate = DateTime.Now,
            });

            return listurs;
        }
    }
}