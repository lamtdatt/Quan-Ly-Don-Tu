using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using DTO_CuaHangBanh;

namespace GUI_CuaHangBanh
{
    public partial class InHoaDon : Form
    {
        private DTOHoaDon hoaDon;
        private List<DTOChiTietSPTheoBan> chiTiet;
        private string tenBan;


        public InHoaDon(DTOHoaDon hd, List<DTOChiTietSPTheoBan> ds, string tenBan)
        {
            InitializeComponent();
            hoaDon = hd;
            chiTiet = ds;
            this.tenBan = tenBan;
        }
        private void InHoaDon_Load(object sender, EventArgs e)
        {
            if (hoaDon == null || chiTiet == null) return;
            txtMaHD.Text = hoaDon.MaHoaDon;
            txtMaKH.Text = hoaDon.MaKhachHang;
            txtMaNV.Text = hoaDon.MaNhanVien;
            txtMaBan.Text = hoaDon.MaBan.ToString();
            txtGioVao.Text = hoaDon.DateCheck.ToString("HH:mm");
            txtGioRa.Text = hoaDon.DateOut.ToString("HH:mm");
            decimal tongTien = chiTiet.Sum(sp => sp.SoLuong * sp.DonGia);
            decimal tienGiam = tongTien * hoaDon.GiamGia / 100;
            decimal thanhToan = tongTien - tienGiam;
            txtGiamGia.Text = $"{hoaDon.GiamGia}%";
            txtTongTien.Text = thanhToan.ToString("N0");
            DataTable dt = new DataTable();
            dt.Columns.Add("STT", typeof(int));
            dt.Columns.Add("Tên sản phẩm", typeof(string));
            dt.Columns.Add("Số lượng", typeof(int));
            dt.Columns.Add("Đơn giá", typeof(decimal));
            dt.Columns.Add("Thành tiền", typeof(decimal));
            int stt = 1;
            foreach (var item in chiTiet)
            {
                dt.Rows.Add(stt++, item.TenSanPham, item.SoLuong, item.DonGia, item.ThanhTien);
            }
            dgvInHoaDon.DataSource = dt;
            dgvInHoaDon.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvInHoaDon.Columns["STT"].Width = 50;
            dgvInHoaDon.Columns["Đơn giá"].DefaultCellStyle.Format = "N0";
            dgvInHoaDon.Columns["Thành tiền"].DefaultCellStyle.Format = "N0";
        }

        private void dgvInHoaDon_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
