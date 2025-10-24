using System.Collections.Generic;
using System.Data;
using DTO_CuaHangBanh;
using DAL_CuaHangBanh;
using System.Data.SqlClient;

public class DALSanPham
{
    public List<DTOSanPham> GetAll()
    {
        List<DTOSanPham> ds = new List<DTOSanPham>();
        string query = "SELECT * FROM SanPham WHERE Xoa = 0";

        using (SqlDataReader reader = DBUtil.Query(query, new List<object>()))
        {
            while (reader.Read())
            {
                DTOSanPham sp = new DTOSanPham
                {
                    MaSanPham = Convert.ToInt32(reader["MaSanPham"]),
                    MaDanhMuc = Convert.ToInt32(reader["MaDanhMuc"]),
                    TenSanPham = reader["TenSanPham"].ToString(),
                    SoLuong = Convert.ToInt32(reader["SoLuong"]),
                    MoTa = reader["MoTa"].ToString(),
                    DonGia = Convert.ToDecimal(reader["DonGia"]),
                    HinhAnh = reader["HinhAnh"].ToString(),
                    Xoa = Convert.ToBoolean(reader["Xoa"]) 
                };
                ds.Add(sp);
            }
        }

        return ds;
    }

    public string LayMaSanPhamTheoTen(string tenSP)
    {
        string sql = "SELECT MaSanPham FROM SanPham WHERE TenSanPham = @0";
        List<object> args = new List<object>() { tenSP };

        object result = DBUtil.Value(sql, args);
        return result?.ToString();
    }
    public void Insert(DTOSanPham sp)
    {
        string sql = "INSERT INTO SanPham(MaDanhMuc, TenSanPham, SoLuong, MoTa, DonGia, HinhAnh, Xoa) VALUES (@0, @1, @2, @3, @4, @5, 0)";
        List<object> args = new List<object>
    {
        sp.MaDanhMuc,
        sp.TenSanPham,
        sp.SoLuong,
        sp.MoTa,
        sp.DonGia,
        sp.HinhAnh ?? (object)DBNull.Value   
    };
        DBUtil.Update(sql, args);
    }
    public void Update(DTOSanPham sp)
    {
        string sql = "UPDATE SanPham SET MaDanhMuc = @0, TenSanPham = @1, SoLuong = @2, MoTa = @3, DonGia = @4, HinhAnh = @5 WHERE MaSanPham = @6";
        List<object> args = new List<object>
    {
        sp.MaDanhMuc,
        sp.TenSanPham,
        sp.SoLuong,
        sp.MoTa,
        sp.DonGia,
        sp.HinhAnh ?? (object)DBNull.Value, 
        sp.MaSanPham
    };
        DBUtil.Update(sql, args);
    }
    public void CapNhatSoLuongSauKhiBan(string maSP, int soLuongMua)
    {
        string sql = "UPDATE SanPham SET SoLuong = SoLuong - @0 WHERE MaSanPham = @1";
        List<object> args = new List<object>() { soLuongMua, maSP };
        DBUtil.Update(sql, args);
    }


public void Delete(int maSP)
    {
        string query = "DELETE FROM SanPham WHERE MaSanPham = @0";
        List<object> args = new List<object> { maSP };
        DBUtil.Update(query, args);
    }
}
