using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RavenDAL
{
    public class ObservationsMapper : Mapper
    {
        int OffsetToObsID;
        int OffsetToSpeed;
        int OffsetToLatNumber;
        int OffsetToLongNumber;
        int OffsetToPlateID;
        int OffsetToRegisteredOwner;
        int OffsetToDroneID;
        int OffsetToDroneName;

        public ObservationsMapper(System.Data.SqlClient.SqlDataReader reader)
        {
            OffsetToObsID = reader.GetOrdinal("ObsID");
            Assert(0 == OffsetToObsID, "The ObsID is not 0 as expected");

            OffsetToSpeed = reader.GetOrdinal("Speed");
            Assert(1 == OffsetToSpeed, "The Speed is not 1 as expected");

            OffsetToLatNumber = reader.GetOrdinal("LatNumber");
            Assert(2 == OffsetToLatNumber, "The LatNumber is not 2 as expected");

            OffsetToLongNumber = reader.GetOrdinal("LongNumber");
            Assert(3 == OffsetToLongNumber, "The LongNumber is not 3 as expected");

            OffsetToPlateID = reader.GetOrdinal("PlateID");
            Assert(4 == OffsetToPlateID, "The PlateID is not 4 as expected");

            OffsetToRegisteredOwner = reader.GetOrdinal("RegisteredOwner");
            Assert(5 == OffsetToRegisteredOwner, "The RegisteredOwner is not 5 as expected");

            OffsetToDroneID = reader.GetOrdinal("DroneID");
            Assert(6 == OffsetToDroneID, "The DroneID is not 6 as expected");

            OffsetToDroneName = reader.GetOrdinal("DroneName");
            Assert(7 == OffsetToDroneName, "The DroneName is not 7 as expected");

        }
        public ObservationsDAL ObservationFromReader(System.Data.SqlClient.SqlDataReader reader)
        {
            ObservationsDAL proposedReturnValue = new ObservationsDAL();
            proposedReturnValue.ObsID = GetInt32OrDefault(reader, OffsetToObsID);
            proposedReturnValue.Speed = GetInt32OrDefault(reader, OffsetToSpeed);
            proposedReturnValue.LatNumber = GetStringOrDefault(reader, OffsetToLatNumber);
            proposedReturnValue.LongNumber = GetStringOrDefault(reader, OffsetToLongNumber);
            proposedReturnValue.PlateID = GetInt32OrDefault(reader, OffsetToPlateID);
            proposedReturnValue.RegisteredOwner = GetStringOrDefault(reader, OffsetToRegisteredOwner);
            proposedReturnValue.DroneID = GetInt32OrDefault(reader, OffsetToDroneID);
            proposedReturnValue.DroneName = GetStringOrDefault(reader, OffsetToDroneName);

            return proposedReturnValue;
        }
    }
   


   

}
