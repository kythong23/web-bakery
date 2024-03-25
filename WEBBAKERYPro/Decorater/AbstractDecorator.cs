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
        public override void DatBanh(CHITIETDONHANG ct,ref bakeryEntities dtb, ref int a)
        {
            if (c != null)
            {
                c.DatBanh(ct,ref dtb,ref a);
            }
        }
    }
}