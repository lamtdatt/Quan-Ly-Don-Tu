using DAL_CuaHangBanh;
using System;
using System.Data;

namespace BLL_CuaHangBanh
{
    public class BUSThongKe
    {
        private DALThongKe.DAL_ThongKe dalThongKe = new DALThongKe.DAL_ThongKe();

        // -----------------------------
        // Lấy danh sách hóa đơn
        // -----------------------------
        public static DataTable LayTatCaHoaDon()
        {
            return DBUtil.GetAllInvoices();
        }

        // -----------------------------
        // Lấy chi tiết hóa đơn
        // -----------------------------
        public static DataTable LayChiTietHoaDon(int maHD)
        {
            return DBUtil.GetInvoiceDetails(maHD);
        }

        // -----------------------------
        // Thống kê tổng quát
        // -----------------------------
        public DataTable GetThongKeTongQuat(DateTime? ngay = null)
        {
            return dalThongKe.GetThongKeTongQuat(ngay);
        }

        // -----------------------------
        // Doanh thu theo tháng
        // -----------------------------
        public DataTable GetDoanhThuTheoThang(DateTime? ngay = null)
        {
            return dalThongKe.GetDoanhThuTheoThang(ngay);
        }

        // -----------------------------
        // Doanh thu 7 ngày gần nhất
        // -----------------------------
        public DataTable LayDoanhThu7NgayGanNhat()
        {
            return dalThongKe.GetDoanhThu7NgayGanNhat();
        }

        // -----------------------------
        // Top sản phẩm bán chạy
        // -----------------------------
        public DataTable LayTopSanPhamBanChay()
        {
            return dalThongKe.GetTopSanPhamBanChay();
        }
    }
}
