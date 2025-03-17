using WebBanGiay.Models.ViewModel;

namespace WebBanGiay.Service
{
    public interface IMomoService
    {
        Task<MomoCreatePaymentResponseModel> CreatePaymentMoMo(MomoExecuteResponseModel model );
        MomoExecuteResponseModel PaymentExecuteAsync(IQueryCollection collection);
    }
}
