using Project_63131631.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Project_63131631.Controllers
{
	public class ThanhToan_63131631Controller : Controller
	{
		UserContext_63131631 db = new UserContext_63131631();
		// GET: Shopper/ThanhToan
		public ActionResult Index()
		{
			List<CartItem_63131631> giohang = Session["giohang"] as List<CartItem_63131631>;
			if (Session["use"] == null || Session["use"].ToString() == "")
			{
				return RedirectToAction("Dangnhap", "Account_63131631");
			}
			else
			{
				//ViewBag.UserInfo = Session["use"] as Customer;
				ViewBag.FullName= Session["UserName"] as string;
				ViewBag.Phone = Session["UserPhone"] as string;
				ViewBag.Address = Session["UserAddress"] as string;
				ViewBag.email = Session["UserEmail"] as string;
				return View(giohang);
			}
		}

		[HttpPost]
		public ActionResult StepEnd()
		{
			Customer_63131631 newCus = new Customer_63131631();
			//Nhận reqest từ trang index
			string note = string.IsNullOrEmpty(Request.Form["note"]) ? "Trống" : Request.Form["note"];
			string ttoan = Request.Form["ttoan"];
			string email = Session["UserEmail"] as string;

			string fullname = Session["UserName"] as string;
			string phone = Session["UserPhone"] as string;
			string address = Session["UserAddress"] as string;
			//kiểm tra xem có customer chưa và cập nhật lại
			var cus = db.Customers.FirstOrDefault(p => p.cusEmail.Equals(email));
			if (cus != null)
			{
				//nếu có số điện thoại trong db rồi
				//cập nhật thông tin và lưu
				cus.cusFullName = fullname;
				cus.cusPhone = phone;
				cus.cusAddress = address;
				db.Entry(cus).State = System.Data.Entity.EntityState.Modified;
				db.SaveChanges();
			}
			else
			{
				//nếu chưa có sđt trong db
				//thêm thông tin và lưu
				newCus.cusPhone = phone;
				newCus.cusFullName = fullname;
				newCus.cusEmail = email;
				newCus.cusAddress = address;
				db.Customers.Add(newCus);
				db.SaveChanges();
			}
			//Thêm thông tin vào order và orderdetail
			List<CartItem_63131631> giohang = Session["giohang"] as List<CartItem_63131631>;
			//thêm order mới
			Order_63131631 newOrder = new Order_63131631();

			// Lấy số đơn hàng cuối cùng từ cơ sở dữ liệu
			//int latestOrderNumber = db.Orders.Any() ? Int32.Parse(db.Orders.Max(p => p.orderID.Replace("HD", ""))) : 10;

			// Tạo mã đơn hàng mới bằng cách tăng số đơn hàng cuối cùng lên 1
			//string newIDOrder = "HD" + (latestOrderNumber + 1).ToString();
			//newOrder.orderID = newIDOrder;

			Random random = new Random();
			string newIDOrder = "HD" + random.Next(10000).ToString("D4");


			//string newIDOrder = (Int32.Parse(db.Orders.OrderByDescending(p => p.orderDateTime).FirstOrDefault().orderID.Replace("HD", "")) + 1).ToString();
			newOrder.cusAddress = cus.cusAddress;
			newOrder.orderID = newIDOrder;
			newOrder.cusPhone = phone;
			newOrder.orderMessage = note;
			newOrder.orderDateTime = DateTime.Now.ToString();
			//newOrder.orderStatus = "0";
			if (ttoan == "COD")
			{
				newOrder.orderStatus = "1"; // Hoặc giá trị tương ứng với COD
			}
			else if (ttoan == "VNPay")
			{
				newOrder.orderStatus = "0"; // Hoặc giá trị tương ứng với Vnpay
			}
			db.Orders.Add(newOrder);
			db.SaveChanges();
			//thêm details
			for (int i = 0; i < giohang.Count; i++)
			{
				OrderDetail_63131631 newOrdts = new OrderDetail_63131631();
				newOrder.cusID = cus.cusID;
				newOrdts.orderID = newOrder.orderID;
				newOrdts.proID = giohang.ElementAtOrDefault(i).SanPhamID;
				newOrdts.ordtsQuantity = giohang.ElementAtOrDefault(i).SoLuong;
				newOrdts.ordtsThanhTien = giohang.ElementAtOrDefault(i).ThanhTien.ToString();
				db.OrderDetails.Add(newOrdts);
				db.SaveChanges();
			}
			Session["MHD"] = newIDOrder;
			Session["Phone"] = phone;
			//xoá sạch giỏ hàng
			giohang.Clear();
			return RedirectToAction("HoaDon", "ThanhToan_63131631");
		}

		public ActionResult HoaDon()
		{
			return View();
		}
	}
}