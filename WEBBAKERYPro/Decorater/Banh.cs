using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WEBBAKERYPro.Models;

namespace WEBBAKERYPro.Decorater
{
    public class Banh : Component
    {
        public override void DatBanh(ref CHITIETDONHANG ct,ref bakeryEntities dtb, DONHANG donhang,MatHangMua sanpham, bool[] bools,ref List<int >arr )
        {
            ct.MaDH = donhang.MaDH;
            ct.MaSP = sanpham.MaBanh;
            ct.SoLuong = sanpham.SoLuong;
            ct.ThanhTien = (int)sanpham.GiaBanh;
            for (int i = 0; i < bools.Count(); i++)
            {
                if (bools[i] == true)
                    arr.Add(i);

            }
        }
    }
}