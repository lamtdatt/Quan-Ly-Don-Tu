using GUI_CuaHangBanh;

namespace QuanLyCuaHangBanh
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new ManHinhCho());
            //Application.Run(new ManHinhChinh());
            //ManHinhChinh frm = new ManHinhChinh(Convert.ToString(1));
            //frm.Show();
        }
    }
}