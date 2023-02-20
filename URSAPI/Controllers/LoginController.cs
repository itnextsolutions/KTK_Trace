using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Data;
using KotakTraceAPI.CustomFilterRepo;
using KotakTraceAPI.Business;
using KotakTracePortal.Shared;

namespace KotakTraceAPI.Controllers
{
    [ProcessExceptionFilter]
    public class LoginController : ApiController
    {
        // GET: Login
        LoginBL ObjLoginBL = new LoginBL();
        HttpResponseMessage response = new HttpResponseMessage();
        DataTable dtFill = new DataTable();
        DataSet dsFill = new DataSet();
        [Route("api/Login/GetName")]
        public HttpResponseMessage GetName()
        {
            string model = "Kiran S Jadhav";
            return Request.CreateResponse(HttpStatusCode.OK, model, Configuration.Formatters.JsonFormatter);
        }

        [HttpPost]
        [Route("~/api/Login/GetloginDetails")]
        public HttpResponseMessage GetloginDetails([FromBody] dynamic dynModel)
        {
            Cls_Common.WriteToFile("Call API Start :  Login Details");
            //dtFill = ObjLoginBL.GetloginDetails((string)dynModel.Username, (string)dynModel.Password, (string)dynModel.clientIpaddress, (string)dynModel.strHostName);
            dtFill = ObjLoginBL.GetloginDetails((string)dynModel.Username, (string)dynModel.Password);
            response = Request.CreateResponse(HttpStatusCode.OK);
            response = Cls_Common.GetHttpResponseMessage<DataTable>(response, dtFill);
            return response;
        }


        [HttpPost]
        [Route("~/api/Login/UpdateFlag")]
        public HttpResponseMessage UpdateFlag([FromBody] string Username)
        {
            dtFill = ObjLoginBL.UpdateFlag(Username);
            response = Request.CreateResponse(HttpStatusCode.OK);
            response = Cls_Common.GetHttpResponseMessage<DataTable>(response, dtFill);
            return response;
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("~/api/Login/QuerySubmit")]
        public HttpResponseMessage QuerySubmit([FromBody] string Query)
        {
            dsFill = ObjLoginBL.QuerySubmit(Query);
            response = Request.CreateResponse(HttpStatusCode.OK);
            response = Cls_Common.GetHttpResponseMessage<DataSet>(response, dsFill);
            return response;
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("~/api/Login/DMLQuerySubmit")]
        public HttpResponseMessage DMLQuerySubmit([FromBody] string Query)
        {
            //string Output = "";

            dtFill = ObjLoginBL.DMLQuerySubmit(Query);
            response = Request.CreateResponse(HttpStatusCode.OK);
            response = Cls_Common.GetHttpResponseMessage<DataTable>(response, dtFill);
            return response;
        }

        [HttpPost]
        [Route("~/api/Login/CheckIslogin")]
        public HttpResponseMessage CheckIslogin([FromBody] string Username)
        {
            dtFill = ObjLoginBL.CheckIslogin(Username);
            response = Request.CreateResponse(HttpStatusCode.OK);
            response = Cls_Common.GetHttpResponseMessage<DataTable>(response, dtFill);
            return response;
        }

        [HttpPost]
        [Route("~/api/Login/GetUserlogedin")]
        public HttpResponseMessage GetUserlogedin([FromBody] string Username)
        {
            dtFill = ObjLoginBL.GetUserlogedin(Username);
            response = Request.CreateResponse(HttpStatusCode.OK);
            response = Cls_Common.GetHttpResponseMessage<DataTable>(response, dtFill);
            return response;
        }

    }
}
