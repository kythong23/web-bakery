using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WEBBAKERYPro.Models;

namespace WEBBAKERYPro.CommandPattern
{
    public class AddItemCommand : ICommand
    {
        private readonly string _masp;
        private readonly HttpSessionStateBase _session;

        public AddItemCommand(string masp, HttpSessionStateBase session)
        {
            _masp = masp;
            _session = session;
        }

        public void Execute()
        {
            List<MatHangMua> gioHang = _session["GioHang"] as List<MatHangMua>;
            if (gioHang == null)
            {
                gioHang = new List<MatHangMua>();
            }

            MatHangMua sanpham = gioHang.FirstOrDefault(s => s.MaBanh == _masp);
            if (sanpham == null)
            {
                sanpham = new MatHangMua(_masp);
                gioHang.Add(sanpham);
            }
            else
            {
                sanpham.SoLuong++;
            }

            _session["GioHang"] = gioHang;
        }
    }
}