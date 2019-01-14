using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace NetFBAPISandbox.Controllers
{
    [RoutePrefix("api")]
    public class SettingController : ApiController
    {
        [Route("Settings")]
        [HttpGet]
        // GET: api/Settings
        public string Settings()
        {

            return "Settings";
        }
    }
}
