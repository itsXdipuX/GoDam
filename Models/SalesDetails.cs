using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GoDam.Models
{
    public class SalesDetails
    {
        [Key]
        public int SDID { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public int Price { get; set; }

        [ForeignKey("Produc")]
        public int ProductID { get; set; }

        [ForeignKey("Sal")]
        public int SalesID { get; set; }

        public virtual Product Produc { get; set; }

        public virtual Sales Sal { get; set; }
    }
}
