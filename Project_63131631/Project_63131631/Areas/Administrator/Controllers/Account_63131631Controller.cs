using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project_63131631.Areas.Administrator.Controllers
{
	public class Account_63131631Controller : Controller
	{
		Models.AdminContext_63131631 dbLog = new Models.AdminContext_63131631();
		public ActionResult Login()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken()]
		public ActionResult Login(Models.Administrator_63131631 adLogin)
		{
			try
			{
				var model = dbLog.Administrators.SingleOrDefault(a => a.adAcc.Equals(adLogin.adAcc));
				if (model != null)
				{
					// Kiểm tra mật khẩu
					if (model.adPass == adLogin.adPass)
					{
						Session["accname"] = model.adAcc;
						return RedirectToAction("Index", "Home_63131631");
					}
					else
					{
						Session["accname"] = null;
						ViewBag.LoginError = "Sai tài khoản hoặc mật khẩu.";
					}
				}
				else
				{
					Session["accname"] = null;
					ViewBag.LoginError = "Sai tài khoản hoặc mật khẩu.";
				}
			}
			catch (Exception)
			{
				Session["accname"] = null;
				ViewBag.LoginError = "Sai tài khoản hoặc mật khẩu.";
			}
			return View();
		}

		public ActionResult Logout()
		{
			System.Web.Security.FormsAuthentication.SignOut();
			Session["accname"] = null;
			return RedirectToAction("Login", "Account_63131631");
		}
		
	}
}