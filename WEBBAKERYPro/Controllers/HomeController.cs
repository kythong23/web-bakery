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
        public ActionResult Search()
        {
            return PartialView();
        }
        bakeryEntities database = new bakeryEntities();
        private List<SANPHAM> LaySanPham(int soluong)
        {
            return database.SANPHAMs.Take(soluong).ToList();
        }
        public ActionResult Index(int? page, string id)
        {
            //phân trang
            int pageSize = 6; // số lượng sản phẩm trong 1 trang
            int pageNum = (page ?? 1);
            var dsSanPham = database.SANPHAMs.ToList();
            var dsSP = LaySanPham(dsSanPham.Count()); // tổng số lượng sản phẩm
            if (id != null)
            {
                var dsSPTheoLoai = database.SANPHAMs.Where(a => a.MaLoai == id).ToList();
                return View("Index", dsSPTheoLoai.ToPagedList(pageNum, pageSize));
            }
            return View(dsSP.ToPagedList(pageNum, pageSize));
        }
        [HttpPost]
        public ActionResult Index(string SearchString, int? page, string id)
        {
            if (!SearchString.IsEmpty())
            {
                var sp = database.SANPHAMs.Where(x => x.TenSP.Contains(SearchString));
                int pageSize = 6; // số lượng sản phẩm trong 1 trang
                int pageNum = (page ?? 1);
                var dsSanPham = sp.ToList();
                var dsSP = LaySanPham(dsSanPham.Count()); // tổng số lượng sản phẩm
                return View(dsSanPham.ToPagedList(pageNum, pageSize));
            }
            else
            {
                int pageSize = 6;
                int pageNum = (page ?? 1);
                var dsSanPham = database.SANPHAMs.ToList();
                var dsSP = LaySanPham(dsSanPham.Count()); // tổng số lượng sản phẩm
                return View(dsSP.ToPagedList(pageNum, pageSize));
            }
        }

        public ActionResult LayLoaiSanPham()
        {
            var dsLoaiSP = database.LOAISANPHAMs.ToList();
            return PartialView(dsLoaiSP);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Details(string id)
        {
            var Sp = database.SANPHAMs.FirstOrDefault(s => s.MaSP == id);
            var list = getAccessory();
            ViewBag.listAccessory = list;
            return View(Sp);
        }
        public IList<SelectListItem> getAccessory()
        {
            var list = database.CHITIETDIKEMs.ToList();
            List<SelectListItem> listAccessory = new List<SelectListItem>();
            foreach(var i in list)
            {
                listAccessory.Add(new SelectListItem { Text = i.TenSPK, Value = i.MACTSPK.ToString(), Selected = false });
            }
            return listAccessory;
        }
        public ActionResult Logout(int? page)
        {
            int pageSize = 6; // số lượng sản phẩm trong 1 trang
            int pageNum = (page ?? 1);
            if (Session["TaiKhoan"] != null)
                Session.Clear();
            var dsSanPham = database.SANPHAMs.ToList();
            return View("Index", dsSanPham.ToPagedList(pageNum, pageSize));
        }
    }
}