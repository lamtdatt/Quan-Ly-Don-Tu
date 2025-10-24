namespace GUI_CuaHangBanh
{
    partial class HoaDon
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea3 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend3 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            lblTongDoanhThu = new Guna.UI2.WinForms.Guna2HtmlLabel();
            lblTongSP = new Guna.UI2.WinForms.Guna2HtmlLabel();
            lblSPBanChay = new Guna.UI2.WinForms.Guna2HtmlLabel();
            chartDoanhThu = new System.Windows.Forms.DataVisualization.Charting.Chart();
            dtpNgayThongKe = new Guna.UI2.WinForms.Guna2DateTimePicker();
            btnThongKeNgay = new Guna.UI2.WinForms.Guna2Button();
            chartSanPhamBanChay = new System.Windows.Forms.DataVisualization.Charting.Chart();
            chartDoanhThuThang = new System.Windows.Forms.DataVisualization.Charting.Chart();
            label1 = new Label();
            ((System.ComponentModel.ISupportInitialize)chartDoanhThu).BeginInit();
            ((System.ComponentModel.ISupportInitialize)chartSanPhamBanChay).BeginInit();
            ((System.ComponentModel.ISupportInitialize)chartDoanhThuThang).BeginInit();
            SuspendLayout();
            // 
            // lblTongDoanhThu
            // 
            lblTongDoanhThu.BackColor = Color.Transparent;
            lblTongDoanhThu.Font = new Font("Segoe UI Semibold", 10.8F, FontStyle.Bold);
            lblTongDoanhThu.Location = new Point(62, 55);
            lblTongDoanhThu.Name = "lblTongDoanhThu";
            lblTongDoanhThu.Size = new Size(136, 27);
            lblTongDoanhThu.TabIndex = 0;
            lblTongDoanhThu.Text = "Tổng doanh thu";
            // 
            // lblTongSP
            // 
            lblTongSP.BackColor = Color.Transparent;
            lblTongSP.Font = new Font("Segoe UI Semibold", 10.8F, FontStyle.Bold);
            lblTongSP.Location = new Point(62, 128);
            lblTongSP.Name = "lblTongSP";
            lblTongSP.Size = new Size(216, 27);
            lblTongSP.TabIndex = 1;
            lblTongSP.Text = "Tống sản phẩm bán được: ";
            // 
            // lblSPBanChay
            // 
            lblSPBanChay.BackColor = Color.Transparent;
            lblSPBanChay.Font = new Font("Segoe UI Semibold", 10.8F, FontStyle.Bold);
            lblSPBanChay.Location = new Point(62, 195);
            lblSPBanChay.Name = "lblSPBanChay";
            lblSPBanChay.Size = new Size(207, 27);
            lblSPBanChay.TabIndex = 2;
            lblSPBanChay.Text = "Sản phẩm bán chạy nhất: ";
            // 
            // chartDoanhThu
            // 
            chartArea1.Name = "ChartArea1";
            chartDoanhThu.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            chartDoanhThu.Legends.Add(legend1);
            chartDoanhThu.Location = new Point(62, 383);
            chartDoanhThu.Name = "chartDoanhThu";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            chartDoanhThu.Series.Add(series1);
            chartDoanhThu.Size = new Size(615, 402);
            chartDoanhThu.TabIndex = 3;
            chartDoanhThu.Text = "chart1";
            chartDoanhThu.Click += chartDoanhThu_Click;
            // 
            // dtpNgayThongKe
            // 
            dtpNgayThongKe.Checked = true;
            dtpNgayThongKe.CustomFormat = "dd/MM/yyyy";
            dtpNgayThongKe.CustomizableEdges = customizableEdges1;
            dtpNgayThongKe.Font = new Font("Segoe UI", 9F);
            dtpNgayThongKe.Format = DateTimePickerFormat.Custom;
            dtpNgayThongKe.Location = new Point(62, 274);
            dtpNgayThongKe.MaxDate = new DateTime(9998, 12, 31, 0, 0, 0, 0);
            dtpNgayThongKe.MinDate = new DateTime(1753, 1, 1, 0, 0, 0, 0);
            dtpNgayThongKe.Name = "dtpNgayThongKe";
            dtpNgayThongKe.ShadowDecoration.CustomizableEdges = customizableEdges2;
            dtpNgayThongKe.Size = new Size(250, 45);
            dtpNgayThongKe.TabIndex = 4;
            dtpNgayThongKe.Value = new DateTime(2025, 10, 22, 3, 10, 16, 418);
            // 
            // btnThongKeNgay
            // 
            btnThongKeNgay.CustomizableEdges = customizableEdges3;
            btnThongKeNgay.DisabledState.BorderColor = Color.DarkGray;
            btnThongKeNgay.DisabledState.CustomBorderColor = Color.DarkGray;
            btnThongKeNgay.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            btnThongKeNgay.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            btnThongKeNgay.Font = new Font("Segoe UI", 9F);
            btnThongKeNgay.ForeColor = Color.White;
            btnThongKeNgay.Location = new Point(335, 274);
            btnThongKeNgay.Name = "btnThongKeNgay";
            btnThongKeNgay.ShadowDecoration.CustomizableEdges = customizableEdges4;
            btnThongKeNgay.Size = new Size(225, 45);
            btnThongKeNgay.TabIndex = 5;
            btnThongKeNgay.Text = "Thống kê";
            btnThongKeNgay.Click += btnThongKeNgay_Click;
            // 
            // chartSanPhamBanChay
            // 
            chartArea2.Name = "ChartArea1";
            chartSanPhamBanChay.ChartAreas.Add(chartArea2);
            legend2.Name = "Legend1";
            chartSanPhamBanChay.Legends.Add(legend2);
            chartSanPhamBanChay.Location = new Point(771, 383);
            chartSanPhamBanChay.Name = "chartSanPhamBanChay";
            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Pie;
            series2.Legend = "Legend1";
            series2.Name = "Series1";
            chartSanPhamBanChay.Series.Add(series2);
            chartSanPhamBanChay.Size = new Size(565, 412);
            chartSanPhamBanChay.TabIndex = 6;
            chartSanPhamBanChay.Text = "chart1";
            // 
            // chartDoanhThuThang
            // 
            chartArea3.Name = "ChartArea1";
            chartDoanhThuThang.ChartAreas.Add(chartArea3);
            legend3.Name = "Legend1";
            chartDoanhThuThang.Legends.Add(legend3);
            chartDoanhThuThang.Location = new Point(771, 128);
            chartDoanhThuThang.Name = "chartDoanhThuThang";
            series3.ChartArea = "ChartArea1";
            series3.Legend = "Legend1";
            series3.Name = "Series1";
            chartDoanhThuThang.Series.Add(series3);
            chartDoanhThuThang.Size = new Size(565, 237);
            chartDoanhThuThang.TabIndex = 7;
            chartDoanhThuThang.Text = "chart2";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 24F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(513, 9);
            label1.Name = "label1";
            label1.Size = new Size(494, 54);
            label1.TabIndex = 8;
            label1.Text = "DOANH THU BÁN HÀNG";
            // 
            // HoaDon
            // 
            AutoScaleDimensions = new SizeF(14F, 35F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1453, 953);
            Controls.Add(label1);
            Controls.Add(chartDoanhThuThang);
            Controls.Add(chartSanPhamBanChay);
            Controls.Add(btnThongKeNgay);
            Controls.Add(dtpNgayThongKe);
            Controls.Add(chartDoanhThu);
            Controls.Add(lblSPBanChay);
            Controls.Add(lblTongSP);
            Controls.Add(lblTongDoanhThu);
            Font = new Font("Segoe UI", 15F);
            Margin = new Padding(5);
            Name = "HoaDon";
            Text = "Thống Kê Bán Hàng";
            Load += ThongKe2_Load;
            ((System.ComponentModel.ISupportInitialize)chartDoanhThu).EndInit();
            ((System.ComponentModel.ISupportInitialize)chartSanPhamBanChay).EndInit();
            ((System.ComponentModel.ISupportInitialize)chartDoanhThuThang).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Guna.UI2.WinForms.Guna2HtmlLabel lblTongDoanhThu;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblTongSP;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblSPBanChay;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartDoanhThu;
        private Guna.UI2.WinForms.Guna2DateTimePicker dtpNgayThongKe;
        private Guna.UI2.WinForms.Guna2Button btnThongKeNgay;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartSanPhamBanChay;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartDoanhThuThang;
        private Label label1;
    }
}