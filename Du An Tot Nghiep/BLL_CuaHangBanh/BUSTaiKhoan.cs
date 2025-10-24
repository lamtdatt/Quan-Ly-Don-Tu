using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL_CuaHangBanh;
using DTO_CuaHangBanh;


namespace BLL_CuaHangBanh
{
    public class BUSTaiKhoan
    {
        DALTaiKhoan dal = new DALTaiKhoan();

        public DTOTaiKhoan KiemTraDangNhap(string tenDangNhap, string matKhau)
        {
            return dal.DangNhap(tenDangNhap, matKhau);
        }
    }
}
