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
    public partial class SanPham : Form
    {
        private string tenFileAnhDuocChon = null;
        private BUSSanPham busSP = new BUSSanPham();
        public SanPham()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
        }
        private void SanPham_Load(object sender, EventArgs e)
        {
            LoadSanPham();
            LoadDanhMuc();
        }
        private void LoadSanPham()
        {
            dgvSanPhamm.Columns.Clear();
            dgvSanPhamm.Rows.Clear();
            dgvSanPhamm.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvSanPhamm.RowTemplate.Height = 50;
            dgvSanPhamm.Columns.Add("MaSanPham", "Mã SP");
            dgvSanPhamm.Columns.Add("TenSanPham", "Tên Sản Phẩm");
            dgvSanPhamm.Columns.Add("SoLuong", "Số Lượng");
            dgvSanPhamm.Columns.Add("DonGia", "Đơn Giá");
            dgvSanPhamm.Columns.Add("MoTa", "Mô Tả");
            dgvSanPhamm.Columns.Add("MaDanhMuc", "Mã Danh Mục");
            DataGridViewImageColumn imgCol = new DataGridViewImageColumn();
            imgCol.Name = "HinhAnh";
            imgCol.HeaderText = "Hình Ảnh";
            imgCol.ImageLayout = DataGridViewImageCellLayout.Zoom;
            dgvSanPhamm.Columns.Add(imgCol);

            List<DTOSanPham> ds = busSP.LayDanhSach();
            foreach (var sp in ds)
            {
                Image img = null;
                if (!string.IsNullOrEmpty(sp.HinhAnh))
                {
                    string path = Path.Combine(Application.StartupPath, "Images", sp.HinhAnh);
                    if (File.Exists(path))
                    {
                        using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
                        {
                            img = Image.FromStream(fs);
                        }
                    }
                }

                dgvSanPhamm.Rows.Add(
                    sp.MaSanPham,
                    sp.TenSanPham,
                    sp.SoLuong,
                    sp.DonGia.ToString("N0"),
                    sp.MoTa,
                    sp.MaDanhMuc,
                    img
                );
            }
        }
        private void ResetForm()
        {
            txtMaSanPham.Clear();
            txtTenSanPham.Clear();
            txtDonGia.Clear();
            nudSoLuong.Value = 0;
            rtbMoTa.Clear();
            cmbMaDanhMuc.SelectedIndex = 0;
            txtTimKiemSP.Clear();
            picSanPham.Image = null;
            tenFileAnhDuocChon = null;

            LoadSanPham(); 
        }
        private void LoadDanhMuc()
        {
            BUSDanhMuc busDanhMuc = new BUSDanhMuc();
            List<DTODanhMuc> danhMucList = busDanhMuc.LayDanhSach();
            cmbMaDanhMuc.DataSource = danhMucList;
            cmbMaDanhMuc.DisplayMember = "TenDanhMuc";
            cmbMaDanhMuc.ValueMember = "MaDanhMuc";
        }
        private void btnThemSanPham_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTenSanPham.Text) || Regex.IsMatch(txtTenSanPham.Text.Trim(), @"^\d+$"))
            {
                MessageBox.Show("Tên sản phẩm không hợp lệ! Không được để trống hoặc chỉ chứa số.",
                                "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenSanPham.Focus();
                return;
            }

            // ✅ Sửa thông báo như bạn yêu cầu
            if (!decimal.TryParse(txtDonGia.Text.Trim(), out decimal donGia) || donGia <= 0)
            {
                MessageBox.Show("Đơn giá không hợp lệ! Vui lòng nhập đơn giá > 0.",
                                "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDonGia.Focus();
                return;
            }

            if (!int.TryParse(nudSoLuong.Text.Trim(), out int soLuong))
            {
                MessageBox.Show("Số lượng không hợp lệ!",
                                "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                nudSoLuong.Focus();
                return;
            }

            if (soLuong == 0)
            {
                MessageBox.Show("Số lượng sản phẩm không được để 0!",
                                "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                nudSoLuong.Focus();
                return;
            }

            if (string.IsNullOrEmpty(tenFileAnhDuocChon))
            {
                MessageBox.Show("Vui lòng chọn hình ảnh cho sản phẩm.",
                                "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DTOSanPham sp = new DTOSanPham
            {
                TenSanPham = txtTenSanPham.Text.Trim(),
                MaDanhMuc = Convert.ToInt32(cmbMaDanhMuc.SelectedValue),
                DonGia = donGia,
                SoLuong = soLuong,
                MoTa = rtbMoTa.Text.Trim(),
                HinhAnh = tenFileAnhDuocChon
            };

            try
            {
                BUSSanPham bus = new BUSSanPham();
                bus.ThemSanPham(sp);
                LoadSanPham();
                ResetForm();
                MessageBox.Show("Thêm sản phẩm thành công!",
                                "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thêm sản phẩm: " + ex.Message,
                                "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSuaSanPham_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaSanPham.Text))
            {
                MessageBox.Show("Vui lòng chọn sản phẩm cần sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtTenSanPham.Text) || Regex.IsMatch(txtTenSanPham.Text.Trim(), @"^\d+$"))
            {
                MessageBox.Show("Tên sản phẩm không hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenSanPham.Focus();
                return;
            }

            if (!decimal.TryParse(txtDonGia.Text.Trim(), out decimal donGia) || donGia <= 0)
            {
                MessageBox.Show("Đơn giá không hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDonGia.Focus();
                return;
            }

            if (!int.TryParse(nudSoLuong.Text.Trim(), out int soLuong) || soLuong < 0)
            {
                MessageBox.Show("Số lượng không hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                nudSoLuong.Focus();
                return;
            }

            string tenAnh = null;

            if (!string.IsNullOrEmpty(tenFileAnhDuocChon))
            {
                string tenFile = Path.GetFileName(tenFileAnhDuocChon); // chỉ lấy tên file
                string thuMucAnh = Path.Combine(Application.StartupPath, "Images");
                if (!Directory.Exists(thuMucAnh))
                    Directory.CreateDirectory(thuMucAnh);

                string duongDanLuu = Path.Combine(thuMucAnh, tenFile);
                if (!File.Exists(duongDanLuu))
                {
                    try
                    {
                        File.Copy(tenFileAnhDuocChon, duongDanLuu);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi khi sao chép ảnh: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                tenAnh = tenFile;
            }
            else
            {
                if (dgvSanPhamm.CurrentRow != null)
                    tenAnh = dgvSanPhamm.CurrentRow.Cells["HinhAnh"].Value?.ToString();
            }

            DTOSanPham sp = new DTOSanPham
            {
                MaSanPham = Convert.ToInt32(txtMaSanPham.Text),
                TenSanPham = txtTenSanPham.Text.Trim(),
                MaDanhMuc = Convert.ToInt32(cmbMaDanhMuc.SelectedValue),
                DonGia = donGia,
                SoLuong = soLuong,
                MoTa = rtbMoTa.Text.Trim(),
                HinhAnh = tenAnh
            };

            try
            {
                BUSSanPham bus = new BUSSanPham();
                bus.CapNhatSanPham(sp);

                LoadSanPham();
                ResetForm();
                tenFileAnhDuocChon = "";

                MessageBox.Show("Sửa sản phẩm thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi sửa sản phẩm: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnXoaSanPham_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaSanPham.Text))
            {
                MessageBox.Show("Vui lòng chọn sản phẩm cần xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int maSP = Convert.ToInt32(txtMaSanPham.Text);

            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa sản phẩm này không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                try
                {
                    BUSSanPham bus = new BUSSanPham();
                    bus.XoaSanPham(maSP);
                    LoadSanPham();
                    ResetForm();
                    MessageBox.Show("Xóa sản phẩm thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi xóa sản phẩm: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void btnLamMoiSanPham_Click(object sender, EventArgs e)
        {
            ResetForm();
        }
        private void dgvSanPhamm_Click(object sender, EventArgs e)
        {
        }
        private void dgvSanPhamm_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvSanPhamm.Rows[e.RowIndex];

                txtMaSanPham.Text = row.Cells["MaSanPham"].Value.ToString();
                txtTenSanPham.Text = row.Cells["TenSanPham"].Value.ToString();
                txtDonGia.Text = row.Cells["DonGia"].Value.ToString();
                nudSoLuong.Value = Convert.ToInt32(row.Cells["SoLuong"].Value);
                rtbMoTa.Text = row.Cells["MoTa"].Value.ToString();

                if (int.TryParse(row.Cells["MaDanhMuc"].Value.ToString(), out int maDM))
                {
                    cmbMaDanhMuc.SelectedValue = maDM;
                }
                if (row.Cells["HinhAnh"].Value != null && row.Cells["HinhAnh"].Value is Image img)
                {
                    picSanPham.Image = img;
                    picSanPham.SizeMode = PictureBoxSizeMode.StretchImage;
                }
                else
                {
                    picSanPham.Image = null;
                }
                }
            }
        


        private void btnTimKiemMSP_Click(object sender, EventArgs e)
        {
        }

        private void btnTimKiemTSP_Click(object sender, EventArgs e)
        {
            string tuKhoa = txtTimKiemSP.Text.ToLower();

            var ds = busSP.LayDanhSach()
                          .Where(sp =>
                              sp.TenSanPham.ToLower().Contains(tuKhoa) ||
                              sp.MoTa.ToLower().Contains(tuKhoa) ||
                              sp.DonGia.ToString().Contains(tuKhoa) ||
                              sp.SoLuong.ToString().Contains(tuKhoa) ||
                              sp.MaSanPham.ToString().Contains(tuKhoa)
                          )
                          .ToList();

            dgvSanPhamm.Rows.Clear(); // Xóa dòng cũ

            foreach (var sp in ds)
            {
                Image img = null;
                if (!string.IsNullOrEmpty(sp.HinhAnh))
                {
                    string path = Path.Combine(Application.StartupPath, "Images", sp.HinhAnh);
                    if (File.Exists(path))
                    {
                        using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
                        {
                            img = Image.FromStream(fs);


                        }
                    }
                }
            }
        }
        private void btnTimKiemMDM_Click(object sender, EventArgs e)
        {
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void btnAnhSP_Click(object sender, EventArgs e)
        {
            OpenFileDialog opf = new OpenFileDialog();
            opf.Filter = "Choose Image (*.jpg;*.png;*.gif)|*.jpg;*.png;*.gif";

            if (opf.ShowDialog() == DialogResult.OK)
            {
                picSanPham.Image = Image.FromFile(opf.FileName);
                picSanPham.SizeMode = PictureBoxSizeMode.Zoom;
                tenFileAnhDuocChon = opf.FileName;
            }
        }

        private void btnChonAnh_Click(object sender, EventArgs e)
        {

            OpenFileDialog opf = new OpenFileDialog();
            opf.Filter = "Choose Image (*.jpg;*.png;*.gif)|*.jpg;*.png;*.gif";

            if (opf.ShowDialog() == DialogResult.OK)
            {
                picSanPham.Image = Image.FromFile(opf.FileName);
                picSanPham.SizeMode = PictureBoxSizeMode.Zoom;
            }
    }
}
}



