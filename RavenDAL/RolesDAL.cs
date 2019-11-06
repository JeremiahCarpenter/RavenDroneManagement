using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RavenDAL
{
    public class RolesDAL
    {
        #region Direct Properties
        public int RoleID { get; set; }
        public string RoleName { get; set; }

        #endregion Direct Properties
        #region Indirect Properties
        //There is no FK for the Roles Table, so there are no indirect properties
        #endregion Indirect Properties
        public override string ToString()
        {
            return $"RoleID:{RoleID,5} RoleName:{RoleName}";
        }
    }
}
