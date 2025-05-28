using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ClssLib
{
    public class De_Giay
    {
        public Guid ID { get; set; }

        [Display(Name = "Tên đế giày :")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Hãy nhập tên đế giày")]
        public string ten_de_giay { get; set; }
        public DateTime ngay_tao { get; set; }
        [JsonIgnore]
        public virtual ICollection<San_Pham>? San_Phams { get; set; }
        [JsonIgnore]
        public virtual ICollection<San_Pham_Chi_Tiet>? San_Pham_Chi_Tiets { get; set; }
    }
}
