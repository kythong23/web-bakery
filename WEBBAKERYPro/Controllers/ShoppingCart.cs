using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WEBBAKERYPro.Models;

namespace WEBBAKERYPro.Controllers
{
    public class ShoppingCart
    {
        public static List<MatHangMua> instance;

        public static List<MatHangMua> getCart()
        {
            if (instance == null)
            {
                instance = new List<MatHangMua>();
            }
            return instance;
        }
    }
}