using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Guna.UI2.WinForms;
using System.Reflection;

namespace GUI_CuaHangBanh
{
    public partial class ManHinhChinh : Form
    {
        private string tenDangNhap;
        private Guna2Button currentButton;
        public ManHinhChinh(string maNhanVien)
        {
            InitializeComponent();
            this.Size = new Size(1520, 783);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.tenDangNhap = maNhanVien;

            // Gán Dock Fill cho các button menu
            foreach (Control ctrl in tableLayoutPanel1.Controls)
            {
                if (ctrl is Guna2Button btn)
                {
                    btn.Dock = DockStyle.Fill;
                    btn.Click += MenuButton_Click; // Gắn sự kiện Click
                }
            }
        }
        private void LoadForm(Form frm)
        {
            panelChinhH.Controls.Clear();
            frm.TopLevel = false;
            frm.FormBorderStyle = FormBorderStyle.None;
            frm.Dock = DockStyle.Fill;
            frm.Size = panelChinhH.ClientSize;
            panelChinhH.Controls.Add(frm);
            frm.BringToFront();
            frm.Show();
        }
        private void ManHinhChinh_Load(object sender, EventArgs e)
        {
            panel1.Dock = DockStyle.Top;
            panelMenu.Dock = DockStyle.Left;
            panelChinhH.Dock = DockStyle.Fill;
        }

        private void MenuButton_Click(object sender, EventArgs e)
        {
            var clickedBtn = sender as Guna2Button;
            if (clickedBtn == null) return;

            // Reset màu toàn bộ button
            foreach (Control ctrl in tableLayoutPanel1.Controls)
            {
                if (ctrl is Guna2Button btn)
                {
                    btn.FillColor = Color.White;
                    btn.ForeColor = Color.Black;
                }
            }

            // Gắn màu cho button đang chọn
            clickedBtn.FillColor = Color.Orange;
            clickedBtn.ForeColor = Color.Black;
            currentButton = clickedBtn;

            // Load form tương ứng
            switch (clickedBtn.Name)
            {
                case "btnDatHang":
                    LoadForm(new DatHang());
                    break;
                case "btnSanPham":
                    LoadForm(new SanPham());
                    break;
                case "btnNhanVien":
                    LoadForm(new NhanVien());
                    break;
                case "btnBanAn":
                    LoadForm(new BanAN());
                    break;
                case "btnKhachHang":
                    LoadForm(new KhachHang());
                    break;
                case "btnThongKe":
                    LoadForm(new ThongKe());
                    break;
                case "btnDanhMuc":
                    LoadForm(new DanhMuc());
                    break;
                case "btnHoaDon":
                    LoadForm(new HoaDon());
                    break;
            }
        }

        private void btnDatHang_Click(object sender, EventArgs e)
        {
            var btn = (Guna.UI2.WinForms.Guna2Button)sender;
            HighlightButton(btn);
            LoadForm(new DatHang());
        }
        private void HighlightButton(Guna2Button btn)
        {
            foreach (Control ctrl in tableLayoutPanel1.Controls)
            {
                if (ctrl is Button button)
                {
                    // Reset màu tất cả các nút về trắng
                    btn.BackColor = Color.White;
                    btn.ForeColor = Color.Black;
                }
            }
            currentButton = btn;
            currentButton.FillColor = Color.FromArgb(230, 230, 230);
            currentButton.FillColor = Color.Orange;
        }

        private void btnTaiKhoan_Click(object sender, EventArgs e)
        {

        }

        private void btnSanPham_Click(object sender, EventArgs e)
        {
            var btn = (Guna.UI2.WinForms.Guna2Button)sender;
            HighlightButton(btn);
            LoadForm(new SanPham());
        }

        private void btnThongKe_Click(object sender, EventArgs e)
        {
            var btn = (Guna.UI2.WinForms.Guna2Button)sender;
            HighlightButton(btn);
            LoadForm(new ThongKe());
        }

        private void btnNhanVien_Click(object sender, EventArgs e)
        {
            var btn = (Guna.UI2.WinForms.Guna2Button)sender;
            HighlightButton(btn);
            LoadForm(new NhanVien());
        }

        private void btnBanAn_Click(object sender, EventArgs e)
        {
            var btn = (Guna.UI2.WinForms.Guna2Button)sender;
            HighlightButton(btn);
            LoadForm(new BanAN());
        }

        private void btnKhachHang_Click(object sender, EventArgs e)
        {
            var btn = (Guna.UI2.WinForms.Guna2Button)sender;
            HighlightButton(btn);
            LoadForm(new KhachHang());
        }

        private void btnDanhMuc_Click(object sender, EventArgs e)
        {
            var btn = (Guna.UI2.WinForms.Guna2Button)sender;
            HighlightButton(btn);
            LoadForm(new DanhMuc());
        }

        private void btnDatHang_Click_1(object sender, EventArgs e)
        {
            var btn = (Guna.UI2.WinForms.Guna2Button)sender;
            HighlightButton(btn);
            LoadForm(new DatHang());
        }

        private void btnDanhMuc_Click_1(object sender, EventArgs e)
        {
            var btn = (Guna.UI2.WinForms.Guna2Button)sender;
            HighlightButton(btn);
            LoadForm(new DanhMuc());
        }

