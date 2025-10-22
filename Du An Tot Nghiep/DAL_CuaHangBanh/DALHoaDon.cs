using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using DTO_CuaHangBanh;

namespace DAL_CuaHangBanh
{
    public class DALHoaDon
    {
        private static string connString =
          @"Data Source=ADMIN\TIENDAT1;Initial Catalog=DATN_QLCuaHangBanh;Integrated Security=True;Encrypt=False";
        public void InsertHoaDon(DTOHoaDon hd)
        {
            string sql = "INSERT INTO HoaDon (DateCheck, DateOut, MaNhanVien, MaKhachHang, MaBan, TrangThai, TongHoaDon, GiamGia) " +
                         "VALUES (@0, @1, @2, @3, @4, @5, @6, @7)";

            List<object> args = new List<object>
            {
                hd.DateCheck,
                hd.DateOut,
                hd.MaNhanVien,
                hd.MaKhachHang,
                hd.MaBan,
                hd.TrangThai,
                hd.TongHoaDon,
                hd.GiamGia
            };

            DBUtil.Update(sql, args);
        }

        public bool XoaHoaDon(int maHoaDon)
        {
            try
            {
                string sqlCT = "DELETE FROM ChiTietHoaDon WHERE MaHoaDon = @0";
                DBUtil.Update(sqlCT, new List<object> { maHoaDon });
                string sqlHD = "DELETE FROM HoaDon WHERE MaHoaDon = @0";
                DBUtil.Update(sqlHD, new List<object> { maHoaDon });

                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool XoaHoaDonVaKhachHang(int maHoaDon, int maKhachHang)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                SqlTransaction tran = conn.BeginTransaction();

                try
                {
                    // Xóa chi tiết hóa đơn trước
                    SqlCommand cmdCT = new SqlCommand("DELETE FROM CT_HoaDon WHERE MaHoaDon = @MaHoaDon", conn, tran);
                    cmdCT.Parameters.AddWithValue("@MaHoaDon", maHoaDon);
                    cmdCT.ExecuteNonQuery();

                    // Xóa hóa đơn
                    SqlCommand cmdHD = new SqlCommand("DELETE FROM HoaDon WHERE MaHoaDon = @MaHoaDon", conn, tran);
                    cmdHD.Parameters.AddWithValue("@MaHoaDon", maHoaDon);
                    cmdHD.ExecuteNonQuery();

                    // Xóa khách hàng
                    SqlCommand cmdKH = new SqlCommand("DELETE FROM KhachHang WHERE MaKhachHang = @MaKhachHang", conn, tran);
                    cmdKH.Parameters.AddWithValue("@MaKhachHang", maKhachHang);
                    cmdKH.ExecuteNonQuery();

                    tran.Commit();
                    return true;
                }
                catch
                {
                    tran.Rollback();
                    return false;
                }
            }
        }
        public DataTable LayHoaDonTheoNgay(DateTime ngay)
        {
            string query = "SELECT * FROM HoaDon WHERE CAST(DateCheck AS DATE) = @0";
            List<object> parameters = new List<object>() { ngay.Date };

            using (SqlDataReader reader = DBUtil.Query(query, parameters, CommandType.Text))
            {
                DataTable dt = new DataTable();
                dt.Load(reader);
                return dt;

            }
        }
        public DTOHoaDon GetById(int maHoaDon)
        {
            string sql = @"
        SELECT 
            hd.MaHoaDon,
            hd.DateCheck,
            hd.DateOut,
            hd.MaNhanVien,
            hd.MaKhachHang,
            hd.MaBan,
            b.TenBan,
            hd.TrangThai,
            hd.TongHoaDon,
            hd.GiamGia
        FROM HoaDon hd
        JOIN Ban b ON hd.MaBan = b.MaBan
        WHERE hd.MaHoaDon = @0";

            DTOHoaDon hd = null;

            using (SqlDataReader reader = DBUtil.Query(sql, new List<object> { maHoaDon }))
            {
                if (reader.Read())
                {
                    hd = new DTOHoaDon
                    {
                        MaHoaDon = reader["MaHoaDon"]?.ToString(),
                        DateCheck = reader["DateCheck"] is DBNull ? DateTime.Now : Convert.ToDateTime(reader["DateCheck"]),
                        DateOut = reader["DateOut"] is DBNull ? DateTime.Now : Convert.ToDateTime(reader["DateOut"]),
                        MaNhanVien = reader["MaNhanVien"]?.ToString(),
                        MaKhachHang = reader["MaKhachHang"]?.ToString(),
                        MaBan = Convert.ToInt32(reader["MaBan"]),
                        TenBan = reader["TenBan"]?.ToString(),
                        TrangThai = reader["TrangThai"]?.ToString(),
                        TongHoaDon = reader["TongHoaDon"] is DBNull ? 0 : Convert.ToDecimal(reader["TongHoaDon"]),
                        GiamGia = reader["GiamGia"] is DBNull ? 0 : Convert.ToDecimal(reader["GiamGia"])
                    };
                }
            }

            return hd;
        }
        public DTOHoaDon GetLastHoaDon()
        {
            string sql = @"
        SELECT TOP 1 
            hd.MaHoaDon,
            hd.DateCheck,
            hd.DateOut,
            hd.MaNhanVien,
            hd.MaKhachHang,
            hd.MaBan,
            b.TenBan,
            hd.TrangThai,
            hd.TongHoaDon,
            hd.GiamGia
        FROM HoaDon hd
        JOIN Ban b ON hd.MaBan = b.MaBan
        ORDER BY MaHoaDon DESC";

            DTOHoaDon hd = null;

            using (SqlDataReader reader = DBUtil.Query(sql, new List<object>()))
            {
                if (reader.Read())
                {
                    hd = new DTOHoaDon
                    {
                        MaHoaDon = reader["MaHoaDon"]?.ToString(),
                        DateCheck = reader["DateCheck"] is DBNull ? DateTime.Now : Convert.ToDateTime(reader["DateCheck"]),
                        DateOut = reader["DateOut"] is DBNull ? DateTime.Now : Convert.ToDateTime(reader["DateOut"]),
                        MaNhanVien = reader["MaNhanVien"]?.ToString(),
                        MaKhachHang = reader["MaKhachHang"]?.ToString(),
                        MaBan = Convert.ToInt32(reader["MaBan"]),
                        TenBan = reader["TenBan"]?.ToString(), 
                        TrangThai = reader["TrangThai"]?.ToString(),
                        TongHoaDon = reader["TongHoaDon"] is DBNull ? 0 : Convert.ToDecimal(reader["TongHoaDon"]),
                        GiamGia = reader["GiamGia"] is DBNull ? 0 : Convert.ToDecimal(reader["GiamGia"])
                    };
                }
            }

            return hd;
        }

    }
}