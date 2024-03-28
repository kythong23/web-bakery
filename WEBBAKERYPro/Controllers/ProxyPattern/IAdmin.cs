using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using WEBBAKERYPro.Models;

namespace WEBBAKERYPro
{
    public interface IAdmin
    {
        ActionResult Login(Admin admin);
        ActionResult Create(string role);
        ActionResult Edit(string id,string role);
        ActionResult Delete(string id,string role);
        ActionResult EditDH(int id,string role);
        ActionResult DeleteDH(int id, string role);
        ActionResult DeleteKH(int id,string role);
    }
}
