using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KAMANDAX.Models
{
    public class PasswordRequest
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        [Required(ErrorMessage = "Please repeat your password")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "Your password needs to be at least 6 characters long")]
        public string repeatedPassword { get; set; }
        [Required(ErrorMessage = "Please enter your new password")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "Your password needs to be at least 6 characters long")]
        public string newPassword { get; set; }
    }
}
