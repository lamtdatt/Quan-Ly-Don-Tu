    using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DTO_CuaHangBanh;
using BLL_CuaHangBanh;
using Guna.UI2.WinForms;
using DAL_CuaHangBanh;
using Microsoft.VisualBasic;


namespace GUI_CuaHangBanh
{
    public partial class DatHang : Form
    {
        private Guna2Button currentSelectedButton;
        private Guna2Button currentButton;
        private string maHoaDon;
        private DateTime _gioVao;
        private string maBanDangChon = "";
        private string tenBanDangChon = "";

        public DatHang()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
        }
        private void DatHang_Load(object sender, EventArgs e)
        {
            LoadDSSanPham();
            LoadDanhMucToComboBox();
            pbSanPham.SizeMode = PictureBoxSizeMode.StretchImage;
            nudTongTien.Maximum = 100000000;
            foreach (Control ctrl in groupBan.Controls)
            {
                if (ctrl is Button btn)
                {
                    btn.Click += Ban_Click;
                }
            }
        }
        private BUSSanPham busSP = new BUSSanPham();
        private List<DTOChiTietSPTheoBan> dsSanPhamTheoBan = new List<DTOChiTietSPTheoBan>();
        private void LoadDSSanPham()
        {
            var ds = busSP.LayDanhSach()
                .Select(sp => new
                {
                    sp.TenSanPham,
                    sp.DonGia,
                    sp.MoTa,
                    sp.HinhAnh
                }).ToList();
            dgvSanPham.DataSource = ds;
            dgvSanPham.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvSanPham.Columns["TenSanPham"].HeaderText = "Tên sản phẩm";
            dgvSanPham.Columns["DonGia"].HeaderText = "Đơn giá";
            dgvSanPham.Columns["MoTa"].HeaderText = "Mô tả";
            dgvSanPham.Columns["DonGia"].DefaultCellStyle.Format = "N0";
            dgvSanPham.Columns["HinhAnh"].Visible = false;
        }
        private void groupBox4_Enter(object sender, EventArgs e)
        {
        }

        private void guna2PictureBox2_Click(object sender, EventArgs e)
        {
        }

        private void dgvSanPham_CellClick(object sender, DataGridViewCellEventArgs e)
        {
        }
        BUSDanhMuc danhMucBUS = new BUSDanhMuc();
        private BUSDanhMuc busDanhMuc = new BUSDanhMuc();

