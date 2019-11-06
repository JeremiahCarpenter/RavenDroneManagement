using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RavenBLL
{
    public class HistoryBLL
    {
        #region Direct Properties
        public int HistoryID { get; set; }
        public int PlateID { get; set; }
        [Required]
        public string PaidFine { get; set; }
        [Required]
        public string RegisteredOwner { get; set; }
        [Required]
        public string Address1 { get; set; }
        [Required]
        public string State { get; set; }
        public int ViolationID { get; set; }
        #endregion Direct Properties
        #region Indirect Properties to the Violations Table
        [Required]
        public string ViolationDesc { get; set; }
        public int RecordSpeed { get; set; }

        #endregion Indirect Properties
        public HistoryBLL()
        {

        }
        public HistoryBLL(RavenDAL.HistoryDAL dal)
        {
            this.HistoryID = dal.HistoryID;
            this.PlateID = dal.PlateID;
            this.PaidFine = dal.PaidFine;
            this.RegisteredOwner = dal.RegisteredOwner;
            this.Address1 = dal.Address1;
            this.State = dal.State;
            this.ViolationID = dal.ViolationID;
            this.ViolationDesc = dal.ViolationDesc;
            this.RecordSpeed = dal.RecordSpeed;

        }
        public override string ToString()
        {
            return $"HistoryID: {HistoryID,5} PlateID:{PlateID} PaidFine:{PaidFine} RegisteredOwner:{RegisteredOwner} Address1:{Address1} State:{State} ViolationID:{ViolationID} ViolationDesc:{ViolationDesc} RecordSpeed:{RecordSpeed}";
        }
    }
}
