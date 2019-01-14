using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace NetFBAPISandbox.Connect
{
    [RoutePrefix("Connect")]
    public class DBConnectionController : ApiController
    {
        [Route("TestDBConnection")]
        [HttpGet]
        // GET: Connect/TestDBConnection
        public string TestDBConnection()
        {

            return "TestDBConnection";
        }
    }
}
