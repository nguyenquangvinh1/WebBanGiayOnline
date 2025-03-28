using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using WebBanGiay.Models.ViewModel;

namespace WebBanGiay.Service
{
    public interface IGhnService
    {
        Task<int> CalculateFeeAsync(GHNShipping request);
    }

    public class GhnService : IGhnService
    {
        private readonly HttpClient _httpClient;
        private const string ApiUrl = "https://online-gateway.ghn.vn/shiip/public-api/v2/shipping-order/fee";
        private const string Token = "be4fd050-0497-11f0-b971-3a146ce707b3";
        private const int ShopId = 5692148;

        public GhnService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<int> CalculateFeeAsync(GHNShipping request)
        {
            try
            {
                _httpClient.DefaultRequestHeaders.Clear();
                _httpClient.DefaultRequestHeaders.Add("Token", Token);
                _httpClient.DefaultRequestHeaders.Add("ShopId", ShopId.ToString());

                var jsonContent = JsonSerializer.Serialize(request);
                var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync(ApiUrl, httpContent);
                var responseBody = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception("GHN API trả về lỗi: " + responseBody);
                }

                var result = JsonSerializer.Deserialize<GhnFeeResponse>(responseBody);
                return result?.data?.total ?? 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi gọi API GHN: " + ex.Message);
                return 0;
            }
        }
    }
}
