using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS.Models
{
    public class ResDClose
    {
        [Key]
        public int RDCId { get; set; }
        [Required]
        [DisplayName("Current Date (MM/DD/YYYY): ")]
        [DataType(DataType.Date)]
        public DateTime RDCDate { get; set; } = DateTime.Now.Date;
        [Required]
        [StringLength(50)]
        [DisplayName("Last Day Closed User:")]
        public string CUser { get; set; } 
    }
}
