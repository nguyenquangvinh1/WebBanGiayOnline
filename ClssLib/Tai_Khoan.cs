using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ClssLib
{
    public class Tai_Khoan
    {
        public Guid Tai_KhoanID { get; set; }
        public string ten_vai_tro { get; set; }
        public string user_name { get; set; }
        public string pass_word { get; set; }
        public int trang_thai { get; set; }
        public DateTime ngay_tao { get; set; }
        public DateTime ngay_sua { get; set; }

        public Nhan_Vien? Nhan_Vien { get; set; }

        public Khach_Hang? Khach_Hang { get; set; }
    }
}
