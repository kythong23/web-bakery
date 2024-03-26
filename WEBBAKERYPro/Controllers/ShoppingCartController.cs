using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WEBBAKERYPro.Decorater;
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
        [HttpPost]
        public ActionResult ThemSanPhamVaoGio(string MASP, bool[] selectedItems)
        {
            Session["sanphamkem"] = selectedItems;
            List<MatHangMua> giohang = LayGioHang();
            MatHangMua sanpham = giohang.FirstOrDefault(s => s.MaBanh == MASP);
            if (sanpham == null)
            {
                sanpham = new MatHangMua(MASP);
                giohang.Add(sanpham);
            }
            else
            {
                sanpham.SoLuong++;
            }
            return Redirect(Request.UrlReferrer.ToString()); // Reload lai trang ma user dang su dung
            //return RedirectToAction("Details", "Home", new { id = masp });
        }
        public ActionResult CapNhatMatHang(string MaSP, int soluong)
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
        public ActionResult XoaMatHang(string MaSP)
        {
            List<MatHangMua> gioHang = LayGioHang();
            var sanpham = gioHang.FirstOrDefault(s => s.MaBanh == MaSP);
            if (sanpham != null)
            {
                gioHang.RemoveAll(s => s.MaBanh == MaSP);
                return RedirectToAction("Index");
            }
            if (gioHang.Count == 0)
            {
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("Index");
        }
        public ActionResult Index()
        {
            List<MatHangMua> gioHang = LayGioHang();
            if (gioHang == null || gioHang.Count == 0)
            {
                return RedirectToAction("Index", "Home");
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
                Session["DangNhap"] = "chuadangnhap";
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
            ThemChiTietSanPham(donhang);
            Session["GioHang"] = null;
            return RedirectToAction("DatHangThanhCong");
        }
        public void ThemChiTietSanPham(DONHANG donhang)
        {
            List<int> arr = new List<int>();
            bool[] bools = Session["sanphamkem"] as bool[];
            List<MatHangMua> giohang = LayGioHang();
            foreach (var sanpham in giohang)
            {
                CHITIETDONHANG chitiet = new CHITIETDONHANG();
                Banh banh = new Banh();
                banh.DatBanh(ref chitiet, ref database, donhang, sanpham, bools, ref arr);
                if (arr.Count == 0)
                    chitiet.MASPK = 0;
                else
                {
                    chitiet.MASPK = 1;
                    DecorateCake(ref arr, ref chitiet, ref database, banh, donhang, sanpham, bools);
                }
                database.CHITIETDONHANGs.Add(chitiet);
                database.SaveChanges();
            }
        }
        public void DecorateCake(ref List<int> i, ref CHITIETDONHANG ct, ref bakeryEntities dtb, Banh b, DONHANG donhang, MatHangMua sanpham, bool[] bools)
        {
            AbstractDecorator deco1 = new ExDeco1();
            AbstractDecorator deco2 = new ExDeco2();
            AbstractDecorator deco3 = new ExDeco3();
            if (i.Count() == 1)
            {
                if (i[0] == 0)
                {
                    deco1.SetComponent(b);
                    deco1.DatBanh(ref ct, ref dtb,donhang,sanpham,bools,ref i);
                }
                if (i[0] == 1)
                {
                    deco2.SetComponent(b);
                    deco2.DatBanh(ref ct, ref dtb, donhang, sanpham, bools, ref i);
                }
                if (i[0] == 2)
                {
                    deco3.SetComponent(b);
                    deco3.DatBanh(ref ct, ref dtb, donhang, sanpham, bools, ref i);
                }
            }
            else if (i.Count() == 2)
            {
                if (i[0] == 0 && i[1] == 1)
                {
                    deco1.SetComponent(b);
                    deco1.DatBanh(ref ct, ref dtb, donhang, sanpham, bools, ref i);
                    deco2.SetComponent(deco1);
                    deco2.DatBanh(ref ct, ref dtb, donhang, sanpham, bools, ref i);
                }
                else if (i[0] == 1 && i[1] == 2)
                {
                    deco2.SetComponent(b);
                    deco2.DatBanh(ref ct, ref dtb, donhang, sanpham, bools, ref i);
                    deco3.SetComponent(deco2);
                    deco3.DatBanh(ref ct, ref dtb, donhang, sanpham, bools, ref i);
                }
                else
                {
                    deco1.SetComponent(b);
                    deco1.DatBanh(ref ct, ref dtb, donhang, sanpham, bools, ref i);
                    deco3.SetComponent(deco1);
                    deco3.DatBanh(ref ct, ref dtb, donhang, sanpham, bools, ref i);
                }
            }
            else
            {
                deco1.SetComponent(b);
                deco1.DatBanh(ref ct, ref dtb, donhang, sanpham, bools, ref i);
                deco2.SetComponent(deco1);
                deco2.DatBanh(ref ct, ref dtb, donhang, sanpham, bools, ref i);
                deco3.SetComponent(deco2);
                deco3.DatBanh(ref ct, ref dtb, donhang, sanpham, bools, ref i);
            }
        }
    }
}