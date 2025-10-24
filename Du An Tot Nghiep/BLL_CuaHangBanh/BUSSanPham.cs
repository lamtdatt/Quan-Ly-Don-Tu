    using System.Collections.Generic;
    using DAL_CuaHangBanh;
    using DTO_CuaHangBanh;

    public class BUSSanPham
    {
        private DALSanPham dal = new DALSanPham();

        public List<DTOSanPham> LayDanhSach()
        {
            return dal.GetAll();
        }

    public string LayMaSanPhamTheoTen(string tenSP)
    {
        return dal.LayMaSanPhamTheoTen(tenSP);
    }

    public void ThemSanPham(DTOSanPham sp)
        {
            dal.Insert(sp);
        }

        public void CapNhatSanPham(DTOSanPham sp)
        {
            dal.Update(sp);
        }

        public void XoaSanPham(int maSP)
        {
            dal.Delete(maSP);
        }
    }
