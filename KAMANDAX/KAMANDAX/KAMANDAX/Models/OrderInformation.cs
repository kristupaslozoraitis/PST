using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KAMANDAX.Models
{
    public class OrderInformation
    {
        // ------------------------------------------------
        // Information
        [Key]
        public Guid OrderID { get; set; }
        public Guid userID { get; set; }
        public string EmailAddress { get; set; }
        public string FullName { get; set; }
        public string HomeAddress { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string PostalCode { get; set; }

        // ------------------------------------------------
        // Receiver Information
        public string ReceiverEmailAddress { get; set; }
        public string ReceiverFullName { get; set; }
        public string ReceiverHomeAddress { get; set; }
        public string ReceiverCity { get; set; }
        public string ReceiverCountry { get; set; }

        // ------------------------------------------------
        // Delivery

        public string CompanyName { get; set; }
        public string DeliveryType { get; set; }
        public double? DeliveryPrice { get; set; }
        public string TerminalAddress { get; set; }
        public string PhoneNumber { get; set; }

        // ------------------------------------------------
        // Card

        public string PaymentType { get; set; } // MasterCard, VISA, Cash
        public string CardHolder { get; set; }
        [StringLength(19, ErrorMessage = "Please enter a valid credit card number")]
        public string CardNumber { get; set; }
        [Range(01, 12)]
        public int? CardMonth { get; set; }
        [Range(21, 50)]
        public int? CardYear { get; set; }
        [Range(100, 999)]
        public int? CardCVC { get; set; }

        public double? TotalPrice { get; set; }


        // ------------------------------------------------

        public DateTime OrderDate { get; set; }
        public bool ConfirmedOrder { get; set; }



        public OrderInformation()
        {
            DeliveryPrice = null;
           
            TotalPrice = null;

            ConfirmedOrder = false;
        }
    }

}
