using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ClssLib
{
    public class Tai_Khoan
    {

        public Guid ID { get; set; }
        public string user_name { get; set; }
        public string? pass_word { get; set; }
        public string ho_ten { get; set; }
       
        public string ma { get; set; }
        public DateTime ngay_sinh { get; set; }
        public int gioi_tinh { get; set; }
        public string sdt { get; set; }
        public string email { get; set; }
        public int trang_thai { get; set; }
        public string cccd { get; set; }
        public DateTime ngay_tao { get; set; }
        public DateTime? ngay_sua { get; set; }
        [ForeignKey("Vai_Tro")]
        public Guid Vai_TroID { get; set; }
        [JsonIgnore]
        public virtual ICollection<Dia_Chi>? Dia_Chi { get; set; }
        [JsonIgnore]
        public virtual Vai_Tro Vai_Tro { get; set; }
        [JsonIgnore]
        public virtual ICollection<Tai_Khoan_Hoa_Don>? Tai_Khoan_Hoa_Don { get; set; }
        [JsonIgnore]
        public virtual ICollection<Phieu_Giam_Gia_Tai_Khoan>? Phieu_Giam_Gia_Tai_Khoans { get; set; }



    }
}
