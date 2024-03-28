using Microsoft.Ajax.Utilities;
using Microsoft.SqlServer.Server;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using WEBBAKERYPro.Models;

namespace WEBBAKERYPro.Prototype
{
    public class PrototypeView : Controller, ICloneable
    {
        private static PrototypeView instance;
        public string pagereturn;
        public int? page;
        public int pagesize;
        public bakeryEntities dtb;
        public List<SANPHAM> dssp;
        public PrototypeView() { }
        public PrototypeView(int?page,int pagesize,bakeryEntities dtb, List<SANPHAM> dssp,string pagereturn) 
        {
            this.pagereturn = pagereturn;
            this.dssp = dssp;
            this.pagesize =pagesize;
            this.page = page;
            this.dtb = dtb;
        }
        public static PrototypeView getInstance() 
        {
            if (instance == null)
            {
                instance=new PrototypeView();
            }
            return instance;
        }
        public ActionResult getView(int? page, string id) 
        {
            int pageNum = (page ?? 1);
            if (id != null)
            {
                var dsSPTheoLoai = dtb.SANPHAMs.Where(a => a.MaLoai == id).ToList();
                return View(pagereturn, dsSPTheoLoai.ToPagedList(pageNum, pagesize));
            }
            return View(pagereturn, dssp.ToPagedList(pageNum, pagesize)); 
        }
        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}