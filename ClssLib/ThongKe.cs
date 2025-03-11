//using System;
//using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations.Schema;
//using System.Linq;
//using System.Text;
//using System.Text.Json.Serialization;
//using System.Threading.Tasks;

//namespace ClssLib
//{
//    public class ThongKe
//    { 
//        public Guid? Id { get; set; }

//        public decimal TongTien { get; set; }
//         public int DonThanhCong { get; set; }
//        public int DonHuy { get; set; }

//        public int SoLuongSP { get; set; }
//        public DateTime DoanhThu { get; set; }



//        [ForeignKey("Hoa_Don")]
//        public Guid Hoa_DonID { get; set; }
//        [ForeignKey("San_Pham_Chi_Tiet")]
//        public Guid San_Pham_Chi_TietID { get; set; }
//        [JsonIgnore]
//        public virtual Hoa_Don Hoa_Don { get; set; }
//        [JsonIgnore]
//        public virtual San_Pham_Chi_Tiet San_Pham_Chi_Tiet { get; set; }
//    }
//}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ClssLib
{
    public class ThongKe
    {
        public Guid? Id { get; set; }

        public decimal TongTien { get; set; }
        public int DonThanhCong { get; set; }
        public int DonHuy { get; set; }

        public int SoLuongSP { get; set; }
        public DateTime DoanhThu { get; set; }



        [ForeignKey("Hoa_Don")]
        public Guid Hoa_DonID { get; set; }

        [ForeignKey("San_Pham_Chi_Tiet")]
        public Guid? San_Pham_Chi_TietID { get; set; }

        [JsonIgnore]
        public virtual Hoa_Don Hoa_Don { get; set; }
        [JsonIgnore]
        public virtual San_Pham_Chi_Tiet San_Pham_Chi_Tiet { get; set; }


    }
}