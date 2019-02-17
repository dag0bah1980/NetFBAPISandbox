using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using FirebirdSql.Data.FirebirdClient;
using NetFBAPISandbox.Exceptions;

namespace NetFBAPISandbox.Connect
{
    public class FBConnection
    {
        public FbConnection fbconnect;
        public bool testconnect;

        public void CloseConnection(FBConnection c)
        {
            try
            {
                c.fbconnect.Close();

            }
            catch (Exception ex)
            {
                ExceptionsLogByFile logger = new ExceptionsLogByFile();
                logger.LogException(ex);

            }
        }

        public FBConnection()
        {
            try
            {
                FbConnectionStringBuilder fbconnstr = new FbConnectionStringBuilder();
                fbconnstr.DataSource = WebConfigurationManager.AppSettings["DBHost"];
                fbconnstr.Database = String.Concat(WebConfigurationManager.AppSettings["DBPath"], WebConfigurationManager.AppSettings["DBFile"]);
                fbconnstr.Port = Int32.Parse(WebConfigurationManager.AppSettings["DBPort"]);
                fbconnstr.UserID = WebConfigurationManager.AppSettings["DBUser"];
                fbconnstr.Password = WebConfigurationManager.AppSettings["DBPassword"];
                fbconnstr.ConnectionLifeTime = Int32.Parse(WebConfigurationManager.AppSettings["DBConnectionLifetime"]);
                fbconnstr.Pooling = Convert.ToBoolean(WebConfigurationManager.AppSettings["DBPooling"]);
                fbconnstr.MinPoolSize = Int32.Parse(WebConfigurationManager.AppSettings["DBMinPoolSize"]);
                fbconnstr.MaxPoolSize = Int32.Parse(WebConfigurationManager.AppSettings["DBMaxPoolSize"]);

                fbconnect = new FbConnection(fbconnstr.ToString());
            }
            catch (Exception ex)
            {
                ExceptionsLogByFile logger = new ExceptionsLogByFile();
                logger.LogException(ex);
            }
        }

        public FBConnection(string host, string data, int port, string user, string password, int connectionlifetime, bool pooling, int minpoolsize, int maxpoolsize)
        {
            try
            {
                FbConnectionStringBuilder fbconnstr = new FbConnectionStringBuilder();
                fbconnstr.DataSource = host;
                fbconnstr.Database = data;
                fbconnstr.Port = port;
                fbconnstr.UserID = user;
                fbconnstr.Password = password;
                fbconnstr.ConnectionLifeTime = connectionlifetime;
                fbconnstr.Pooling = pooling;
                fbconnstr.MinPoolSize = minpoolsize;
                fbconnstr.MaxPoolSize = maxpoolsize;

                fbconnect = new FbConnection(fbconnstr.ToString());
            }
            catch (Exception ex)
            {
                ExceptionsLogByFile logger = new ExceptionsLogByFile();
                logger.LogException(ex);
            }
        }

        public bool TestConnection(string host, string data, int port, string user, string password, int connectionlifetime, bool pooling, int minpoolsize, int maxpoolsize)
        {
            bool result = false;
            FbConnectionStringBuilder fbconnstr = new FbConnectionStringBuilder();
            fbconnstr.DataSource = host;
            fbconnstr.Database = data;
            fbconnstr.Port = port;
            fbconnstr.UserID = user;
            fbconnstr.Password = password;
            fbconnstr.ConnectionLifeTime = connectionlifetime;
            fbconnstr.Pooling = pooling;
            fbconnstr.MinPoolSize = minpoolsize;
            fbconnstr.MaxPoolSize = maxpoolsize;

            fbconnect = new FbConnection(fbconnstr.ToString());


            using (fbconnect)
            {
                try
                {
                    fbconnect.Open();
                    result = true;
                }
                catch (Exception ex)
                {
                    ExceptionsLogByFile logger = new ExceptionsLogByFile();
                    logger.LogException(ex);
                }
                finally
                {
                    fbconnect.Close();
                }
                return result;
            }
        }
    }
}