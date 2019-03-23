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
using NetFBAPISandbox.JWTSecurity.Filters;
using FirebirdSql.Data.FirebirdClient;

namespace NetFBAPISandbox.Controllers.DataSetup
{
    [RoutePrefix("api/DataSetup")]
    public class PriorityController : ApiController
    {
        [Route("Priorities")]
        [HttpGet]
        // GET: api/DataSetup/Priorities
        // Returns all fields from PRIORITIES
        public IHttpActionResult GetPriorities()
        {
            //FBConnection fbconndetails = new FBConnection();

            FBConnection selectconnection = new FBConnection();

            string sqlcmd = "select * from PRIORITIES order by ORDERVALUE asc";
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


        [Route("PrioritiesDropDown")]
        [HttpGet]
        // GET: api/DataSetup/PrioritiesDropDown
        // Returns the ID, Code, Name, and Description fields in the table PRIORITIES that are ACTIVE and NOT DELETED (used for dropdown population).
        public IHttpActionResult GetPrioritiesDropDown()
        {
            //FBConnection fbconndetails = new FBConnection();

            FBConnection selectconnection = new FBConnection();

            string sqlcmd = "select ID, CODE, NAME from PRIORITIES where ISACTIVE is true and ISDELETED is false order by ORDERVALUE asc";
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


        [Route("Priority/{requestid}")]
        [HttpGet]
        // GET: api/DataSetup/Priority/{ID}
        // Returns all fields for a specific Priority by ID
        public IHttpActionResult GetPriority(int requestid)
        {
            //FBConnection fbconndetails = new FBConnection();

            FBConnection selectconnection = new FBConnection();

            string sqlcmd = "select * from PRIORITIES where id = @requestid";
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
    }
}
