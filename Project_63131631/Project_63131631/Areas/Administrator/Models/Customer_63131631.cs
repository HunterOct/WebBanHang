namespace Project_63131631.Areas.Administrator.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	using System.Data.Entity.Spatial;
	[Table("Customers")]

	public partial class Customer_63131631
	{
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		public Customer_63131631()
		{
			Orders = new HashSet<Order_63131631>();
		}

		[Key]
		[Display(Name = "Mã Khách Hàng")]
		public string cusID { get; set; }

		[StringLength(200)]
		[Display(Name = "Tên Khách Hàng")]
		public string cusFullName { get; set; }
		[Display(Name = "Mật Khẩu")]
		public string cusPass { get; set; }
		[Display(Name = "Số Điện Thoại")]
		[StringLength(20)]
		public string cusPhone { get; set; }
		[Display(Name = "Email")]
		[StringLength(100)]
		public string cusEmail { get; set; }
		[Display(Name = "Địa Chỉ")]
		[StringLength(500)]
		public string cusAddress { get; set; }
		[Display(Name = "Ảnh Đại Diện")]
		public string cusIMG { get; set; }
		[Display(Name = "Giới Tính")]
		public bool GioiTinh { get; set; }

		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
		public virtual ICollection<Order_63131631> Orders { get; set; }
	}
}
