using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RavenBLL
{
    public class ViolationsBLL
    {

        #region Direct Properties
        public int ViolationID { get; set; }
        [Required]
        public string ViolationDesc { get; set; }
        public int RecordSpeed { get; set; }
        public Decimal FineAmount { get; set; }
        public int PlateID { get; set; }
        public int ObsID { get; set; }
        #endregion Direct Properties
        #region Indirect Properties
        [Required]
        public string LatNumber { get; set; }
        [Required]
        public string LongNumber { get; set; }
        [Required]
        public string RegisteredOwner { get; set; }
        #endregion Indirect Properties

        public ViolationsBLL()
        {

        }
        public ViolationsBLL(RavenDAL.ViolationsDAL dal)
        {
            this.ViolationID = dal.ViolationID;
            this.ViolationDesc = dal.ViolationDesc;
            this.RecordSpeed = dal.RecordSpeed;
            this.FineAmount = dal.FineAmount;
            this.PlateID = dal.PlateID;
            this.ObsID = dal.ObsID;
            this.LatNumber = dal.LatNumber;
            this.LongNumber = dal.LongNumber;
            this.RegisteredOwner = dal.RegisteredOwner;

        }
        public override string ToString()
        {
            return $"ViolationID: {ViolationID,5} ViolationDesc:{ViolationDesc} RecordSpeed:{RecordSpeed} FineAmount:{FineAmount} PlateID:{PlateID} ObsID:{ObsID} LatNumber:{LatNumber} LongNumber:{LongNumber} RegisteredOwner:{RegisteredOwner}";
        }
    }
}
