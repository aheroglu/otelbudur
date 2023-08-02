using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

namespace Presentation.Models.MailService
{
    public class RegisterAsHotelOwnerMailService
    {
        public void SendMessage(string fullName, string email)
        {
            MimeMessage mimeMessage = new MimeMessage();

            MailboxAddress mailboxAddress = new MailboxAddress("OtelBudur Management", "otelbudurr@gmail.com");
            mimeMessage.From.Add(mailboxAddress);

            MailboxAddress mailboxAddressTo = new MailboxAddress(fullName, email);
            mimeMessage.To.Add(mailboxAddressTo);

            mimeMessage.Subject = "Registration For Hotel Owner";

            var bodyBuilder = new BodyBuilder();
            bodyBuilder.HtmlBody = $"<p>Dear {fullName},</p> <p>Your registration was successfully received. We will contact you via e-mail about the registration status.</p> <p>Reservation Status: <span style='color: #ff8d29;'>Pending</span></p>";
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
