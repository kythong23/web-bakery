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
        bakeryEntities database = new bakeryEntities();
        public List<MatHangMua> LayGioHang() 
        {
            List<MatHangMua> gioHang = Session["GioHang"] as List<MatHangMua>;        
            if (gioHang == null)
            {
                gioHang = new List<MatHangMua>();
                Session["GioHang"] = gioHang;
            }
            return gioHang;
        }
        public ActionResult ThemSanPhamVaoGio(string masp)
        {
            List<MatHangMua> giohang = LayGioHang();
            MatHangMua sanpham = giohang.FirstOrDefault(s=> s.MaBanh == masp );
            if (sanpham == null)
            {
                sanpham = new MatHangMua(masp);
                giohang.Add(sanpham);
            }
            else
            {
                sanpham.SoLuong++;
            }
            return Redirect(Request.UrlReferrer.ToString()); // Reload lai trang ma user dang su dung
         //   return RedirectToAction("Details", "Home", new { id = masp });
        }
        public ActionResult CapNhatMatHang(string MaSP,int soluong)
        {
            List<MatHangMua> gioHang = LayGioHang();
            var sanpham = gioHang.FirstOrDefault(s => s.MaBanh == MaSP);
            if (sanpham != null)
            {
                sanpham.SoLuong = soluong;
            }
            return RedirectToAction("Index");
        }
        public int TingTongSL()
        {
            int tongSL = 0;
            List<MatHangMua> gioHang = LayGioHang();
            if (gioHang != null)
                tongSL = gioHang.Sum(sp => sp.SoLuong);
            return tongSL;
        }
        public double TingTongTien()
        {
            double TongTien = 0;
            List<MatHangMua> gioHang = LayGioHang();
            if (gioHang != null)
                TongTien = gioHang.Sum(sp => sp.ThanhTien());
            return TongTien;
        }
        public ActionResult XoaMatHang(string MaSP) {
            List<MatHangMua> gioHang = LayGioHang();
            var sanpham = gioHang.FirstOrDefault(s => s.MaBanh == MaSP);
            if(sanpham != null)
            {
                gioHang.RemoveAll(s => s.MaBanh == MaSP);
                return RedirectToAction("Index");
            }
            if(gioHang.Count == 0)
            {
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("Index");
        }
        public ActionResult Index()
        {
            List<MatHangMua> gioHang = LayGioHang();
            if(gioHang == null || gioHang.Count == 0)
            {
                return RedirectToAction("Index","Home");
            }
            ViewBag.TongSL = TingTongSL();
            ViewBag.TongTien = TingTongTien();
            return View(gioHang);
        }
        public ActionResult GioHangPartial()
        {
            ViewBag.TongSL = TingTongSL();
            ViewBag.TongTien = TingTongTien();
            return PartialView();
        }
        public ActionResult DatHang()
        {
            if (Session["TaiKhoan"] == null)
            {
                Session["DangNhap"] = "chuadangnhap" ;
                return RedirectToAction("Index");
            }
            List<MatHangMua> gioHang = LayGioHang();
            ViewBag.TongSL = TingTongSL();
            ViewBag.TongTien = TingTongTien();
            return View(gioHang);
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
            List<MatHangMua> giohang = LayGioHang();

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