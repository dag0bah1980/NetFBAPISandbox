using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data;
using NetFBAPISandbox.Models;
using NetFBAPISandbox.Connect;
using NetFBAPISandbox.Exceptions;
using FirebirdSql.Data.FirebirdClient;

namespace NetFBAPISandbox.Controllers
{
    [RoutePrefix("api")]
    public class SettingController : ApiController
    {
        [Route("StringSettings")]
        [HttpGet]
        // GET: api/StringSettings
        public string StringSettings()
        {

            return "Settings";
        }

        [Route("TestSettings")]
        [HttpGet]
        // GET: api/TestSettings
        public IHttpActionResult Settings()
        {
            var _testSetting = new Setting();
            _testSetting.ID = 1;
            _testSetting.CREATED = DateTime.Now;
            return Ok(_testSetting);
        }

        [Route("Setting/{requestid}")]
        [HttpGet]
        // GET: api/TestSettings
        public IHttpActionResult GetSetting(int requestid)
        {
            //FBConnection fbconndetails = new FBConnection();

            FBConnection selectconnection = new FBConnection();

            string sqlcmd = "select * from settings where id = @requestid";
            DataTable result = new DataTable();

            using (selectconnection.fbconnect)
            {

                try
                {

                    selectconnection.fbconnect.Open();
                    FbTransaction fbtrans = selectconnection.fbconnect.BeginTransaction();
                    FbParameter idParam = new FbParameter("@requestid", FbDbType.BigInt);
                    idParam.Value = requestid;

                    FbCommand fbcmd = new FbCommand(sqlcmd, selectconnection.fbconnect, fbtrans);
                    fbcmd.Parameters.Add(idParam);


                    using (FbDataReader fbsqlreader = fbcmd.ExecuteReader())
                    {
                        try
                        {
                            result.Load(fbsqlreader);
                            return Ok(result);
                        }
                        catch (Exception e)
                        {
                            ExceptionsLogByFile logger = new ExceptionsLogByFile();
                            logger.LogException(e);
                            return InternalServerError(e);
                        }
                    }

                }
                catch (Exception ex)
                {
                    ExceptionsLogByFile logger = new ExceptionsLogByFile();
                    logger.LogException(ex);
                    return InternalServerError(ex);
                }
                finally
                {
                    selectconnection.fbconnect.Close();
                }
                
            }
        }

        [Route("Settings")]
        [HttpGet]
        // GET: api/Settings
        public IHttpActionResult GetSettings()
        {
            //FBConnection fbconndetails = new FBConnection();

            FBConnection selectconnection = new FBConnection();

            string sqlcmd = "select * from settings";
            DataTable result = new DataTable();

            using (selectconnection.fbconnect)
            {

                try
                {
                    selectconnection.fbconnect.Open();
                    FbTransaction fbtrans = selectconnection.fbconnect.BeginTransaction();
                    FbCommand fbcmd = new FbCommand(sqlcmd, selectconnection.fbconnect, fbtrans);

                    using (FbDataReader fbsqlreader = fbcmd.ExecuteReader())
                    {
                        try
                        {

                            result.Load(fbsqlreader);
                            return Ok(result);
                            
                        }
                        catch (Exception e)
                        {
                            ExceptionsLogByFile logger = new ExceptionsLogByFile();
                            logger.LogException(e);
                            return InternalServerError(e);
                        }
                    }

                }
                catch (Exception ex)
                {
                    ExceptionsLogByFile logger = new ExceptionsLogByFile();
                    logger.LogException(ex);
                    return InternalServerError(ex);
                }
                finally
                {
                    selectconnection.fbconnect.Close();
                }

            }
        }
    }
}
