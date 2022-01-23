using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GoDam.Models
{
    public class PurchaseDetails
    {
        [Key]
        public int PDID { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public int Amount { get; set; }

        [Required]
        public int LineTotal { get; set; }

        [ForeignKey("Prod")]
        public int ProductID { get; set; }

        [ForeignKey("Pur")]
        public int PurchaseID { get; set; }

        public virtual Product Prod { get; set; }

        public virtual Purchase Pur { get; set; }

    }
}
