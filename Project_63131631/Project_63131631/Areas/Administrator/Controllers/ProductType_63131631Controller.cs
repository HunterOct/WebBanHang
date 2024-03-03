using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project_63131631.Areas.Administrator.Controllers
{
    public class ProductType_63131631Controller : Controller
    {
        Models.AdminContext_63131631 dbType = new Models.AdminContext_63131631();
        //
        // GET: /Administrator/ProductType/
        [HandleError]
		public ActionResult Index(string error, int? page)
		{
			if (Session["accname"] == null)
			{
				Session["accname"] = null;
				return RedirectToAction("Login", "Account_63131631");
			}
			else
			{
				ViewBag.TypeError = error;

				var pageNumber = page ?? 1;
				var model = dbType.ProductTypes.ToList();
				var pageSize = 8; // Số mục muốn hiển thị trên mỗi trang
				var pagedModel = model.ToPagedList(pageNumber, pageSize);

				var modelType = dbType.ProductTypes.OrderBy(t => t.typeID).ToList();

				// Sử dụng ToPagedList để tạo danh sách phân trang
				return View(pagedModel);
			}
		}

		[HandleError]
        [HttpGet]
        public ActionResult Create()
        {
            if (Session["accname"] == null)
            {
                Session["accname"] = null;
                return RedirectToAction("Login", "Account_63131631");
            }
            else
            {
                ViewBag.cateListCreate = new SelectList(dbType.Categories, "cateID", "cateName");
                return View();
            }
        }

        [HandleError]
        [HttpPost]
        public ActionResult Create(Models.ProductType_63131631 createType)
        {
            if (Session["accname"] == null)
            {
                Session["accname"] = null;
                return RedirectToAction("Login", "Account_63131631");
            }
            else
            {
                ViewBag.cateListCreate = new SelectList(dbType.Categories, "cateID", "cateName");
                try
                {
                    if (ModelState.IsValid)
                    {
                        dbType.ProductTypes.Add(createType);
                        dbType.SaveChanges();
                        ViewBag.CreateTypeError = "Thêm loại sản phẩm thành công.";
						return RedirectToAction("Index", "ProductType_63131631");

					}
				}
                catch (Exception)
                {
                    ViewBag.CreateTypeError = "Không thể thêm loại sản phẩm.";
                }
                return View();
            }
        }

        [HandleError]
        [HttpGet]
        public ActionResult Edit(int id)
        {
            if (Session["accname"] == null)
            {
                Session["accname"] = null;
                return RedirectToAction("Login", "Account_63131631");
            }
            else
            {
                ViewBag.cateListEdit = new SelectList(dbType.Categories, "cateID", "cateName");
                return View(dbType.ProductTypes.SingleOrDefault(e => e.typeID.Equals(id)));
            }
        }

        [HandleError]
        [HttpPost]
        public ActionResult Edit(Models.ProductType_63131631 editType)
        {
            if (Session["accname"] == null)
            {
                Session["accname"] = null;
                return RedirectToAction("Login", "Account_63131631");
            }
            else
            {
                ViewBag.cateListEdit = new SelectList(dbType.Categories, "cateID", "cateName");
                try
                {
                    if (ModelState.IsValid)
                    {
                        dbType.Entry(editType).State = System.Data.Entity.EntityState.Modified;
                        dbType.SaveChanges();
                        ViewBag.EditTypeError = "Cập nhật loại sản phẩm thành công.";
						return RedirectToAction("Index", "ProductType_63131631");
					}
				}
                catch (Exception)
                {
                    ViewBag.EditTypeError = "Không thể cập nhật loại sản phẩm.";
                }
                return View();
            }
        }

        [HandleError]
        public ActionResult Delete(int id)
        {
            if (Session["accname"] == null)
            {
                Session["accname"] = null;
                return RedirectToAction("Login", "Account_63131631");
            }
            else
            {
                var model = dbType.ProductTypes.SingleOrDefault(h => h.typeID.Equals(id));
                try
                {
                    if (model != null)
                    {
                        dbType.ProductTypes.Remove(model);
                        dbType.SaveChanges();
                        return RedirectToAction("Index", "ProductType_63131631", new { error = "Xoá loại sản phẩm thành công." });
                    }
                    else
                    {
                        return RedirectToAction("Index", "ProductType_63131631", new { error = "Loại sản phẩm không tồn tại." });
                    }
                }
                catch (Exception)
                {
                    return RedirectToAction("Index", "ProductType_63131631", new { error = "Không thể xoá loại sản phẩm." });
                }
            }
        }
    }
}