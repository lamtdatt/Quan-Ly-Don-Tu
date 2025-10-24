using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BLL_CuaHangBanh;
using DAL_CuaHangBanh;
using DTO_CuaHangBanh;

namespace GUI_CuaHangBanh
{
    public partial class DanhMuc : Form
    {
        private BUSDanhMuc bus = new BUSDanhMuc();


        public DanhMuc()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
        }
        private void DanhMuc_Load(object sender, EventArgs e)
        {
            LoadDanhMuc();
        }
        private void LoadDanhMuc()
        {
            dgvDanhMuc.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            List<DTODanhMuc> ds = bus.LayDanhSach();
            dgvDanhMuc.DataSource = ds;

            dgvDanhMuc.Columns["MaDanhMuc"].HeaderText = "Mã Danh Mục";
            dgvDanhMuc.Columns["TenDanhMuc"].HeaderText = "Tên Danh Mục";
            dgvDanhMuc.Columns["Xoa"].Visible = false;
        }
        private void guna2PictureBox2_Click(object sender, EventArgs e)
        {
        }
        private void dgvDanhMuc_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvDanhMuc.Rows[e.RowIndex];

                txtMaDanhMuc.Text = row.Cells["MaDanhMuc"].Value.ToString();
                txtTenDanhMuc.Text = row.Cells["TenDanhMuc"].Value.ToString();
            }
        }
        private void ResetForm()
        {
            txtMaDanhMuc.Clear();
            txtTenDanhMuc.Clear();
            txtTenDanhMuc.Focus();
        }

        private void btnThemDM_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTenDanhMuc.Text))
            {
                MessageBox.Show("Vui lòng nhập tên danh mục.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenDanhMuc.Focus();
                return;
            }

            if (txtTenDanhMuc.Text.All(char.IsDigit))
            {
                MessageBox.Show("Tên danh mục không hợp lệ! Không được toàn là số.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenDanhMuc.Focus();
                return;
            }
            DTODanhMuc dm = new DTODanhMuc
            {
                TenDanhMuc = txtTenDanhMuc.Text.Trim()
            };
            try
            {
                bus.ThemDanhMuc(dm);
                MessageBox.Show("Thêm danh mục thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadDanhMuc();
                ResetForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Thêm danh mục thất bại!\n" + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnSuaDM_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTenDanhMuc.Text) || string.IsNullOrWhiteSpace(txtMaDanhMuc.Text))
            {
                MessageBox.Show("Vui lòng chọn danh mục cần sửa.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (txtTenDanhMuc.Text.All(char.IsDigit))
            {
                MessageBox.Show("Tên danh mục không hợp lệ! Không được toàn là số.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenDanhMuc.Focus();
                return;
            }

            DTODanhMuc dm = new DTODanhMuc
            {
                MaDanhMuc = int.Parse(txtMaDanhMuc.Text),
                TenDanhMuc = txtTenDanhMuc.Text.Trim()
            };

            try
            {
                bus.CapNhatDanhMuc(dm);
                MessageBox.Show("Cập nhật danh mục thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadDanhMuc();
                ResetForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Cập nhật thất bại!\n" + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnXoaDM_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaDanhMuc.Text))
            {
                MessageBox.Show("Vui lòng chọn danh mục cần xóa.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            int maDM = int.Parse(txtMaDanhMuc.Text);

            DialogResult result = MessageBox.Show("Bạn có chắc muốn xóa danh mục này không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                try
                {
                    bus.XoaDanhMuc(maDM);
                    MessageBox.Show("Xóa danh mục thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadDanhMuc();
                    ResetForm();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Xóa thất bại!\n" + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void btnLamMoiDM_Click(object sender, EventArgs e)
        {
            txtMaDanhMuc.Clear();
            txtTenDanhMuc.Clear();
            txtTenDanhMuc.Focus();
        }

        private void txtTenDanhMuc_TextChanged(object sender, EventArgs e)
        {

        }
        private BUSDanhMuc busDM = new BUSDanhMuc();

        private void btnTimKiemDM_Click(object sender, EventArgs e)
        {
            string tuKhoa = txtTenDanhMuc.Text.ToLower(); // Lấy nội dung từ TextBox tìm kiếm

            var ds = busDM.LayDanhSach()
                               .Where(dm => dm.TenDanhMuc.ToLower().Contains(tuKhoa))
                               .Select(dm => new
                               {
                                   dm.MaDanhMuc,
                                   dm.TenDanhMuc
                               }).ToList();

            dgvDanhMuc.DataSource = ds;
        }
    }
}

