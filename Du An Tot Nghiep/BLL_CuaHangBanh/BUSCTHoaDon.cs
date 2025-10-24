using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL_CuaHangBanh;
using DTO_CuaHangBanh;
public class BUSCTHoaDon
{
    DALCTHoaDon ctDAL = new DALCTHoaDon();

    public int ThemChiTietHoaDon(DTOCTHoaDon ct)
    {
        return ctDAL.InsertCTHoaDon(ct);
    }
    public List<DTOChiTietSPTheoBan> LayChiTietTheoHoaDon(int maHoaDon)
    {
        return ctDAL.GetByMaHoaDon(maHoaDon);

    }
}
