using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ClssLib
{
    public class Tai_Khoan_Hoa_Don
    {
        public Guid ID { get; set; }
        public string Ten { get; set; }
        public string vai_tro { get; set; }
        public DateTime ngay_tao { get; set; }
        public DateTime? ngay_sua { get; set; }
        [ForeignKey("Tai_Khoan")]
        public Guid? Tai_KhoanID { get; set; }
        [JsonIgnore]
        public virtual Tai_Khoan Tai_Khoan { get; set; }
        [ForeignKey("Hoa_Don")]
        public Guid Hoa_DonID { get; set; }
        
        [JsonIgnore]
        public virtual Hoa_Don Hoa_Don { get; set; }
    }
}
