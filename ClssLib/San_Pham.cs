using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClssLib
{
    public class San_Pham
    {
        public Guid ID { get; set; }
        public string ten_san_pham { get; set; }
        public string mo_ta { get; set; }
        public int trang_thai { get; set; }
        public DateTime ngay_tao { get; set; }
        public DateTime ngay_sua { get; set; }
        public string nguoi_tao { get; set; }
        public string nguoi_sua { get; set; }
    }
}
