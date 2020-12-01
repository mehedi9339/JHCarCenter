using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarPro.ViewModels
{
    public class QuatationVm
    {
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
        public string CustomerName { get; set; }
        public string Address { get; set; }
        public string Ac { get; set; }
    }
}
