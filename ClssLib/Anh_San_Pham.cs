using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ClssLib
{
    public class Anh_San_Pham
    {
        public Guid ID { get; set; }
        public string anh_url { get; set; }
        [ForeignKey("San_Pham")]
        public Guid? San_PhamID { get; set; }
        [JsonIgnore]
        public virtual ICollection<Anh_San_Pham_San_Pham_Chi_Tiet>?  Anh_San_Pham_San_Pham_Chi_Tiets  { get; set; }
        [JsonIgnore]
        public virtual San_Pham? San_Pham { get; set; }
    }
}
