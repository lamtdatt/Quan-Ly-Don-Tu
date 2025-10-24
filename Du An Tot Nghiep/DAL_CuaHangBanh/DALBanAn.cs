using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using DTO_CuaHangBanh;

namespace DAL_CuaHangBanh
{
    public class DALBanAn
    {
        public List<DTOBanAn> GetAll()
        {
            List<DTOBanAn> list = new List<DTOBanAn>();
            string sql = "SELECT * FROM Ban";
            SqlDataReader reader = DBUtil.Query(sql, new List<object>(), CommandType.Text);

            while (reader.Read())
            {
                DTOBanAn dto = new DTOBanAn();
                dto.MaBan = Convert.ToInt32(reader["MaBan"]);
                dto.TenBan = reader["TenBan"].ToString();
                dto.TrangThai = reader["TrangThai"].ToString();
                list.Add(dto);
            }

            reader.Close();
            return list;
        }
        public DTOBanAn TimBanTheoMa(int maBan)
        {
            string sql = "SELECT * FROM Ban WHERE MaBan = @0";
            List<object> args = new List<object> { maBan };
            SqlDataReader reader = DBUtil.Query(sql, args, CommandType.Text);

            if (reader.Read())
            {
                DTOBanAn dto = new DTOBanAn();
                dto.MaBan = Convert.ToInt32(reader["MaBan"]);
                dto.TenBan = reader["TenBan"].ToString();
                dto.TrangThai = reader["TrangThai"].ToString();
                reader.Close();
                return dto;
            }

            reader.Close();
            return null;
        }
        public void UpdateTrangThai(int maBan, string trangThai)
        {
            string sql = "UPDATE Ban SET TrangThai = @0 WHERE MaBan = @1";
            List<object> args = new List<object> { trangThai, maBan };
            DBUtil.Update(sql, args);
        }


        public DTOBanAn GetById(int maBan)
        {
            string sql = "SELECT * FROM Ban WHERE MaBan = @0";
            List<object> args = new List<object> { maBan };
            SqlDataReader reader = DBUtil.Query(sql, args, CommandType.Text);

            DTOBanAn dto = null;
            if (reader.Read())
            {
                dto = new DTOBanAn();
                dto.MaBan = Convert.ToInt32(reader["MaBan"]);
                dto.TenBan = reader["TenBan"].ToString();
                dto.TrangThai = reader["TrangThai"].ToString();
            }

            reader.Close();
            return dto;
        }

        public void Insert(DTOBanAn ban)
        {
            string sql = "INSERT INTO Ban (TenBan, TrangThai) VALUES (@0, @1)";
            List<object> args = new List<object> { ban.TenBan, ban.TrangThai };
            DBUtil.Update(sql, args);
        }

        public void Update(DTOBanAn ban)
        {
            string sql = "UPDATE Ban SET TenBan = @0, TrangThai = @1 WHERE MaBan = @2";
            List<object> args = new List<object> { ban.TenBan, ban.TrangThai, ban.MaBan };
            DBUtil.Update(sql, args);
        }

        public void Delete(int maBan)
        {
            string sql = "DELETE FROM Ban WHERE MaBan = @0";
            List<object> args = new List<object> { maBan };
            DBUtil.Update(sql, args);
        }
    }
}
