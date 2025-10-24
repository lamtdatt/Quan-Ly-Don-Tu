using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO_CuaHangBanh;


namespace DAL_CuaHangBanh
{
    public class DALTaiKhoan
    {
        public DTOTaiKhoan DangNhap(string tenDangNhap, string matKhau)
        {
            string sql = "SELECT * FROM DangNhap WHERE TenDangNhap = @0 AND MatKhau = @1 AND Xoa = 0";
            List<object> parameters = new List<object> { tenDangNhap, matKhau };

            using (SqlDataReader reader = DBUtil.Query(sql, parameters))
            {
                if (reader.Read())
                {
                    return new DTOTaiKhoan
                    {
                        TenDangNhap = reader["TenDangNhap"].ToString(),
                        MatKhau = reader["MatKhau"].ToString(),
                        MaNhanVien = reader["MaNhanVien"].ToString(),
                        Xoa = Convert.ToInt32(reader["Xoa"])
                    };
                }
            }
            return null;
        }
    }
}
