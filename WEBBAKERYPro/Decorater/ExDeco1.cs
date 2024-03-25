using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WEBBAKERYPro.Models;

namespace WEBBAKERYPro.Decorater
{
    class ExDeco1 : AbstractDecorator
    {
        public override void DatBanh(CHITIETDONHANG ct, ref bakeryEntities dtb, ref int a)
        {
            if (a == 0)
            {
                base.DatBanh(ct, ref dtb, ref a);
                AddSet(ct, ref dtb);
                a++;
            }

        }
        public void AddSet(CHITIETDONHANG ct, ref bakeryEntities dtb)
        {
            SANPHAMDIKEM spk = new SANPHAMDIKEM();
            spk.MACTDH = ct.MACTDH;
            spk.MaCTSPK = 1;
            dtb.SANPHAMDIKEMs.Add(spk);
        }
    }
}