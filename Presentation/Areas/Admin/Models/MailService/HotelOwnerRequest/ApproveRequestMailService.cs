using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using Presentation.Models.MailService;

namespace Presentation.Areas.Admin.Models.MailService.HotelOwnerRequest
{
    public class ApproveRequestMailService
    {
        public void SendMessage(string fullName, string userName, string email, string phoneNumber, string hotelName)
        {
            MimeMessage mimeMessage = new MimeMessage();

            MailboxAddress mailboxAddress = new MailboxAddress("OtelBudur Management", "otelbudurr@gmail.com");
            mimeMessage.From.Add(mailboxAddress);

            MailboxAddress mailboxAddressTo = new MailboxAddress(fullName, email);
            mimeMessage.To.Add(mailboxAddressTo);

            mimeMessage.Subject = "Registration For The Hotel Owner";

            var bodyBuilder = new BodyBuilder();
            bodyBuilder.HtmlBody = $"<p>Dear {fullName},</p> <p>Your registration was successfully approved.</p> <p>Click the link below to create your account:</p><a href='https://localhost:44382/SignUp/CreateHotelOwnerAccount?email={email}&fullName={fullName}&userName={userName}&phoneNumber={phoneNumber}&hotelName={hotelName}'>Create Account</a> <p>Reservation Status: <span style='color: #00ff66;'>Approved</span></p>";
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