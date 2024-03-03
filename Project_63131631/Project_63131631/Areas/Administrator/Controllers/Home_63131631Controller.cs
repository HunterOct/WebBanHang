using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;
using PagedList;

namespace Project_63131631.Areas.Administrator.Controllers
{	public class Home_63131631Controller : Controller
	{
		Models.AdminContext_63131631 db = new Models.AdminContext_63131631();

		[HandleError]
		public ActionResult Index(int? page)
		{
			if (Session["accname"] == null)
			{
				Session["accname"] = null;
				return RedirectToAction("Login", "Account_63131631");
			}
			else
			{

				// Lấy tổng số lượng đơn hàng
				var totalOrders = db.Orders.Count();
				ViewBag.TotalOrders = totalOrders;

				var pageNumber = page ?? 1; // Trang mặc định nếu không có trang được chọn
				var pageSize = 5; // Số lượng dòng trên mỗi trang

				var model = db.Orders.OrderByDescending(o => o.orderDateTime).Take(5).ToList();
				// Phân trang
				var pagedModel = model.ToPagedList(pageNumber, pageSize);
				IPagedList<Models.Order_63131631> pagedList = model.ToPagedList(pageNumber, pageSize);


				return View(pagedModel);
			}
		}
	}

}