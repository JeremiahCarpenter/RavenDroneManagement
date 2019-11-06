using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace LogTrace
{
    public class LogTrace
    {
        static string connectionstring;
        static LogTrace()
        {
            try
            {
                connectionstring = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnectionString"].ConnectionString;
            }
            catch (Exception ex) 
            {

                throw new Exception("Error while getting DefaultConnectionString for Logger");
            }
        }
        public static void Log(Exception ex)
        {
            try
            {
                using(System.Data.SqlClient.SqlConnection con = new System.Data.SqlClient.SqlConnection(connectionstring))
                {
                    con.Open();
                    using(var com = con.CreateCommand())
                    {
                        com.CommandText = "InsertLogItem";
                        com.CommandType = System.Data.CommandType.StoredProcedure;
                        com.Parameters.AddWithValue("@Message", ex.Message);
                        com.Parameters.AddWithValue("@Trace", ex.StackTrace.ToString());
                        com.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception exc)
            {

                var p = System.Web.HttpContext.Current.Server.MapPath("~");
                p += "ErrorLog.Log";
                System.IO.File.AppendAllText(p, "while attempting to record the original exception to the database, this exception occurred\r\n");
                System.IO.File.AppendAllText(p, exc.ToString());
                System.IO.File.AppendAllText(p, "This is the Original Exception that was attempting to be written to the database\r\n");
                System.IO.File.AppendAllText(p, ex.ToString());
            }
        }
    }
}
