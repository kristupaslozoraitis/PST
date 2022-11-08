using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KAMANDAX.Models
{
    public class ProductRequest
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Category { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public byte[] ImageData { get; set; }
        [Required]
        public string SellingType { get; set; }
        public int Price { get; set; }
        public string AuctionType { get; set; }
        public DateTime StartDate { get; set; }
        public Guid ProductBuyerID { get; set; }

        public ProductRequest()
        {

        }
    }
}
