namespace Project_63131631.Areas.Administrator.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	using System.Data.Entity.Spatial;
	[Table("Orders")]

	public partial class Order_63131631
	{
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:CollectionPropertiesShouldBeReadOnly")]
		public Order_63131631()
		{
			OrderDetails = new HashSet<OrderDetail_63131631>();
		}
		[Key]
		[StringLength(20)]
		public string orderID { get; set; }
		public string cusID { get; set; }

		[StringLength(20)]
		public string cusPhone { get; set; }
		public string cusAddress { get; set; }

		public string orderMessage { get; set; }

		[StringLength(50)]
		public string orderDateTime { get; set; }

		[StringLength(50)]
		public string orderStatus { get; set; }

		public virtual Customer_63131631 Customer { get; set; }

		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
		public virtual ICollection<OrderDetail_63131631> OrderDetails { get; set; }
	}
}