using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Http;
using KotakTraceAPI.CustomFilterRepo;

namespace KotakTraceAPI.Controllers
{
    [ProcessExceptionFilter]
    public class HomeController : ApiController
    {
       
    }
}
