using API_PBL.Models.DtoModels;
using Microsoft.Extensions.Options;
using MimeKit;
using MailKit.Net;
using MailKit.Net.Smtp;

namespace API_PBL.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;
        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task SendEmail(EmailDto emailRequest)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(_configuration.GetSection("SMTP:EmailName").Value));
            email.To.Add(MailboxAddress.Parse(emailRequest.ToEmail));
            email.Subject = emailRequest.Subject;
            email.Body = new TextPart(MimeKit.Text.TextFormat.Text) { Text = emailRequest.Body };

            var smtp = new SmtpClient();
            smtp.Connect("smtp.gmail.com",587,MailKit.Security.SecureSocketOptions.StartTls);
            smtp.Authenticate(_configuration.GetSection("SMTP:EmailName").Value, _configuration.GetSection("SMTP:EmailPassword").Value);
            await smtp.SendAsync(email);
            smtp.Disconnect(true);  
                        
        } 
    }
}
