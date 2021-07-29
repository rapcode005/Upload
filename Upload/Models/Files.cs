using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Upload.Models
{
    public class Files
    {
        [Required]
        [MaxLength(100, ErrorMessage = "TI can be 100 characters only")]
        public string ti { get; set; }
        [Required]
        public decimal a { get; set; }
        [Required]
        [MaxLength(3, ErrorMessage = "CC can be 3 characters only")]
        public string  cc { get; set; }
        [Required]
        public DateTime td { get; set; }
        [Required]
        [MaxLength(1, ErrorMessage = "CC can be 1 character only")]
        public string s { get; set; }
    }
}
