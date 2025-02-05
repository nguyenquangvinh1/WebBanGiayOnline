using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ClssLib
{
    public class Phuong_Thuc_Thanh_Toan
    {
        public Guid ID { get; set; }
        public string ma { get; set; }
        public string ten_phuong_thuc { get; set; }
        [JsonIgnore]
        public virtual ICollection<Thanh_Toan>? Thanh_Toans { get; set; }
    }
}
