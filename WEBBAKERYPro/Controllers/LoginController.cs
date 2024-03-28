using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WEBBAKERYPro.Models;
using System.Web.Mvc;

namespace WEBBAKERYPro.Controllers
{
    public class LoginController : Controller
    {
        // Khởi tạo đối tượng Entity Framework để truy cập vào cơ sở dữ liệu
        bakeryEntities database = new bakeryEntities();

        // Phương thức đăng xuất
        public ActionResult Logout()
        {
            // Hủy toàn bộ session
            Session.Abandon();
            // Chuyển hướng về trang chủ
            return RedirectToAction("Index", "Home");
        }

        // Phương thức lấy giao diện đăng nhập (GET)
        [HttpGet]
        public ActionResult Login()
        {
            return PartialView();
        }

        // Phương thức xử lý đăng nhập (POST)
        [HttpPost]
        public ActionResult Login(KHACHHANG kh)
        {
            // Kiểm tra tính hợp lệ của model
            if (ModelState.IsValid)
            {
                // Kiểm tra xem trường Email và Mật khẩu có rỗng không
                if (string.IsNullOrEmpty(kh.Email))
                    ModelState.AddModelError(string.Empty, "Tên đăng nhập không được để trống");
                if (string.IsNullOrEmpty(kh.MatKhau))
                    ModelState.AddModelError(string.Empty, "Mật khẩu không được để trống");
                // Nếu model hợp lệ
                if (ModelState.IsValid)
                {
                    // Kiểm tra xem có phải là Admin không
                    if (kh.Email == "admin" && kh.MatKhau == "123")
                    {
                        return RedirectToAction("Admin", "Admin");
                    }

                    // Tìm kiếm khách hàng trong cơ sở dữ liệu
                    var khach = database.KHACHHANGs.FirstOrDefault(k => k.Email == kh.Email && k.MatKhau == kh.MatKhau);

                    // Nếu khách hàng tồn tại
                    if (khach != null)
                    {
                        // Lưu thông tin đăng nhập vào session
                        Session["TaiKhoan"] = khach;
                        Session["TenKH"] = khach.HoTen;
                    }
                    else
                    {
                        // Hiển thị thông báo lỗi
                        ViewBag.ThongBao = "Tên đăng nhập hoặc mật khẩu không đúng";
                        // Chuyển hướng về trang đăng nhập
                        return RedirectToAction("Login", "Login");
                    }
                }
            }
            // Chuyển hướng về trang chủ
            return RedirectToAction("Index", "Home");
        }

        // Phương thức lấy giao diện đăng ký (GET)
        [HttpGet]
        public ActionResult Register()
        {
            // Trả về partial view cho trang đăng ký
            return PartialView();
        }

        // Phương thức xử lý đăng ký (POST)
        [HttpPost]
        public ActionResult Register(KHACHHANG kh)
        {
            // Kiểm tra tính hợp lệ của model
            if (ModelState.IsValid)
            {
                // Kiểm tra xem các trường thông tin có rỗng không
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

                // Kiểm tra xem đã có người dùng này chưa
                var khachhang = database.KHACHHANGs.FirstOrDefault(k => k.Email == kh.Email);
                if (khachhang != null)
                {
                    ModelState.AddModelError(string.Empty, "Đã có tên người dùng này");
                }

                // Nếu model hợp lệ
                if (ModelState.IsValid)
                {
                    // Thêm khách hàng mới vào cơ sở dữ liệu
                    database.KHACHHANGs.Add(kh);
                    database.SaveChanges();
                }
                else
                {
                    // Trả về view đăng ký với các lỗi
                    return View();
                }
            }
            // Chuyển hướng về trang đăng nhập
            return RedirectToAction("Login");
        }

    }
}
