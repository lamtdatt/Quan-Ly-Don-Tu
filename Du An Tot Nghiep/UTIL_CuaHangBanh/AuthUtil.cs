using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTIL_CuaHangBanh
{
    public class AuthUtil
    {
        private static object user;

        public static bool IsLogin()
        {
            if (user == null)
            {
                return false;
            }
            if (string.IsNullOrWhiteSpace("user.TenDangNhap"))
            {
                return false;
            }

            return true;
        }
    }
}
    
    
