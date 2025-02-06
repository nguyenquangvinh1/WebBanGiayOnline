using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ClssLib
{
    public class Thanh_Toan
    {
        public Guid ID { get; set; }
        public int trang_thai { get; set; }
        public decimal so_tien_thanh_toan { get; set; }
        public DateTime ngay_thanh_toan { get; set; }
        public string mo_ta { get; set; }
        [ForeignKey("Hoa_Don")]
        public Guid Hoa_DonID { get; set; }
        [ForeignKey("Phuong_Thuc_Thanh_Toan")]
        public Guid? Phuong_Thuc_Thanh_ToanID { get; set; }
        [JsonIgnore]
        public virtual Hoa_Don Hoa_Don { get; set; }
        [JsonIgnore]
        public virtual Phuong_Thuc_Thanh_Toan? Phuong_Thuc_Thanh_Toan { get; set; }
    }
}