        private void btnBanAn_Click_1(object sender, EventArgs e)
        {
            var btn = (Guna.UI2.WinForms.Guna2Button)sender;
            HighlightButton(btn);
            LoadForm(new BanAN());
        }

        private void btnKhachHangg_Click(object sender, EventArgs e)
        {
            var btn = (Guna.UI2.WinForms.Guna2Button)sender;
            HighlightButton(btn);
            LoadForm(new KhachHang());
        }

        private void btnThongKe_Click_1(object sender, EventArgs e)
        {
            var btn = (Guna.UI2.WinForms.Guna2Button)sender;
            HighlightButton(btn);
            LoadForm(new ThongKe());
        }

        private void btnNhanVien_Click_1(object sender, EventArgs e)
        {
            var btn = (Guna.UI2.WinForms.Guna2Button)sender;
            HighlightButton(btn);
            LoadForm(new NhanVien());
        }

        private void btnSanPham_Click_1(object sender, EventArgs e)
        {
            var btn = (Guna.UI2.WinForms.Guna2Button)sender;
            HighlightButton(btn);
            LoadForm(new SanPham());
        }

        private void panelchinhH_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            var btn = (Guna.UI2.WinForms.Guna2Button)sender;
            HighlightButton(btn);
            LoadForm(new DatHang());
        }

        private void btnSanPham_Click_2(object sender, EventArgs e)
        {
            var btn = (Guna.UI2.WinForms.Guna2Button)sender;
            HighlightButton(btn);
            LoadForm(new SanPham());
        }

        private void btnThongKe_Click_2(object sender, EventArgs e)
        {
            var btn = (Guna.UI2.WinForms.Guna2Button)sender;
            HighlightButton(btn);
            LoadForm(new ThongKe());
        }

        private void btnNhanVien_Click_2(object sender, EventArgs e)
        {
            var btn = (Guna.UI2.WinForms.Guna2Button)sender;
            HighlightButton(btn);
            LoadForm(new NhanVien());
        }

        private void btnBanAn_Click_2(object sender, EventArgs e)
        {
            var btn = (Guna.UI2.WinForms.Guna2Button)sender;
            HighlightButton(btn);
            LoadForm(new BanAN());
        }

        private void btnKhachHangg_Click_1(object sender, EventArgs e)
        {
            var btn = (Guna.UI2.WinForms.Guna2Button)sender;
            HighlightButton(btn);
            LoadForm(new DanhMuc());
        }

        private void btnDanhMuc_Click_2(object sender, EventArgs e)
        {
            var btn = (Guna.UI2.WinForms.Guna2Button)sender;
            HighlightButton(btn);
            LoadForm(new DanhMuc());
        }

        private void guna2Button_Click(object sender, EventArgs e)
        {
            var btn = (Guna.UI2.WinForms.Guna2Button)sender;
            HighlightButton(btn);
            LoadForm(new BanAN());
            btnBanAn.Dock = DockStyle.Fill;

        }

        private void btnDatHang_Click_2(object sender, EventArgs e)
        {
            var btn = (Guna.UI2.WinForms.Guna2Button)sender;
            HighlightButton(btn);
            LoadForm(new DatHang());
            btnDatHang.Dock = DockStyle.Fill;

        }

        private void btnSanPham_Click_3(object sender, EventArgs e)
        {
            var btn = (Guna.UI2.WinForms.Guna2Button)sender;
            HighlightButton(btn);
            LoadForm(new SanPham());
            btnSanPham.Dock = DockStyle.Fill;

        }

        private void btnNhanVien_Click_3(object sender, EventArgs e)
        {
            var btn = (Guna.UI2.WinForms.Guna2Button)sender;
            HighlightButton(btn);
            LoadForm(new NhanVien());
            btnNhanVien.Dock = DockStyle.Fill;

        }

        private void btnThongKe_Click_3(object sender, EventArgs e)
        {
            var btn = (Guna.UI2.WinForms.Guna2Button)sender;
            HighlightButton(btn);
            LoadForm(new HoaDon());
            btnThongKe.Dock = DockStyle.Fill;

        }

        private void btnKhachHangg_Click_2(object sender, EventArgs e)
        {
            var btn = (Guna.UI2.WinForms.Guna2Button)sender;
            HighlightButton(btn);
            LoadForm(new KhachHang());
            btn.Dock = DockStyle.Fill;

        }

        private void btnDanhMuc_Click_3(object sender, EventArgs e)
        {
            var btn = (Guna.UI2.WinForms.Guna2Button)sender;
            HighlightButton(btn);
            LoadForm(new DanhMuc());
            btnDanhMuc.Dock = DockStyle.Fill;

        }

        private void btnBanAn_Click_3(object sender, EventArgs e)
        {
            var btn = (Guna.UI2.WinForms.Guna2Button)sender;
            HighlightButton(btn);
            LoadForm(new BanAN());
            btnBanAn.Dock = DockStyle.Fill;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblThoiGian.Text = DateTime.Now.ToString("HH:mm:ss - dd/MM/yyyy");
        }

        private void btnHoaDon_Click(object sender, EventArgs e)
        {
            var btn = (Guna.UI2.WinForms.Guna2Button)sender;
            HighlightButton(btn);
            LoadForm(new ThongKe());
            btnThongKe.Dock = DockStyle.Fill;
        }
    }
}


