using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarPro.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Ac { get; set; }

        public List<Offer> Offers { get; set; }
    }
}
