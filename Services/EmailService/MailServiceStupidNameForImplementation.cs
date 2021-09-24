using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace new_project.Services.EmailService
{
    public class MailServiceImplementation: MailService
    {
        private readonly EmailSettings _emailSettings;
        public MailServiceImplementation(IOptions<EmailSettings> emailSettings)
        {
            this._emailSettings = emailSettings.Value;
        }

        public async Task SendEmailAsync(MailRequest mailRequest)
        {
            var email = new MimeMessage();
            email.Sender = MailboxAddress.Parse(_emailSettings.Mail);
            email.To.Add(MailboxAddress.Parse(mailRequest.reciver));
            email.Subject = mailRequest.subject;
            var builder = new BodyBuilder();
            if (mailRequest.attachment != null)
            {
                builder.Attachments.Add("cze.pdf", mailRequest.attachment);
            }
            builder.HtmlBody = mailRequest.body;
            email.Body = builder.ToMessageBody();

            using (var smtp = new SmtpClient())
            {
                smtp.Connect(_emailSettings.Host, _emailSettings.Port, SecureSocketOptions.StartTls);
                smtp.Authenticate(_emailSettings.Mail, _emailSettings.Password);
                await smtp.SendAsync(email);
                smtp.Disconnect(true);
            }
        }
    }
}
