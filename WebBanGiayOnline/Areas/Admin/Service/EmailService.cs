using MailKit.Net.Smtp;
using MimeKit;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

public class EmailService
{
    //private readonly IConfiguration _configuration;

    //public EmailService(IConfiguration configuration)
    //{
    //    _configuration = configuration;
    //}

    //public async Task SendEmailAsync(string toEmail, string subject, string body)
    //{
    //    var emailMessage = new MimeMessage();

    //    // Khởi tạo MailboxAddress với cả tên người gửi và địa chỉ email
    //    emailMessage.From.Add(new MailboxAddress(_configuration["EmailSettings:FromName"], _configuration["EmailSettings:FromAddress"]));

    //    // Khởi tạo địa chỉ người nhận
    //    emailMessage.To.Add(new MailboxAddress(toEmail));

    //    emailMessage.Subject = subject;
    //    emailMessage.Body = new TextPart("plain")
    //    {
    //        Text = body
    //    };

    //    using (var client = new SmtpClient())
    //    {
    //        await client.ConnectAsync(_configuration["EmailSettings:SmtpServer"], int.Parse(_configuration["EmailSettings:SmtpPort"]), false);
    //        await client.AuthenticateAsync(_configuration["EmailSettings:SmtpUsername"], _configuration["EmailSettings:SmtpPassword"]);
    //        await client.SendAsync(emailMessage);
    //        await client.DisconnectAsync(true);
    //    }
    //}
}
