using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL_CuaHangBanh;
using DTO_CuaHangBanh;

namespace BLL_CuaHangBanh
{
    public class BUSNhanVien
    {
        private DALNhanVien dal = new DALNhanVien();

        public List<DTONhanVien> LayDanhSach()
        {
            return dal.GetAllNhanVien();
        }
        public void ThemNhanVien(DTONhanVien nv)
        {
            dal.Insert(nv);
        }

        public void CapNhatNhanVien(DTONhanVien nv)
        {
            dal.Update(nv);
        }

        public void XoaNhanVien(int maNV)
        {
            DALNhanVien dal = new DALNhanVien();
            dal.Delete(maNV);
        }
}
}
