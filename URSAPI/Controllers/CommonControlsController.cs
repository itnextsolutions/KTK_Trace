using KotakTraceAPI.CustomFilterRepo;
using KotakTraceAPI.Business;
using KotakTracePortal.Shared;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace KotakTraceAPI.Controllers
{
    [ProcessExceptionFilter]
    public class CommonControlsController : ApiController
    {
        // GET: CommonControls
        public bool success = false;
        HttpResponseMessage response = new HttpResponseMessage();
        SendMail objSendMail = new SendMail();
        DataTable dtFill = new DataTable();
        DataSet dsFill = new DataSet();
        CommonControlsBL objCommonBAL = new CommonControlsBL();

        [HttpPost]
        [Route("~/api/CommonControls/SendMailURS")]
        public HttpResponseMessage SendMailURS([FromBody] dynamic dynModel)
        {
            DataTable dt = Cls_Common.GetModelFromJSONObject<object, DataTable>(dynModel.dt);
            byte[] FileBytes = Cls_Common.GetModelFromJSONObject<object, byte[]>(dynModel.FileBytes);
            string FileName = Convert.ToString(dynModel.FileName); 
            success = objSendMail.SendEmailURS(dt, FileBytes, FileName);
            response = Request.CreateResponse(HttpStatusCode.OK);
            response = Cls_Common.GetHttpResponseMessage<bool>(response, success);
            return response;
        }

        [HttpGet]
        [Route("~/api/CommonControls/SendMailURS")]
        public HttpResponseMessage UserDetails([FromBody] dynamic dynModel)
        {
            DataTable dt = Cls_Common.GetModelFromJSONObject<object, DataTable>(dynModel.dt);
            byte[] FileBytes = Cls_Common.GetModelFromJSONObject<object, byte[]>(dynModel.FileBytes);
            string FileName = Convert.ToString(dynModel.FileName);
            success = objSendMail.SendEmailURS(dt, FileBytes, FileName);
            response = Request.CreateResponse(HttpStatusCode.OK);
            response = Cls_Common.GetHttpResponseMessage<bool>(response, success);
            return response;
        }
    }

}