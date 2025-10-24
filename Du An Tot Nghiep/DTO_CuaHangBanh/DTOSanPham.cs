using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO_CuaHangBanh
{
    public class DTOSanPham
    {

        public int MaSanPham { get; set; }
        public int MaDanhMuc { get; set; }
        public string TenSanPham { get; set; }
        public int SoLuong { get; set; }
        public string MoTa { get; set; }
        public decimal DonGia { get; set; }
        public string HinhAnh { get; set; }
        public bool Xoa { get; set; }
    }

    }


