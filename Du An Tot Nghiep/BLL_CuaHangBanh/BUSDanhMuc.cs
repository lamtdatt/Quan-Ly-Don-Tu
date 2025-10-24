using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL_CuaHangBanh;
using DTO_CuaHangBanh;

namespace BLL_CuaHangBanh
{
    public class BUSDanhMuc
    {
        private DALDanhMuc dal = new DALDanhMuc();

        public List<DTODanhMuc> LayDanhSach()
        {
            return dal.GetAll();
        }

        public void ThemDanhMuc(DTODanhMuc dm)
        {
            dal.Insert(dm);
        }

        public void CapNhatDanhMuc(DTODanhMuc dm)
        {
            dal.Update(dm);
        }

        public void XoaDanhMuc(int maDanhMuc)
        {
            dal.Delete(maDanhMuc);
        }
    }
}
    
