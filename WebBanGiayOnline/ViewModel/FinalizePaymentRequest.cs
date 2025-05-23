﻿namespace WebBanGiay.ViewModel
{
    public class FinalizePaymentRequest
    {

        public Guid InvoiceId { get; set; }
        public Guid? VoucherId { get; set; }
        public double FinalTotal { get; set; }
        public double? ship { get; set; }
        public string VoucherCodeString { get; set; }
		public string FullAddress { get; set; }  // Địa chỉ người nhận

		public List<PaymentRowDto> PaymentRows { get; set; }
    }
    public class PaymentRowDto
    {
        public double Amount { get; set; }
        public string Method { get; set; } // "cash" / "transfer"
    }
}
