using Microsoft.Extensions.Configuration;
using MimeKit;
using MailKit.Net.Smtp;
using MailKit.Security;

namespace BlazorApp.Services
{
    public class EmailService
    {
        readonly IConfiguration cfg;

        public EmailService(IConfiguration cfg)
        {
            this.cfg = cfg;
        }

        public async Task SendAsync(string toEmail, string subject, string body)
        {
            string host = cfg["Email:SmtpHost"];
            int port = int.Parse(cfg["Email:SmtpPort"]);
            bool tls = bool.Parse(cfg["Email:UseStartTls"]);

            string fromEmail = cfg["Email:FromEmail"];
            string fromName = cfg["Email:FromName"];
            string user = cfg["Email:Username"];
            string pass = cfg["Email:Password"];

            var msg = new MimeMessage();
            msg.From.Add(new MailboxAddress(fromName, fromEmail));
            msg.To.Add(MailboxAddress.Parse(toEmail));
            msg.Subject = subject;

            msg.Body = new TextPart("plain")
            {
                Text = body
            };

            using var smtp = new SmtpClient();
            await smtp.ConnectAsync(host, port, tls ? SecureSocketOptions.StartTls : SecureSocketOptions.Auto);
            await smtp.AuthenticateAsync(user, pass);
            await smtp.SendAsync(msg);
            await smtp.DisconnectAsync(true);
        }
    }
}