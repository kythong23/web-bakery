using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WEBBAKERYPro.Controllers
{
    public abstract class CartItem
    {
        public abstract double CalculatePrice();
    }
}