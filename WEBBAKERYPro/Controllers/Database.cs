using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WEBBAKERYPro.Models;

namespace WEBBAKERYPro.Controllers
{
    public class Database
    {
        public static bakeryEntities instanceDatabase;

        public static bakeryEntities getDatabase()
        {
            if (instanceDatabase == null)
            {
                instanceDatabase = new bakeryEntities();
            }
            return instanceDatabase;
        }

    }
}