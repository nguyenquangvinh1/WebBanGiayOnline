using System;
using System.Text.RegularExpressions;
namespace WebBanGiay.Areas.Helpers
{
    public class CccdParser
    {
        // Ví dụ: trích xuất họ tên từ chuỗi có dạng "Họ tên: Nguyễn Văn A"
        public string ExtractFullName(string text)
        {
            var match = Regex.Match(text, @"Họ\s*tên\s*:\s*(?<name>[^\n\r]+)", RegexOptions.IgnoreCase);
            return match.Success ? match.Groups["name"].Value.Trim() : "";
        }

        // Ví dụ: trích xuất ngày sinh có định dạng dd/mm/yyyy hoặc dd-mm-yyyy
        public DateTime? ExtractBirthDate(string text)
        {
            var match = Regex.Match(text, @"Ngày\s*sinh\s*:\s*(?<date>\d{1,2}[/-]\d{1,2}[/-]\d{2,4})", RegexOptions.IgnoreCase);
            if (match.Success && DateTime.TryParse(match.Groups["date"].Value, out DateTime birthDate))
            {
                return birthDate;
            }
            return null;
        }

        // Ví dụ: trích xuất số CCCD từ chuỗi có dạng "Số CCCD: 034304011077"
        public string ExtractCccdNumber(string text)
        {
            var match = Regex.Match(text, @"Số\s*CCCD\s*:\s*(?<cccd>\d+)", RegexOptions.IgnoreCase);
            return match.Success ? match.Groups["cccd"].Value.Trim() : "";
        }
    }
}
