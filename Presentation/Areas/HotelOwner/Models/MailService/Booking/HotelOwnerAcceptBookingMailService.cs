using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

namespace Presentation.Areas.HotelOwner.Models.MailService.Booking
{
    public class HotelOwnerAcceptBookingMailService
    {
        public void SendMessage(string fullName, string email)
        {
            MimeMessage mimeMessage = new MimeMessage();

            MailboxAddress mailboxAddress = new MailboxAddress("OtelBudur Management", "otelbudurr@gmail.com");
            mimeMessage.From.Add(mailboxAddress);

            MailboxAddress mailboxAddressTo = new MailboxAddress(fullName, email);
            mimeMessage.To.Add(mailboxAddressTo);

            mimeMessage.Subject = "Your Reservation Status";

            var bodyBuilder = new BodyBuilder();
            bodyBuilder.HtmlBody = $"<p>Dear {fullName},</p> <p>Your booking was successfully accepted.</p> <p>Reservation Status: <span style='color: #00ff66;'>Accepted</span></p>";
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
