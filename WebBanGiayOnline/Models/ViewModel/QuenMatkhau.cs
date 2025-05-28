using System.ComponentModel.DataAnnotations;

namespace WebBanGiay.Models.ViewModel
{
    public class QuenMatkhau
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
