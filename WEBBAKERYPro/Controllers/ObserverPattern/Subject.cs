using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using WEBBAKERYPro.Models;

namespace WEBBAKERYPro
{
    public class Subject : ISubject
    {
        private List<IObserver> observers = new List<IObserver>();
        private string notification;

        public void Attach(IObserver observer)
        {
            observers.Add(observer);
        }

        public void Detach(IObserver observer)
        {
            observers.Remove(observer);
        }

        public void Notify(KHACHHANG kh,ref bakeryEntities dtb)
        {
            foreach (var observer in observers)
            {
                //observer.Update(notification,ref kh);
                //dtb.Entry(kh).CurrentValues.SetValues(kh);

                observer.Update(notification, ref kh);
                dtb.KHACHHANGs.AddOrUpdate(kh); // Thêm khCopy vào context
            }
            dtb.SaveChanges();
            Console.WriteLine(kh.Notification);
        }
        public void SetNotification(string notification,KHACHHANG kh,ref bakeryEntities dtb)
        {
            this.notification = notification;
            Notify(kh,ref dtb);
        }
    }
}