using System;
using System.Collections.Generic;
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

        [HttpGet]
        public ActionResult Login ()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login (Admin admin) 
        {
            if(ModelState.IsValid)
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
    }
}