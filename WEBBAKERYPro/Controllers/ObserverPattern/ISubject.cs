using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WEBBAKERYPro.Models;

namespace WEBBAKERYPro
{
    internal interface ISubject
    {
        void Attach(IObserver observer);

        // Detach an observer from the subject.
        void Detach(IObserver observer);

        // Notify all observers about an event.
        void Notify(KHACHHANG kh,ref bakeryEntities dtb);
    }
}
