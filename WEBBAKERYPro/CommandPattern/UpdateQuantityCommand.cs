using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WEBBAKERYPro.Models;

namespace WEBBAKERYPro.CommandPattern
{
    public class UpdateQuantityCommand : ICommand
    {
        private readonly string _maSP;
        private readonly int _soLuong;
        private readonly HttpSessionStateBase _session;

        public UpdateQuantityCommand(string maSP, int soLuong, HttpSessionStateBase session)
        {
            _maSP = maSP;
            _soLuong = soLuong;
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
                    sanpham.SoLuong = _soLuong;
                }

                // Update the cart in the session
                _session["GioHang"] = gioHang;
            }
        }
    }
}