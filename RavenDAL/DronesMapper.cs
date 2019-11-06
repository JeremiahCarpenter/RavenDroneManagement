using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RavenDAL
{
    public class DronesMapper : Mapper
    {
        int OffsetToDroneID;
        int OffsetToRoleID;
        int OffsetToDroneName;
        int OffsetToUserID;
        int OffsetToUserName;
        int OffsetToEmail;

        public DronesMapper (System.Data.SqlClient.SqlDataReader reader)
        {
            OffsetToDroneID = reader.GetOrdinal("DroneID");
            Assert(0 == OffsetToDroneID, "The DroneID is not 0 as expected");

            OffsetToRoleID = reader.GetOrdinal("RoleID");
            Assert(1 == OffsetToRoleID, "The RoleID is not 1 as expected");

            OffsetToDroneName = reader.GetOrdinal("DroneName");
            Assert(2 == OffsetToDroneName, "The DroneName is not 2 as expected");

            OffsetToUserID = reader.GetOrdinal("UserID");
            Assert(3 == OffsetToUserID, "The UserID is not 3 as expected");

            OffsetToUserName = reader.GetOrdinal("UserName");
            Assert(4 == OffsetToUserName, "The UserName is not 4 as expected");

            OffsetToEmail = reader.GetOrdinal("Email");
            Assert(5 == OffsetToEmail, "The Email is not 5 as expected");
            
        }
        public DronesDAL DroneFromReader(System.Data.SqlClient.SqlDataReader reader)
        {
            DronesDAL proposedReturnValue = new DronesDAL();
            proposedReturnValue.DroneID = GetInt32OrDefault(reader, OffsetToDroneID);
            proposedReturnValue.RoleID = GetInt32OrDefault(reader, OffsetToRoleID);
            proposedReturnValue.DroneName = GetStringOrDefault(reader, OffsetToDroneName);
            proposedReturnValue.UserID = GetInt32OrDefault(reader, OffsetToUserID);
            proposedReturnValue.UserName = GetStringOrDefault(reader, OffsetToUserName);
            proposedReturnValue.Email = GetStringOrDefault(reader, OffsetToEmail);
            return proposedReturnValue;
        }

    }
}
