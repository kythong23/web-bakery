using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages;
using WEBBAKERYPro.Models;
using PagedList;
using System.Web.UI;
using System.ComponentModel;

namespace WEBBAKERYPro.Controllers
{
    public class HomeController : Controller
    {
        // Hiển thị partial view cho phần tìm kiếm
        public ActionResult Search()
        {
            return PartialView();
        }

        // Khởi tạo đối tượng Entity Framework để truy cập vào cơ sở dữ liệu
        bakeryEntities database = new bakeryEntities();

        // Phương thức lấy danh sách sản phẩm
        private List<SANPHAM> LaySanPham(int soluong)
        {
            return database.SANPHAMs.Take(soluong).ToList();
        }

        // Action method cho trang chính (trang chủ)
        public ActionResult Index(int? page, string id)
        {
            // Phân trang
            int pageSize = 6; // số lượng sản phẩm trong 1 trang
            int pageNum = (page ?? 1);
            var dsSanPham = database.SANPHAMs.ToList();
            var dsSP = LaySanPham(dsSanPham.Count()); // tổng số lượng sản phẩm

            // Nếu có tham số id (loại sản phẩm), lọc sản phẩm theo loại
            if (id != null)
            {
                var dsSPTheoLoai = database.SANPHAMs.Where(a => a.MaLoai == id).ToList();
                return View("Index", dsSPTheoLoai.ToPagedList(pageNum, pageSize));
            }

            // Trả về trang chủ với danh sách sản phẩm đã phân trang
            return View(dsSP.ToPagedList(pageNum, pageSize));
        }

        // Action method xử lý tìm kiếm sản phẩm
        [HttpPost]
        public ActionResult Index(string SearchString,int? page, string id)
        {
            // Nếu chuỗi tìm kiếm không rỗng
            if (!SearchString.IsEmpty())
            {
                // Lấy danh sách sản phẩm thỏa mãn điều kiện tìm kiếm
                var sp = database.SANPHAMs.Where(x => x.TenSP.Contains(SearchString));
                int pageSize = 6; // số lượng sản phẩm trong 1 trang
                int pageNum = (page ?? 1);
                var dsSanPham = sp.ToList();
                var dsSP = LaySanPham(dsSanPham.Count()); // tổng số lượng sản phẩm
                return View(dsSanPham.ToPagedList(pageNum, pageSize));
            }
            else
            {
                // Nếu không có tìm kiếm, trả về trang chính với danh sách sản phẩm đã phân trang
                int pageSize = 6;
                int pageNum = (page ?? 1);
                var dsSanPham = database.SANPHAMs.ToList();
                var dsSP = LaySanPham(dsSanPham.Count()); // tổng số lượng sản phẩm
                return View(dsSP.ToPagedList(pageNum,pageSize));
            }
        }

        // Action method lấy danh sách loại sản phẩm
        public ActionResult LayLoaiSanPham()
        {
            var dsLoaiSP = database.LOAISANPHAMs.ToList();
            return PartialView(dsLoaiSP);
        }

        // Action method cho trang Giới thiệu
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        // Action method cho trang Liên hệ
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        // Action method hiển thị chi tiết sản phẩm
        public ActionResult Details(string id)
        {
            var Sp = database.SANPHAMs.FirstOrDefault(s => s.MaSP == id);
            return View(Sp);
        }

        // Action method đăng xuất
        public ActionResult Logout(int? page)
        {
            int pageSize = 6; // số lượng sản phẩm trong 1 trang
            int pageNum = (page ?? 1);
            // Xóa thông tin đăng nhập khỏi session
            if (Session["TaiKhoan"]!=null)
                Session.Clear();
            var dsSanPham = database.SANPHAMs.ToList();
            // Trả về trang chính với danh sách sản phẩm đã phân trang
            return View("Index", dsSanPham.ToPagedList(pageNum,pageSize));
        }
    }
}