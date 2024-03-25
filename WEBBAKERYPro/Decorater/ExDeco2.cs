using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WEBBAKERYPro.Models;

namespace WEBBAKERYPro.Decorater
{
    class ExDeco2 : AbstractDecorator
    {
        public override void DatBanh(CHITIETDONHANG ct,ref SANPHAMDIKEM spk,ref bakeryEntities dtb)
        {
            base.DatBanh(ct,ref spk,ref dtb);
            AddCandle(ct,ref spk,ref dtb);
        }
        public void AddCandle(CHITIETDONHANG ct,ref SANPHAMDIKEM spk,ref bakeryEntities dtb)
        {
            spk.MACTDH = ct.MACTDH;
            spk.MaCTSPK = 2;
            dtb.SANPHAMDIKEMs.Add(spk);
        }
    }
}