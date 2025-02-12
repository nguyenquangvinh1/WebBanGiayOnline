using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ClssLib
{
    public class Khach_Hang
    {
        public Guid ID { get; set; }

        public Guid Tai_KhoanID { get; set; }
        public string ho_ten { get; set; }
        public string ma { get; set; }
        public string ngay_sinh { get; set; }
        public int gioi_tinh { get; set; }
        public string sdt { get; set; }
        public string email { get; set; }
        public int trang_thai { get; set; }
        public DateTime ngay_tao { get; set; }

        public Tai_Khoan?Tai_Khoan { get; set; }
     
        [JsonIgnore]
        public virtual ICollection<Dia_Chi> Dia_Chi { get; set; }

        [JsonIgnore]
        public virtual ICollection<Hoa_Don> Hoa_Dons { get; set; }
    }
}
