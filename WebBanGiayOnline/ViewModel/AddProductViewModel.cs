using System;
using System.ComponentModel.DataAnnotations;

namespace WebBanGiay.ViewModel
{
    public class AddProductViewModel
    {
        [Required]
        public Guid InvoiceId { get; set; }

        [Required]
        public Guid ProductId { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public decimal Price { get; set; }
    }
}
