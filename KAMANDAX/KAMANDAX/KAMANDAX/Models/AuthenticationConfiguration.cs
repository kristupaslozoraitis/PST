using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KAMANDAX.Models
{
    public class AuthenticationConfiguration
    {
        public string Key { get; set; }
        public double ExpireTime { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string RefreshTokenSecret { get; set; }
        public double RefreshTokenExpiration { get; set; }

    }
}
