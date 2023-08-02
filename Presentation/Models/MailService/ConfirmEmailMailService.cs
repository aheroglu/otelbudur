using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

namespace Presentation.Models.MailService
{
    public class ConfirmEmailMailService
    {
        public void SendMail(string email)
        {
            MimeMessage mimeMessage = new MimeMessage();

            MailboxAddress mailboxAddress = new MailboxAddress("OtelBudur Management", "otelbudurr@gmail.com");
            mimeMessage.From.Add(mailboxAddress);

            MailboxAddress mailboxAddressTo = new MailboxAddress(string.Empty, email);
            mimeMessage.To.Add(mailboxAddressTo);

            mimeMessage.Subject = "Confirm Your Email Address";

            var bodyBuilder = new BodyBuilder();
            bodyBuilder.HtmlBody = $"<p>Click the link for confirm your e-mail address.</p> <a href='https://localhost:44382/ConfirmEmail/Confirm?email={email}'>Confirm Email</a>";
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
