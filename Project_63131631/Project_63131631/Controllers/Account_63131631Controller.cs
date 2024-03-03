using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Project_63131631.Models;

namespace Project_63131631.Controllers
{
	public class Account_63131631Controller : Controller
	{
		UserContext_63131631 db = new UserContext_63131631();
		public ActionResult DangNhap()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken()]
		public ActionResult DangNhap(FormCollection userlog)
		{
			string userMail = userlog["cusEmail"].ToString();
			string password = userlog["cusPass"].ToString();
			var islogin = db.Customers.SingleOrDefault(x => x.cusEmail.Equals(userMail) && x.cusPass.Equals(password));

			if (islogin != null)
			{
					Session["use"] = islogin;

					Session["UserEmail"] = userMail;
					Session["UserName"] = islogin.cusFullName;
					Session["UserAddress"] = islogin.cusAddress;
					Session["UserPhone"] = islogin.cusPhone;

				return RedirectToAction("Index", "Home_63131631");
			}
			else
			{
				ViewBag.Fail = "Tài khoản hoặc mật khẩu không chính xác.";
				return View("Dangnhap");
			}
		}
		public ActionResult DangKy()
		{
			return View();
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult DangKy(Customer_63131631 customer)
		{
			var cusIMG = Request.Files["Avatar"];
			if (cusIMG != null && cusIMG.ContentLength > 0)
			{
				// Tệp đã được tải lên
				string postedFileName = Path.GetFileName(cusIMG.FileName);
				var path = Server.MapPath("/Images/");
				cusIMG.SaveAs(Path.Combine(path, postedFileName));
				customer.cusIMG = "/Images/" + postedFileName;

			}
			else
			{
				// Không có tệp tải lên, sử dụng giá trị mặc định
				customer.cusIMG = Url.Content("~/Images/user.png");
			}
			if (ModelState.IsValid)
			{
				// Check if the email is already registered
				if (db.Customers.Any(c => c.cusEmail == customer.cusEmail))
				{
					ViewBag.Fail = "Địa chỉ email đã được sử dụng. Vui lòng chọn một địa chỉ email khác.";
					return View(customer);
				}

				// Tạo giá trị cusID ngẫu nhiên
				string newCusID = GenerateRandomNumericID();

				// Kiểm tra xem giá trị cusID có tồn tại trong cơ sở dữ liệu chưa
				while (db.Customers.Any(c => c.cusID == newCusID))
				{
					newCusID = GenerateRandomNumericID(); // Nếu đã tồn tại, tạo lại giá trị ngẫu nhiên
				}
				customer.cusID = newCusID;
				//customer.cusIMG = postedFileName;
				db.Customers.Add(customer);
				db.SaveChanges();
				return RedirectToAction("dangnhap", "Account_63131631");
			}

			return View(customer);
		}


		private string GenerateRandomNumericID()
		{
			// Tạo một chuỗi ngẫu nhiên chỉ gồm số có độ dài là 4
			Random random = new Random();
			int randomNumber = random.Next(10000); // Số ngẫu nhiên từ 0 đến 9999
			return randomNumber.ToString("D4"); // Format số thành chuỗi với độ dài là 4
		}


		public ActionResult Details()
		{
			// Lấy thông tin khách hàng từ session
			Customer_63131631 customer = Session["use"] as Customer_63131631;

			if (customer != null)
			{
				return View(customer);
			}
			else
			{
				return RedirectToAction("DangNhap", "Account_63131631");
			}
		}
		public ActionResult Edit(string id)
		{
			// Lấy thông tin khách hàng từ session
			if (Session["use"] == null)
			{
				Session["use"] = null;
				return RedirectToAction("DangNhap", "Account_63131631");
			}
			var model = db.Customers.SingleOrDefault(c => c.cusID.Equals(id));

			if (model == null)
			{
				// Handle the case where the customer is not found
				return HttpNotFound();
			}

			return View(model);
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit(Customer_63131631 editedCustomer, HttpPostedFileBase Avatar)
		{
			if (ModelState.IsValid)
			{
				// Kiểm tra xem email đã được sử dụng bởi một khách hàng khác chưa

				var existingCustomerWithSameEmail = db.Customers.FirstOrDefault(c => c.cusID != editedCustomer.cusID && c.cusEmail == editedCustomer.cusEmail);

				if (existingCustomerWithSameEmail != null)
				{
					ModelState.AddModelError("cusEmail", "Email đã được sử dụng bởi một Tài Khoản khác.");
					return View(editedCustomer);
				}
				// Update thông tin của khách hàng trong cơ sở dữ liệu
				Customer_63131631 existingCustomer = db.Customers.Find(editedCustomer.cusID);


				if (existingCustomer != null)
				{
					existingCustomer.cusEmail = editedCustomer.cusEmail;
					//existingCustomer.cusPass = editedCustomer.cusPass;
					existingCustomer.cusFullName = editedCustomer.cusFullName;
					existingCustomer.cusPhone = editedCustomer.cusPhone;
					existingCustomer.cusAddress = editedCustomer.cusAddress;
					existingCustomer.GioiTinh = editedCustomer.GioiTinh;


					if (Avatar != null && Avatar.ContentLength > 0)
					{
						// Lưu ảnh mới vào thư mục và cập nhật đường dẫn trong đối tượng Customer
						string fileName = Path.GetFileName(Avatar.FileName);
						string path = Path.Combine(Server.MapPath("~/Images/"), fileName);
						Avatar.SaveAs(path);

						existingCustomer.cusIMG = "/Images/" + fileName;
					}
					// Add similar checks for other properties you want to update

					db.Entry(existingCustomer).State = EntityState.Modified;
					db.SaveChanges();

					// Update thông tin trong session

					Session["use"] = existingCustomer;
					Session["UserName"] = existingCustomer.cusFullName;
					Session["UserPhone"] = existingCustomer.cusPhone;
					Session["UserAddress"] = existingCustomer.cusAddress;
					Session["UserEmail"] = existingCustomer.cusEmail;
					return RedirectToAction("Details", "Account_63131631");
				}
				else
				{
					// Không tìm thấy khách hàng cần sửa
					return HttpNotFound();
				}
			}
			return View(editedCustomer);
		}
		public ActionResult Logout()
		{
			Session["use"] = null;
			return RedirectToAction("index", "home_63131631");
		}


	}
}