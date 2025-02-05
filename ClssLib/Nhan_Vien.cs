using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ClssLib
{
    public class Nhan_Vien
    {
        public Guid ID { get; set; }
        [ForeignKey("Tai_Khoan")]
        public Guid? Tai_KhoanID { get; set; }
        public string ho_ten { get; set; }
        public string ma { get; set; }
        public string ngay_sinh { get; set; }
        public int gioi_tinh { get; set; }
        public string sdt { get; set; }
        public string email { get; set; }
        public int trang_thai { get; set; }
        public string cccd { get; set; }
        public DateTime ngay_tao { get; set; }
        [JsonIgnore]
        public Tai_Khoan Tai_Khoan { get; set; }
     
        [JsonIgnore]
        public virtual ICollection<Hoa_Don>? Hoa_Dons { get; set; }
    }
}
