using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarPro.Models
{
    public class Offer
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string Chassis { get; set; }
        public string Engine { get; set; }
        public string ManfYear { get; set; }
        public string Cc { get; set; }
        public string Colour { get; set; }
        public string LoadCapacity { get; set; }
        public string Accessories { get; set; }
        public decimal Price { get; set; }
        public DateTime OfferDate { get; set; }
        public int Delivery { get; set; }
        public int Validity { get; set; }
        [ForeignKey("Customer")]
        public int CustomerId { get; set; }

        public Customer Customer { get; set; }
    }
}
