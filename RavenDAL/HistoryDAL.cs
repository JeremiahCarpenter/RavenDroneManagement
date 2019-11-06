using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RavenDAL
{
    public class HistoryDAL
    {
        #region Direct Properties
        public int HistoryID { get; set; }
        public int PlateID { get; set; }
        public string PaidFine { get; set; }
        public string RegisteredOwner { get; set; }
        public string Address1 { get; set; }
        public string State { get; set; }
        public int ViolationID { get; set; }
        #endregion Direct Properties
        #region Indirect Properties to the Violations Table
        public string ViolationDesc { get; set; }
        public int RecordSpeed { get; set; }

        #endregion Indirect Properties
        public override string ToString()
        {
            return $"HistoryID: {HistoryID,5} PlateID:{PlateID} PaidFine:{PaidFine} RegisteredOwner:{RegisteredOwner} Address1:{Address1} State:{State} ViolationID:{ViolationID} ViolationDesc:{ViolationDesc} RecordSpeed:{RecordSpeed}";
        }

    }
}
