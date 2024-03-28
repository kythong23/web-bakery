using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WEBBAKERYPro.Models;

namespace WEBBAKERYPro.Controllers
{
    public class AdminController : Controller
    {
        // Khởi tạo đối tượng Entity Framework để truy cập vào cơ sở dữ liệu
        bakeryEntities database = new bakeryEntities();

        // Action method cho trang chính của trang quản trị
        public ActionResult Index()
        {
            return View();
        }

        // Action method quản lý sản phẩm
        public ActionResult QuanLySP()
        {
            var lstSanPham = database.SANPHAMs.ToList();
            return View(lstSanPham);
        }

        // Action method đăng nhập cho admin (GET)
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        // Action method đăng nhập cho admin (POST)
        [HttpPost]
        public ActionResult Login(Admin admin)
        {
            if (ModelState.IsValid)
            {
                if (string.IsNullOrEmpty(admin.UserAd))
                {
                    ModelState.AddModelError(string.Empty, "Username không được bỏ trống");
                }
                if (string.IsNullOrEmpty(admin.PassAd))
                {
                    ModelState.AddModelError(string.Empty, "Password không được để trống");
                }

                //Check admin already had or not
                var adminDB = database.Admins.FirstOrDefault(ad => ad.UserAd == admin.UserAd && ad.PassAd == admin.PassAd);
                if (adminDB == null)
                {
                    ModelState.AddModelError(string.Empty, "Tên đăng nhập hoặc mật khẩu không hợp lệ");
                }
                else
                {
                    // Lưu thông tin đăng nhập vào session
                    Session["Admin"] = adminDB;
                    Session["TenAdmin"] = adminDB.HoTen;
                    ViewBag.ThongBao = "Đăng nhập thành công";
                    return RedirectToAction("Index", "Admin");
                }
            }
            return View();
        }

        // Action method tạo mới sản phẩm (GET)
        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.MaLoai = new SelectList(database.LOAISANPHAMs.ToList(), "MaLoai", "TenLoai");
            return View();
        }

        // Action method tạo mới sản phẩm (POST)
        [HttpPost]
        public ActionResult Create(SANPHAM sp, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Lưu file ảnh sản phẩm
                    if (file != null)
                    {
                        string path = Path.Combine(Server.MapPath("~/Images"), Path.GetFileName(file.FileName));
                        file.SaveAs(path);
                        database.SaveChanges();
                    }
                    ViewBag.FileStatus = "File uploaded successfully.";
                }
                catch (Exception)
                {

                    ViewBag.FileStatus = "Error while file uploading.";
                }

                // Thêm sản phẩm vào cơ sở dữ liệu
                database.SANPHAMs.Add(sp);
                database.SaveChanges();

                return RedirectToAction("QuanLySP");
            }
            ViewBag.MaLoai = new SelectList(database.LOAISANPHAMs.ToList(), "MaLoai", "TenLoai");
            return View();
        }

        // Action method chỉnh sửa sản phẩm (GET)
        [HttpGet]
        public ActionResult Edit(string id)
        {
            ViewBag.MaLoai = new SelectList(database.LOAISANPHAMs.ToList(), "MaLoai", "TenLoai");
            var product = database.SANPHAMs.Find(id);
            return View(product);
        }

        // Action method chỉnh sửa sản phẩm (POST)
        [HttpPost]
        public ActionResult Edit(SANPHAM sp, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Lưu file ảnh sản phẩm
                    if (file != null)
                    {
                        string path = Path.Combine(Server.MapPath("~/Images"), Path.GetFileName(file.FileName));
                        file.SaveAs(path);
                        database.SaveChanges();
                    }
                    ViewBag.FileStatus = "File uploaded successfully.";
                }
                catch (Exception)
                {

                    ViewBag.FileStatus = "Error while file uploading.";
                }

                // Cập nhật thông tin sản phẩm vào cơ sở dữ liệu
                database.Entry(sp).State = System.Data.Entity.EntityState.Modified;
                database.SaveChanges();
                return RedirectToAction("QuanLySP");
            }
            return View(sp);
        }

        // Action method xóa sản phẩm
        [HttpGet]
        public ActionResult Delete(string id)
        {
            SANPHAM sp = database.SANPHAMs.Find(id);
            database.SANPHAMs.Remove(sp);
            database.SaveChanges();
            return RedirectToAction("QuanLySP");
        }

        // Action method quản lý đơn hàng
        public ActionResult QuanLyDH()
        {
            var lstDonHang = database.DONHANGs.ToList();
            return View(lstDonHang);
        }

        // Action method chỉnh sửa đơn hàng (GET)
        [HttpGet]
        public ActionResult EditDH(int id)
        {
            ViewBag.MaKH = new SelectList(database.KHACHHANGs.ToList(), "MaKH", "HoTen");
            ViewBag.MaHT = new SelectList(database.HINHTHUCGIAOHANGs.ToList(), "MaHT", "TenHT");
            ViewBag.MaTT = new SelectList(database.TINHTRANGDONHANGs.ToList(), "MaTT", "LoaiTT");
            var product = database.DONHANGs.Find(id);
            return View(product);
        }

        // Action method chỉnh sửa đơn hàng (POST)
        [HttpPost]
        public ActionResult EditDH(DONHANG donhang)
        {
            if (ModelState.IsValid)
            {
                // Cập nhật thông tin đơn hàng vào cơ sở dữ liệu
                database.Entry(donhang).State = System.Data.Entity.EntityState.Modified;
                database.SaveChanges();
                return RedirectToAction("QuanLyDH");
            }
            return View(donhang);
        }

        // Action method xóa đơn hàng
        [HttpGet]
        public ActionResult DeleteDH(int id)
        {
            // Xóa đơn hàng cùng các chi tiết liên quan
            var maTT = database.DONHANGs.Where(k => k.MaTT == id).ToList();
            database.DONHANGs.RemoveRange(maTT);

            var maHT = database.DONHANGs.Where(k => k.MaHT == id).ToList();
            database.DONHANGs.RemoveRange(maHT);

            var maKH = database.DONHANGs.Where(k => k.MaKH == id).ToList();
            database.DONHANGs.RemoveRange(maKH);

            var chitiet = database.CHITIETDONHANGs.Where(k => k.MaDH == id).ToList();
            database.CHITIETDONHANGs.RemoveRange(chitiet);
            DONHANG dh = database.DONHANGs.Find(id);
            database.DONHANGs.Remove(dh);
            database.SaveChanges();
            return RedirectToAction("QuanLyDH");
        }

        // Action method quản lý khách hàng
        public ActionResult QuanLyKH()
        {
            var lstKhachHang = database.KHACHHANGs.ToList();
            return View(lstKhachHang);
        }

        // Action method xóa khách hàng
        [HttpGet]
        public ActionResult DeleteKH(int id)
        {
            KHACHHANG dh = database.KHACHHANGs.Find(id);
            database.KHACHHANGs.Remove(dh);
            database.SaveChanges();
            return RedirectToAction("QuanLyKH");
        }
    }
}