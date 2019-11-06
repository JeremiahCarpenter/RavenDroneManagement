using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RavenDAL
{
    class ViolationsMapper : Mapper
    {
        int OffsetToViolationID;
        int OffsetToViolationDesc;
        int OffsetToRecordSpeed;
        int OffsetToFineAmount;
        int OffsetToPlateID;
        int OffsetToObsID;
        int OffsetToLatNumber;
        int OffsetToLongNumber;
        int OffsetToRegisteredOwner;

        public ViolationsMapper(System.Data.SqlClient.SqlDataReader reader)
        {
            OffsetToViolationID = reader.GetOrdinal("ViolationID");
            Assert(0 == OffsetToViolationID, "The ViolationID is not 0 as expected");

            OffsetToViolationDesc = reader.GetOrdinal("ViolationDesc");
            Assert(1 == OffsetToViolationDesc, "The ViolationDesc is not 1 as expected");

            OffsetToRecordSpeed = reader.GetOrdinal("RecordSpeed");
            Assert(2 == OffsetToRecordSpeed, "The RecordSpeed is not 2 as expected");

            OffsetToFineAmount = reader.GetOrdinal("FineAmount");
            Assert(3 == OffsetToFineAmount, "The FineAmount is not 3 as expected");

            OffsetToPlateID = reader.GetOrdinal("PlateID");
            Assert(4 == OffsetToPlateID, "The PlateID is not 4 as expected");

            OffsetToObsID = reader.GetOrdinal("ObsID");
            Assert(5 == OffsetToObsID, "The ObsID is not 5 as expected");

            OffsetToLatNumber = reader.GetOrdinal("LatNumber");
            Assert(6 == OffsetToLatNumber, "The LatNumber is not 6 as expected");

            OffsetToLongNumber = reader.GetOrdinal("LongNumber");
            Assert(7 == OffsetToLongNumber, "The LongNumber is not 7 as expected");

            OffsetToRegisteredOwner = reader.GetOrdinal("RegisteredOwner");
            Assert(8 == OffsetToRegisteredOwner, "The RegisteredOwner is not 8 as expected");

        }
        public ViolationsDAL ViolationFromReader(System.Data.SqlClient.SqlDataReader reader)
        {
            ViolationsDAL proposedReturnValue = new ViolationsDAL();
            proposedReturnValue.ViolationID = GetInt32OrDefault(reader, OffsetToViolationID);
            proposedReturnValue.ViolationDesc = GetStringOrDefault(reader, OffsetToViolationDesc);
            proposedReturnValue.RecordSpeed = GetInt32OrDefault(reader, OffsetToRecordSpeed);
            proposedReturnValue.FineAmount = GetDecimalOrDefault(reader, OffsetToFineAmount);
            proposedReturnValue.PlateID = GetInt32OrDefault(reader, OffsetToPlateID);
            proposedReturnValue.ObsID = GetInt32OrDefault(reader, OffsetToObsID);
            proposedReturnValue.LatNumber = GetStringOrDefault(reader, OffsetToLatNumber);
            proposedReturnValue.LongNumber = GetStringOrDefault(reader, OffsetToLongNumber);
            proposedReturnValue.RegisteredOwner = GetStringOrDefault(reader, OffsetToRegisteredOwner);

            return proposedReturnValue;
        }
    }
}
