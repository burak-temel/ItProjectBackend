using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using MimeKit;
using MimeKit.Text;
using System;
using System.Linq;

namespace Core.Utilities.Mail
{
    public class MailManager : IMailService
    {

        private readonly IConfiguration _configuration;
        public MailManager(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public void Send(EmailMessage emailMessage)
        {
            var message = new MimeMessage();

            message.From.Add(MailboxAddress.Parse(_configuration.GetSection("EmailConfiguration").GetSection("UserNAme").Value));
            message.To.Add(MailboxAddress.Parse(emailMessage.ToAddress));
            message.Subject = emailMessage.Subject;
            message.Body = new TextPart(TextFormat.Html)
            {
                Text = emailMessage.Content
            };

            using (var emailClient = new SmtpClient())
            {
                emailClient.Connect(
                    _configuration.GetSection("EmailConfiguration").GetSection("SmtpServer").Value,
                    Convert.ToInt32(_configuration.GetSection("EmailConfiguration").GetSection("SmtpPort").Value),
                    MailKit.Security.SecureSocketOptions.Auto);
                emailClient.Authenticate(_configuration.GetSection("EmailConfiguration").GetSection("UserName").Value, _configuration.GetSection("EmailConfiguration").GetSection("Password").Value);
                emailClient.Send(message);
                emailClient.Disconnect(true);
            }
        }
    }
}
