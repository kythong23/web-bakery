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
            return RedirectToAction("Details", "Home", new { id = masp });
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
    }
}