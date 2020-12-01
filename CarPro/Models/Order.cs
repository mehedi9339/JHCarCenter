using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CarPro.Models
{
    public class Order
    {
        public int Id { get; set; }
        [ForeignKey("Offer")]
        public int OfferId { get; set; }
        public int Quantity { get; set; }
        public string Remarks { get; set; }
        public int ChallanNo { get; set; }
        public int BillNo { get; set; }
        public int ReceiptNo { get; set; }
        public string PaymentType { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalPrice { get; set; }



        public Offer Offer { get; set; }
    }
}
