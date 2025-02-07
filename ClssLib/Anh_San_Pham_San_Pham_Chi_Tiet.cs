using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ClssLib
{
    public class Anh_San_Pham_San_Pham_Chi_Tiet
    {
        public Guid ID { get; set; }
        [ForeignKey("Anh_San_Pham")]
        public Guid Anh_San_PhamID { get; set; }
        [JsonIgnore]
        public virtual Anh_San_Pham Anh_San_Pham { get; set; }
        [ForeignKey("San_Pham_Chi_Tiet")]
        public Guid San_Pham_Chi_TietID { get; set; }
        [JsonIgnore]
        public virtual San_Pham_Chi_Tiet San_Pham_Chi_Tiet { get; set; }
    }
}
