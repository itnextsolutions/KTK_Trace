using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using URSPortal.Shared;
using URSAPI.Business;

namespace URSAPI.Controllers
{
    public class UserDashboardController : ApiController
    {
        UserDashboardBL objUserDashBL = new UserDashboardBL();
        HttpResponseMessage Response = new HttpResponseMessage();
        DataTable dtFill = new DataTable();
        DataSet dsFill = new DataSet();
        
        [HttpPost]
        [Route("~/api/UserDashboard/GetUserDashboard")]

        public DataSet GetUserDashboard()
        {
            dsFill = objUserDashBL.GetUserDashboard();
            Response = Request.CreateResponse(HttpStatusCode.OK);
            Response = Cls_Common.GetHttpResponseMessage<DataSet>(Response, dsFill);
            return Response;
        }

    }
}