using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WEBBAKERYPro.Models;

namespace WEBBAKERYPro.Controllers
{
    public class AdminController : Controller, IAdmin
    {
        bakeryEntities database = Database.getDatabase();
        string role;

        private Subject subject = new Subject();

        public ActionResult Index()
        {
            var customers = database.KHACHHANGs.ToList();
            return View(customers);
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

                //Kiểm tra xem admin đã có hay chưa 
                var adminDB = database.Admins.FirstOrDefault(ad => ad.UserAd == admin.UserAd && ad.PassAd == admin.PassAd);
                if (adminDB == null)
                {
                    ModelState.AddModelError(string.Empty, "Tên đăng nhập hoặc mật khẩu không hợp lệ");
                }
                else
                {
                    System.Web.HttpContext.Current.Session["Admin"] = adminDB; // Thêm admin hiện tại vào Session["Admin"]
                    ViewBag.ThongBao = "Đăng nhập thành công";
                    var adminFromSession = System.Web.HttpContext.Current.Session["Admin"];
                    if (adminFromSession != null)
                    {
                        role = adminDB.VaiTro;
                    }
                    IAdmin admin1 = new ProxyAdminController(role);
                    admin1.Login(admin);
                    return admin1.Login(admin);
                }
            }
            return View();
        }
        [HttpGet]
        public ActionResult Create(string role)
        {
            var adminFromSession = System.Web.HttpContext.Current.Session["Admin"]; // Lấy Admin từ Session hiện tại gán cho biến adminFromSession
            IAdmin admin1 = new ProxyAdminController(((Admin)adminFromSession).VaiTro); // Ép kiểu adminFromSession và lấy VaiTro của Admin
            return admin1.Create(((Admin)adminFromSession).VaiTro);

        }
        [HttpPost]
        public ActionResult Create(SANPHAM sp, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Kiểm tra file hình ảnh
                    if (file != null)
                    {
                        string path = Path.Combine(Server.MapPath("~/Images"), Path.GetFileName(file.FileName));
                        file.SaveAs(path);
                        database.SaveChanges();
                    }
                    ViewBag.FileStatus = "File uploaded successfully.";
                    UpdateCustomerNotificationsForNewProduct(sp.TenSP);
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
        public ActionResult Edit(string id, string role)
        { 
            var adminFromSession = System.Web.HttpContext.Current.Session["Admin"];
            IAdmin admin1 = new ProxyAdminController(((Admin)adminFromSession).VaiTro);
            return admin1.Edit(id, ((Admin)adminFromSession).VaiTro);
        }
        [HttpPost]
        public ActionResult Edit(SANPHAM sp, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Kiểm tra file hình ảnh
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
        public ActionResult Delete(string id,string role)
        {
            var adminFromSession = System.Web.HttpContext.Current.Session["Admin"];
            IAdmin admin1 = new ProxyAdminController(((Admin)adminFromSession).VaiTro);
            return admin1.Delete(id, ((Admin)adminFromSession).VaiTro);
        }

        public ActionResult QuanLyDH()
        {
            var lstDonHang = database.DONHANGs.ToList();
            return View(lstDonHang);
        }
        [HttpGet]
        public ActionResult EditDH (int id, string role)
        {
            var adminFromSession = System.Web.HttpContext.Current.Session["Admin"];
            IAdmin admin1 = new ProxyAdminController(((Admin)adminFromSession).VaiTro);
            return admin1.EditDH(id, ((Admin)adminFromSession).VaiTro);
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
        public ActionResult DeleteDH(int id,string role)
        {
            var adminFromSession = System.Web.HttpContext.Current.Session["Admin"];
            IAdmin admin1 = new ProxyAdminController(((Admin)adminFromSession).VaiTro);
            return admin1.DeleteDH(id, ((Admin)adminFromSession).VaiTro);
        }
        private void UpdateCustomerNotificationsForNewProduct(string productName)
        {
            var customers = database.KHACHHANGs.ToList();
            foreach (var customer in customers)
            {
                subject.Attach(customer);
                subject.SetNotification("Admin đã thêm sản phẩm mới: " + productName,customer,ref database);
            }
            var k = database.KHACHHANGs.FirstOrDefault(kh => kh.Email == "gnuvt2003");
        }

        // Trang Quản lý khách hàng bao gồm chức năng xóa khách hàng
        public ActionResult QuanLyKH()
        {
            var lstKhachHang = database.KHACHHANGs.ToList(); // Lấy danh sách khách hàng 
            return View(lstKhachHang);
        }
        
        [HttpGet]
        public ActionResult DeleteKH(int id,string role)
        {
            var adminFromSession = System.Web.HttpContext.Current.Session["Admin"];
            IAdmin admin1 = new ProxyAdminController(((Admin)adminFromSession).VaiTro);
            return admin1.DeleteKH(id, ((Admin)adminFromSession).VaiTro);
        }
    }
}