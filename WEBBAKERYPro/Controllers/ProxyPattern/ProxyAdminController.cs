using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WEBBAKERYPro.Controllers;
using WEBBAKERYPro.Models;

namespace WEBBAKERYPro
{
    public class ProxyAdminController : Controller, IAdmin
    {
        bakeryEntities database = new bakeryEntities();

        string role;

        public ProxyAdminController(string role) 
        {
            this.role = role;
        }
        public ActionResult Create(string r)
        {
            if (r == "Quan Ly" || r =="Nhan Vien")
            {
                ViewBag.MaLoai = new SelectList(database.LOAISANPHAMs.ToList(), "MaLoai", "TenLoai");
                return View();
            }
            return RedirectToAction("Index", "Admin");
        }

        public ActionResult Delete(string id,string r)
        {
            if (r == "Quan Ly" )
            {
                SANPHAM sp = database.SANPHAMs.Find(id);
                database.SANPHAMs.Remove(sp);
                database.SaveChanges();
                return RedirectToAction("QuanLySP");
            }
            return RedirectToAction("Index", "Admin");
        }

        public ActionResult DeleteDH(int id,string r)
        {
            if (r == "Quan Ly" || r == "Nhan Vien")
            {
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
            return RedirectToAction("Index", "Admin");
        }

        public ActionResult DeleteKH(int id, string r)
        {
            // nếu role là quản lý thì sẽ cho xóa khách hàng
            if (r == "Quan Ly" )
            {
                KHACHHANG dh = database.KHACHHANGs.Find(id);
                database.KHACHHANGs.Remove(dh);
                database.SaveChanges();
                return RedirectToAction("QuanLyKH");
            }
            return RedirectToAction("Index", "Admin");
        }

        public ActionResult Edit(string id,string r)
        {
            // Kiểm tra role của nhân viên nếu đúng thì sẽ cho edit
            if(r== "Quan Ly" || r == "Nhan Vien")
            {
                ViewBag.MaLoai = new SelectList(database.LOAISANPHAMs.ToList(), "MaLoai", "TenLoai");
                var product = database.SANPHAMs.Find(id);
                return View(product);
            }
            return RedirectToAction("QuanLySP","Admin");
        }

        public ActionResult EditDH(int id,string r)
        {
            if (r == "Quan Ly" || r == "Nhan Vien")
            {
                ViewBag.MaKH = new SelectList(database.KHACHHANGs.ToList(), "MaKH", "HoTen");
                ViewBag.MaHT = new SelectList(database.HINHTHUCGIAOHANGs.ToList(), "MaHT", "TenHT");
                ViewBag.MaTT = new SelectList(database.TINHTRANGDONHANGs.ToList(), "MaTT", "LoaiTT");
                var product = database.DONHANGs.Find(id);
                return View(product);
            }
            return RedirectToAction("QuanLyDH", "Admin");
        }



        public ActionResult Login(Admin ad)
        {

            return RedirectToAction("Index", "Admin");
        }

    }
}