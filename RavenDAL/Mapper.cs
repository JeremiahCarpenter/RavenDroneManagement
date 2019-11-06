using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace RavenDAL
{
    //This is the parent class to the other mappers.
    public class Mapper
    {
        //This is used to varify that the condition is true, if the condition is false the exception is thrown
        public void Assert(bool condition, string Message)
        {
            //This is a conditional Logic it is a bool that can either be true or 
            //false
            if(!condition)
            {
                throw new Exception(Message);
            }
        }
        public string GetStringOrDefault(SqlDataReader reader, int ordinal, string defaultValue = "")
        {
            if( reader.IsDBNull(ordinal) )
            {
                return defaultValue;
            }
            else
            {
                return reader.GetString(ordinal);
            }

        } 
        //These are to handle the DBNull values
        public int GetInt32OrDefault(SqlDataReader reader, int Ordinal, int defaultValue = 0)
        {
            if(reader.IsDBNull(Ordinal))
            {
                return defaultValue;
            }
            else
            {
                return reader.GetInt32(Ordinal);
            }
        }
        public DateTime GetDateTimeOrDefault(SqlDataReader reader, int Ordinal, DateTime defaultValue)
        {
            if (reader.IsDBNull(Ordinal))
            {
                return defaultValue;
            }
            else
            {
                return reader.GetDateTime(Ordinal);
            }
            

        }
        //I have this because I have a money evaluation in one of my tables that requires
        // me to convert it to a decimal. FineAmount if a decimal value because I want
        //the most accurate value (18,2)
        public Decimal GetDecimalOrDefault(SqlDataReader reader, int Ordinal, Decimal defaultValue = 0m)
        {
            if (reader.IsDBNull(Ordinal))
            {
                return defaultValue;
            }
            else
            {
                return reader.GetDecimal(Ordinal);
            }
        }

    }
}
