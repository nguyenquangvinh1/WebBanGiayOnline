using Newtonsoft.Json.Linq;
using WebBanGiay.Areas.Admin.Models.ViewModel;

namespace WebBanGiay.Areas.Admin.Service
{
    public class AppSettingsHelper
    {
        //private static readonly JObject _config;
        //
        //static AppSettingsHelper()
        //{
        //    string jsonPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "appsettings.json");
        //    if (File.Exists(jsonPath))
        //    {
        //        string json = File.ReadAllText(jsonPath);
        //        _config = JObject.Parse(json);
        //    }
        //    else
        //    {
        //        _config = new JObject();
        //    }
        //}

        //public static string Get(string key)
        //{
        //    return _config.SelectToken(key)?.ToString();
        //}

        //public static EmailSetting GetEmailSettings()
        //{
        //    return new EmailSetting
        //    {
        //        Domain = Get("EmailSettings.Domain"),
        //        SmtpServer = Get("EmailSettings.SmtpServer"),
        //        Port = int.Parse(Get("EmailSettings.Port")),
        //        SenderEmail = Get("EmailSettings.SenderEmail"),
        //        SenderPassword = Get("EmailSettings.SenderPassword"),
        //        EnableSSL = bool.Parse(Get("EmailSettings.EnableSSL"))
        //    };
        //}
    }
}
