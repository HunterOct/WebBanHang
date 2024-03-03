using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using PagedList;

namespace Project_63131631.Areas.Administrator.Controllers
{
    public class HoaDon_63131631Controller : Controller
    {
		Models.AdminContext_63131631 db = new Models.AdminContext_63131631();

		// GET: Administrator/HoaDon
		[HandleError]
		public ActionResult Index(string error, string id, int? page)
        {
			if (Session["accname"] == null)
			{
				Session["accname"] = null;
				return RedirectToAction("Login", "Account_63131631");
			}
			else
			{
				ViewBag.ProError = error;

				var pageNumber = page ?? 1; // Trang mặc định nếu không có trang được chọn
				var pageSize = 7; // Số lượng dòng trên mỗi trang
								  // Sử dụng ToPagedList để tạo danh sách phân trang
				var model = db.Orders.ToList();
				var pagedModel = model.ToPagedList(pageNumber, pageSize);
				var modelCate = db.Orders.OrderBy(c => c.orderID.Contains(id)).ToList();
				
				return View(pagedModel);
			}
		}
		[HandleError]
		public ActionResult HoadonChiTiet(string error, string id)
		{
			if (Session["accname"] == null)
			{
				Session["accname"] = null;
				return RedirectToAction("Login", "Account_63131631");
			}
			else
			{
				ViewBag.ProError = error;

				// Lấy danh sách các mặt hàng từ OrderDetails tương ứng với orderID
				var modelCate = db.OrderDetails.Where(c => c.orderID == id).ToList();

				return View(modelCate);
			}
		}
		public ActionResult Delete(string id)
		{
			// Lấy thông tin đơn hàng cần xóa
			var order = db.Orders.Find(id);

			if (order != null)
			{
				// Xóa chi tiết đơn hàng liên quan
				var orderDetails = db.OrderDetails.Where(x => x.orderID == id).ToList();

				// Xóa từng chi tiết đơn hàng
				foreach (var detail in orderDetails)
				{
					db.OrderDetails.Remove(detail);
				}

				// Xóa đơn hàng
				db.Orders.Remove(order);

				// Lưu thay đổi
				db.SaveChanges();

				// Trả về thông báo
				TempData["Message"] = "Xóa đơn hàng thành công";
			}

			return RedirectToAction("Index");
		}
	}
}