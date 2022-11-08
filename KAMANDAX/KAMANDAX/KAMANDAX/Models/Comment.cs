using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KAMANDAX.Models
{
    public class Comment
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Data { get; set; }
        public Guid ProductId { get; set; }
    }
}
