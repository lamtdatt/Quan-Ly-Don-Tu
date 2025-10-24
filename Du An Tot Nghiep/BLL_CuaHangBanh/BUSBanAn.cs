using System.Collections.Generic;
using DAL_CuaHangBanh;
using DTO_CuaHangBanh;

namespace BLL_CuaHangBanh
{
    public class BUSBanAn
    {
        private DALBanAn dal = new DALBanAn();

        public List<DTOBanAn> LayDanhSach()
        {
            return dal.GetAll();
        }
        public DTOBanAn TimBanTheoMa(int maBan)
        {
            return dal.TimBanTheoMa(maBan);
        }
        public void CapNhatTrangThai(int maBan, string trangThai)
        {
            dal.UpdateTrangThai(maBan, trangThai);
        }


        public DTOBanAn LayTheoMa(int maBan)
        {
            return dal.GetById(maBan);
        }

        public void Them(DTOBanAn ban)
        {
            dal.Insert(ban);
        }

        public void CapNhat(DTOBanAn ban)
        {
            dal.Update(ban);
        }

        public void Xoa(int maBan)
        {
            dal.Delete(maBan);
        }
    }
}
