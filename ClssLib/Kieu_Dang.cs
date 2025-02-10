using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ClssLib
{
    public class Kieu_Dang
    {
        public Guid ID { get; set; }
        public string ten_kieu_dang { get; set; }
        public DateTime Ngay_them { get; set; }
        [JsonIgnore]
        public virtual ICollection<San_Pham>? San_Phams { get; set; }
    }
}
