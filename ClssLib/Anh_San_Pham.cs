using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ClssLib
{
    public class Anh_San_Pham
    {
        public Guid ID { get; set; }
        public string ma { get; set; }
        public string anh_url { get; set; }
        public int trang_thai { get; set; }
        public string mo_ta { get; set; }
        [JsonIgnore]
        public virtual ICollection<San_Pham_Chi_Tiet>? San_Pham_Chi_Tiets { get; set; }
        [JsonIgnore]
        public virtual ICollection<San_Pham>? San_Phams { get; set; }
    }
}
