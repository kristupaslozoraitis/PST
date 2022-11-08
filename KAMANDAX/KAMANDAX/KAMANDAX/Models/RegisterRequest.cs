using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KAMANDAX.Models
{
    public class RegisterRequest
    {
        [Required (ErrorMessage = "Full Name is required")]
        public string FullName { get; set; }
        [Required (ErrorMessage = "Please enter your email address")]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "Your password needs to be at least 6 characters long")]
        public string Password { get; set; }

        public RegisterRequest()
        {

        }
    }
}
