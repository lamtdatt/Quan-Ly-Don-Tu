using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
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
    public partial class NhanVien : Form
    {
        public NhanVien()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            LoadNhanVien();
        }
        private BUSNhanVien busNhanVien = new BUSNhanVien();
        private void LoadNhanVien()
        {
            try
            {
                List<DTONhanVien> dsNhanVien = busNhanVien.LayDanhSach();
                List<DTONhanVien> dsChuaXoa = dsNhanVien.Where(nv => nv.Xoa == false).ToList();
                dgvNhanVien.DataSource = dsChuaXoa;
                dgvNhanVien.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dgvNhanVien.Columns["MaNhanVien"].HeaderText = "Mã Nhân Viên";
                dgvNhanVien.Columns["HoTen"].HeaderText = "Họ Tên";
                dgvNhanVien.Columns["Luong"].HeaderText = "Lương";
                dgvNhanVien.Columns["Luong"].DefaultCellStyle.Format = "N0";
                dgvNhanVien.Columns["Luong"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvNhanVien.Columns["DiaChi"].HeaderText = "Địa Chỉ";
                dgvNhanVien.Columns["SDT"].HeaderText = "Số Điện Thoại";
                dgvNhanVien.Columns["Email"].HeaderText = "Email";
                dgvNhanVien.Columns["GioiTinh"].HeaderText = "Giới Tính";
                dgvNhanVien.Columns["CaLamViec"].HeaderText = "Ca Làm Việc";
                dgvNhanVien.Columns["Xoa"].Visible = false;
                dgvNhanVien.Columns["HinhAnh"].Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi load nhân viên: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void guna2TextBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            // Kiểm tra SDT chỉ chứa số
            if (!Regex.IsMatch(txtSoDienThoai.Text.Trim(), @"^\d+$"))
            {
                MessageBox.Show("Số điện thoại không hợp lệ! Vui lòng chỉ nhập số.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSoDienThoai.Focus();
                return;
            }

            // Kiểm tra Email định dạng hợp lệ
            if (!Regex.IsMatch(txtEmail.Text.Trim(), @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                MessageBox.Show("Email không hợp lệ! Vui lòng nhập đúng định dạng.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEmail.Focus();
                return;
            }

            // Kiểm tra lương có phải số không
            if (!decimal.TryParse(txtLuong.Text, out decimal luong))
            {
                MessageBox.Show("Lương không hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtLuong.Focus();
                return;
            }
            BUSNhanVien bus = new BUSNhanVien();

            DTONhanVien nv = new DTONhanVien
            {
                HoTen = txtHoTen.Text,
                Luong = decimal.Parse(txtLuong.Text),
                DiaChi = txtDiaChi.Text,
                SDT = txtSoDienThoai.Text,
                Email = txtEmail.Text,
                GioiTinh = rdbNam.Checked ? "Nam" : "Nữ",
                CaLamViec = txtCaLamViec.Text,

            };
            bus.ThemNhanVien(nv);
            LoadNhanVien();
        }
        private void NhanVien_Load(object sender, EventArgs e)
        {
            LoadNhanVien();
        }
        private void dgvNhanVien_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvNhanVien.Rows[e.RowIndex];

                txtMaNV.Text = row.Cells["MaNhanVien"].Value.ToString();
                txtHoTen.Text = row.Cells["HoTen"].Value.ToString();
                txtLuong.Text = row.Cells["Luong"].Value.ToString();
                txtDiaChi.Text = row.Cells["DiaChi"].Value.ToString();
                txtSoDienThoai.Text = row.Cells["SDT"].Value.ToString();
                txtEmail.Text = row.Cells["Email"].Value.ToString();
                txtCaLamViec.Text = row.Cells["CaLamViec"].Value.ToString();
                string gioiTinh = row.Cells["GioiTinh"].Value.ToString();
                if (gioiTinh == "Nam")
                    rdbNam.Checked = true;
                else if (gioiTinh == "Nữ")
                    rdbNu.Checked = true;
            }
        }
        private void btnSuaNV_Click(object sender, EventArgs e)
        {



            if (!decimal.TryParse(txtLuong.Text, out decimal luong))
            {
                MessageBox.Show("Lương không hợp lệ!");
                return;
            }

            if (!Regex.IsMatch(txtSoDienThoai.Text.Trim(), @"^\d+$"))
            {
                MessageBox.Show("Số điện thoại không hợp lệ!");
                txtSoDienThoai.Focus();
                return;
            }
            if (!Regex.IsMatch(txtEmail.Text.Trim(), @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                MessageBox.Show("Email không hợp lệ!");
                txtEmail.Focus();
                return;
            }
            BUSNhanVien bus = new BUSNhanVien();
            DTONhanVien nv = new DTONhanVien
            {
                HoTen = txtHoTen.Text.Trim(),
                Luong = luong,
                DiaChi = txtDiaChi.Text.Trim(),
                SDT = txtSoDienThoai.Text.Trim(),
                Email = txtEmail.Text.Trim(),
                GioiTinh = rdbNam.Checked ? "Nam" : "Nữ",
                CaLamViec = txtCaLamViec.Text.Trim()
            };

            bus.CapNhatNhanVien(nv);
            MessageBox.Show("Cập nhật thành công");
            LoadNhanVien();
        }


        private void btnLamMoiNV_Click(object sender, EventArgs e)
        {
            txtMaNV.Clear();
            txtHoTen.Clear();
            txtLuong.Clear();
            txtDiaChi.Clear();
            txtSoDienThoai.Clear();
            txtEmail.Clear();
            txtCaLamViec.Clear();
            txtTimNhanVien.Clear();
            rdbNam.Checked = true;
            rdbNu.Checked = false;
            guna2PictureBox2.Image = null;
            txtHoTen.Focus();
        }
        private BUSNhanVien busNV = new BUSNhanVien();
        private void guna2Button5_Click(object sender, EventArgs e)
        {
            string tuKhoa = txtTimNhanVien.Text.ToLower(); // Lấy nội dung từ TextBox tìm kiếm

            var ds = busNV.LayDanhSach()
                               .Where(nv => nv.HoTen.ToLower().Contains(tuKhoa))
                               .Select(nv => new
                               {
                                   nv.HoTen,
                                   nv.MaNhanVien,
                                   nv.Luong,
                                   nv.DiaChi,
                                   nv.SDT,
                                   nv.Email,
                               }).ToList();

            dgvNhanVien.DataSource = ds;
        }

        private void btnXoaNV_Click(object sender, EventArgs e)
        {

            if (!int.TryParse(txtMaNV.Text, out int maNV))
            {
                MessageBox.Show("Chọn nhân viên để xóa!");
                return;
            }
            var result = MessageBox.Show("Bạn có chắc muốn xóa?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                try
                {
                    BUSNhanVien bus = new BUSNhanVien();
                    bus.XoaNhanVien(maNV);
                    MessageBox.Show("Xóa thành công", "Thông báo");
                    LoadNhanVien();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi xóa: " + ex.Message);
                }

            }
        }
    }
}