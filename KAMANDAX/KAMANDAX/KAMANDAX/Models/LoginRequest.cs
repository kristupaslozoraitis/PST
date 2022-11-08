using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KAMANDAX.Models
{
    public class LoginRequest
    {
        [Required (ErrorMessage = "Please enter your email address")]
        [EmailAddress]
        public string EmailAdress { get; set; }
        [Required (ErrorMessage = "Please enter your password")]
        public string Password { get; set; }

        public LoginRequest()
        {

        }
    }
}