        private void LoadDanhMucToComboBox()
        {
            List<DTODanhMuc> dsDanhMuc = busDanhMuc.LayDanhSach();
            cmbTimKiemLoaiBanh.DataSource = dsDanhMuc;
            cmbTimKiemLoaiBanh.DisplayMember = "TenDanhMuc";
            cmbTimKiemLoaiBanh.ValueMember = "MaDanhMuc";
        }
        private void dgvSanPham_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            DataGridViewRow row = dgvSanPham.Rows[e.RowIndex];
            txtTenSP.Text = row.Cells["TenSanPham"].Value?.ToString();
            txtDonGia.Text = Convert.ToDecimal(row.Cells["DonGia"].Value).ToString("N0");
            string fileName = row.Cells["HinhAnh"].Value?.ToString();
            nudSoLuong.Value = 1;
            if (!string.IsNullOrEmpty(fileName))
            {
                string imagePath = Path.Combine(Application.StartupPath, "Images", fileName);

                if (File.Exists(imagePath))
                {
                    try
                    {
                        using (FileStream fs = new FileStream(imagePath, FileMode.Open, FileAccess.Read))
                        {
                            pbSanPham.Image = Image.FromStream(fs);
                        }
                        pbSanPham.SizeMode = PictureBoxSizeMode.StretchImage;
                    }
                    catch
                    {
                        pbSanPham.Image = null;
                        MessageBox.Show("Không thể đọc ảnh!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    pbSanPham.Image = null;
                }
            }
        }
        private void btnThemSP_Click(object sender, EventArgs e)
        {
            if (dgvSanPham.CurrentRow != null)
            {
                string tenSP = dgvSanPham.CurrentRow.Cells["TenSanPham"].Value?.ToString();

                object donGiaObj = dgvSanPham.CurrentRow.Cells["DonGia"].Value;
                if (donGiaObj == null)
                {
                    MessageBox.Show("Không có đơn giá!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                int donGia = Convert.ToInt32(donGiaObj);

                int soLuong = (int)nudSoLuong.Value;
                if (soLuong <= 0)
                {
                    MessageBox.Show("Vui lòng nhập số lượng > 0", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var spDaCo = dsSanPhamTheoBan.FirstOrDefault(sp => sp.TenSanPham == tenSP);
                if (spDaCo != null)
                {
                    spDaCo.SoLuong += soLuong;
                }
                else
                {
                    dsSanPhamTheoBan.Add(new DTOChiTietSPTheoBan
                    {
                        TenSanPham = tenSP,
                        DonGia = donGia,
                        SoLuong = soLuong
                    });
                }

                CapNhatGridViewSanPhamTheoBan();
                CapNhatTongTien();
            }
        }
        private void CapNhatGridViewSanPhamTheoBan()
        {
            dgvSanPhamTheoBan.DataSource = null;
            dgvSanPhamTheoBan.DataSource = dsSanPhamTheoBan;
            dgvSanPhamTheoBan.Columns["TenSanPham"].HeaderText = "Tên Sản Phẩm";
            dgvSanPhamTheoBan.Columns["SoLuong"].HeaderText = "Số Lượng";
            dgvSanPhamTheoBan.Columns["DonGia"].HeaderText = "Đơn Giá";
            dgvSanPhamTheoBan.Columns["ThanhTien"].HeaderText = "Tổng Đơn Giá";
            dgvSanPhamTheoBan.Columns["DonGia"].DefaultCellStyle.Format = "N0";
            dgvSanPhamTheoBan.Columns["ThanhTien"].DefaultCellStyle.Format = "N0";
        }
        private void CapNhatTongTien()
        {
            int tong = dsSanPhamTheoBan.Sum(sp => sp.ThanhTien);
            decimal tongTien = 0;

            foreach (DataGridViewRow row in dgvSanPhamTheoBan.Rows)
            {
                if (row.Cells["ThanhTien"].Value != null)
                {
                    tongTien += Convert.ToDecimal(row.Cells["ThanhTien"].Value);
                }
            }
            if (tongTien > nudTongTien.Maximum)
            {
                nudTongTien.Maximum = tongTien + 100000;
            }
            nudTongTien.ThousandsSeparator = true;
            nudTongTien.Value = tongTien;
        }
        private void btnThanhToan_Click(object sender, EventArgs e)
        {
            try
            {
                string tenKH = txtTenKhachHang.Text.Trim();
                string sdt = txtSoDienThoai.Text.Trim();

                // ✅ 1. Kiểm tra nhập đầy đủ thông tin
                if (string.IsNullOrEmpty(tenKH) || string.IsNullOrEmpty(sdt))
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ thông tin khách hàng!",
                                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // ✅ 2. Kiểm tra tên khách hàng hợp lệ (không được toàn số, không ký tự đặc biệt)
                if (tenKH.All(char.IsDigit))
                {
                    MessageBox.Show("Tên khách hàng không được chỉ chứa số!",
                                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (!System.Text.RegularExpressions.Regex.IsMatch(tenKH, @"^[\p{L}\s]+$"))
                {
                    MessageBox.Show("Tên khách hàng chỉ được chứa chữ và khoảng trắng!",
                                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // ✅ 3. Kiểm tra số điện thoại hợp lệ (chỉ số, độ dài 9–11)
                if (!System.Text.RegularExpressions.Regex.IsMatch(sdt, @"^\d{9,11}$"))
                {
                    MessageBox.Show("Số điện thoại không hợp lệ! Chỉ được nhập số (9–11 chữ số).",
                                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // ✅ 4. Kiểm tra bàn
                if (string.IsNullOrEmpty(maBanDangChon))
                {
                    MessageBox.Show("Vui lòng chọn bàn trước khi thanh toán!",
                                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // --- Tiếp tục phần xử lý gốc ---
                BUSKhachHang busKH = new BUSKhachHang();
                var khach = busKH.TimKhachHangTheoSDT(sdt);
                if (khach == null)
                {
                    DTOKhachHang khMoi = new DTOKhachHang { HoTen = tenKH, SDT = sdt };
                    if (!busKH.ThemKhachHang(khMoi))
                    {
                        MessageBox.Show("Thêm khách hàng mới thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    khach = busKH.TimKhachHangTheoSDT(sdt);
                    if (khach == null)
                    {
                        MessageBox.Show("Không thể lấy mã khách hàng vừa thêm!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                string maKhachHang = khach.MaKhachHang.ToString();
                int[] danhSachMaNV = { 1, 2, 3, 4, 5 };
                Random rnd = new Random();
                string maNhanVien = danhSachMaNV[rnd.Next(danhSachMaNV.Length)].ToString();

                if (_gioVao == DateTime.MinValue)
                    _gioVao = DateTime.Now;

                int tongTienGoc = (int)nudTongTien.Value;
                int phanTramGiam = (int)nudKhuyenMai.Value;
                decimal soTienGiam = tongTienGoc * (phanTramGiam / 100m);
                int tongSauGiam = tongTienGoc - (int)soTienGiam;

                string tenBanFromDB = null;
                int maBanInt = 0;

                if (maBanDangChon != "MangVe")
                {
                    maBanInt = int.Parse(new string(maBanDangChon.Where(char.IsDigit).ToArray()));
                    BUSBanAn busBan = new BUSBanAn();
                    var banInfo = busBan.TimBanTheoMa(maBanInt);
                    if (banInfo != null)
                        tenBanFromDB = banInfo.TenBan;
                }

                if (string.IsNullOrEmpty(tenBanFromDB))
                    tenBanFromDB = tenBanDangChon;

                DTOHoaDon hd = new DTOHoaDon
                {
                    DateCheck = _gioVao,
                    DateOut = DateTime.Now,
                    MaKhachHang = maKhachHang,
                    MaNhanVien = maNhanVien,
                    MaBan = maBanDangChon == "MangVe" ? 0 : maBanInt,
                    TenBan = tenBanFromDB,
                    TrangThai = "Đã thanh toán",
                    TongHoaDon = tongSauGiam,
                    GiamGia = phanTramGiam
                };

                BUSHoaDon busHD = new BUSHoaDon();
                busHD.ThemHoaDon(hd);

                DTOHoaDon hoaDonMoi = busHD.LayHoaDonMoiNhat();
                if (hoaDonMoi == null)
                {
                    MessageBox.Show("Không thể lấy hóa đơn mới!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                BUSCTHoaDon busCTHD = new BUSCTHoaDon();
                BUSSanPham busSP = new BUSSanPham();

                foreach (DataGridViewRow row in dgvSanPhamTheoBan.Rows)
                {
                    if (row.IsNewRow) continue;
                    string tenSP = row.Cells["TenSanPham"].Value.ToString();
                    string maSP = busSP.LayMaSanPhamTheoTen(tenSP);

                    DTOCTHoaDon cthd = new DTOCTHoaDon
                    {
                        MaHoaDon = hoaDonMoi.MaHoaDon,
                        MaSanPham = maSP,
                        SoLuong = Convert.ToInt32(row.Cells["SoLuong"].Value),
                        DonGia = Convert.ToInt32(row.Cells["DonGia"].Value)
                    };

                    busCTHD.ThemChiTietHoaDon(cthd);
                }

                if (maBanDangChon != "MangVe")
                {
                    BUSBanAn busBan = new BUSBanAn();
                    busBan.CapNhatTrangThai(maBanInt, "Có khách");
                }

                DialogResult dr = MessageBox.Show("Thanh toán thành công!\nBạn có muốn in hóa đơn không?",
                                                  "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (dr == DialogResult.Yes)
                {
                    List<DTOChiTietSPTheoBan> dsChiTiet = new List<DTOChiTietSPTheoBan>();
                    DataTable dtChiTiet = BUSHoaDon.LayChiTietHoaDon(hoaDonMoi.MaHoaDon);
                    foreach (DataRow row in dtChiTiet.Rows)
                    {
                        dsChiTiet.Add(new DTOChiTietSPTheoBan
                        {
                            TenSanPham = row["TenSanPham"].ToString(),
                            SoLuong = Convert.ToInt32(row["SoLuong"]),
                            DonGia = Convert.ToInt32(row["DonGia"])
                        });
                    }

                    if (dsChiTiet.Count == 0)
                    {
                        MessageBox.Show("Không tìm thấy chi tiết hóa đơn!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    InHoaDon frm = new InHoaDon(hoaDonMoi, dsChiTiet, tenBanFromDB);
                    frm.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private int indexSelected = -1;
        private void btnSuaSP_Click(object sender, EventArgs e)
        {
            if (indexSelected >= 0 && indexSelected < dsSanPhamTheoBan.Count)
            {
                dsSanPhamTheoBan[indexSelected].SoLuong = (int)nudSoLuong.Value;
                CapNhatGridViewSanPhamTheoBan();
                CapNhatTongTien();
                indexSelected = -1;
                btnSuaSP.Enabled = false;
                btnXoaSP.Enabled = false;
            }
        }
        private void dgvSanPhamTheoBan_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dsSanPhamTheoBan.Count)
            {
                indexSelected = e.RowIndex;

                var selected = dsSanPhamTheoBan[indexSelected];
                txtTenSP.Text = selected.TenSanPham;
                txtDonGia.Text = selected.DonGia.ToString("N0");
                nudSoLuong.Value = selected.SoLuong;
                btnSuaSP.Enabled = true;
                btnXoaSP.Enabled = true;
            }
        }
        private void btnXoaSP_Click(object sender, EventArgs e)
        {
            if (indexSelected >= 0 && indexSelected < dsSanPhamTheoBan.Count)
            {
                dsSanPhamTheoBan.RemoveAt(indexSelected);

                CapNhatGridViewSanPhamTheoBan();
                CapNhatTongTien();

                indexSelected = -1;
                btnSuaSP.Enabled = false;
                btnXoaSP.Enabled = false;
            }
        }
        private void Ban_Click(object sender, EventArgs e)
        {
            List<Control> allButtons = new List<Control>()
              {
        btnBan1, btnBan2, btnBan3, btnBan4, btnBan5, btnBan6,
        btnMangVee
              };
            foreach (var btn in allButtons)
            {
                if (btn is Button b)
                {
                    b.BackColor = Color.White;
                    b.ForeColor = Color.Black;
                }
                else if (btn is Guna.UI2.WinForms.Guna2Button gbtn)
                {
                    gbtn.FillColor = Color.White;
                    gbtn.ForeColor = Color.Black;
                }
            }
            if (sender is Button clickedBtn)
            {
                clickedBtn.BackColor = Color.Orange;
                clickedBtn.ForeColor = Color.White;
            }
            else if (sender is Guna.UI2.WinForms.Guna2Button clickedGunaBtn)
            {
                clickedGunaBtn.FillColor = Color.Orange;
                clickedGunaBtn.ForeColor = Color.White;
            }
        }
        private void btnThemTTKH_Click(object sender, EventArgs e)
        {
            string tenKH = Microsoft.VisualBasic.Interaction.InputBox("Nhập tên khách hàng:", "Thông tin khách hàng", "");
            if (string.IsNullOrWhiteSpace(tenKH))
            {
                MessageBox.Show("Vui lòng nhập tên khách hàng!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string soDT = Microsoft.VisualBasic.Interaction.InputBox("Nhập số điện thoại:", "Thông tin khách hàng", "");
            txtTenKhachHang.Text = tenKH;
            txtSoDienThoai.Text = soDT;
            MessageBox.Show($"Tên: {tenKH}\nSĐT: {soDT}", "Thông tin khách hàng đã nhập", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void btnTimKiemBanh_Click(object sender, EventArgs e)
        {
            string tuKhoa = txtTimKiemBanh.Text.ToLower();
            var ds = busSP.LayDanhSach()
                          .Where(sp => sp.TenSanPham.ToLower().Contains(tuKhoa))
                          .Select(sp => new
                          {
                              sp.TenSanPham,
                              sp.DonGia,
                              sp.MoTa
                          }).ToList();

            dgvSanPham.DataSource = ds;
        }
        private void btnMangVe_Click(object sender, EventArgs e)
        {
            maBanDangChon = "MangVe";
        }
        private void btnBan2_MouseClick(object sender, MouseEventArgs e)
        {
        }
        private void txtTimKiemBanh_TextChanged(object sender, EventArgs e)
        {
        }
        private void btnTimKiemLoaiBanh_Click(object sender, EventArgs e)
        {
            if (cmbTimKiemLoaiBanh.SelectedValue != null)
            {
                int maDM = Convert.ToInt32(cmbTimKiemLoaiBanh.SelectedValue);
                var ds = busSP.LayDanhSach()
                              .Where(sp => sp.MaDanhMuc == maDM)
                              .Select(sp => new
                              {
                                  sp.TenSanPham,
                                  sp.DonGia,
                                  sp.MoTa
                              }).ToList();

                dgvSanPham.DataSource = ds;
            }
        }

        private void btnLamMoiTimKiem_Click(object sender, EventArgs e)
        {
            txtTimKiemBanh.Clear();
            cmbTimKiemLoaiBanh.SelectedIndex = -1; // Reset combo box

            dgvSanPham.DataSource = busSP.LayDanhSach()
                                         .Select(sp => new
                                         {
                                             sp.TenSanPham,
                                             sp.DonGia,
                                             sp.MoTa
                                         }).ToList();
        }

        private void btnBan1_Click(object sender, EventArgs e)
        {
            maBanDangChon = "Ban1";
            tenBanDangChon = "Bàn 1";
        }

        private void btnBan2_Click(object sender, EventArgs e)
        {
            maBanDangChon = "Ban2";
            tenBanDangChon = "Bàn 2";
        }

        private void btnBan3_Click(object sender, EventArgs e)
        {
            maBanDangChon = "Ban3";
            tenBanDangChon = "Bàn 3";
        }

        private void btnBan4_Click(object sender, EventArgs e)
        {
            maBanDangChon = "Ban4";
            tenBanDangChon = "Bàn 4";
        }

        private void btnBan5_Click(object sender, EventArgs e)
        {
            maBanDangChon = "Ban5";
            tenBanDangChon = "Bàn 5";
        }

        private void btnBan6_Click(object sender, EventArgs e)
        {
            maBanDangChon = "Ban6";
            tenBanDangChon = "Bàn 6";
        }
    }

}




