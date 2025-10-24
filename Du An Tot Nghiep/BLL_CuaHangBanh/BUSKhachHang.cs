    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using DAL_CuaHangBanh;
    using DTO_CuaHangBanh;

    namespace BLL_CuaHangBanh
    {
        public class BUSKhachHang
        {
            private DALKhachHang dalKH = new DALKhachHang();

            public List<DTOKhachHang> LayDanhSachKhachHang()
            {
                return dalKH.GetAllKhachHang();
            }

            public bool ThemKhachHang(DTOKhachHang kh)
            {
                return dalKH.InsertKhachHang(kh);
            }

            public bool CapNhatKhachHang(DTOKhachHang kh)
            {
                return dalKH.UpdateKhachHang(kh);
            }

            public bool XoaKhachHang(string maKH)
            {
                return dalKH.DeleteKhachHang(maKH);
            }

            public DTOKhachHang TimKhachHangTheoSDT(string sdt)
            {
                return dalKH.GetKhachHangBySDT(sdt);
            }
        public string LayMaKhachHangTheoSDT(string sdt)
        {
            var khach = dalKH.GetKhachHangBySDT(sdt);
            return khach != null ? khach.MaKhachHang.ToString() : null;
        }

    }
}