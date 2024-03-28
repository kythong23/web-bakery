using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WEBBAKERYPro.CommandPattern;
using WEBBAKERYPro.Models;
using WEBBAKERYPro.Strategy;

namespace WEBBAKERYPro.Controllers
{
    public class ShoppingCartController : Controller
    {
        // Entity Framework database context
        bakeryEntities database = new bakeryEntities();

        // Payment strategy interface for handling different payment methods
        private IPaymentStrategy _paymentStrategy;

        // Constructor with parameter injection for payment strategy
        public ShoppingCartController(IPaymentStrategy paymentStrategy)
        {
            _paymentStrategy = paymentStrategy;
        }

        // Default constructor
        public ShoppingCartController()
        {

        }

        // Method to retrieve shopping cart items from session
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

        // Action method to add a product to the shopping cart
        public ActionResult ThemSanPhamVaoGio(string masp)
        {
            ICommand command = new AddItemCommand(masp, Session);
            command.Execute();
            return Redirect(Request.UrlReferrer.ToString());
        }

        // Action method to update the quantity of a product in the cart
        public ActionResult CapNhatMatHang(string MaSP, int SoLuong)
        {
            ICommand command = new UpdateQuantityCommand(MaSP, SoLuong, Session);
            command.Execute();
            return RedirectToAction("Index");
        }

        // Method to calculate total quantity of items in the cart
        public int TinhTongSL()
        {
            int tongSL = 0;
            List<MatHangMua> gioHang = LayGioHang();
            if (gioHang != null)
                tongSL = gioHang.Sum(sp => sp.SoLuong);
            return tongSL;
        }

        // Method to calculate total price of items in the cart
        public double TinhTongTien()
        {
            double TongTien = 0;
            List<MatHangMua> gioHang = LayGioHang();
            if (gioHang != null)
                TongTien = gioHang.Sum(sp => sp.ThanhTien());
            return TongTien;
        }

        // Action method to remove a product from the cart
        public ActionResult XoaMatHang(string MaSP)
        {
            ICommand command = new RemoveItemCommand(MaSP, Session);
            command.Execute();

            // Check if the cart is empty after removing the item
            if (LayGioHang().Count == 0)
            {
                // Clear the session
                Session.Remove("GioHang");
                return RedirectToAction("Index", "Home");
            }

            return RedirectToAction("Index");
        }

        // Action method to display the shopping cart
        public ActionResult Index()
        {
            List<MatHangMua> gioHang = LayGioHang();
            if (gioHang == null || gioHang.Count == 0)
            {
                return RedirectToAction("Index", "Home");
            }
            ViewBag.TongSL = TinhTongSL();
            ViewBag.TongTien = TinhTongTien();
            return View(gioHang);
        }

        // Partial view to display cart summary
        public ActionResult GioHangPartial()
        {
            ViewBag.TongSL = TinhTongSL();
            ViewBag.TongTien = TinhTongTien();
            return PartialView();
        }

        // Action method to place an order
        public ActionResult DatHang()
        {
            if (Session["TaiKhoan"] == null)
            {
                Session["DangNhap"] = "chuadangnhap";
                return RedirectToAction("Index");
            }
            List<MatHangMua> gioHang = LayGioHang();
            ViewBag.TongSL = TinhTongSL();
            ViewBag.TongTien = TinhTongTien();

            return View(gioHang);
        }

        // Action method to confirm the order
        public ActionResult DatHangThanhCong()
        {
            return View();
        }

        // Action method to remove login status from session
        public ActionResult XoaThongTinDangNhap()
        {
            Session.Remove("DangNhap");
            return RedirectToAction(Request.UrlReferrer?.ToString());
        }

        // Action method to process the order with selected payment method
        public ActionResult DongYDatHang(string paymentMethod)
        {
            KHACHHANG kHACHHANG = Session["TaiKhoan"] as KHACHHANG;
            List<MatHangMua> giohang = LayGioHang();

            DONHANG donhang = new DONHANG();
            donhang.MaKH = kHACHHANG.MaKH;
            donhang.NgayDat = DateTime.Now;
            donhang.TenNN = kHACHHANG.HoTen;
            donhang.DiaChiNhanHang = kHACHHANG.DiaChi;
            donhang.SDT = kHACHHANG.SDT;
            donhang.MaTT = 1;

            // Determine payment method and set corresponding payment strategy
            if (paymentMethod == "COD")
            {
                _paymentStrategy = new COD();
            }
            else if (paymentMethod == "CreditCard")
            {
                _paymentStrategy = new CreditCard();
            }

            // Process payment
            double totalAmount = TinhTongTien();
            donhang.TongGia = (int)_paymentStrategy.ProcessPayment(totalAmount);

            // Save payment method information
            donhang.MaHT = _paymentStrategy.GetMaHT();

            // Save order details to database
            database.DONHANGs.Add(donhang);
            database.SaveChanges();

            // Save order items to database
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
            Session["GioHang"] = null; // Clear the shopping cart
            return RedirectToAction("DatHangThanhCong");
        }
    }
}