using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GoDam.Models
{
    public class Purchase
    {
        [Key]
        public int PurchaseID { get; set; }

        [Required]
        public int BillNumber { get; set; }

        [Required]
        public string VendorName { get; set; }

        [Required]
        public string VendorAddress { get; set; }

        [Required]
        public DateTime PurchaseDate { get; set; }


    }
}
