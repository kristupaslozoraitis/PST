using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KAMANDAX.Models
{
    public class OrderedProducts
    {
        [Key]
        public Guid OrderedProductID { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Category { get; set; }
        [Required]
        public byte[] ImageData { get; set; }
        [Required]
        public int Price { get; set; }
        [Required]
        public Guid OrderID { get; set; }

    }
}
