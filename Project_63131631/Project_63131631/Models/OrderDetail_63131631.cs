namespace Project_63131631.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
	[Table("OrderDetails")]

	public partial class OrderDetail_63131631
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(20)]
        public string orderID { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(50)]
        public string proID { get; set; }

        public int? ordtsQuantity { get; set; }

        [StringLength(50)]
        public string ordtsThanhTien { get; set; }

        public virtual Order_63131631 Order { get; set; }

        public virtual Product_63131631 Product { get; set; }
    }
}
