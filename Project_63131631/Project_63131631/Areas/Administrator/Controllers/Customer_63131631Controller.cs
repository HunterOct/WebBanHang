using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using Project_63131631.Areas.Administrator.Models;

namespace Project_63131631.Areas.Administrator.Controllers
{
	public class Customer_63131631Controller : Controller
	{
		Models.AdminContext_63131631 dbCus = new Models.AdminContext_63131631();

		[HandleError]
		public ActionResult Index(string name, string error, int? page)
		{
			if (Session["accname"] == null)
			{
				Session["accname"] = null;
				return RedirectToAction("Login", "Account_63131631");
			}
			else
			{
				ViewBag.CusError = error;

				var pageNumber = page ?? 1; // Trang mặc định nếu không có trang được chọn
				var pageSize = 5; // Số lượng dòng trên mỗi trang

				var model = dbCus.Customers.ToList();

				// Lọc theo tên nếu có
				if (!string.IsNullOrEmpty(name))
				{
					model = model.Where(p => p.cusEmail.Contains(name)).ToList();
				}

				// Sắp xếp theo cusID (hoặc theo trường khác nếu cần)
				model = model.OrderBy(p => p.cusID).ToList();

				// Phân trang
				var pagedModel = model.ToPagedList(pageNumber, pageSize);

				return View(pagedModel);
			}
		}
		public ActionResult Create()
		{
			if (Session["accname"] == null)
			{
				Session["accname"] = null;
				return RedirectToAction("Login", "Account_63131631");
			}
			else
			{
				return View();
			}
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create(Customer_63131631 customer)
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
				if (dbCus.Customers.Any(c => c.cusEmail == customer.cusEmail))
				{
					ViewBag.Fail = "Địa chỉ email đã được sử dụng. Vui lòng chọn một địa chỉ email khác.";
					return View(customer);
				}
				if (customer.cusPass.Length < 8 || !customer.cusPass.Any(char.IsUpper) || !customer.cusPass.Any(char.IsDigit) || !customer.cusPass.Any(c => !char.IsLetterOrDigit(c)))
				{
					ModelState.AddModelError("cusPass", "Mật khẩu phải có ít nhất 8 ký tự, 1 chữ hoa, 1 số, và 1 ký tự đặc biệt.");
					return View(customer);
				}


				// Tạo giá trị cusID ngẫu nhiên
				string newCusID = GenerateRandomNumericID();

				// Kiểm tra xem giá trị cusID có tồn tại trong cơ sở dữ liệu chưa
				while (dbCus.Customers.Any(c => c.cusID == newCusID))
				{
					newCusID = GenerateRandomNumericID(); // Nếu đã tồn tại, tạo lại giá trị ngẫu nhiên
				}
				customer.cusID = newCusID;
				//customer.cusIMG = postedFileName;
				dbCus.Customers.Add(customer);
				dbCus.SaveChanges();
				return RedirectToAction("Index", "Customer_63131631");
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
			Customer_63131631 customer = Session["accname"] as Customer_63131631;

			if (customer != null)
			{
				return View(customer);
			}
			else
			{
				// Không có thông tin khách hàng, bạn có thể xử lý tùy thuộc vào yêu cầu của bạn,
				// ví dụ chuyển hướng đến trang đăng nhập hoặc trang chính
				return RedirectToAction("DangNhap", "Account_63131631");
			}
		}
		[HandleError]
		public ActionResult Delete(string id)
		{
			var customer = dbCus.Customers.Find(id);

			if (customer != null)
			{
				// Lấy danh sách các đơn hàng của khách hàng
				var orders = dbCus.Orders.Where(o => o.cusID == id).ToList();

				// Duyệt qua từng đơn hàng
				foreach (var order in orders)
				{
					// Lấy chi tiết đơn hàng 
					var details = dbCus.OrderDetails
									.Where(d => d.orderID == order.orderID)
									.ToList();

					// Xóa chi tiết đơn hàng      
					foreach (var detail in details)
					{
						dbCus.OrderDetails.Remove(detail);
					}

					// Xóa đơn hàng
					dbCus.Orders.Remove(order);
				}

				// Xóa khách hàng 
				dbCus.Customers.Remove(customer);

				// Lưu thay đổi
				dbCus.SaveChanges();

				TempData["Message"] = "Xóa thành công";

				return RedirectToAction("Index");
			}

			return View(customer);
		}
		public ActionResult Edit(string id)
		{
			// Lấy thông tin khách hàng từ session
			if (Session["accname"] == null)
			{
				Session["accname"] = null;
				return RedirectToAction("Login", "Account_63131631");
			}
			var model = dbCus.Customers.SingleOrDefault(c => c.cusID.Equals(id));

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

				var existingCustomerWithSameEmail = dbCus.Customers.FirstOrDefault(c => c.cusID != editedCustomer.cusID && c.cusEmail == editedCustomer.cusEmail);

				if (existingCustomerWithSameEmail != null)
				{
					ModelState.AddModelError("cusEmail", "Email đã được sử dụng bởi một Tài Khoản khác.");
					return View(editedCustomer);
				}
				// Update thông tin của khách hàng trong cơ sở dữ liệu
				Customer_63131631 existingCustomer = dbCus.Customers.Find(editedCustomer.cusID);


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

					dbCus.Entry(existingCustomer).State = EntityState.Modified;
					dbCus.SaveChanges();

					// Update thông tin trong session
					Session["accname"] = existingCustomer;

					return RedirectToAction("Index", "Customer_63131631");	
				}
				else
				{
					// Không tìm thấy khách hàng cần sửa
					return HttpNotFound();
				}
			}
			return View(editedCustomer);
		}
	}
}
