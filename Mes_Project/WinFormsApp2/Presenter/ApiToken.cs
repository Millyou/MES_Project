using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsApp2.Presenter
{
    public static class ApiToken
    {
        public static string? UserId { get; set; }
        public static string? UserPw { get; set; }
        public static string? Token { get; set; } // jwt토근
        public static string? role { get; set; }
        public static string? Shift { get; set; }
        public static int? EmployeeNumber { get; set; }

    }

    public static class User
    {
        public static string UserName { get; set; } // 작업자 이름

        public static string publicKey { get; set; }
    }

    public static class Products
    {

        public static string? LotNum { get; set; } // lot 번호
        public static string? PartId { get; set; } // lot 번호Planned Quantity
        public static string? LineId { get; set; } // lot 번호Planned Quantity
        public static int? PlannQty { get; set; }
    }
}
