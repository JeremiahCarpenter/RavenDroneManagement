using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RavenDAL
{
    public class ViolationsDAL
    {
        #region Direct Properties 
        public int ViolationID { get; set; }
        public string ViolationDesc { get; set; }
        public int RecordSpeed { get; set; }
        public Decimal FineAmount { get; set; }
        public int PlateID { get; set; }
        public int ObsID { get; set; }
        #endregion Direct Properties
        #region Indirect Properties to Violations Table
        public string LatNumber { get; set; }
        public string LongNumber { get; set; }
        public string RegisteredOwner { get; set; }
        #endregion Indirect Properties

        public override string ToString()
        {
            return $"ViolationID: {ViolationID,5} ViolationDesc:{ViolationDesc} RecordSpeed:{RecordSpeed} FineAmount:{FineAmount} PlateID:{PlateID} ObsID:{ObsID} LatNumber:{LatNumber} LongNumber:{LongNumber} RegisteredOwner:{RegisteredOwner}";
        }
    }
}
