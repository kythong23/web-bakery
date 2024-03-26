using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WEBBAKERYPro.Models;

namespace WEBBAKERYPro.Decorater
{
    abstract class AbstractDecorator : Component
    {
        protected Component c; 
        public void SetComponent(Component com)
        {
            c=com;
        }
        public override void DatBanh(ref CHITIETDONHANG ct,ref bakeryEntities dtb, DONHANG donhang, MatHangMua sanpham, bool[] bools, ref List<int> arr)
        {
            if (c != null)
            {
                c.DatBanh(ref ct, ref dtb, donhang, sanpham, bools, ref arr);
            }
        }
    }
}