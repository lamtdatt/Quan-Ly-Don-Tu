using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
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
    public partial class ThongKe : Form
    {
        private BUSHoaDon bus = new BUSHoaDon();
        int maHoaDonDuocChon = -1;
        private BUSBanAn banBUS;
        private BUSCTHoaDon chiTietBUS;
        private BUSHoaDon hoaDonBUS;

        public ThongKe()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
        }
        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {
        }
        private void frmThongKe_Load(object sender, EventArgs e)
        {
            dgvDSHD.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvDSHD.MultiSelect = false;
            LoadcmbSanPham();
            LoadcmbNhanVien();
            LoadDanhSachHoaDon();
        }
        private void LoadcmbSanPham()
        {
            BUSSanPham busSP = new BUSSanPham();
            cmbSanPham.DataSource = busSP.LayDanhSach();
            cmbSanPham.DisplayMember = "TenSanPham";
            cmbSanPham.ValueMember = "MaSanPham";
            cmbSanPham.SelectedIndex = -1;
        }
        private void LoadcmbNhanVien()
        {
            BUSNhanVien busNV = new BUSNhanVien();
            cmbMaNhanVien.DataSource = busNV.LayDanhSach();
            cmbMaNhanVien.DisplayMember = "MaNhanVien";
            cmbMaNhanVien.ValueMember = "MaNhanVien";
            cmbMaNhanVien.SelectedIndex = -1;
        }
        private void LoadDanhSachHoaDon()
        {
            dgvDSHD.DataSource = BUSThongKe.LayTatCaHoaDon();
            dgvDSHD.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            Dictionary<string, string> columnHeaders = new Dictionary<string, string>
              {
        { "MaHoaDon", "Mã hóa đơn" },
        { "DateCheck", "Ngày tạo" },
        { "DateOut", "Ngày thanh toán" },
        { "TenBan", "Tên bàn" },
        { "TenNhanVien", "Nhân viên" },
        { "TenKhach", "Khách hàng" },
        { "TongHoaDon", "Tổng tiền" }
             };

            foreach (var col in columnHeaders)
            {
                if (dgvDSHD.Columns.Contains("TongHoaDon"))
                {
                    dgvDSHD.Columns["TongHoaDon"].DefaultCellStyle.Format = "N0";
                    dgvDSHD.Columns["TongHoaDon"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                }
            }
        }
        private void label5_Click(object sender, EventArgs e)
        {
        }

        private void dgvDSHD_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void dgvDSHD_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dgvDSHD.Columns.Contains("MaHoaDon"))
            {
                object cellValue = dgvDSHD.Rows[e.RowIndex].Cells["MaHoaDon"].Value;

                if (cellValue != null)
                {
                    string maHD = cellValue.ToString();
                    DataTable dtCT = DAL_CuaHangBanh.DALThongKe.TK_ChiTietHoaDon(maHD);

                    if (dtCT != null && dtCT.Rows.Count > 0)
                    {
                        dgvChiTietHoaDon.DataSource = dtCT;
                        dgvChiTietHoaDon.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                        if (dtCT.Columns.Contains("DonGia"))
                        {
                            dgvChiTietHoaDon.Columns["DonGia"].DefaultCellStyle.Format = "N0";
                            dgvChiTietHoaDon.Columns["DonGia"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                        }
                        decimal tongTien = 0;
                        foreach (DataRow row in dtCT.Rows)
                        {
                            if (int.TryParse(row["SoLuong"].ToString(), out int sl) &&
                                decimal.TryParse(row["DonGia"].ToString(), out decimal dg))
                            {
                                tongTien += sl * dg;
                            }
                        }
                        txtTongTienn.Text = tongTien.ToString("N0");
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy chi tiết hóa đơn.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        dgvChiTietHoaDon.DataSource = null;
                        txtTongTienn.Clear();
                    }
                }
                else
                {
                    MessageBox.Show("Không thể xác định mã hóa đơn!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }
        private void btnTimKiemHD_Click(object sender, EventArgs e)
        {
            try
            {
                string maHD = txtMaHD.Text.Trim();
                string sql = "SELECT * FROM HoaDon WHERE MaHoaDon = @0";
                List<object> args = new List<object> { maHD };

                using (SqlDataReader reader = DBUtil.Query(sql, args))
                {
                    DataTable dt = new DataTable();
                    dt.Load(reader);
                    dgvDSHD.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tìm kiếm hóa đơn: " + ex.Message);
            }
        }

        private void btnTimKiemSanPHAM_Click(object sender, EventArgs e)
        {
            try
            {
                string tenSP = cmbSanPham.Text.Trim();
                string sql = @"
            SELECT hd.* 
            FROM HoaDon hd 
            JOIN CT_HoaDon ct ON hd.MaHoaDon = ct.MaHoaDon
            JOIN SanPham sp ON ct.MaSanPham = sp.MaSanPham
            WHERE sp.TenSanPham LIKE '%' + @0 + '%'";

                List<object> args = new List<object> { tenSP };
                using (SqlDataReader reader = DBUtil.Query(sql, args))
                {
                    DataTable dt = new DataTable();
                    dt.Load(reader);
                    dgvDSHD.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tìm kiếm theo sản phẩm: " + ex.Message);
            }
        }
        private void btnTimKiemMaNV_Click(object sender, EventArgs e)
        {
            try
            {
                string maNV = cmbMaNhanVien.SelectedValue?.ToString();
                string sql = @"
            SELECT * 
            FROM HoaDon 
            WHERE MaNhanVien = @0";

                List<object> args = new List<object> { maNV };

                using (SqlDataReader reader = DBUtil.Query(sql, args))
                {
                    DataTable dt = new DataTable();
                    dt.Load(reader);
                    dgvDSHD.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tìm kiếm theo mã nhân viên: " + ex.Message);
            }
        }
        private void btnTimKiemTheoNgay_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime ngay = dtpNgay.Value.Date;
                string sql = "SELECT * FROM HoaDon WHERE CONVERT(date, DateCheck) = @0";
                List<object> args = new List<object> { ngay };

                using (SqlDataReader reader = DBUtil.Query(sql, args))
                {
                    DataTable dt = new DataTable();
                    dt.Load(reader);
                    dgvDSHD.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tìm kiếm theo ngày: " + ex.Message);
            }
        }
        private void btnLamMoiThongKe_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtMaHD != null)
                    txtMaHD.Clear();
                if (cmbSanPham != null && cmbSanPham.Items.Count > 0)
                    cmbSanPham.SelectedIndex = -1;
                if (cmbMaNhanVien != null && cmbMaNhanVien.Items.Count > 0)
                    cmbMaNhanVien.SelectedIndex = -1;
                if (dtpNgay != null)
                    dtpNgay.Value = DateTime.Now.Date;
                if (dgvDSHD != null)
                    dgvDSHD.DataSource = BUSHoaDon.LayTatCaHoaDon();
                if (dgvChiTietHoaDon != null)
                    dgvChiTietHoaDon.DataSource = null;

                if (txtTongTien != null)
                    txtTongTien.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi làm mới thống kê: " + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private string connString = @"Data Source=ADMIN\TIENDAT1;Initial Catalog=DATN_QLCuaHangBanh;Integrated Security=True;Encrypt=False";
        private void btnXoaHoaDon_Click(object sender, EventArgs e)
        {
            if (dgvDSHD.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn hóa đơn cần xóa!");
                return;
            }
            int maHoaDon = Convert.ToInt32(dgvDSHD.SelectedRows[0].Cells["MaHoaDon"].Value);
            int maKhachHang;
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                string queryGetKH = "SELECT MaKhachHang FROM HoaDon WHERE MaHoaDon = @mahd";
                SqlCommand cmd = new SqlCommand(queryGetKH, conn);
                cmd.Parameters.AddWithValue("@mahd", maHoaDon);
                object result = cmd.ExecuteScalar();
                if (result == null)
                {
                    MessageBox.Show("Không tìm thấy khách hàng cho hóa đơn này!");
                    return;
                }
                maKhachHang = Convert.ToInt32(result);
            }
            if (MessageBox.Show("Bạn có chắc muốn xóa hóa đơn này và khách hàng liên quan?",
                                "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
            {
                return;
            }
            var busHD = new BUSHoaDon();
            bool isDeleted = busHD.XoaHoaDonVaKhachHang(maHoaDon, maKhachHang);

            if (isDeleted)
            {
                MessageBox.Show("Xóa thành công!");
                LoadDanhSachHoaDon();
            }
            else
            {
                MessageBox.Show("Có lỗi xảy ra khi xóa!");

            }
        }
        private void btnInHoaDon_Click(object sender, EventArgs e)
        {
            if (dgvDSHD.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn hóa đơn để in lại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Khởi tạo BUS nếu chưa khởi tạo
                if (hoaDonBUS == null) hoaDonBUS = new BUSHoaDon();
                if (chiTietBUS == null) chiTietBUS = new BUSCTHoaDon();
                if (banBUS == null) banBUS = new BUSBanAn();

                // Lấy mã hóa đơn từ DataGridView
                object cellValue = dgvDSHD.CurrentRow.Cells["MaHoaDon"].Value;
                if (cellValue == null || cellValue == DBNull.Value)
                {
                    MessageBox.Show("Không thể xác định mã hóa đơn!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                int maHoaDon = Convert.ToInt32(cellValue);

                // Lấy hóa đơn
                DTOHoaDon hoaDon = hoaDonBUS.LayHoaDonTheoMa(maHoaDon);
                if (hoaDon == null)
                {
                    MessageBox.Show("Không tìm thấy hóa đơn!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Lấy danh sách chi tiết
                List<DTOChiTietSPTheoBan> chiTiet = chiTietBUS.LayChiTietTheoHoaDon(maHoaDon);
                if (chiTiet == null || chiTiet.Count == 0)
                {
                    MessageBox.Show("Không có chi tiết cho hóa đơn này!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // Lấy tên bàn
                var ban = banBUS.TimBanTheoMa(hoaDon.MaBan);
                string tenBan = ban != null ? ban.TenBan : "Không rõ bàn";

                // Mở form in hóa đơn
                using (InHoaDon frm = new InHoaDon(hoaDon, chiTiet, tenBan))
                {
                    frm.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi in hóa đơn: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

            }
    }



