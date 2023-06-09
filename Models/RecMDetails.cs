﻿namespace RMS.Models
{
    public class RecMDetails
    {
        [Key]
        public int RMDId { get; set; }
        [StringLength(50)]
        public string SIGItemCode { get; set; }
        [StringLength(50)]
        public string SIGItemName { get; set; }
        [StringLength(50)]
        public string SIGUnit { get; set; }

        public float SGSUPrice { get; set; }
        public int RMDQty { get; set; }

        [ForeignKey("RMMaster")]
        public int RMMId { get; set; }
        public virtual RecMMaster RMMaster { get; set; }

    }
}
