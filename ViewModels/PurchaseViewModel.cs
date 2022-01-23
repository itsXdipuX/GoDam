using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GoDam.Models;

namespace GoDam.ViewModel
{
    public class PurchaseViewModel
    {
        public Purchase Purchase { get; set; }
        public List<PurchaseDetails> ListPurchaseDetails { get; set; }
    }
}
