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

            // ===========================
            // ⚠️ Kiểm tra có dữ liệu không
            // ===========================
            if (dtThongKe == null || dtThongKe.Rows.Count == 0 ||
                dtThongKe.Rows[0]["TongDoanhThu"] == DBNull.Value)
            {
                MessageBox.Show(
                    $"Không có báo cáo thống kê cho ngày {ngayChon:dd/MM/yyyy}!",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

                // Xóa thông tin và biểu đồ cũ (nếu có)
                lblTongDoanhThu.Text = "Tổng doanh thu: 0 VNĐ";
                lblTongSP.Text = "Tổng sản phẩm bán được: 0";
                lblSPBanChay.Text = "Sản phẩm bán chạy nhất: Không có dữ liệu";

                chartDoanhThuThang.Series.Clear();
                chartSanPhamBanChay.Series.Clear();
                return;
            }

            // ===========================
            // 1️⃣ Thông tin tổng quát
            // ===========================
            decimal doanhThu = dtThongKe.Rows[0]["TongDoanhThu"] == DBNull.Value ? 0 : Convert.ToDecimal(dtThongKe.Rows[0]["TongDoanhThu"]);
            int soLuong = dtThongKe.Rows[0]["TongSanPham"] == DBNull.Value ? 0 : Convert.ToInt32(dtThongKe.Rows[0]["TongSanPham"]);
            string spBanChay = dtThongKe.Rows[0]["SanPhamBanChayNhat"] == DBNull.Value ? "Không có" : dtThongKe.Rows[0]["SanPhamBanChayNhat"].ToString();

            lblTongDoanhThu.Text = $"Tổng doanh thu: {doanhThu:N0} VNĐ";
            lblTongSP.Text = $"Tổng sản phẩm bán được: {soLuong}";
            lblSPBanChay.Text = $"Sản phẩm bán chạy nhất: {spBanChay}";

            // ===========================
            // 2️⃣ Biểu đồ doanh thu theo tháng (Column)
            // ===========================
            chartDoanhThuThang.Series.Clear();
            chartDoanhThuThang.ChartAreas[0].AxisX.Title = "Tháng";
            chartDoanhThuThang.ChartAreas[0].AxisY.Title = "Doanh thu (VNĐ)";
            chartDoanhThuThang.ChartAreas[0].AxisX.Interval = 1;
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

            if (dtDoanhThu != null && dtDoanhThu.Rows.Count > 0)
            {
                foreach (DataRow row in dtDoanhThu.Rows)
                {
                    int thang = Convert.ToInt32(row["Thang"]);
                    double tongDoanhThu = Convert.ToDouble(row["TongDoanhThu"]);
                    seriesDoanhThu.Points.AddXY("Tháng " + thang, tongDoanhThu);
                }
            }

            chartDoanhThuThang.Series.Add(seriesDoanhThu);
            chartDoanhThuThang.Titles.Clear();
            chartDoanhThuThang.Titles.Add("Biểu đồ doanh thu theo tháng");
            chartDoanhThuThang.Titles[0].Font = new Font("Segoe UI", 12, FontStyle.Bold);
            chartDoanhThuThang.Titles[0].ForeColor = Color.FromArgb(40, 40, 40);

            // ===========================
            // 3️⃣ Biểu đồ sản phẩm bán chạy (Pie)
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
            seriesSanPham["PieLabelStyle"] = "Outside";
            seriesSanPham["PieLineColor"] = "Black";

            // Dữ liệu thật từ DB
            DataTable dtTiLeSP = busThongKe.GetTiLeSanPhamBanChay(ngayChon);
            if (dtTiLeSP != null && dtTiLeSP.Rows.Count > 0)
            {
                foreach (DataRow row in dtTiLeSP.Rows)
                {
                    string tenSP = row["TenSanPham"].ToString();
                    int soLuongBan = Convert.ToInt32(row["SoLuongBan"]);
                    int pointIndex = seriesSanPham.Points.AddXY(tenSP, soLuongBan);
                    seriesSanPham.Points[pointIndex].Label = tenSP + ": #PERCENT{P1}";
                }
            }

            chartSanPhamBanChay.Series.Add(seriesSanPham);
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

        private void chartDoanhThu_Click(object sender, EventArgs e)
        {

        }
    }
}
