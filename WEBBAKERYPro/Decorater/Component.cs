using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WEBBAKERYPro.Models;

namespace WEBBAKERYPro.Decorater
{
    abstract class Component
    {
       public abstract void DatBanh(CHITIETDONHANG ct,ref bakeryEntities dtb,ref int a);
    }
}