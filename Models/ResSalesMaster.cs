using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace RMS.Models
{
    public class ResSalesMaster
    {
        [Key]
        public int RSMId { get; set; }
        [Required]
        [StringLength(10)]
        [Display(Name = "Table No.")]
        public string RSMTableNo { get; set; }
        [Required]
        [ForeignKey("RDClose")]
        [Display(Name = "Date")]
        public int RDCId { get; set; }
        public DateTime RDCDate { get; set; }
        [NotMapped]
        public string RDCDateString
        {
            get
            {
                return RDCDate == null ? string.Empty : RDCDate.ToString("MM/dd/yyyy");
            }
        }
        [Required]
        [ForeignKey("HREmpDetails")]
        [Display(Name = "Waiter Name")]
        public int HREDId { get; set; }
        public virtual HREmpDetails HREmpDetails { get; set; }
        [NotMapped]
        public string HREmpDetailsName { get; set; }
        [Required]
        [Display(Name = "Time")]
        public RSMTime RSMTime { get; set; }
        [Required]
        [Display(Name = "Person")]
        public int RSMPerson { get; set; }
        [Required]
        public bool IsBPrint { get; set; }
        [Required]
        public bool IsBPaid { get; set; }
        [Required]
        [StringLength(50)]
        public string CUser { get; set; }
        [Required]
        public DateTime CreateDate { get; set; } = DateTime.Now;
        [NotMapped]
        public string RSItems { get; set; }
        public virtual List<ResSalesDetails> ResSalesDetails { get; set; }
    }
}

namespace RMS.Models
{
    public enum RSMTime
    {
        Breakfast, Lunch, Dinner 
    }
}