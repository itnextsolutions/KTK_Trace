using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using KotakTraceAPI.Business;
using KotakTracePortal.Shared;

namespace KotakTraceAPI.Controllers
{
    public class UserRoleController : ApiController
    {
        UserRoleBL ObjUserRoleBL = new UserRoleBL();
        HttpResponseMessage response = new HttpResponseMessage();
        DataTable dtFill = new DataTable();
        DataSet dsFill = new DataSet();

        [HttpPost]
        [Route("~/api/UserRole/GetUserDetailList")]

        public HttpResponseMessage GetUserDetailList()
        {
            dsFill = ObjUserRoleBL.GetUserDetailList();
            response = Request.CreateResponse(HttpStatusCode.OK);
            response = Cls_Common.GetHttpResponseMessage<DataSet>(response, dsFill);
            return response;
        }

        [HttpPost]
        [Route("~/api/UserRole/GetUserRoleList")]

        public HttpResponseMessage GetUserRoleList()
        {
            dtFill = ObjUserRoleBL.GetUserRoleList();
            response = Request.CreateResponse(HttpStatusCode.OK);
            response = Cls_Common.GetHttpResponseMessage<DataTable>(response, dtFill);
            return response;
        }

        [HttpPost]
        [Route("~/api/UserRole/AddUserRoleList")]

        public HttpResponseMessage AddUserRoleList([FromBody] dynamic dynModel)
        {
            dtFill = ObjUserRoleBL.AddUserRoleList((string)dynModel.ROLEID, (string)dynModel.EMPNO, (string)dynModel.CREATEDBY);
            response = Request.CreateResponse(HttpStatusCode.OK);
            response = Cls_Common.GetHttpResponseMessage<DataTable>(response, dtFill);
            return response;
        }

        [HttpPost]
        [Route("~/api/UserRole/DeleteUserRole")]

        public HttpResponseMessage DeleteUserRole([FromBody] string EmpId)

        {
            dtFill = ObjUserRoleBL.DeleteUserRole(EmpId);
            response = Request.CreateResponse(HttpStatusCode.OK);
            response = Cls_Common.GetHttpResponseMessage<DataTable>(response, dtFill);
            return response;
        }

    }
}