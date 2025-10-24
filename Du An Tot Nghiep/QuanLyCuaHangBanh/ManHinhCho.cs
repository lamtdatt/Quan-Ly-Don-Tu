using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QuanLyCuaHangBanh;

namespace GUI_CuaHangBanh
{
    public partial class ManHinhCho : Form
    {
        public ManHinhCho()
        {
            InitializeComponent();
        }
        private void ManHinhCho_Load(object sender, EventArgs e)
        {
            axWindowsMediaPlayer1.uiMode = "none";
            axWindowsMediaPlayer1.URL = Application.StartupPath + @"\Videointroduan(1).mp4";
            axWindowsMediaPlayer1.stretchToFit = true;
            axWindowsMediaPlayer1.Dock = DockStyle.Fill;
            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;
            this.BackColor = Color.Black;
            axWindowsMediaPlayer1.Ctlcontrols.play();
            System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
            timer.Interval = 6000; 
            timer.Tick += (s, args) =>
            {
                timer.Stop();
                axWindowsMediaPlayer1.Ctlcontrols.stop();
                axWindowsMediaPlayer1.Visible = false;
                this.BackgroundImage = Image.FromFile(Application.StartupPath + @"\anhnen.png");
                this.BackgroundImageLayout = ImageLayout.Stretch;

                // ❗ Hiện form đăng nhập đè lên (có hiệu ứng fade-in)
                DangNhap f = new DangNhap();
                f.StartPosition = FormStartPosition.CenterScreen;
                f.Opacity = 0;
                f.Show();

                System.Windows.Forms.Timer fade = new System.Windows.Forms.Timer();
                fade.Interval = 20; // thời gian mỗi bước tăng độ đậm
                fade.Tick += (ss, ee) =>
                {
                    if (f.Opacity < 1)
                        f.Opacity += 0.05;
                    else
                        fade.Stop();
                };
                fade.Start();
            };

            timer.Start();
        }
        private void AxWindowsMediaPlayer1_PlayStateChange(object sender, AxWMPLib._WMPOCXEvents_PlayStateChangeEvent e)
        {
        }
        
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void axWindowsMediaPlayer1_Enter(object sender, EventArgs e)
        {

        }
    }
}
