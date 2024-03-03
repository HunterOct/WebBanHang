using PagedList.Mvc;
using PagedList;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;

namespace Project_63131631.Areas.Administrator.Controllers
{
    public class Producer_63131631Controller : Controller
    {
        Models.AdminContext_63131631 dbPdc = new Models.AdminContext_63131631();
        //
        // GET: /Administrator/Producer/
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
				var pageNumber = page ?? 1; // Trang mặc định nếu không có trang được chọn
				var pageSize = 5; // Số lượng dòng trên mỗi trang
								  // Sử dụng ToPagedList để tạo danh sách phân trang
				var model = dbPdc.Producers.ToList();
				var pagedModel = model.ToPagedList(pageNumber, pageSize);
				var modelType = dbPdc.Producers.OrderBy(t => t.pdcID).ToList();
				ViewBag.PdcError = error;

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
                return View();
            }
        }

        [HandleError]
        [HttpPost]
        public ActionResult Create(Models.Producer_63131631 createPdc, HttpPostedFileBase file)
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
                            createPdc.pdcPhoto = "/Images/" + nameFile;
                        }
                        catch (Exception)
                        {
                            ViewBag.CreatePdcError = "Không thể chọn ảnh.";
                        }
                    }
                    var pdc = dbPdc.Producers.SingleOrDefault(c => c.pdcName.Equals(createPdc.pdcName));
                    try
                    {
                        if (ModelState.IsValid)
                        {
                            if (pdc != null)
                            {
                                ViewBag.CreatePdcError = "Hãng sản xuất đã tồn tại.";
                            }
                            else
                            {
                                dbPdc.Producers.Add(createPdc);
                                dbPdc.SaveChanges();
                                ViewBag.CreatePdcError = "Thêm hãng sản xuất thành công.";
								return RedirectToAction("Index", "Producer_63131631");

							}
						}
                    }
                    catch (Exception)
                    {
                        ViewBag.CreatePdcError = "Không thể thêm hãng sản xuất.";
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
        public ActionResult Edit(int id)
        {
            if (Session["accname"] == null)
            {
                Session["accname"] = null;
                return RedirectToAction("Login", "Account_63131631");
            }
            else
            {
                return View(dbPdc.Producers.SingleOrDefault(e => e.pdcID.Equals(id)));
            }
        }

        [HandleError]
        [HttpPost]
        public ActionResult Edit(Models.Producer_63131631 editPdc, HttpPostedFileBase file)
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
                            file.SaveAs(Path.Combine(Server.MapPath("/Image"), nameFile));
                            editPdc.pdcPhoto = "/Images/" + nameFile;
                        }
                        catch (Exception)
                        {
                            ViewBag.EditPdcError = "Không thể chọn ảnh.";
                        }
                    }
                }
                try
                {
                    if (ModelState.IsValid)
                    {
                        dbPdc.Entry(editPdc).State = System.Data.Entity.EntityState.Modified;
                        dbPdc.SaveChanges();
                        ViewBag.EditPdcError = "Cập nhật hãng sản xuất thành công.";
						return RedirectToAction("Index");

					}
				}
                catch (Exception)
                {
                    ViewBag.EditPdcError = "Không thể cập nhật hãng sản xuất.";
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

				var producer = dbPdc.Producers.Find(id);

				if (producer != null)
				{
					// Lấy danh sách các sản phẩm liên kết 
					var products = dbPdc.Products.Where(p => p.pdcID == id).ToList();

					// Xóa các sản phẩm liên kết
					foreach (var product in products)
					{
						dbPdc.Products.Remove(product);
					}

					// Xóa hãng sản xuất
					dbPdc.Producers.Remove(producer);

					dbPdc.SaveChanges();

					return RedirectToAction("Index");
				}

				return View();
			}
        }
    }
}