using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WEBBAKERYPro.Models;

namespace WEBBAKERYPro.Decorater
{
    abstract class AbstractDecorator : Component
    {
        private Component c; 
        public void SetComponent(Component com)
        {
            c=com;
        }
        public override void DatBanh(CHITIETDONHANG ct,ref SANPHAMDIKEM spk,ref bakeryEntities dtb)
        {
            if (c != null)
            {
                c.DatBanh(ct, ref spk,ref dtb);
            }
        }
    }
}