using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WEBBAKERYPro.Models;

namespace WEBBAKERYPro.Decorater
{
    class ExDeco2 : AbstractDecorator
    {
        public override void DatBanh(CHITIETDONHANG ct, ref bakeryEntities dtb, ref int a)
        {
            if (a == 1)
            {
                base.DatBanh(ct, ref dtb, ref a);
                AddCandle(ct, ref dtb);
                a++;
            }
        }
        public void AddCandle(CHITIETDONHANG ct, ref bakeryEntities dtb)
        {
            SANPHAMDIKEM spk = new SANPHAMDIKEM();
            spk.MACTDH = ct.MACTDH;
            spk.MaCTSPK = 2;
            dtb.SANPHAMDIKEMs.Add(spk);
        }
    }
}