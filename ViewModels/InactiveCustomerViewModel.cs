using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoDam.ViewModels
{
    public class InactiveCustomerViewModel
    {
        public int CustomerID { get; set; }
        public string Name { get; set; }
        public string LastPurchasedON { get; set; }
    }
}
