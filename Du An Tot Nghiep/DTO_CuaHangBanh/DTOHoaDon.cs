using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO_CuaHangBanh
{
    public class DTOHoaDon
    {
        public string MaHoaDon { get; set; }
        public DateTime DateCheck { get; set; }
        public DateTime DateOut { get; set; }
        public string MaKhachHang { get; set; }
        public string MaNhanVien { get; set; }
        public int MaBan { get; set; }
        public string TenBan { get; set; }
        public decimal TongHoaDon { get; set; }
        public decimal GiamGia { get; set; }
        public string TrangThai { get; set; }
    }

}
