using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using BLL_CuaHangBanh;
using DAL_CuaHangBanh;
using DTO_CuaHangBanh;

namespace GUI_CuaHangBanh
{
    public partial class KhachHang : Form
    {
        private BUSKhachHang bus = new BUSKhachHang();
        BUSKhachHang busKH = new BUSKhachHang();


        public KhachHang()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Load += KhachHang_Load;
        }
        private void guna2PictureBox2_Click(object sender, EventArgs e)
        {
        }
        private void KhachHang_Load(object sender, EventArgs e)
        {
            LoadDanhSachKhacHhang();
        }
        private void LoadDanhSachKhacHhang()
        {
            dgvKhachHang.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            List<DTOKhachHang> ds = bus.LayDanhSachKhachHang();
            dgvKhachHang.DataSource = ds;

            if (dgvKhachHang.Columns.Contains("MaKhachHang"))
                dgvKhachHang.Columns["MaKhachHang"].HeaderText = "Mã Khách Hàng";

            if (dgvKhachHang.Columns.Contains("HoTen"))
                dgvKhachHang.Columns["HoTen"].HeaderText = "Họ Tên";

            if (dgvKhachHang.Columns.Contains("SDT"))
                dgvKhachHang.Columns["SDT"].HeaderText = "SĐT";
        }

        private void dgvKhachHang_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvKhachHang.Rows[e.RowIndex];
                txtMaKhachHang.Text = row.Cells["MaKhachHang"].Value.ToString();
                txtTenKH.Text = row.Cells["HoTen"].Value.ToString();
                txtSDTKH.Text = row.Cells["SDT"].Value.ToString();
            }
        }
        private void ResetForm()
        {
            txtMaKhachHang.Clear();
            txtTenKH.Clear();
            txtSDTKH.Clear();
        }
        private void btnThemKH_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTenKH.Text))
            {
                MessageBox.Show("Vui lòng nhập tên khách hàng.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenKH.Focus();
                return;
            }
            if (txtTenKH.Text.All(char.IsDigit))
            {
                MessageBox.Show("Tên khách hàng không hợp lệ! Không được toàn là số.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenKH.Focus();
                return;
            }
            if (!Regex.IsMatch(txtSDTKH.Text.Trim(), @"^\d{9,11}$"))
            {
                MessageBox.Show("Số điện thoại không hợp lệ! Phải là số và từ 9 đến 11 chữ số.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSDTKH.Focus();
                return;
            }
            DTOKhachHang kh = new DTOKhachHang
            {
                HoTen = txtTenKH.Text.Trim(),
                SDT = txtSDTKH.Text.Trim()
            };
            BUSKhachHang bus = new BUSKhachHang();
            if (bus.ThemKhachHang(kh))
            {
                MessageBox.Show("Thêm khách hàng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadDanhSachKhacHhang();
                ResetForm();
            }
            else
            {
                MessageBox.Show("Thêm khách hàng thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnSuaKH_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaKhachHang.Text))
            {
                MessageBox.Show("Vui lòng chọn khách hàng cần sửa!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(txtTenKH.Text) || txtTenKH.Text.All(char.IsDigit))
            {
                MessageBox.Show("Tên khách hàng không hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!Regex.IsMatch(txtSDTKH.Text.Trim(), @"^\d{9,11}$"))
            {
                MessageBox.Show("Số điện thoại không hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!int.TryParse(txtMaKhachHang.Text, out int maKH))
            {
                MessageBox.Show("Mã khách hàng không hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            DTOKhachHang kh = new DTOKhachHang
            {
                MaKhachHang = maKH,
                HoTen = txtTenKH.Text.Trim(),
                SDT = txtSDTKH.Text.Trim()
            };
            try
            {
                BUSKhachHang bus = new BUSKhachHang();
                bus.CapNhatKhachHang(kh);
                MessageBox.Show("Cập nhật khách hàng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadDanhSachKhacHhang();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi cập nhật: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnXoaKhachHang_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaKhachHang.Text))
            {
                MessageBox.Show("Vui lòng chọn khách hàng để xóa!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa khách hàng này không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                string maKH = txtMaKhachHang.Text.Trim();
                BUSKhachHang bus = new BUSKhachHang();
                bool success = bus.XoaKhachHang(maKH);

                if (success)
                {
                    MessageBox.Show("Xóa khách hàng thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadDanhSachKhacHhang();
                    ResetForm();
                }
                else
                {
                    MessageBox.Show("Xóa thất bại. Có thể khách hàng đang được sử dụng ở bảng khác!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnLamMoiKH_Click(object sender, EventArgs e)
        {
            txtMaKhachHang.Clear();
            txtTenKH.Clear();
            txtSDTKH.Clear();
            txtTimKiemKH.Clear();
            dgvKhachHang.DataSource = busKH.LayDanhSachKhachHang()
             .Select(kh => new
             {
                 kh.MaKhachHang,
                 kh.HoTen,
                 kh.SDT
             }).ToList();

            txtTenKH.Focus();
        }

        private void btnTimKiemKH_Click(object sender, EventArgs e)
        {
            string tuKhoa = txtTimKiemKH.Text.ToLower();
            var ds = busKH.LayDanhSachKhachHang()
                          .Where(kh => kh.HoTen.ToLower().Contains(tuKhoa) || kh.SDT.Contains(tuKhoa))
                          .Select(kh => new
                          {
                              kh.MaKhachHang,
                              kh.HoTen,
                              kh.SDT
                          }).ToList();

            dgvKhachHang.DataSource = ds;
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void txtMaKhachHang_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtTenKH_TextChanged(object sender, EventArgs e)
        {

        }
    }
}

