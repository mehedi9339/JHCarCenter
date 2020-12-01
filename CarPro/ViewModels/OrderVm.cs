using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarPro.ViewModels
{
    public class OrderVm
    {
        public int OfferId { get; set; }
        public int Quantity { get; set; }
        public string Remarks { get; set; }
        public int ChallanNo { get; set; }
        public int BillNo { get; set; }
        public int ReceiptNo { get; set; }
        public string PaymentType { get; set; }
        public DateTime OrderDate { get; set; }
    }
}
