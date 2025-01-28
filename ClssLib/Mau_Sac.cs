using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ClssLib
{
    public class Mau_Sac
    {
        public Guid ID { get; set; }
        public string ma { get; set; }
        public string ten_mau { get; set; }
        public int trang_thai { get; set; }
        public string mo_ta { get; set; }
        [JsonIgnore]
        public virtual ICollection<San_Pham_Chi_Tiet> San_Pham_Chi_Tiets { get; set; }
    }
}
