using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

namespace Presentation.Areas.Admin.Models.MailService
{
    public class SendForAdminsMailService
    {
        public void SendMessage(string fullName, string email, string subject, string body)
        {
            MimeMessage mimeMessage = new MimeMessage();

            MailboxAddress mailboxAddress = new MailboxAddress("OtelBudur Management", "otelbudurr@gmail.com");
            mimeMessage.From.Add(mailboxAddress);

            MailboxAddress mailboxAddressTo = new MailboxAddress(fullName, email);
            mimeMessage.To.Add(mailboxAddressTo);

            mimeMessage.Subject = subject;

            var bodyBuilder = new BodyBuilder();
            bodyBuilder.HtmlBody = $"<p>Dear {fullName},</p> <p>{body}</p>";
            mimeMessage.Body = bodyBuilder.ToMessageBody();

            using (var client = new SmtpClient())
            {
                client.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
                client.Authenticate("otelbudurr@gmail.com", "vnxlkgbgxiwncnlx");
                client.Send(mimeMessage);
                client.Disconnect(true);
            }
        }
    }
}
