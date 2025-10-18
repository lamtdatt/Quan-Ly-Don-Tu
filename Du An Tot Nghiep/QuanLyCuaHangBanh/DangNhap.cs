using GUI_CuaHangBanh;
using BLL_CuaHangBanh;
using UTIL_CuaHangBanh;

namespace QuanLyCuaHangBanh
{
    public partial class DangNhap : Form
    {
        public DangNhap()
        {
            InitializeComponent();

        }

        private void button1_Click(object sender, EventArgs e)
        {
        }
        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            string tenDN = txtTenDangNhap.Text.Trim();
            string mk = txtMatKhau.Text.Trim();

            BUSTaiKhoan bus = new BUSTaiKhoan();
            var result = bus.KiemTraDangNhap(tenDN, mk);

            if (result != null)
            {
                this.Hide();
                ManHinhChinh frm = new ManHinhChinh(result.MaNhanVien);
                frm.Show();
            }
            else
            {
                MessageBox.Show("Sai tài khoản hoặc mật khẩu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn thoát?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
    }

    }
    }

