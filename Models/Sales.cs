using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GoDam.Models
{
    public class Sales
    {
        [Key]
        public int SalesID { get; set; }

        [Required]
        public int BillNumber { get; set; }

        [Required]
        public DateTime SalesDate { get; set; }

        [ForeignKey("Cust")]
        public int CustomerID { get; set; }

        public virtual Customer Cust { get; set; }
    }
}
