using WebBanGiay.Models.ViewModel;

namespace WebBanGiay.Service
{
    public interface IVnPayService
    {
      
            string CreatePaymentUrl(HttpContext context, VnPaymentRequestModel model);
            VnPaymentResponseModel PaymentExecute(IQueryCollection collections);
        
    }
}
