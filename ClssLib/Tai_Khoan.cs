using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
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
        [ForeignKey("Nhan_Vien")]
        public Guid? Nhan_VienID { get; set; }
        [ForeignKey("Khach_Hang")]
        public Guid? Khach_HangID { get; set; }
        [JsonIgnore]
        public Nhan_Vien? Nhan_Vien { get; set; }
        [JsonIgnore]
        public Khach_Hang? Khach_Hang { get; set; }
    }
}
