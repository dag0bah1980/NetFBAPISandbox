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
    public class UserController : ApiController
    {
        [Route("Users")]
        [HttpGet]
        // GET: api/DataSetup/Users
        // Returns all fields from USERS
        public IHttpActionResult GetUsers()
        {
            //FBConnection fbconndetails = new FBConnection();

            FBConnection selectconnection = new FBConnection();

            string sqlcmd = "select * from USERS order by ID asc";
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


        [Route("UsersDropDown")]
        [HttpGet]
        // GET: api/DataSetup/UsersDropDown
        // Returns the ID, Code, First Name, and Last Name fields in the table USERS that are ACTIVE and NOT DELETED (used for dropdown population).
        public IHttpActionResult GetUsersDropDown()
        {
            //FBConnection fbconndetails = new FBConnection();

            FBConnection selectconnection = new FBConnection();

            string sqlcmd = "select ID, CODE, FIRSTNAME, LASTNAME from USERS where ISACTIVE is true and ISDELETED is false order by ID asc";
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


        [Route("User/{requestid}")]
        [HttpGet]
        // GET: api/DataSetup/User/{ID}
        // Returns all fields for a specific User by ID
        public IHttpActionResult GetUser(int requestid)
        {
            //FBConnection fbconndetails = new FBConnection();

            FBConnection selectconnection = new FBConnection();

            string sqlcmd = "select * from USERS where id = @requestid";
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
