using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO_CuaHangBanh;

namespace DAL_CuaHangBanh
{
    public class DALNhanVien
    {
        public List<DTONhanVien> GetAllNhanVien()
        {
            List<DTONhanVien> ds = new List<DTONhanVien>();

            // ✅ Lấy cả cột Xoa
            string query = "SELECT MaNhanVien, HoTen, Luong, DiaChi, SDT, Email, GioiTinh, CaLamViec, Xoa FROM NhanVien";

            using (SqlDataReader reader = DBUtil.Query(query, new List<object>()))
            {
                while (reader.Read())
                {
                    DTONhanVien nv = new DTONhanVien
                    {
                        MaNhanVien = Convert.ToInt32(reader["MaNhanVien"]),
                        HoTen = reader["HoTen"].ToString(),
                        Luong = Convert.ToDecimal(reader["Luong"]),
                        DiaChi = reader["DiaChi"].ToString(),
                        SDT = reader["SDT"].ToString(),
                        Email = reader["Email"].ToString(),
                        GioiTinh = reader["GioiTinh"].ToString(),
                        CaLamViec = reader["CaLamViec"].ToString(),
                        Xoa = Convert.ToBoolean(reader["Xoa"])
                    };
                    ds.Add(nv);
                }
            }
            return ds;
        }

        public void Insert(DTONhanVien nv)
        {
            string query = "INSERT INTO NhanVien(HoTen, Luong, DiaChi, SDT, Email, GioiTinh, CaLamViec, Xoa) VALUES (@0, @1, @2, @3, @4, @5, @6, 0)";
            List<object> args = new List<object>
            {
                nv.HoTen, nv.Luong, nv.DiaChi, nv.SDT, nv.Email, nv.GioiTinh, nv.CaLamViec
            };
            DBUtil.Update(query, args);
        }

        public void Update(DTONhanVien nv)
        {
            string query = "UPDATE NhanVien SET HoTen = @0, Luong = @1, DiaChi = @2, SDT = @3, Email = @4, GioiTinh = @5, CaLamViec = @6 WHERE MaNhanVien = @7";
            List<object> args = new List<object>
            {
                nv.HoTen, nv.Luong, nv.DiaChi, nv.SDT, nv.Email, nv.GioiTinh, nv.CaLamViec, nv.MaNhanVien
            };
            DBUtil.Update(query, args);
        }

        public void Delete(int maNV)
        {
            string query = "DELETE FROM NhanVien WHERE MaNhanVien = @0";
            List<object> args = new List<object> { maNV };
            DBUtil.Update(query, args);

        }
}
}