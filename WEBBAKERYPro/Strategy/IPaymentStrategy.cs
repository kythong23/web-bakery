using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WEBBAKERYPro.Strategy
{
    public interface IPaymentStrategy
    {
        double ProcessPayment(double totalAmount);
        int GetMaHT();
    }
}