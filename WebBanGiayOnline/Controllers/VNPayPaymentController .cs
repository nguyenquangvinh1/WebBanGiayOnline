using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
namespace WebBanGiay.Controllers
{
    [Area("Admin")]
    [Route("Admin/[controller]/[action]")]// Route rõ ràng nằm dưới Admin

    public class VNPayPaymentController : Controller
    {
        private readonly IConfiguration _config;
        public VNPayPaymentController(IConfiguration config)
        {
            _config = config;
        }

        // Các thông số cấu hình VNPay (nên lấy từ appsettings.json để bảo mật)
        private readonly string vnpUrl = "https://sandbox.vnpayment.vn/paymentv2/vpcpay.html"; // URL cho môi trường test
        private readonly string vnp_TmnCode = "GSIGY0LR";     // Thay YOUR_TMN_CODE bằng mã merchant của bạn
        private readonly string vnp_HashSecret = "W32ZCWCRDTAUN6RG27J1KG39ISHAYDST"; // Thay YOUR_SECRET_KEY bằng khóa bí mật của bạn
        private readonly string returnUrl = "https://localhost:7243/BHTQ/PaymentCallBack";

        /// <summary>
        /// Endpoint tạo request thanh toán VNPayS
        /// </summary>
        /// <param name="amount">Số tiền thanh toán (đơn vị VND)</param>
        /// <param name="orderInfo">Thông tin mô tả đơn hàng</param>
        /// <returns>Redirect URL đến VNPay</returns>

        public IActionResult CreatePayment(double amount, string orderInfo)
        {
            try
            {
                // Lấy cấu hình từ appsettings.json
                var vnp_Version = _config["VnPay:Version"] ?? "2.1.0";
                var vnp_Command = _config["VnPay:Command"] ?? "pay";

                var vnp_TmnCode = _config["VnPay:TmnCode"];
                var vnp_Amount = ((int)(amount * 100)).ToString(); // Đổi ra đơn vị VND * 100
                var vnp_CreateDate = DateTime.Now.ToString("yyyyMMddHHmmss");
                var vnp_CurrCode = _config["VnPay:CurrCode"] ?? "VND";
                var vnp_IpAddr = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "127.0.0.1";
                var vnp_Locale = _config["VnPay:Locale"] ?? "vn";
                var vnp_ReturnUrl = _config["VnPay:PaymentBackReturnUrl"];
                var vnp_Url = _config["VnPay:BaseUrl"];

                var vnp_HashSecret = _config["VnPay:HashSecret"];
                

                // Thông tin đơn hàng
                var vnp_TxnRef = DateTime.Now.Ticks.ToString(); // Mã đơn hàng (duy nhất)

                var vnp_Params = new SortedDictionary<string, string>
        {
            { "vnp_Version", vnp_Version },
            { "vnp_Command", vnp_Command },
            { "vnp_TmnCode", vnp_TmnCode },
            { "vnp_Amount", vnp_Amount },
                     { "vnp_CreateDate", vnp_CreateDate },
            { "vnp_CurrCode", vnp_CurrCode },
                        { "vnp_IpAddr", vnp_IpAddr },
                                    { "vnp_Locale", vnp_Locale },
            { "vnp_OrderInfo", orderInfo ?? "Thanh toan don hang" },
                        { "vnp_OrderType", "other" },
            { "vnp_ReturnUrl", vnp_ReturnUrl },

            { "vnp_TxnRef", vnp_TxnRef }
   
        };

                // Bước 2: Tạo raw data từ vnp_Params để tạo chuỗi hash
                var rawData = string.Join("&", vnp_Params.Select(kvp => $"{Uri.EscapeDataString(kvp.Key)}={Uri.EscapeDataString(kvp.Value)}"));
                // Bước 3: Tạo mã hash bảo mật SHA512
                string vnp_SecureHash = CalculateVnpaySecureHash(vnp_HashSecret, vnp_Params);

                // Bước 4: Gắn SecureHash vào tham số
                vnp_Params.Add("vnp_SecureHash", vnp_SecureHash);

                // Bước 5: Tạo URL chuyển hướng thanh toán
                string query = string.Join("&", vnp_Params.Select(kvp => $"{kvp.Key}={Uri.EscapeDataString(kvp.Value)}"));
                string paymentUrl = $"{vnp_Url}?{query}";

                return Json(new { success = true, redirectUrl = paymentUrl });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = $"Lỗi tạo request VNPay: {ex.Message}" });
            }
        }


        private string CalculateVnpaySecureHash(string key, SortedDictionary<string, string> inputData)
        {
            // Bước 1: Tạo chuỗi dữ liệu raw (đã URL-encode, theo thứ tự key a-z)
            var rawData = string.Join("&", inputData
                .Where(kvp => !string.IsNullOrEmpty(kvp.Value)) // bỏ qua value rỗng
                .Select(kvp => $"{Uri.EscapeDataString(kvp.Key)}={Uri.EscapeDataString(kvp.Value)}"));

            // Bước 2: Tính HMAC SHA512 với secret key
            byte[] keyBytes = Encoding.UTF8.GetBytes(key);
            byte[] inputBytes = Encoding.UTF8.GetBytes(rawData);
            using (var hmac = new HMACSHA512(keyBytes))
            {
                byte[] hashValue = hmac.ComputeHash(inputBytes);
                return BitConverter.ToString(hashValue).Replace("-", "").ToUpper(); // chữ in hoa
            }
        }



        /// <summary>
        /// Hàm tính HMAC SHA512 dựa trên secret key và chuỗi data
        /// </summary>
        private string CalculateHMACSHA512(string key, string data)
        {
            byte[] keyBytes = Encoding.UTF8.GetBytes(key);
            byte[] dataBytes = Encoding.UTF8.GetBytes(data);
            using (var hmac = new HMACSHA512(keyBytes))
            {
                byte[] hashBytes = hmac.ComputeHash(dataBytes);
                return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
            }
        }


        /// <summary>
        /// Endpoint callback sau khi thanh toán VNPay xong
        /// </summary>
        [HttpGet("PaymentReturn")]
        public IActionResult PaymentReturn()
        {
            // VNPay sẽ trả về các tham số trong query string
            var vnp_Params = Request.Query.ToDictionary(q => q.Key, q => q.Value.ToString());
            string vnp_ResponseCode = vnp_Params.ContainsKey("vnp_ResponseCode") ? vnp_Params["vnp_ResponseCode"] : "";

            // Kiểm tra secure hash và trạng thái giao dịch nếu cần
            if (vnp_ResponseCode == "00")
            {
                // Nếu giao dịch thành công, cập nhật trạng thái đơn hàng trong CSDL
                ViewBag.Message = "Thanh toán thành công!";
            }
            else
            {
                ViewBag.Message = "Thanh toán thất bại!";
            }

            return View(); // Trả về view hiển thị thông báo
        }
    }
}
