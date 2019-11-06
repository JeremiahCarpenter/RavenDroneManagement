using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RavenDAL
{
    public class ObservationsDAL
    {
        #region Direct Properties
        public int ObsID { get; set; }
        public int Speed { get; set; }
        public string LatNumber {get; set;}
        public string LongNumber { get; set; }
        public int PlateID { get; set; }
        public string RegisteredOwner { get; set; }
        public int DroneID { get; set; }
        #endregion Direct Properties
        #region Indirect Properties to the Drones Table
        public string DroneName { get; set; }

        #endregion Indirect Properties
        public override string ToString()
        {
            return $"ObsID: {ObsID,5} Speed:{Speed,-3} LatNumber:{LatNumber,15} LongNumber:{LongNumber,15} PlateID:{PlateID} RegisteredOwner:{RegisteredOwner,20} DroneID:{DroneID} DroneName:{DroneName}";
        }
    }
}
