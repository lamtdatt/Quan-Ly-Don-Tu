using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace DAL_CuaHangBanh
{
    public class DBUtil
    {
        private static string connString = @"Data Source=ADMIN\TIENDAT1;Initial Catalog=DATN_QLCuaHangBanh;Integrated Security=True";

        /// <summary> Xây dựng SqlCommand </summary>
        public static SqlCommand GetCommand(string sql, List<Object> args, CommandType cmdType)
        {
            SqlConnection conn = new SqlConnection(connString);
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.CommandType = cmdType;
            for (int i = 0; i < args.Count; i++)
            {
                cmd.Parameters.AddWithValue($"@{i}", args[i]);
            }
            return cmd;
        }
        /// <summary> Thực hiện lệnh SQL thao tác Insert–Delete–Update </summary>
        public static void Update(string sql, List<Object> args, CommandType cmdType = CommandType.Text)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                using (SqlTransaction transaction = conn.BeginTransaction())
                using (SqlCommand cmd = new SqlCommand(sql, conn, transaction))
                {
                    cmd.CommandType = cmdType;

                    for (int i = 0; i < args.Count; i++)
                    {
                        cmd.Parameters.AddWithValue($"@{i}", args[i] ?? DBNull.Value);
                    }

                    try
                    {
                        cmd.ExecuteNonQuery();
                        transaction.Commit();
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        throw;
                    }
        }
            }
        }
        
        /// <summary> Thực hiện lệnh SQL truy vấn (select) dữ liệu </summary>
        public static SqlDataReader Query(string sql, List<Object> args, CommandType cmdType = CommandType.Text)
        {
            try
            {
                SqlCommand cmd = GetCommand(sql, args, cmdType);
                cmd.Connection.Open();
                return cmd.ExecuteReader();
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary> Thực hiện lệnh SQL truy vấn (select) dữ liệu </summary>
        public static Object Value(string sql, List<Object> args, CommandType cmdType = CommandType.Text)
        {
            try
            {
                SqlCommand cmd = GetCommand(sql, args, cmdType);
                cmd.Connection.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return reader.IsDBNull(0) ? null : reader.GetValue(0);
                    }
                    return null;
                }
            }
            catch (Exception)
            {
                throw;
            }

        }

        public object ScalarQuery(string sql, List<object> thamSo)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                SqlCommand cmd = new SqlCommand(sql, conn);
                for (int i = 0; i < thamSo.Count; i++)
                {
                    cmd.Parameters.AddWithValue("@" + i, thamSo[i]);
                }
                conn.Open();
                return cmd.ExecuteScalar();
            }
        }

        /// <summary> Truy vấn dữ liệu với tham số tên rõ ràng, dùng cho stored procedure </summary>
        public static SqlDataReader QueryWithNamedParams(string sql, Dictionary<string, object> namedParams, CommandType cmdType = CommandType.StoredProcedure)
        {
            SqlConnection conn = new SqlConnection(connString);
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.CommandType = cmdType;

            if (namedParams != null)
            {
                foreach (var param in namedParams)
                {
                    cmd.Parameters.AddWithValue(param.Key, param.Value ?? DBNull.Value);
                }
            }

            conn.Open();
            return cmd.ExecuteReader(CommandBehavior.CloseConnection);
        }

        public static DataTable GetAllInvoices()
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                SqlCommand cmd = new SqlCommand("sp_GetAllInvoices", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }
        public static DataTable GetInvoiceDetails(int maHoaDon)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                SqlCommand cmd = new SqlCommand("sp_GetInvoiceDetails", conn);  
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@MaHoaDon", maHoaDon);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }


    }
}


    
