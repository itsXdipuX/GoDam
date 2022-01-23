using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GoDam.Models;

namespace GoDam.ViewModel
{
    public class SalesViewModel
    {
        public Sales Sales { get; set; }
        public List<SalesDetails> ListSalesDetails { get; set; }
    }
}
