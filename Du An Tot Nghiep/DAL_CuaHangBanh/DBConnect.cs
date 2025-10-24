using System;
using System.Data;
using System.Data.SqlClient;

namespace DAL_CuaHangBanh
{
    public class DBConnect
    {
        // 👉 Kết nối đến SQL Server
        protected SqlConnection _conn = new SqlConnection(
            "Data Source=.;Initial Catalog=DATN_QLCuaHangBanh;Integrated Security=True"
        );

        // 👉 Hàm lấy dữ liệu (SELECT)
        public DataTable GetData(string query, SqlParameter[] parameters = null)
        {
            DataTable dt = new DataTable();
            try
            {
                SqlCommand cmd = new SqlCommand(query, _conn);
                if (parameters != null)
                    cmd.Parameters.AddRange(parameters);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi truy vấn dữ liệu: " + ex.Message);
            }
            return dt;
        }

        // 👉 Hàm thực thi câu lệnh không trả về dữ liệu (INSERT, UPDATE, DELETE)
        public int ExecuteNonQuery(string query, SqlParameter[] parameters = null)
        {
            int result = 0;
            try
            {
                _conn.Open();
                SqlCommand cmd = new SqlCommand(query, _conn);
                if (parameters != null)
                    cmd.Parameters.AddRange(parameters);
                result = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi thực thi câu lệnh SQL: " + ex.Message);
            }
            finally
            {
                _conn.Close();
            }
            return result;
        }
    }
}
