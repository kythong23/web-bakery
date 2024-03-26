using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WEBBAKERYPro.Models;

namespace WEBBAKERYPro.Decorater
{
    public abstract class Component
    {
       public abstract void DatBanh(ref CHITIETDONHANG ct, ref bakeryEntities dtb, DONHANG donhang, MatHangMua sanpham, bool[] bools, ref List<int> arr);
    }
}