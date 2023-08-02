using DataAccess.Concrete;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

namespace Presentation.Models.MailService
{
    public class ReservationConfirmedMailService
    {
        public void SendMail(string fullName, string userMail, int reservationId)
        {
            MimeMessage mimeMessage = new MimeMessage();

            MailboxAddress mailboxAddress = new MailboxAddress("OtelBudur Management", "otelbudurr@gmail.com");
            mimeMessage.From.Add(mailboxAddress);

            MailboxAddress mailboxAddressTo = new MailboxAddress(fullName, userMail);
            mimeMessage.To.Add(mailboxAddressTo);

            mimeMessage.Subject = "Your Reservation is Confirmed";

            var bodyBuilder = new BodyBuilder();
            bodyBuilder.HtmlBody = $"<p>Dear {fullName}, Your reservation has been confirmed. We will inform you for about reservation status with email. Thanks for reserving our hotel!</p> <p>Reservation ID: {reservationId}</p> <p>Reservation Status: <span style='color: #ff8d29;'>Pending</span></p>";
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
