using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarPro.ViewModels
{
    public class ReceiptVm
    {
        public string CustomerName { get; set; }
        public string Address { get; set; }
        
        public string TypeOfVehicle { get; set; }
        public string ChassisNo { get; set; }
        public string EngineNo { get; set; }
        public string ManfYear { get; set; }
        public string Cc { get; set; }
        public string Colour { get; set; }
        public string LoadCapacity { get; set; }
        public string Accessories { get; set; }

        public int ReceiptlNo { get; set; }
        public int ChallanNo { get; set; }
        public int BillNo { get; set; }
        public DateTime OrderDate { get; set; }
        public string PaymentType { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public string Remarks { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
