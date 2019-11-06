using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RavenDAL
{
    public class HistoryMapper : Mapper
    {
            int OffsetToHistoryID; //0
            int OffsetToPlateID; //1
            int OffsetToPaidFine; //2
            int OffsetToRegisteredOwner; //3
            int OffsetToAddress1; //4
            int OffsetToState; //5
            int OffsetToViolationID; //6
            int OffsetToViolationDesc; //7
            int OffsetToRecordSpeed; //8

        public HistoryMapper(System.Data.SqlClient.SqlDataReader reader)
        {
            OffsetToHistoryID = reader.GetOrdinal("HistoryID");
            Assert(0 == OffsetToHistoryID, "The HistoryID is not 0 as expected");

            OffsetToPlateID = reader.GetOrdinal("PlateID");
            Assert(1 == OffsetToPlateID, "The PlateID is not 1 as expected");

            OffsetToPaidFine = reader.GetOrdinal("PaidFine");
            Assert(2 == OffsetToPaidFine, "The PaidFine is not 2 as expected");

            OffsetToRegisteredOwner = reader.GetOrdinal("RegisteredOwner");
            Assert(3 == OffsetToRegisteredOwner, "The RegisteredOwner is not 3 as expected");

            OffsetToAddress1 = reader.GetOrdinal("Address1");
            Assert(4 == OffsetToAddress1, "The Address1 is not 4 as expected");

            OffsetToState = reader.GetOrdinal("State");
            Assert(5 == OffsetToState, "The State is not 5 as expected");

            OffsetToViolationID = reader.GetOrdinal("ViolationID");
            Assert(6 == OffsetToViolationID, "The ViolationID is not 6 as expected");

            OffsetToViolationDesc = reader.GetOrdinal("ViolationDesc");
            Assert(7 == OffsetToViolationDesc, "The ViolationID is not 7 as expected");

            OffsetToRecordSpeed = reader.GetOrdinal("RecordSpeed");
            Assert(8 == OffsetToRecordSpeed, "The RecordSpeed is not 8 as expected");

        }
            public HistoryDAL HistoryFromReader(System.Data.SqlClient.SqlDataReader reader)
            {
                HistoryDAL proposedReturnValue = new HistoryDAL();
                proposedReturnValue.HistoryID = GetInt32OrDefault(reader, OffsetToHistoryID);
                proposedReturnValue.PlateID = GetInt32OrDefault(reader, OffsetToPlateID);
                proposedReturnValue.PaidFine = GetStringOrDefault(reader, OffsetToPaidFine);
                proposedReturnValue.RegisteredOwner = GetStringOrDefault(reader, OffsetToRegisteredOwner);
                proposedReturnValue.Address1 = GetStringOrDefault(reader, OffsetToAddress1);
                proposedReturnValue.State = GetStringOrDefault(reader, OffsetToState);
                proposedReturnValue.ViolationID = GetInt32OrDefault(reader, OffsetToViolationID);
                proposedReturnValue.ViolationDesc = GetStringOrDefault(reader, OffsetToViolationDesc);
                proposedReturnValue.RecordSpeed = GetInt32OrDefault(reader, OffsetToRecordSpeed);

            return proposedReturnValue;
            }
        




    }
}
