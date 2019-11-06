using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RavenBLL
{
    public class ObservationsBLL
    {
        #region Direct Properties
        public int ObsID { get; set; }
        public int Speed { get; set; }
        [Required]
        public string LatNumber { get; set; }
        [Required]
        public string LongNumber { get; set; }
        public int PlateID { get; set; }
        [Required]
        public string RegisteredOwner { get; set; }
        public int DroneID { get; set; }
        #endregion Direct Properties
        #region Indirect Properties to the Drones Table
        [Required]
        public string DroneName { get; set; }

        #endregion Indirect Properties
        public ObservationsBLL()
        {

        }
        public ObservationsBLL(RavenDAL.ObservationsDAL dal)
        {
            this.ObsID = dal.ObsID;
            this.Speed = dal.Speed;
            this.LatNumber = dal.LatNumber;
            this.LongNumber = dal.LongNumber;
            this.PlateID = dal.PlateID;
            this.RegisteredOwner = dal.RegisteredOwner;
            this.DroneID = dal.DroneID;
            this.DroneName = dal.DroneName;

        }
        public override string ToString()
        {
            return $"ObsID: {ObsID,5} Speed:{Speed,-3} LatNumber:{LatNumber,15} LongNumber:{LongNumber,15} PlateID:{PlateID} RegisteredOwner:{RegisteredOwner,20} DroneID:{DroneID} DroneName:{DroneName}";
        }
    }
}
