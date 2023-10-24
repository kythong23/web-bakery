using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WEBBAKERYPro.Models
{
    public class MatHangMua
    {
        bakeryEntities db = new bakeryEntities();
        public string MaBanh { get; set; }
        public string TenBanh { get; set; }
        public string AnhBanh { get; set;}
        public int GiaBanh { get; set;}
        public int SoLuong { get; set;}

        public double ThanhTien()
        {
            return SoLuong * GiaBanh;
        }
        public MatHangMua(string MaBanh) {
            this.MaBanh = MaBanh;
            var banh = db.SANPHAMs.Single(s=> s.MaSP==this.MaBanh);
            this.TenBanh = banh.TenSP;
            this.AnhBanh = banh.HinhSP;
            this.GiaBanh = banh.Gia;
            this.SoLuong = 1;
        }
    }
}