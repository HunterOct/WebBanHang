using Project_63131631.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project_63131631.Controllers
{
    public class GioHang_63131631Controller : Controller
    {
        UserContext_63131631 db = new UserContext_63131631();
        // GET: Shopper/GioHang
        public ActionResult Index()
        {
            List<CartItem_63131631> giohang = Session["giohang"] as List<CartItem_63131631>;
            return View(giohang);

        }
        public ActionResult ThemVaoGio(string SanPhamID)
        {
            if (Session["giohang"] == null) // Nếu giỏ hàng chưa được khởi tạo
            {
                Session["giohang"] = new List<CartItem_63131631>();  // Khởi tạo Session["giohang"] là 1 List<CartItem>
            }

            List<CartItem_63131631> giohang = Session["giohang"] as List<CartItem_63131631>;  // Gán qua biến giohang dễ code

            // Kiểm tra xem sản phẩm khách đang chọn đã có trong giỏ hàng chưa

            if (giohang.FirstOrDefault(m => m.SanPhamID == SanPhamID) == null) // ko co sp nay trong gio hang
            {
                Models.Product_63131631 sp = db.Products.Find(SanPhamID);  // tim sp theo sanPhamID

                CartItem_63131631 newItem = new CartItem_63131631()
                {
                    SanPhamID = SanPhamID,
                    TenSanPham = sp.proName,
                    SoLuong = 1,
                    Hinh = sp.proPhoto,
                    DonGia = (Int32.Parse(sp.proPrice) - (Int32.Parse(sp.proPrice) * sp.proDiscount)/100).ToString()

                };  // Tạo ra 1 CartItem mới

                giohang.Add(newItem);  // Thêm CartItem vào giỏ 
            }
            else
            {
                // Nếu sản phẩm khách chọn đã có trong giỏ hàng thì không thêm vào giỏ nữa mà tăng số lượng lên.
                CartItem_63131631 cardItem = giohang.FirstOrDefault(m => m.SanPhamID == SanPhamID);
                cardItem.SoLuong++;
            }

            // Action này sẽ chuyển hướng về trang chi tiết sp khi khách hàng đặt vào giỏ thành công. Bạn có thể chuyển về chính trang khách hàng vừa đứng bằng lệnh return Redirect(Request.UrlReferrer.ToString()); nếu muốn.
            return Redirect(Request.UrlReferrer.ToString());
        }
        //Sửa số lượng

        public ActionResult SuaSoLuong(string SanPhamID, int soluongmoi)
        {
            // tìm carditem muon sua
            List<CartItem_63131631> giohang = Session["giohang"] as List<CartItem_63131631>;
            CartItem_63131631 itemSua = giohang.FirstOrDefault(m => m.SanPhamID.Equals(SanPhamID));
            if (itemSua != null)
            {
                if (soluongmoi < 1 || soluongmoi > 100)
                {

                }
                else
                {
                    @ViewBag.GioError = "";
                    itemSua.SoLuong = soluongmoi;
                }
            }
            return RedirectToAction("Index");

        }
        //Xoá khỏi giỏ
        public ActionResult XoaKhoiGio(string SanPhamID)
        {
            List<CartItem_63131631> giohang = Session["giohang"] as List<CartItem_63131631>;
            CartItem_63131631 itemXoa = giohang.FirstOrDefault(m => m.SanPhamID.Equals(SanPhamID));
            if (itemXoa != null)
            {
                giohang.Remove(itemXoa);
            }
            return RedirectToAction("Index");
        }
    }
}