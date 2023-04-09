using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RMS.Models
{
    public class ResSalesDetails
    {
        [Key]
        public int RSDId { get; set; }
        [Required]
        [ForeignKey("ResSalesMaster")]
        public int RSMId { get; set; }
        public virtual ResSalesMaster ResSalesMaster { get; set; }

        public string RSDItemCode { get; set; }
        public string RSDItemName { get; set; }
        public double RSDUPrice { get; set; }

        
        public int RSDQty { get; set; }
        
        public double RSDTotalPrice { get; set; }
        
        public int? RSDKotNo { get; set; }

        public bool IsCancel { get; set; }
    }
}