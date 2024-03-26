using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WEBBAKERYPro.Models;

namespace WEBBAKERYPro.Decorater
{
    class ExDeco2 : AbstractDecorator
    {
        public override void DatBanh(ref CHITIETDONHANG ct, ref bakeryEntities dtb, DONHANG donhang, MatHangMua sanpham, bool[] bools, ref List<int> arr)
        {
                AddCandle(ct, ref dtb);
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