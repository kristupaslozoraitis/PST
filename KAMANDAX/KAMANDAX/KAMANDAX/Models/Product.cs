using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KAMANDAX.Models
{
    public class Product
    {
        [Key]
        public Guid ProductId { get; set; }
        [Required]
        public Guid ProductUserId { get; set; }
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
        [Required]
        public int Price { get; set; }
        public string AuctionType { get; set; }
        public int TimesViewed { get; set; }
        public Guid OrderedBy { get; set; }
        public DateTime StartDate { get; set; }
        //public Guid ProductBuyerID { get; set; }
    }
}
