using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages;
using WEBBAKERYPro.Models;

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
        public ActionResult Index()
        {
            if (!keysearch.IsEmpty())
            {
                var sp = database.SANPHAMs.Include(s => s.MaSP).Where(x => x.TenSP.Contains(keysearch));
                return View(sp.ToList());
            }
            else
            {
                var dsSanPham = database.SANPHAMs.ToList();
                return View(dsSanPham);
            }
        }
        [HttpPost]
        public ActionResult Index(string SearchString)
        {
            if (!SearchString.IsEmpty())
            {
                var sp = database.SANPHAMs.Where(x => x.TenSP.Contains(SearchString));
                return View(sp.ToList());
            }
            else
            {
                var dsSanPham = database.SANPHAMs.ToList();
                return View(dsSanPham);
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
            return View(Sp);
        }
        public ActionResult SPTheoLoai(string id)
        {
            var dsSPTheoLoai = database.SANPHAMs.Where(a => a.MaLoai == id ).ToList();
            return View("Index",dsSPTheoLoai);
        }
    }
}