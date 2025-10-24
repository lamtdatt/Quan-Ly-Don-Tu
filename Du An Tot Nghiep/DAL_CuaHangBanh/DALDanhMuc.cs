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
    public class DALDanhMuc
    {
        public List<DTODanhMuc> GetAll()
        {
            string sql = "SELECT * FROM DanhMuc WHERE Xoa = 0";
            SqlDataReader reader = DBUtil.Query(sql, new List<object>(), CommandType.Text);
            List<DTODanhMuc> list = new List<DTODanhMuc>();
            while (reader.Read())
            {
                DTODanhMuc dm = new DTODanhMuc
                {
                    MaDanhMuc = Convert.ToInt32(reader["MaDanhMuc"]),
                    TenDanhMuc = reader["TenDanhMuc"].ToString(),
                    Xoa = Convert.ToInt32(reader["Xoa"])
                };
                list.Add(dm);
            }
            reader.Close();
            return list;
        }
        public void Insert(DTODanhMuc dm)
        {
            string sql = "INSERT INTO DanhMuc (TenDanhMuc, Xoa) VALUES (@0, @1)";
            List<object> args = new List<object> { dm.TenDanhMuc, dm.Xoa };
            DBUtil.Update(sql, args);
        }

        public void Update(DTODanhMuc dm)
        {
            string sql = "UPDATE DanhMuc SET TenDanhMuc = @0, Xoa = @1 WHERE MaDanhMuc = @2";
            List<object> args = new List<object> { dm.TenDanhMuc, dm.Xoa, dm.MaDanhMuc };
            DBUtil.Update(sql, args);
        }

        public void Delete(int maDanhMuc)
        {
            string sql = "UPDATE DanhMuc SET Xoa = 1 WHERE MaDanhMuc = @0";
            List<object> args = new List<object> { maDanhMuc };
            DBUtil.Update(sql, args);
        }
    }
}
    