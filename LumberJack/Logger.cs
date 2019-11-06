using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LumberJack
{
    public class Logger
    {
        //In this the connection string is loaded only loaded 1 time in the start as 
        //static.
        static string connectionstring;
        static Logger()
        {
            try
            {
                connectionstring = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnectionString"].ConnectionString;
            }
            catch (Exception)
            {

                throw new Exception("Something went wrong while getting the DefaultConnectionString for Logger");
            }
        }
        // I have this method static so that I will be able to have semantics like 
        //Console.WriteLine.
        public static void Log(Exception ex)
        {
            try
            {
                using (System.Data.SqlClient.SqlConnection con = new System.Data.SqlClient.SqlConnection(connectionstring))
                {
                    con.Open();
                    using (var com = con.CreateCommand())
                    {
                        com.CommandText = "Logger";
                        com.CommandType = System.Data.CommandType.StoredProcedure;
                        com.Parameters.AddWithValue("@Message", ex.Message);
                        com.Parameters.AddWithValue("@Stacktrace", ex.StackTrace.ToString());
                        com.ExecuteNonQuery();
                    }
                }
            }
            // If this is our failsafe if the databse is down
            catch (Exception exc)
            {
                var p = System.Web.HttpContext.Current.Server.MapPath("~");
                p += @"ErrorLog.Log";
                System.IO.File.AppendAllText(p,
                "while attempting to record the original exception to the database, this exception occurred\r\n");
                System.IO.File.AppendAllText(p, exc.ToString());
                System.IO.File.AppendAllText(p,
                "This is the Original Exception that was attempting to be written to the database\r\n");
                System.IO.File.AppendAllText(p, ex.ToString());

            }
        }
    }
}
