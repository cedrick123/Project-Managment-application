using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace new_project.Models
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "You need to provide email address")]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email Adress")]
        public string email { get; set; }
        [Required(ErrorMessage = "You need to provide password")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        [MaxLength(20)]
        [MinLength(8)]
        public string password { get; set; }
        [MaxLength(15)]
        public string userName { get; set; }
       // [DataType(DataType.Custom)]
        public Role role { get; set; }
        
    }
}
