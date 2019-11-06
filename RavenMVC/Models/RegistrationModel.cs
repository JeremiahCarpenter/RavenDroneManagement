using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RavenMVC.Models
{
    public class RegistrationModel
    {
        [Required]
        public string UserName { get; set; }//will be the badge number
        [StringLength(MagiConstants.MaxPasswordLength,
            ErrorMessage = "The {0} must be between {2} and {1} characters long.",
            MinimumLength = MagiConstants.MinPasswordLength)]
        [RegularExpression(MagiConstants.PasswordRequirements,
            ErrorMessage = MagiConstants.PasswordRequirementsMessage)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        public string PasswordAgain { get; set; }
        public string Email { get; set; }
        public string Message { get; set; }

        //  any other user information you wish to collect during registration
    }
}