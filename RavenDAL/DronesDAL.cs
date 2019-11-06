using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RavenDAL;

namespace RavenDAL
{
    public class DronesDAL
    {
        #region Direct Drone Properties
        public int DroneID { get; set; }
        public int RoleID { get; set; }
        public string DroneName { get; set; }
        public int UserID { get; set; }
        #endregion Direct Properties
        #region Indirect properties from the UserTable
        public string UserName { get; set; }
        public string Email { get; set; }
        #endregion Indirect Properties

        public override string ToString()
        {
            return $"DroneID: {DroneID,-5} RoleID:{RoleID-3} DroneName:{DroneName,-20} UserID:{UserID-3} UserName:{UserName,-20} Email:{Email,-30}";
        }

    }
}
