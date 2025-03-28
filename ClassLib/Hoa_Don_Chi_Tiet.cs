﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ClssLib
{
    public class Hoa_Don_Chi_Tiet
    {
        public Guid ID { get; set; }
        public string ma { get; set; }
        public decimal gia { get; set; }
        public int trang_thai { get; set; }
        public decimal thanh_tien { get; set; }
        public int so_luong { get; set; }
        public DateTime ngay_tao { get; set; }
        public DateTime ngay_sua { get; set; }
        public string nguoi_tao { get; set; }
        public string nguoi_sua { get; set; }
        [JsonIgnore]
        public virtual Hoa_Don Hoa_Don { get; set; }

    }
}
