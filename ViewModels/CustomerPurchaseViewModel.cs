using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoDam.ViewModels
{
    public class CustomerPurchaseViewModel
    {
        public int SalesID { get; set; }
        public int CustomerID { get; set; }
        public int BillNumber { get; set; }
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public int Price { get; set; }
        public int DaysBeforePurchsed { get; set; }
    }
}
