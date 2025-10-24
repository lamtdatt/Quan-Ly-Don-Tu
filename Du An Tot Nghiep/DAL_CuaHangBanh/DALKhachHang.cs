using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO_CuaHangBanh;

    namespace DAL_CuaHangBanh
    {
        public class DALKhachHang
        {
            public List<DTOKhachHang> GetAllKhachHang()
            {
                List<DTOKhachHang> ds = new List<DTOKhachHang>();
                string query = "SELECT MaKhachHang, HoTen, SDT FROM KhachHang";
                using (SqlDataReader reader = DBUtil.Query(query, new List<object>()))
                {
                    while (reader.Read())
                    {
                        DTOKhachHang kh = new DTOKhachHang
                        {
                            MaKhachHang = Convert.ToInt32(reader["MaKhachHang"]),
                            HoTen = reader["HoTen"].ToString(),
                            SDT = reader["SDT"].ToString()
                        };
                        ds.Add(kh);
                    }
                }
                return ds;
            }
            public bool InsertKhachHang(DTOKhachHang kh)
            {
                string query = "INSERT INTO KhachHang (HoTen, SDT) VALUES (@0, @1)";
                List<object> args = new List<object> { kh.HoTen, kh.SDT };
                try
                {
                    DBUtil.Update(query, args);
                    return true;
                }
                catch
                {
                    return false;
                }
            }

            public bool UpdateKhachHang(DTOKhachHang kh)
            {
                string query = "UPDATE KhachHang SET HoTen = @0, SDT = @1 WHERE MaKhachHang = @2";
                List<object> args = new List<object> { kh.HoTen, kh.SDT, kh.MaKhachHang };
                try
                {
                    DBUtil.Update(query, args);
                    return true;
                }
                catch
                {
                    return false;
                }
            }
            public bool DeleteKhachHang(string maKH)
            {
                string query = "DELETE FROM KhachHang WHERE MaKhachHang = @0";
                List<object> args = new List<object> { maKH };
                try
                {
                    DBUtil.Update(query, args);
                    return true;
                }
                catch
                {
                    return false;
                }
            }
            public DTOKhachHang GetKhachHangBySDT(string sdt)
            {
                string query = "SELECT TOP 1 MaKhachHang, HoTen, SDT FROM KhachHang WHERE SDT = @0";
                List<object> args = new List<object> { sdt };
                using (SqlDataReader reader = DBUtil.Query(query, args))
                {
                    if (reader.Read())
                    {
                        return new DTOKhachHang
                        {
                            MaKhachHang = Convert.ToInt32(reader["MaKhachHang"]),
                            HoTen = reader["HoTen"].ToString(),
                            SDT = reader["SDT"].ToString()
                        };
                    }
                }
                return null;
            }
        }
    }
