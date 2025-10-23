using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DAL_CuaHangBanh
{
    public class DALThongKe
    {
        // -----------------------------
        // Lấy danh sách hóa đơn
        // -----------------------------
        public static DataTable TK_HoaDon()
        {
            string sql = "sp_GetAllInvoices";
            SqlDataReader reader = DBUtil.Query(sql, null, CommandType.StoredProcedure);
            DataTable dt = new DataTable();
            dt.Load(reader);
            return dt;
        }

        // -----------------------------
        // Lấy chi tiết hóa đơn
        // -----------------------------
        public static DataTable TK_ChiTietHoaDon(string maHD)
        {
            string sql = "sp_GetInvoiceDetails";
            var parameters = new Dictionary<string, object>
            {
                { "@MaHoaDon", maHD }
            };

            SqlDataReader reader = DBUtil.QueryWithNamedParams(sql, parameters, CommandType.StoredProcedure);
            DataTable dt = new DataTable();
            dt.Load(reader);
            return dt;
        }

        // ==============================
        // Class con xử lý thống kê
        // ==============================
        public class DAL_ThongKe
        {
            private string connStr = @"Data Source=ADMIN\TIENDAT1;Initial Catalog=DATN_QLCuaHangBanh;Integrated Security=True";

            // -----------------------------
            // Thống kê tổng quát (theo ngày hoặc toàn bộ)
            // -----------------------------
            public DataTable GetThongKeTongQuat(DateTime? ngay = null)
            {
                string query = @"
                    SELECT TOP 1
                        SUM(CT.SoLuong * SP.DonGia) AS TongDoanhThu,
                        SUM(CT.SoLuong) AS TongSanPham,
                        SP.TenSanPham AS SanPhamBanChayNhat
                    FROM HoaDon HD
                    JOIN CT_HoaDon CT ON HD.MaHoaDon = CT.MaHoaDon
                    JOIN SanPham SP ON CT.MaSanPham = SP.MaSanPham
                    WHERE (@Ngay IS NULL OR CAST(HD.DateCheck AS DATE) = @Ngay)
                    GROUP BY SP.TenSanPham
                    ORDER BY SUM(CT.SoLuong) DESC;
                ";

                using (SqlConnection conn = new SqlConnection(connStr))
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Ngay", (object)ngay ?? DBNull.Value);
                    var dt = new DataTable();
                    conn.Open();
                    using (var rdr = cmd.ExecuteReader())
                    {
                        dt.Load(rdr);
                    }
                    return dt;
                }
            }

            // -----------------------------
            // Doanh thu theo tháng (toàn bộ)
            // -----------------------------
            public DataTable GetDoanhThuTheoThang()
            {
                string query = @"
                    SELECT MONTH(DateCheck) AS Thang,
                           SUM(CT.SoLuong * SP.DonGia) AS TongDoanhThu
                    FROM HoaDon HD
                    JOIN CT_HoaDon CT ON HD.MaHoaDon = CT.MaHoaDon
                    JOIN SanPham SP ON CT.MaSanPham = SP.MaSanPham
                    GROUP BY MONTH(DateCheck)
                    ORDER BY Thang;";

                return GetDataByQuery(query);
            }

            // -----------------------------
            // Doanh thu theo tháng (lọc theo ngày)
            // -----------------------------
            public DataTable GetDoanhThuTheoThang(DateTime? ngay = null)
            {
                string query = @"
                    SELECT 
                        MONTH(DateCheck) AS Thang,
                        SUM(CT.SoLuong * SP.DonGia) AS TongDoanhThu
                    FROM HoaDon HD
                    JOIN CT_HoaDon CT ON HD.MaHoaDon = CT.MaHoaDon
                    JOIN SanPham SP ON CT.MaSanPham = SP.MaSanPham
                    WHERE (@Ngay IS NULL OR CAST(HD.DateCheck AS DATE) = @Ngay)
                    GROUP BY MONTH(DateCheck)
                    ORDER BY Thang;";

                using (SqlConnection conn = new SqlConnection(connStr))
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Ngay", (object)ngay ?? DBNull.Value);
                    var dt = new DataTable();
                    conn.Open();
                    using (var rdr = cmd.ExecuteReader())
                    {
                        dt.Load(rdr);
                    }
                    return dt;
                }
            }

            // -----------------------------
            // Helper
            // -----------------------------
            private DataTable GetDataByQuery(string query)
            {
                var dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(connStr))
                using (SqlCommand cmd = new SqlCommand(query, conn))
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    da.Fill(dt);
                }
                return dt;
            }
            public DataTable GetDoanhThu7NgayGanNhat()
            {
                string query = @"
        SELECT TOP 7 
            CAST(HD.DateCheck AS DATE) AS Ngay,
            SUM(CT.SoLuong * SP.DonGia) AS TongDoanhThu
        FROM HoaDon HD
        JOIN CT_HoaDon CT ON HD.MaHoaDon = CT.MaHoaDon
        JOIN SanPham SP ON CT.MaSanPham = SP.MaSanPham
        GROUP BY CAST(HD.DateCheck AS DATE)
        ORDER BY Ngay DESC;";

                return GetDataByQuery(query);
            }

            // =====================================
            // Top 5 sản phẩm bán chạy nhất
            // =====================================
            public DataTable GetTopSanPhamBanChay()
            {
                string query = @"
        SELECT TOP 5 
            SP.TenSanPham,
            SUM(CT.SoLuong) AS TongSoLuong
        FROM CT_HoaDon CT
        JOIN SanPham SP ON CT.MaSanPham = SP.MaSanPham
        GROUP BY SP.TenSanPham
        ORDER BY TongSoLuong DESC;";

                return GetDataByQuery(query);
            }
        }

    }
}
