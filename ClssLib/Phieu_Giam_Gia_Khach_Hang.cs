using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ClssLib
{
    public class Phieu_Giam_Gia_Khach_Hang
    {
        public Guid ID { get; set; }
        public int trang_thai { get; set; }
        public DateTime ngay_tao { get; set; }
        public DateTime ngay_sua { get; set; }
        public string nguoi_tao { get; set; }
        public string nguoi_sua { get; set; }
        [ForeignKey("Khach_Hang")]
        public Guid Khach_HangID { get; set; }
        [ForeignKey("Phieu_Giam_Gia")]
        public Guid Phieu_Giam_GiaID { get; set; }
        [JsonIgnore]
        public virtual Khach_Hang Khach_Hang { get; set; }
        [JsonIgnore]
        public virtual Phieu_Giam_Gia Phieu_Giam_Gia { get; set; }
    }
}
