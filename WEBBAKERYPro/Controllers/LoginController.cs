using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WEBBAKERYPro.Models;
using System.Web.Mvc;
using System.Security.Policy;

namespace WEBBAKERYPro.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        bakeryEntities database = new bakeryEntities();
        // GET: Login
        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        public ActionResult Login()
        {
            return PartialView();
        }
        [HttpPost]
        public ActionResult Login(KHACHHANG kh)
        {
            string thongBao = "";
            if (ModelState.IsValid)
            {
                if (string.IsNullOrEmpty(kh.Email))
                    ModelState.AddModelError(string.Empty, "Tên đăng nhập không được để trống");
                if (string.IsNullOrEmpty(kh.MatKhau))
                    ModelState.AddModelError(string.Empty, "Mật khẩu không được để trống");
                if (ModelState.IsValid)
                {
                    if(kh.Email=="admin" && kh.MatKhau == "123")
                    {
                        return RedirectToAction("Admin","Admin");
                    }
                    var khach = database.KHACHHANGs.FirstOrDefault(k => k.Email == kh.Email && k.MatKhau == kh.MatKhau);
                    if (khach != null)
                    {
                        Session["TaiKhoan"] = khach;
                        Session["TenKH"] = khach.HoTen;
                        thongBao = khach.Notification;
                        
                    }
                    else
                    {
                        ViewBag.ThongBao = "Tên đăng nhập hoặc mật khẩu không đúng";
                        return RedirectToAction("Login", "Login");
                    }
                }
            }
            Session["NewProductNotification"] = thongBao;
            return RedirectToAction("Index", "Home");

        }

        [HttpGet]
        public ActionResult Register()
        {
            return PartialView();
        }

        [HttpPost]
        public ActionResult Register(KHACHHANG kh)
        {
            if (ModelState.IsValid)
            {
                if (string.IsNullOrEmpty(kh.HoTen))
                    ModelState.AddModelError(string.Empty, "Họ tên không được để trống");
                if (string.IsNullOrEmpty(kh.Email))
                    ModelState.AddModelError(string.Empty, "Email không được để trống");
                if (string.IsNullOrEmpty(kh.MatKhau))
                    ModelState.AddModelError(string.Empty, "Mật khẩu không được để trống");
                if (kh.SDT == null)
                    ModelState.AddModelError(string.Empty, "Số điện thoại không được để trống");
                if (string.IsNullOrEmpty(kh.DiaChi))
                    ModelState.AddModelError(string.Empty, "Địa chỉ không được để trống");

                var khachhang = database.KHACHHANGs.FirstOrDefault(k => k.Email == kh.Email);
                if (khachhang != null)
                {
                    ModelState.AddModelError(string.Empty, "Đã có tên người dùng này");
                }

                if (ModelState.IsValid)
                {
                    database.KHACHHANGs.Add(kh);
                    database.SaveChanges();
                }
                else
                {
                    return View();
                }
            }
            return RedirectToAction("Login");
        }

    }
}
