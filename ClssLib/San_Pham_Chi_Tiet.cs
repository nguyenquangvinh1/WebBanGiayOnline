using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ClssLib
{
    public class San_Pham_Chi_Tiet
    {
        public Guid ID { get; set; }
        public string ma { get; set; }
        public decimal gia { get; set; }
        public int so_luong { get; set; }
        public int trang_thai { get; set; }
        [ForeignKey("Kich_Thuoc")]
        public Guid Kich_ThuocID { get; set; }
        [ForeignKey("Mau_Sac")]
        public Guid Mau_SacID { get; set; }
        [ForeignKey("San_Pham")]
        public Guid San_PhamID { get; set; }
        [JsonIgnore]
        public virtual Kich_Thuoc Kich_Thuoc { get; set; }
        [JsonIgnore]
        public virtual Mau_Sac Mau_Sac { get; set; }
        [JsonIgnore]
        public virtual San_Pham San_Pham { get; set; }
        [JsonIgnore]
        public virtual ICollection<Anh_San_Pham>? Anh_San_Phams { get; set; }
        [JsonIgnore]
        public virtual ICollection<Hoa_Don_Chi_Tiet>? Hoa_Don_Chi_Tiets { get; set; }


    }
}
