using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL_CuaHangBanh;
using DTO_CuaHangBanh;

namespace BLL_CuaHangBanh
{
    public class BUSHoaDon
    {
        DALHoaDon hoaDonDAL = new DALHoaDon();
        public bool XoaHoaDonVaKhachHang(int maHoaDon, int maKhachHang)
        {
            return hoaDonDAL.XoaHoaDonVaKhachHang(maHoaDon, maKhachHang);
        }
        public DTOHoaDon LayHoaDonTheoMa(int maHoaDon)
        {
            return hoaDonDAL.GetById(maHoaDon);
        }

        public static DataTable LayTatCaHoaDon()
        {
            return DALThongKe.TK_HoaDon();
        }

        public bool XoaHoaDon(int maHoaDon)
        {
            return hoaDonDAL.XoaHoaDon(maHoaDon);
        }

        public static DataTable LayChiTietHoaDon(string maHD)
        {
            return DALThongKe.TK_ChiTietHoaDon(maHD);
        }

        public void ThemHoaDon(DTOHoaDon hd)
        {
            hoaDonDAL.InsertHoaDon(hd);
        }
        public DTOHoaDon LayHoaDonMoiNhat()
        {
            return hoaDonDAL.GetLastHoaDon();
        }
    }
}
