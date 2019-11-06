using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RavenDAL
{
    class UserMapper : Mapper
    {

        int OffsetToUserID; //this is expected to be 0
        int OffsetToEmail; // this should be 1
        int OffsetToUserName;// this is expected to be 2
        int OffsetToHash;// this must match with the SQL
        int OffsetToSalt;
        int OffsetToRoleID;
        int OffsetToRoleName;

        public UserMapper(System.Data.SqlClient.SqlDataReader reader)
        {
            //This constructor is using string comparisons and is more efficient than if you use integer offsets.
            //The offset should be 0 otherwise it will throw me an exception to let
            //me know to fix it.
            //This varifies the shape is as expected
            //Get ordinal references the shape from the SQL GetUsers varifying that 
            //Columns are in the correct order.
            OffsetToUserID = reader.GetOrdinal("UserID");
            Assert(0 == OffsetToUserID, $"UserID is {OffsetToUserID} not 0 as expected");

            OffsetToEmail = reader.GetOrdinal("Email");
            Assert(1 == OffsetToEmail, $"Email is {OffsetToEmail} not 1 as expected");

            OffsetToUserName = reader.GetOrdinal("UserName");
            Assert(2 == OffsetToUserName, $"UserName is {OffsetToUserName} not 2 as expected");

            OffsetToHash = reader.GetOrdinal("Hash");
            Assert(3 == OffsetToHash, $"Hash is {OffsetToHash} not 3 as expected");

            OffsetToSalt = reader.GetOrdinal("Salt");
            Assert(4 == OffsetToSalt, $"Salt is {OffsetToSalt} not 4 as expected");

            OffsetToRoleID = reader.GetOrdinal("RoleID");
            Assert(5 == OffsetToRoleID, $"RoleID is {OffsetToRoleID} not 5 as expected");

            OffsetToRoleName = reader.GetOrdinal("RoleName");
            Assert(6 == OffsetToRoleName, $"RoleName is {OffsetToRoleName} not 6 as expected");




        }
        
        public UsersDAL UserFromReader(System.Data.SqlClient.SqlDataReader reader)
        {
            UsersDAL ProposedReturnValue = new UsersDAL();
            ProposedReturnValue.UserID = reader.GetInt32(OffsetToUserID);
            // reader["UserID"]  is very slow and makes a lot of garbage
            // reader[0] makes a lot of garbage
            // reader.GetInt32(0) is fast, but hard codes the offset to 0
            // reader.GetInt32(OffsetToUserID) is best and allows verification
            ProposedReturnValue.Email = reader.GetString(OffsetToEmail);
            ProposedReturnValue.UserName = reader.GetString(OffsetToUserName);
            //The GetStringOrDefault fuction is a Helper from the Parent Mapper because the Hash and Salt are Nullable at the DataBase Level.
            ProposedReturnValue.Hash = GetStringOrDefault(reader,OffsetToHash);
            ProposedReturnValue.Salt = GetStringOrDefault(reader,OffsetToSalt);
            ProposedReturnValue.RoleID = reader.GetInt32(OffsetToRoleID);
            ProposedReturnValue.RoleName = reader.GetString(OffsetToRoleName);




            return ProposedReturnValue;
        }
    }
}
