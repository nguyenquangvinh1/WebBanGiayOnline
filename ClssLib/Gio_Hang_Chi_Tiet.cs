using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ClssLib
{
    public class Gio_Hang_Chi_Tiet
    {
        public Guid ID { get; set; }
        public decimal gia { get; set; }
        public int trang_thai { get; set; }
        public decimal thanh_tien { get; set; }
        public int so_luong { get; set; }
        [ForeignKey("Gio_Hang")]
        public Guid Gio_HangID { get; set; }
        [JsonIgnore]
        public virtual Gio_Hang Gio_Hang { get; set; }
        [ForeignKey("San_Pham_Chi_Tiet")]
        public Guid San_Pham_Chi_TietID { get; set; }
        [JsonIgnore]
        public virtual San_Pham_Chi_Tiet San_Pham_Chi_Tiet { get; set; }
    }
}
