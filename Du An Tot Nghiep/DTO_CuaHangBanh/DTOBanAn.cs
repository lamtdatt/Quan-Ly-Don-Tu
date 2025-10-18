using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO_CuaHangBanh
{
    public class DTOBanAn
    {
        public int MaBan { get; set; }
        public string TenBan { get; set; }
        public string TrangThai { get; set; }
        public DTOBanAn() { }
        public DTOBanAn(int maBan, string tenBan, string trangThai)
        {
            MaBan = maBan;
            TenBan = tenBan;
            TrangThai = trangThai;
        }
    }
}
