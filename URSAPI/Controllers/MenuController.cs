using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using KotakTraceAPI.Business;
using KotakTraceAPI.CustomFilterRepo;
using KotakTracePortal.Shared;

namespace KotakTraceAPI.Controllers
{
    [ProcessExceptionFilter]
    public class MenuController : ApiController
    {
        MenuBL ObjMenuBL = new MenuBL();
        HttpResponseMessage response = new HttpResponseMessage();
        DataTable dtFill = new DataTable();
        DataSet dsFill = new DataSet();

        [HttpPost]
        [Route("~/api/Menu/GetMenuList")]
        public HttpResponseMessage GetMenuList([FromBody] string EmpId)
        {
            dsFill = ObjMenuBL.GetMenuList(EmpId);
            response = Request.CreateResponse(HttpStatusCode.OK);
            response = Cls_Common.GetHttpResponseMessage<DataSet>(response, dsFill);
            return response;
        }
    }
}
