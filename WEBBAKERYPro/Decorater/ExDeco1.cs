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
        public override void DatBanh(ref CHITIETDONHANG ct, ref bakeryEntities dtb, DONHANG donhang, MatHangMua sanpham, bool[] bools, ref List<int> arr)
        {
                AddSet(ct, ref dtb);
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