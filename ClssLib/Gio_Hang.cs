using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ClssLib
{
    public class Gio_Hang
    {
        public Guid ID { get; set; }
        public decimal tong_tien { get; set; }
        public int trang_thai { get; set; }
        [ForeignKey("Tai_Khoan")]
        public Guid Tai_KhoanID { get; set; }
        [JsonIgnore]
        public virtual Tai_Khoan Tai_Khoan { get; set; }
        [ForeignKey("Phieu_Giam_Gia")]
        public Guid Phieu_Giam_GiaID { get; set; }
        [JsonIgnore]
        public virtual Phieu_Giam_Gia Phieu_Giam_Gia { get; set; }
        [JsonIgnore]
        public virtual ICollection<Gio_Hang_Chi_Tiet> Gio_Hang_Chi_Tiets { get; set; }
    }
}
