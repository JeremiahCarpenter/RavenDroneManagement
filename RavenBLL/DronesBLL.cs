using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RavenBLL
{
    public class DronesBLL
    {
        #region Direct Drone Properties
        public int DroneID { get; set; }
        public int RoleID { get; set; }
        [Required]
        [DisplayName("Drone Name")]
        public string DroneName { get; set; }
        public int UserID { get; set; }
        #endregion Direct Properties
        #region Indirect properties from the UserTable
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Email { get; set; }
        #endregion Indirect Properties

        public DronesBLL()
        {

        }
        public DronesBLL(RavenDAL.DronesDAL dal)
        {
            this.DroneID = dal.DroneID;
            this.RoleID = dal.RoleID;
            this.DroneName = dal.DroneName;
            this.UserID = dal.UserID;
            this.UserName = dal.UserName;
            this.Email = dal.Email;
        }
        public override string ToString()
        {
            return $"DroneID: {DroneID,-5} RoleID:{RoleID - 3} DroneName:{DroneName,-20} UserID:{UserID - 3} UserName:{UserName,-20} Email:{Email,-30}";
        }
    }
}
