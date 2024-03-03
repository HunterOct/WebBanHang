namespace Project_63131631.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
	[Table("Rates")]

	public partial class Rate_63131631
    {
        [Key]
        [StringLength(50)]
        public string proID { get; set; }

        public int? rateStar { get; set; }

        public virtual Product_63131631 Product { get; set; }
    }
}
