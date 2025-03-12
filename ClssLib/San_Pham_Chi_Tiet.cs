using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ClssLib
{
    public class San_Pham_Chi_Tiet
    {
        public Guid ID { get; set; }

        public int? MaSP { get; set; }

        public string? moTa { get; set; }
        public string ten_SPCT { get; set; }
        public decimal gia { get; set; }
        public int so_luong { get; set; }
        public int trang_thai { get; set; }
        public DateTime ngay_tao { get; set; }
        public DateTime? ngay_sua { get; set; }
        [ForeignKey("Kich_Thuoc")]
        public Guid? Kich_ThuocID { get; set; }
        [ForeignKey("Mau_Sac")]
        public Guid? Mau_SacID { get; set; }
        [ForeignKey("San_Pham")]
        public Guid San_PhamID { get; set; }
        [JsonIgnore]
        [IgnoreDataMember]
        public virtual Kich_Thuoc? Kich_Thuoc { get; set; }
        [JsonIgnore]
        [IgnoreDataMember]
        public virtual Mau_Sac? Mau_Sac { get; set; }
        [JsonIgnore]
        public virtual San_Pham? San_Pham { get; set; }
        [JsonIgnore]
        public virtual ICollection<Anh_San_Pham_San_Pham_Chi_Tiet>? Anh_San_Pham_San_Pham_Chi_Tiets { get; set; }
        [JsonIgnore]
        public virtual ICollection<Hoa_Don_Chi_Tiet>? Hoa_Don_Chi_Tiets { get; set; }
        
        [JsonIgnore]
        public virtual ICollection<Gio_Hang_Chi_Tiet>? Gio_Hang_Chi_Tiets { get; set; }


    }
}
