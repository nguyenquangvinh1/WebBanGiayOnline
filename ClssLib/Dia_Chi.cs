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
        public string dia_chi_chi_tiet { get; set; }
        public bool? is_default { get; set; }

        public DateTime ngay_tao { get; set; }
        public DateTime ngay_sua { get; set; }
        [ForeignKey("Tai_Khoan")]
        public Guid Tai_KhoanID { get; set; }
        [JsonIgnore]
        public virtual Tai_Khoan? Tai_Khoan { get; set; }
    }
}
