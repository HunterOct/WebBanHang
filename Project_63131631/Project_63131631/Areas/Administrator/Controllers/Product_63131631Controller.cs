using PagedList;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project_63131631.Areas.Administrator.Controllers
{
    public class Product_63131631Controller : Controller
    {
        Models.AdminContext_63131631 dbPro = new Models.AdminContext_63131631();
        Project_63131631.Repository.ShopDAO shopDAO = new Repository.ShopDAO();
        //
        // GET: /Administrator/Product/
        [HandleError]
		public ActionResult Index(string error, string name, int? page)
		{
			if (Session["accname"] == null)
			{
				Session["accname"] = null;
				return RedirectToAction("Login", "Account_63131631");
			}
			else
			{
				ViewBag.ProError = error;
				var pageNumber = page ?? 1;
				var pageSize = 5;
				// Sử dụng ToPagedList trên model đã lọc

				var model = dbPro.Products.ToList();
				if (!string.IsNullOrEmpty(name))
				{
					model = model.Where(p => p.proName.Contains(name)).ToList();
				}

				var pagedModel = model.ToPagedList(pageNumber, pageSize);

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
                ViewBag.pdcListCreate = new SelectList(dbPro.Producers, "pdcID", "pdcName");
                ViewBag.typeListCreate = new SelectList(dbPro.ProductTypes, "typeID", "typeName");
                return View();
            }
        }

        [HandleError]
        [HttpPost]
        public ActionResult Create(Models.Product_63131631 createPro, HttpPostedFileBase file)
        {
            if (Session["accname"] == null)
            {
                Session["accname"] = null;
                return RedirectToAction("Login", "Account_63131631");
            }
            else
            {
                ViewBag.pdcListCreate = new SelectList(dbPro.Producers, "pdcID", "pdcName");
                ViewBag.typeListCreate = new SelectList(dbPro.ProductTypes, "typeID", "typeName");
                var pro = dbPro.Products.SingleOrDefault(c => c.proID.Equals(createPro.proID));
                if (file != null)
                {
                    if (file.ContentLength > 0)
                    {
                        try
                        {
                            string nameFile = Path.GetFileName(file.FileName);
                            file.SaveAs(Path.Combine(Server.MapPath("/Images"), nameFile));
                            createPro.proPhoto = "/Images/" + nameFile;
                        }
                        catch (Exception)
                        {
                            ViewBag.CreateProError = "Không thể chọn ảnh.";
                        }
                    }
                    createPro.proUpdateDate = DateTime.Now.ToString();
                    try
                    {
                        if (pro != null)
                        {
                            ViewBag.CreateProError = "Mã sản phẩm đã tồn tại.";
                        }
                        else
                        {
                            dbPro.Products.Add(createPro);
                            dbPro.SaveChanges();
                            ViewBag.CreateProError = "Thêm sản phẩm thành công.";
                            return RedirectToAction("Index", "Product_63131631");
                        }
                    }
                    catch (Exception)
                    {
                        ViewBag.CreateProError = "Không thể thêm sản phẩm.";
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
        [HttpGet]
        public ActionResult Edit(string id)
        {
            if (Session["accname"] == null)
            {
                Session["accname"] = null;
                return RedirectToAction("Login", "Account_63131631");
            }
            else
            {
                ViewBag.pdcListEdit = new SelectList(dbPro.Producers, "pdcID", "pdcName");
                ViewBag.typeListEdit = new SelectList(dbPro.ProductTypes, "typeID", "typeName");
                var model = dbPro.Products.SingleOrDefault(p => p.proID.Equals(id));
                return View(model);
            }
        }

        [HandleError]
        [HttpPost]
        public ActionResult Edit(Models.Product_63131631 editPro, HttpPostedFileBase file)
        {
            if (Session["accname"] == null)
            {
                Session["accname"] = null;
                return RedirectToAction("Login", "Account_63131631");
            }
            else
            {
                ViewBag.pdcListEdit = new SelectList(dbPro.Producers, "pdcID", "pdcName");
                ViewBag.typeListEdit = new SelectList(dbPro.ProductTypes, "typeID", "typeName");
                if (file != null && file.ContentLength > 0)
                {
                  try
                       {
                          string nameFile = Path.GetFileName(file.FileName);
						   string path = Path.Combine(Server.MapPath("~/Images/"), nameFile);
						    file.SaveAs(path);

						editPro.proPhoto = "/Images/" + nameFile;
                       }
                  catch (Exception)
                      {
                          ViewBag.EditProError = "Không thể chọn ảnh.";
                      }
                }
				editPro.proUpdateDate = DateTime.Now.ToString();
                try
                {
                    dbPro.Entry(editPro).State = System.Data.Entity.EntityState.Modified;
                    dbPro.SaveChanges();
                    ViewBag.EditProError = "Cập nhật sản phẩm thành công.";
                    return RedirectToAction("Index", "Product_63131631");

                }
                catch (Exception)
                {
                    ViewBag.EditProError = "Không thể cập nhật sản phẩm.";
                }
                return View();
            }
        }

        [HandleError]
        public ActionResult Delete(string id)
        {
            if (Session["accname"] == null)
            {
                Session["accname"] = null;
                return RedirectToAction("Login", "Account_63131631");
            }
            else
            {
                var model = dbPro.Products.SingleOrDefault(h => h.proID.Equals(id));
                try
                {
                    if (model != null)
                    {
                        dbPro.Products.Remove(model);
                        dbPro.SaveChanges();
                        return RedirectToAction("Index", "Product_63131631", new { error = "Xoá sản phẩm thành công." });
                    }
                    else
                    {
                        return RedirectToAction("Index", "Product_63131631", new { error = "Sản phẩm không tồn tại." });
                    }
                }
                catch (Exception)
                {
                    return RedirectToAction("Index", "Product_63131631", new { error = "Không thể xoá sản phẩm." });
                }
            }
        }

        public ActionResult Details(string id)
        {
            if (Session["accname"] == null)
            {
                Session["accname"] = null;
                return RedirectToAction("Login", "Account_63131631");
            }
            else
            {
                var model = dbPro.Products.SingleOrDefault(p => p.proID.Equals(id));
                return View(model);
            }
        }
    }
}