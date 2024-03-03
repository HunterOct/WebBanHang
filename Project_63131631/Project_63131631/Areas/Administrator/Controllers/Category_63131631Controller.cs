using PagedList;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project_63131631.Areas.Administrator.Controllers
{
    public class Category_63131631Controller : Controller
    {
        Models.AdminContext_63131631 dbCate = new Models.AdminContext_63131631();
        //
        // GET: /Administrator/Category/
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
				ViewBag.CateError = error;

				var pageNumber = page ?? 1; // Trang mặc định nếu không có trang được chọn
				var pageSize = 5; // Số lượng dòng trên mỗi trang
				var model = dbCate.Categories.ToList();
				// Sử dụng ToPagedList để tạo danh sách phân trang
				var pagedModel = model.ToPagedList(pageNumber, pageSize);

				var modelCate = dbCate.Categories.OrderBy(c => c.cateID).ToList();

				return View(pagedModel);
			}
		}

		[HandleError]
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

        [HandleError]
        [HttpPost]
        public ActionResult Create(Models.Category_63131631 createCate, HttpPostedFileBase file)
        {
            if (Session["accname"] == null)
            {
                Session["accname"] = null;
                return RedirectToAction("Login", "Account_63131631");
            }
            else
            {
                if (file != null)
                {
                    if (file.ContentLength > 0)
                    {
                        try
                        {
                            string nameFile = Path.GetFileName(file.FileName);
                            file.SaveAs(Path.Combine(Server.MapPath("/Images"), nameFile));
                            createCate.catePhoto = "/Images/" + nameFile;
                        }
                        catch (Exception)
                        {
                            ViewBag.CreateCategory = "Không thể chọn ảnh.";
                        }
                    }
                    try
                    {
                        if (dbCate.Categories.SingleOrDefault(cr => cr.cateName.Equals(createCate.cateName)) == null)
                        {
                            dbCate.Categories.Add(createCate);
                            dbCate.SaveChanges();
                            ViewBag.CreateCategory = "Thêm danh mục thành công.";
							return RedirectToAction("Index", "Category_63131631");

						}
						else
                        {
                            ViewBag.CreateCategory = "Tên danh mục đã tồn tại.";
                        }
                    }
                    catch (Exception)
                    {
                        ViewBag.CreateCategory = "Không thể thêm danh mục.";
                    }
                }
                else
                {
                    ViewBag.HinhAnh = "Vui lòng chọn hình ảnh.";
                }
                return View();
            }
        }

        [HandleError]
        public ActionResult Edit(int id)
        {
            if (Session["accname"] == null)
            {
                Session["accname"] = null;
                return RedirectToAction("Login", "Account_63131631");
            }
            else
            {
                var model = dbCate.Categories.SingleOrDefault(c => c.cateID.Equals(id));
                return View(model);
            }
        }

        [HandleError]
        [HttpPost]
        public ActionResult Edit(Models.Category_63131631 editCate, HttpPostedFileBase file)
        {
            if (Session["accname"] == null)
            {
                Session["accname"] = null;
                return RedirectToAction("Login", "Account_63131631");
            }
            else
            {
                if (file != null)
                {
                    if (file.ContentLength > 0)
                    {
                        try
                        {
                            string nameFile = Path.GetFileName(file.FileName);
                            file.SaveAs(Path.Combine(Server.MapPath("/Images"), nameFile));
                            editCate.catePhoto = "/Images/" + nameFile;
                        }
                        catch (Exception)
                        {
                            ViewBag.EditCategory = "Không thể chọn ảnh.";
                        }
                    }
                }
                try
                {
                    if (ModelState.IsValid)
                    {

                            dbCate.Entry(editCate).State = System.Data.Entity.EntityState.Modified;
                            dbCate.SaveChanges();
                            ViewBag.EditCategory = "Cập nhật danh mục thành công.";
						    return RedirectToAction("Index", "Category_63131631");
					}
                }
                catch (Exception)
                {
                    ViewBag.EditCategory = "Không thể cập nhật danh mục.";
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
				try
				{
					// Xóa dữ liệu liên quan trong bảng ProductTypes
					var productTypesToDelete = dbCate.ProductTypes.Where(pt => pt.cateID == id).ToList();
					foreach (var productType in productTypesToDelete)
					{
						// Xóa dữ liệu liên quan trong bảng Products
						var productsToDelete = dbCate.Products.Where(p => p.typeID == productType.typeID).ToList();
						dbCate.Products.RemoveRange(productsToDelete);
					}
					dbCate.ProductTypes.RemoveRange(productTypesToDelete);

					// Xóa dữ liệu liên quan trong bảng Categories
					var categoryToDelete = dbCate.Categories.SingleOrDefault(c => c.cateID == id);
					if (categoryToDelete != null)
					{
						dbCate.Categories.Remove(categoryToDelete);
						dbCate.SaveChanges();
						return RedirectToAction("Index", "Category_63131631", new { error = "Xoá danh mục thành công." });
					}
					else
					{
						return RedirectToAction("Index", "Category_63131631", new { error = "Danh mục không tồn tại." });
					}
				}
				catch (Exception)
				{
					return RedirectToAction("Index", "Category_63131631", new { error = "Không thể xoá danh mục." });
				}
			}
		}
	}
}