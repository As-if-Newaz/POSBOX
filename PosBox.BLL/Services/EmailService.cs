using Microsoft.Extensions.Options;
using PosBox.BLL.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace PosBox.BLL.Services
{
    public class EmailService
    {
        private readonly SmtpSettings _smtpSettings;

        public EmailService(IOptions<SmtpSettings> smtpSettings)
        {
            _smtpSettings = smtpSettings.Value;
        }

        public async Task SendEmailAsync(string toEmail, string subject, string body)
        {
            // Get SMTP settings from environment variables via SecretManager
            string server = SecretManager.SmtpServer;
            int port = SecretManager.SmtpPort;
            string senderEmail = SecretManager.SmtpSenderEmail;
            string password = SecretManager.SmtpPassword;

            var message = new MailMessage
            {
                From = new MailAddress(senderEmail),
                Subject = subject,
                Body = body,
                IsBodyHtml = false
            };
            message.To.Add(new MailAddress(toEmail));

            using (var client = new SmtpClient(server, port))
            {
                client.Credentials = new NetworkCredential(senderEmail, password);
                client.EnableSsl = true;
                await client.SendMailAsync(message);
            }
        }
    }
}
