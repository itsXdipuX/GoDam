using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GoDam.Models
{
    public class ProductStock
    {
        [Key]
        public int PSID { get; set; }

        [Required]
        public int Quantity { get; set; }

        [ForeignKey("Pro")]
        public int ProductID { get; set; }

        public virtual Product Pro { get; set; }

    }
}
