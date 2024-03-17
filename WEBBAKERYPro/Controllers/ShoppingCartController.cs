using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WEBBAKERYPro.Models;

namespace WEBBAKERYPro.Controllers
{
    public class ShoppingCartController : Controller
    {
        List<MatHangMua> cart = ShoppingCart.getCart();
        bakeryEntities database = new bakeryEntities();
        public ActionResult ThemSanPhamVaoGio(string masp)
        {
            MatHangMua sanpham = cart.FirstOrDefault(s => s.MaBanh == masp);
            if (sanpham == null)
            {
                sanpham = new MatHangMua(masp);
                cart.Add(sanpham);
            }
            else
            {
                sanpham.SoLuong++;
            }
            return Redirect(Request.UrlReferrer.ToString()); // Reload lai trang ma user dang su dung
        }
        public ActionResult CapNhatMatHang(string MaSP, int soluong)
        {
            var sanpham = cart.FirstOrDefault(s => s.MaBanh == MaSP);
            if (sanpham != null)
            {
                sanpham.SoLuong = soluong;
            }
            return RedirectToAction("Index");
        }
        public int TingTongSoLuong()
        {
            int tongSL = 0;
            if (cart != null)
                tongSL = cart.Sum(sp => sp.SoLuong);
            return tongSL;
        }
        public double TingTongTien()
        {
            double TongTien = 0; ;
            if (cart != null)
                TongTien = cart.Sum(sp => sp.ThanhTien());
            return TongTien;
        }
        public ActionResult XoaMatHang(string MaSP)
        {
            var sanpham = cart.FirstOrDefault(s => s.MaBanh == MaSP);
            if (sanpham != null)
            {
                cart.RemoveAll(s => s.MaBanh == MaSP);
                return RedirectToAction("Index");
            }
            if (cart.Count == 0)
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
        public ActionResult Index()
        {
            if (cart == null || cart.Count == 0)
            {
                ViewBag.CheckGioHang = cart.Count;
                return View("Index");
            }
            ViewBag.TongSL = TingTongSoLuong();
            ViewBag.TongTien = TingTongTien();
            return View(cart);
        }
        public ActionResult GioHangPartial()
        {
            ViewBag.TongSL = TingTongSoLuong();
            ViewBag.TongTien = TingTongTien();
            return PartialView();
        }
        public ActionResult DatHang()
        {
            if (Session["TaiKhoan"] == null)
            {
                Session["DangNhap"] = "chuadangnhap";
                return RedirectToAction("Index");
            }
            ViewBag.TongSL = TingTongSoLuong();
            ViewBag.TongTien = TingTongTien();
            return View(cart);
        }

        public ActionResult DatHangThanhCong()
        {
            return View();
        }
        public ActionResult Xoass()
        {
            Session.Remove("DangNhap");
            return RedirectToAction(Request.UrlReferrer?.ToString());
        }
        public ActionResult DongYDatHang()
        {
            KHACHHANG kHACHHANG = Session["TaiKhoan"] as KHACHHANG;
            List<MatHangMua> giohang = cart;

            DONHANG donhang = new DONHANG();
            donhang.MaKH = kHACHHANG.MaKH;
            donhang.NgayDat = DateTime.Now;
            donhang.TongGia = (int)TingTongTien();
            donhang.MaHT = 1;
            donhang.TenNN = kHACHHANG.HoTen;
            donhang.DiaChiNhanHang = kHACHHANG.DiaChi;
            donhang.SDT = kHACHHANG.SDT;
            donhang.MaTT = 1;
            database.DONHANGs.Add(donhang);
            database.SaveChanges();

            foreach (var sanpham in giohang)
            {
                CHITIETDONHANG chitiet = new CHITIETDONHANG();
                chitiet.MaDH = donhang.MaDH;
                chitiet.MaSP = sanpham.MaBanh;
                chitiet.SoLuong = sanpham.SoLuong;
                chitiet.ThanhTien = (int)sanpham.GiaBanh;
                database.CHITIETDONHANGs.Add(chitiet);

            }
            database.SaveChanges();
            Session["GioHang"] = null;
            return RedirectToAction("DatHangThanhCong");
        }
    }
}