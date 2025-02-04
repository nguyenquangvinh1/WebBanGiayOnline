using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ClssLib
{
    public class Dia_Chi
    {
        public Guid ID { get; set; }
        public int loai_dia_chi { get; set; }
        public string tinh { get; set; }
        public string huyen { get; set; }
        public string xa { get; set; }
        public DateTime ngay_tao { get; set; }
        public DateTime ngay_sua { get; set; }
        [ForeignKey("Khach_Hang")]
        public Guid Khach_HangID { get; set; }
        [JsonIgnore]
        public virtual Khach_Hang Khach_Hang { get; set; }
    }
}
