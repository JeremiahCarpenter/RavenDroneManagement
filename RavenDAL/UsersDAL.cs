using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RavenDAL;

namespace RavenDAL
{
    //This is the declaration of the class
    public class UsersDAL
    {
        //the region directive is a teaching tool that my instructor taught me, to help organize this.
        #region Direct Properties
        public int UserID { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Hash { get; set; }
        public string Salt { get; set; }
        public int RoleID { get; set; }
        #endregion Direct Properties
        #region Indirect properties to Users Table
        public string RoleName { get; set; }
        #endregion Indirect properties
        //Describe the difference between direct and indirect properties
        //This method is used only in a console application
        public override string ToString()
        {
            return $"UserID: {UserID,-3} Email:{Email,-30} UserName:{UserName,-20} Hash:{Hash} Salt:{Salt}RoleID:{RoleID,-3}RoleName:{RoleName,-15}";
        }
    }
}
