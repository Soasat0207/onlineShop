using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineShop.Common //mục đích của cái class này là ở bên ngoài có thể sử dụng cho tất cả mọi chỗ
{
    public static class CommonConstants
    {
        public static string USER_SESSION = "USER_SESSION";
        public static string SESSION_CREDENTIALS = "SESSION_CREDENTIALS";
        public static string CartSession = "CartSession";
        public static string CurrentCulture { set; get; } //đa ngôn ngữ
    }
}