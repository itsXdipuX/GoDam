using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoDam.ViewModels
{
    public class ItemDetailsViewModel
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public string ProductCategory { get; set; }
        public int Quantity { get; set; }
        public int PricePerPiece { get; set; }
        public string VendorName { get; set; }
        public string PurchaseDate { get; set; }

    }
}
