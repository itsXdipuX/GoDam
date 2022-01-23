using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GoDam.Models
{
    public class Product
    {
        [Key]
        public int ProductID { get; set; }

        [Required]
        public string ProductName { get; set; }

        public DateTime MFDate { get; set; }

        public DateTime ExpDate { get; set; }

        [ForeignKey("Cat")]
        public int CategoryID { get; set; }

        public virtual Category Cat { get; set; }


    }
}
