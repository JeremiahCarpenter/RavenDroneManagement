using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RavenDAL;

namespace RavenBLL
{
    public class UsersBLL
    {
        public UsersBLL()
        {

        }
        public UsersBLL(UsersDAL dal)
        {
            //The mapper here is different because it is mapping from C# to C#
            this.UserID = dal.UserID;
            this.Email = dal.Email;
            this.RoleID = dal.RoleID;
            this.UserName = dal.UserName;
            this.Hash = dal.Hash;
            this.Salt = dal.Salt;
            this.RoleName = dal.RoleName;
        }
        //THey are in order because its easier to problem solve exceptions
        #region Direct Properties
        public int UserID { get; set; }
        [Required]
        public string Email { get; set; }
        public int RoleID { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Hash { get; set; }
        [Required]
        public string Salt { get; set; }
        #endregion Direct Properties
        #region Indirect Properties
        [Required]
        public string RoleName { get; set; }
        #endregion
        public override string ToString()
        {
            return $"UserID:{UserID} Email:{Email}  RoleID:{RoleID} UserName:{UserName}  Hash:{Hash} Salt:{Salt} RoleName:{RoleName}";
        }

    }
}
    


 

