using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WEBBAKERYPro.Strategy
{
    public class COD : IPaymentStrategy
    {
        public double ProcessPayment(double totalAmount)
        {
            totalAmount += 20000;
            return totalAmount;
        }

        public int GetMaHT()
        {
            return 1;
        }
    }
}