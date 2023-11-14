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
        bakeryEntities database = new bakeryEntities();
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult QuanLySP()
        {
            var lstSanPham = database.SANPHAMs.ToList();
            return View(lstSanPham);
        }
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

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
                    Session["Admin"] = adminDB;
                    ViewBag.ThongBao = "Đăng nhập thành công";
                    return RedirectToAction("Index", "Admin");
                }
            }
            return View();
        }
        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.MaLoai = new SelectList(database.LOAISANPHAMs.ToList(), "MaLoai", "TenLoai");
            return View();

        }
        [HttpPost]
        public ActionResult Create(SANPHAM sp, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                try
                {

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
                database.SANPHAMs.Add(sp);
                database.SaveChanges();

                return RedirectToAction("QuanLySP");
            }
            ViewBag.MaLoai = new SelectList(database.LOAISANPHAMs.ToList(), "MaLoai", "TenLoai");
            return View();
        }
        [HttpGet]
        public ActionResult Edit(string id)
        {
            ViewBag.MaLoai = new SelectList(database.LOAISANPHAMs.ToList(), "MaLoai", "TenLoai");
            var product = database.SANPHAMs.Find(id);
            return View(product);
        }
        [HttpPost]
        public ActionResult Edit(SANPHAM sp, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                try
                {

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

                database.Entry(sp).State = System.Data.Entity.EntityState.Modified;
                database.SaveChanges();
                return RedirectToAction("QuanLySP");
            }
            return View(sp);
        }
        [HttpGet]
        public ActionResult Delete(string id)
        {
            SANPHAM sp = database.SANPHAMs.Find(id);
            database.SANPHAMs.Remove(sp);
            database.SaveChanges();
            return RedirectToAction("QuanLySP");
        }

        public ActionResult QuanLyDH()
        {
            var lstDonHang = database.DONHANGs.ToList();
            return View(lstDonHang);
        }
        [HttpGet]
        public ActionResult EditDH (int id)
        {
            ViewBag.MaKH = new SelectList(database.KHACHHANGs.ToList(), "MaKH", "HoTen");
            ViewBag.MaHT = new SelectList(database.HINHTHUCGIAOHANGs.ToList(), "MaHT", "TenHT");
            ViewBag.MaTT = new SelectList(database.TINHTRANGDONHANGs.ToList(), "MaTT", "LoaiTT");
            var product = database.DONHANGs.Find(id);
            return View(product);
        }
        [HttpPost]
        public ActionResult EditDH (DONHANG donhang)
        {
            if (ModelState.IsValid)
            {

                database.Entry(donhang).State = System.Data.Entity.EntityState.Modified;
                database.SaveChanges();
                return RedirectToAction("QuanLyDH");
            }
            return View(donhang);
        }
        [HttpGet]
        public ActionResult DeleteDH(int id)
        {
            DONHANG dh = database.DONHANGs.Find(id);
            database.DONHANGs.Remove(dh);
            database.SaveChanges();
            return RedirectToAction("QuanLyDH");
        }

        public ActionResult QuanLyKH()
        {
            var lstKhachHang = database.KHACHHANGs.ToList();
            return View(lstKhachHang);
        }
        
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