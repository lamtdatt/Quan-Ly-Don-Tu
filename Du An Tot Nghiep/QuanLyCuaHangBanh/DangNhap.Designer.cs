namespace QuanLyCuaHangBanh
{
    partial class DangNhap
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DangNhap));
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges5 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges6 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges7 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges8 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            txtTenDangNhap = new TextBox();
            txtMatKhau = new TextBox();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            pictureBox1 = new PictureBox();
            btnDangNhap = new Guna.UI2.WinForms.Guna2Button();
            btnThoat = new Guna.UI2.WinForms.Guna2Button();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // txtTenDangNhap
            // 
            txtTenDangNhap.BackColor = Color.BlanchedAlmond;
            txtTenDangNhap.Location = new Point(770, 287);
            txtTenDangNhap.Name = "txtTenDangNhap";
            txtTenDangNhap.Size = new Size(259, 27);
            txtTenDangNhap.TabIndex = 0;
            // 
            // txtMatKhau
            // 
            txtMatKhau.BackColor = Color.BlanchedAlmond;
            txtMatKhau.Location = new Point(770, 361);
            txtMatKhau.Name = "txtMatKhau";
            txtMatKhau.PasswordChar = '*';
            txtMatKhau.Size = new Size(259, 27);
            txtMatKhau.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Arial", 13.8F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.BlanchedAlmond;
            label1.Location = new Point(591, 285);
            label1.Name = "label1";
            label1.Size = new Size(173, 26);
            label1.TabIndex = 2;
            label1.Text = "Tên đăng nhập";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Arial", 13.8F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            label2.ForeColor = Color.BlanchedAlmond;
            label2.Location = new Point(617, 359);
            label2.Name = "label2";
            label2.Size = new Size(110, 26);
            label2.TabIndex = 3;
            label2.Text = "Mật khẩu";
            // 
            // label3
            // 
            label3.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            label3.AutoSize = true;
            label3.Font = new Font("Times New Roman", 70.2000046F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label3.ForeColor = Color.PeachPuff;
            label3.Location = new Point(245, 16);
            label3.Name = "label3";
            label3.Size = new Size(640, 133);
            label3.TabIndex = 4;
            label3.Text = "Poly Bakery";
            // 
            // pictureBox1
            // 
            pictureBox1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(-9, 162);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(564, 526);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 5;
            pictureBox1.TabStop = false;
            // 
            // btnDangNhap
            // 
            btnDangNhap.AutoRoundedCorners = true;
            btnDangNhap.CustomizableEdges = customizableEdges5;
            btnDangNhap.DisabledState.BorderColor = Color.DarkGray;
            btnDangNhap.DisabledState.CustomBorderColor = Color.DarkGray;
            btnDangNhap.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            btnDangNhap.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            btnDangNhap.FillColor = Color.FromArgb(255, 224, 192);
            btnDangNhap.Font = new Font("Arial", 10.8F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            btnDangNhap.ForeColor = Color.Black;
            btnDangNhap.Location = new Point(591, 438);
            btnDangNhap.Name = "btnDangNhap";
            btnDangNhap.ShadowDecoration.CustomizableEdges = customizableEdges6;
            btnDangNhap.Size = new Size(225, 56);
            btnDangNhap.TabIndex = 10;
            btnDangNhap.Text = "Đăng nhập";
            btnDangNhap.Click += btnDangNhap_Click;
            // 
            // btnThoat
            // 
            btnThoat.AutoRoundedCorners = true;
            btnThoat.CustomizableEdges = customizableEdges7;
            btnThoat.DisabledState.BorderColor = Color.DarkGray;
            btnThoat.DisabledState.CustomBorderColor = Color.DarkGray;
            btnThoat.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            btnThoat.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            btnThoat.FillColor = Color.FromArgb(255, 224, 192);
            btnThoat.Font = new Font("Arial", 10.8F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            btnThoat.ForeColor = Color.Black;
            btnThoat.Location = new Point(857, 438);
            btnThoat.Name = "btnThoat";
            btnThoat.ShadowDecoration.CustomizableEdges = customizableEdges8;
            btnThoat.Size = new Size(225, 56);
            btnThoat.TabIndex = 11;
            btnThoat.Text = "Thoát";
            btnThoat.Click += btnThoat_Click;
            // 
            // DangNhap
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Sienna;
            ClientSize = new Size(1105, 652);
            Controls.Add(btnThoat);
            Controls.Add(btnDangNhap);
            Controls.Add(pictureBox1);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(txtMatKhau);
            Controls.Add(txtTenDangNhap);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "DangNhap";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Đăng Nhập";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtTenDangNhap;
        private TextBox txtMatKhau;
        private Label label1;
        private Label label2;
        private Label label3;
        private PictureBox pictureBox1;
        private LinkLabel linkLabel1;
        private Guna.UI2.WinForms.Guna2Button btnDangNhap;
        private Guna.UI2.WinForms.Guna2Button btnThoat;
    }
}
