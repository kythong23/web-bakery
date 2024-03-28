using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WEBBAKERYPro.Models;

namespace WEBBAKERYPro.CommandPattern
{
    public class RemoveItemCommand : ICommand
    {
        private readonly string _maSP;
        private readonly HttpSessionStateBase _session;

        public RemoveItemCommand(string maSP, HttpSessionStateBase session)
        {
            _maSP = maSP;
            _session = session;
        }

        public void Execute()
        {
            List<MatHangMua> gioHang = _session["GioHang"] as List<MatHangMua>;
            if (gioHang != null)
            {
                MatHangMua sanpham = gioHang.FirstOrDefault(s => s.MaBanh == _maSP);
                if (sanpham != null)
                {
                    gioHang.Remove(sanpham);
                    _session["GioHang"] = gioHang; // Update session after modifying the cart
                }
            }
        }
    }
}
