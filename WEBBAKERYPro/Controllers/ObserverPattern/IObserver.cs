using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WEBBAKERYPro.Models;

namespace WEBBAKERYPro
{
    public interface IObserver
    {
        void Update(string notification,ref KHACHHANG kh);
    }
}
