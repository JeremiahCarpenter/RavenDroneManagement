using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RavenDAL;

namespace RavenBLL
{
    public class RolesBLL
    {
        public RolesBLL()
        {

        }
        public RolesBLL(RolesDAL dal)
        {
            this.RoleID = dal.RoleID;
            
            this.RoleName = dal.RoleName;
            
        }
        #region Direct Properties
        public int RoleID { get; set; }
        [Required]
        public string RoleName { get; set; }
        
        #endregion Direct Properties
        
        public override string ToString()
        {
            return $"RoleID:{RoleID,5} RoleName:{RoleName}"; ;
        }

    }
}
