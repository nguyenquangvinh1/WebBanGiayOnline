using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ClssLib
{
    public class Phieu_Giam_Gia_Tai_Khoan
    {
        public Guid ID { get; set; }
        public int trang_thai { get; set; }
        public DateTime ngay_tao { get; set; }
        public DateTime? ngay_sua { get; set; }
        public string nguoi_tao { get; set; }
        public string? nguoi_sua { get; set; }
        [ForeignKey("Tai_Khoan")]
        public Guid Tai_KhoanID { get; set; }
        [ForeignKey("Phieu_Giam_Gia")]
        public Guid Phieu_Giam_GiaID { get; set; }
        [JsonIgnore]
        public virtual Tai_Khoan Tai_Khoan { get; set; }
        [JsonIgnore]
        public virtual Phieu_Giam_Gia Phieu_Giam_Gia { get; set; }
    }
}
