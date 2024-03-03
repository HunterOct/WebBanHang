using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project_63131631.Models
{
    public class CartItem_63131631
    {
        public string SanPhamID { get; set; } 
		public string cusID { get; set; }
		public string TenSanPham { get; set; }
        public string Hinh { get; set; }
        public string DonGia { get; set; }
        public int SoLuong { get; set; }
        public int ThanhTien
        {
            get
            {
                return SoLuong * Int32.Parse(DonGia);
            }
        }
		public virtual Customer_63131631 Customer { get; set; }

	}
}