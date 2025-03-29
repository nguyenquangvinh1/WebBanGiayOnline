namespace WebBanGiay.Models.ViewModel
{
    public class GHNShipping
    {
        public int from_district_id { get; set; }
        public string from_ward_code { get; set; } = string.Empty;
        public int service_id { get; set; }
        public int? to_district_id { get; set; }
        public string to_ward_code { get; set; } = string.Empty; 
        public int height { get; set; }
        public int length { get; set; }
        public int weight { get; set; }
        public int width { get; set; }
        public int? insurance_value { get; set; }
        public string? coupon { get; set; }
    }

    public class GhnFeeResponse
    {
        public int code { get; set; }
        public string? message { get; set; }
        public GhnFeeData? data { get; set; }
    }

    public class GhnFeeData
    {
        public int total { get; set; }
    }
}
