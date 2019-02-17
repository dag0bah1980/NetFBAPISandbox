using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using NetFBAPISandbox.Models;

namespace NetFBAPISandbox.Connect
{
    [RoutePrefix("Connect")]
    public class DBConnectionController : ApiController
    {
        [Route("TestDBConnectionString")]
        [HttpGet]
        // GET: Connect/TestDBConnection
        public string TestDBConnectionString()
        {

            return "TestDBConnection";
        }

        [Route("TestDBConnection")]
        [HttpGet]
        // GET: Connect/TestDBConnection
        public IHttpActionResult TestDBConnection()
        {
            var _dbconnection = new DBConnection();
            _dbconnection.DBUser = "XXXX";
            _dbconnection.DBPassword = "*****";
            return Ok(_dbconnection);
        }
    }
}
