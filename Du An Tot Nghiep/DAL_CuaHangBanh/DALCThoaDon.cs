    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using DTO_CuaHangBanh;

    namespace DAL_CuaHangBanh
    {
        public class DALCTHoaDon
        {
            public int InsertCTHoaDon(DTOCTHoaDon ct)
            {
                string sql = "INSERT INTO CT_HoaDon (MaHoaDon, MaSanPham, SoLuong) " +
                             "VALUES (@0, @1, @2)";

                List<object> parameters = new List<object>
                {
                    ct.MaHoaDon,
                    ct.MaSanPham,
                    ct.SoLuong
                };

                DBUtil.Update(sql, parameters); 
                return 1; 
            }
        public List<DTOChiTietSPTheoBan> GetChiTietSPTheoHoaDon(string maHD)
        {
            string sql = @"
        SELECT 
            sp.TenSanPham, 
            cthd.SoLuong, 
            sp.DonGia
        FROM CT_HoaDon cthd
        JOIN SanPham sp ON cthd.MaSanPham = sp.MaSanPham
        WHERE cthd.MaHoaDon = @0";

            List<object> parameters = new List<object> { maHD };

            List<DTOChiTietSPTheoBan> list = new List<DTOChiTietSPTheoBan>();
            var reader = DBUtil.Query(sql, parameters);

            while (reader.Read())
            {
                list.Add(new DTOChiTietSPTheoBan
                {
                    TenSanPham = reader["TenSanPham"].ToString(),
                    SoLuong = Convert.ToInt32(reader["SoLuong"]),
                    DonGia = Convert.ToInt32(reader["DonGia"])
                });
            }

            reader.Close();
            return list;
        }
        public List<DTOChiTietSPTheoBan> GetByMaHoaDon(int maHoaDon)
        {
            string sql = @"
    SELECT 
        sp.TenSanPham,
        cthd.SoLuong,
        sp.DonGia
    FROM CT_HoaDon cthd
    JOIN SanPham sp ON cthd.MaSanPham = sp.MaSanPham
    WHERE cthd.MaHoaDon = @0";

            List<DTOChiTietSPTheoBan> list = new List<DTOChiTietSPTheoBan>();

            using (SqlDataReader reader = DBUtil.Query(sql, new List<object> { maHoaDon }))
            {
                while (reader.Read())
                {
                    list.Add(new DTOChiTietSPTheoBan
                    {
                        TenSanPham = reader["TenSanPham"].ToString(),
                        SoLuong = Convert.ToInt32(reader["SoLuong"]),
                        DonGia = Convert.ToInt32(reader["DonGia"])
                    });
                }
            }

            return list;
        }
        public List<DTOCTHoaDon> GetCTHoaDonByMaHD(string maHD)
            {
                string sql = "SELECT * FROM CT_HoaDon WHERE MaHoaDon = @0";
                List<object> parameters = new List<object>
                {
                    maHD
                };

                List<DTOCTHoaDon> list = new List<DTOCTHoaDon>();
                var reader = DBUtil.Query(sql, parameters);

                while (reader.Read())
                {
                    DTOCTHoaDon ct = new DTOCTHoaDon
                    {
                        MaCTHoaDon = reader["MaCTHoaDon"].ToString(),
                        MaHoaDon = reader["MaHoaDon"].ToString(),
                        MaSanPham = reader["MaSanPham"].ToString(),
                        SoLuong = Convert.ToInt32(reader["SoLuong"])
                    };

                    list.Add(ct);
                }

                reader.Close();
                return list;
            }
     
        
        }

    }


