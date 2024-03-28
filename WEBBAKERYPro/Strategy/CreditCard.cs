using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WEBBAKERYPro.Strategy
{
    public class CreditCard:IPaymentStrategy
    {
        public double ProcessPayment(double totalAmount)
        {
            totalAmount += 15000;
            return totalAmount;
        }

        public int GetMaHT()
        {
            return 2;
        }
    }
}