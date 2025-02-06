using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ClssLib
{
    public class Vai_Tro
    {
        public Guid ID { get; set; }
        public string ten_vai_tro { get; set; }
        public int trang_thai { get; set; }
        public DateTime ngay_tao { get; set; }
        public DateTime? ngay_sua { get; set; }
        [JsonIgnore]
        public ICollection<Tai_Khoan>? tai_Khoans { get; set; }
    }
}
