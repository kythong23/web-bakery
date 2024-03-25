using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WEBBAKERYPro.Controllers
{
    public class BakeryController : Controller
    {
        // GET: Bakery
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult BakeryAccessory()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            list.Add(new SelectListItem { Text = "Set muỗng, đĩa, dao cắt bánh", Value = "0" });
            list.Add(new SelectListItem { Text = "Nến", Value = "1" });
            list.Add(new SelectListItem { Text = "Kẹo cốm", Value = "2" });
            ViewBag.listAccessory = list;
            return View();
        }
    }
}