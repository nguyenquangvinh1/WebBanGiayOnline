using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ClssLib
{
    public class De_Giay
    {
        public Guid ID { get; set; }
        public string ma { get; set; }
        public string ten_de_giay { get; set; }
        public int trang_thai { get; set; }
        public string mo_ta { get; set; }
        [JsonIgnore]
        public virtual ICollection<San_Pham>? San_Phams { get; set; }
    }
}
