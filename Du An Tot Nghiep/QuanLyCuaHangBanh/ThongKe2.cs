using BLL_CuaHangBanh;
using System;
using System.Data;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace GUI_CuaHangBanh
{
    public partial class HoaDon : Form
    {
        private BUSThongKe busThongKe = new BUSThongKe();

        public HoaDon()
        {
            InitializeComponent();
        }

        private void ThongKe2_Load(object sender, EventArgs e)
        {
            LoadThongKe(null); // thống kê toàn bộ
            LoadBieuDo();      // biểu đồ doanh thu
        }

        private void btnThongKeNgay_Click(object sender, EventArgs e)
        {
            DateTime? ngayChon = dtpNgayThongKe.Value.Date;

            DataTable dtThongKe = busThongKe.GetThongKeTongQuat(ngayChon);
            DataTable dtDoanhThu = busThongKe.GetDoanhThuTheoThang(ngayChon);

            // -----------------------------
            // Thông tin tổng quát
            // -----------------------------
            if (dtThongKe.Rows.Count > 0)
            {
                decimal doanhThu = Convert.ToDecimal(dtThongKe.Rows[0]["TongDoanhThu"]);
                int soLuong = Convert.ToInt32(dtThongKe.Rows[0]["TongSanPham"]);
                string spBanChay = dtThongKe.Rows[0]["SanPhamBanChayNhat"].ToString();

                lblTongDoanhThu.Text = $"Tổng doanh thu: {doanhThu:N0} VNĐ";
                lblTongSP.Text = $"Tổng sản phẩm bán được: {soLuong}";
                lblSPBanChay.Text = $"Sản phẩm bán chạy nhất: {spBanChay}";
            }

            // ===========================
            // 1️⃣ Biểu đồ doanh thu theo tháng (Column)
            // ===========================
            chartDoanhThuThang.Series.Clear();
            chartDoanhThuThang.ChartAreas[0].AxisX.Title = "Tháng";
            chartDoanhThuThang.ChartAreas[0].AxisY.Title = "Doanh thu (VNĐ)";
            chartDoanhThuThang.ChartAreas[0].AxisX.Interval = 1;

            // Làm mượt giao diện
            chartDoanhThuThang.ChartAreas[0].AxisX.MajorGrid.LineColor = Color.LightGray;
            chartDoanhThuThang.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.LightGray;
            chartDoanhThuThang.ChartAreas[0].BackColor = Color.WhiteSmoke;

            Series seriesDoanhThu = new Series("Doanh thu");
            seriesDoanhThu.ChartType = SeriesChartType.Column;
            seriesDoanhThu.Color = Color.SkyBlue;
            seriesDoanhThu.BorderWidth = 2;
            seriesDoanhThu.IsValueShownAsLabel = true;
            seriesDoanhThu.LabelForeColor = Color.Black;
            seriesDoanhThu.Font = new Font("Segoe UI", 9, FontStyle.Bold);

            // Dữ liệu ví dụ
            Dictionary<int, double> doanhThuTheoThang = new Dictionary<int, double>()
    {
        {1, 1000000}, {2, 1500000}, {3, 1200000}, {4, 1800000},
        {5, 900000}, {6, 2000000}, {7, 1600000}, {8, 2100000}
    };

            foreach (var item in doanhThuTheoThang)
            {
                seriesDoanhThu.Points.AddXY("Tháng " + item.Key, item.Value);
            }

            chartDoanhThuThang.Series.Add(seriesDoanhThu);

            // Thêm tiêu đề
            chartDoanhThuThang.Titles.Clear();
            chartDoanhThuThang.Titles.Add("Biểu đồ doanh thu theo tháng");
            chartDoanhThuThang.Titles[0].Font = new Font("Segoe UI", 12, FontStyle.Bold);
            chartDoanhThuThang.Titles[0].ForeColor = Color.FromArgb(40, 40, 40);

            // ===========================
            // 2️⃣ Biểu đồ sản phẩm bán chạy (Pie)
            // ===========================
            chartSanPhamBanChay.Series.Clear();
            chartSanPhamBanChay.Legends[0].Enabled = true;
            chartSanPhamBanChay.Legends[0].Docking = Docking.Right;
            chartSanPhamBanChay.Legends[0].Font = new Font("Segoe UI", 9);

            Series seriesSanPham = new Series("Sản phẩm bán chạy");
            seriesSanPham.ChartType = SeriesChartType.Pie;
            seriesSanPham.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            seriesSanPham.IsValueShownAsLabel = true;
            seriesSanPham.LabelForeColor = Color.Black;

            // Dữ liệu ví dụ
            Dictionary<string, int> sanPhamBanChay = new Dictionary<string, int>()
    {
        {"Bánh kem", 50},
        {"Bánh su", 30},
        {"Bánh mì", 40},
        {"Bánh flan", 25},
        {"Bánh bông lan", 35}
    };

            foreach (var item in sanPhamBanChay)
            {
                int pointIndex = seriesSanPham.Points.AddXY(item.Key, item.Value);
                seriesSanPham.Points[pointIndex].Label = item.Key + ": #PERCENT{P1}";
            }

            // Đưa nhãn ra ngoài và vẽ đường nối
            seriesSanPham["PieLabelStyle"] = "Outside";
            seriesSanPham["PieLineColor"] = "Black";

            chartSanPhamBanChay.Series.Add(seriesSanPham);

            // Tiêu đề
            chartSanPhamBanChay.Titles.Clear();
            chartSanPhamBanChay.Titles.Add("Tỷ lệ sản phẩm bán chạy");
            chartSanPhamBanChay.Titles[0].Font = new Font("Segoe UI", 12, FontStyle.Bold);
            chartSanPhamBanChay.Titles[0].ForeColor = Color.FromArgb(40, 40, 40);
        }


        private void LoadThongKe(DateTime? ngay)
        {
            DataTable dt = busThongKe.GetThongKeTongQuat(ngay);

            if (dt != null && dt.Rows.Count > 0)
            {
                var row = dt.Rows[0];
                decimal doanhThu = row["TongDoanhThu"] == DBNull.Value ? 0 : Convert.ToDecimal(row["TongDoanhThu"]);
                int soLuong = row["TongSanPham"] == DBNull.Value ? 0 : Convert.ToInt32(row["TongSanPham"]);
                string spBanChay = row["SanPhamBanChayNhat"]?.ToString() ?? "-";

                lblTongDoanhThu.Text = $"Tổng doanh thu: {doanhThu:N0} VNĐ";
                lblTongSP.Text = $"Tổng sản phẩm bán được: {soLuong}";
                lblSPBanChay.Text = $"Sản phẩm bán chạy nhất: {spBanChay}";
            }
            else
            {
                lblTongDoanhThu.Text = "Tổng doanh thu: 0 VNĐ";
                lblTongSP.Text = "Tổng sản phẩm bán được: 0";
                lblSPBanChay.Text = "Sản phẩm bán chạy nhất: -";
            }
        }

        private void LoadBieuDo()
        {
            DataTable dt = busThongKe.GetDoanhThuTheoThang();

            chartDoanhThu.Series.Clear();
            var series = new Series("DoanhThu")
            {
                ChartType = SeriesChartType.Column,
                IsValueShownAsLabel = true
            };

            if (dt != null)
            {
                foreach (DataRow r in dt.Rows)
                {
                    series.Points.AddXY("Tháng " + r["Thang"].ToString(), r["TongDoanhThu"]);
                }
            }

            chartDoanhThu.Series.Add(series);
            if (chartDoanhThu.ChartAreas.Count > 0)
            {
                chartDoanhThu.ChartAreas[0].AxisX.Title = "Tháng";
                chartDoanhThu.ChartAreas[0].AxisY.Title = "Doanh thu (VNĐ)";
            }
        }

        private void guna2HtmlLabel1_Click(object sender, EventArgs e)
        {

        }
    }
}
