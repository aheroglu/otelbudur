using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

namespace Presentation.Models.MailService
{
    public class EvaluateExperienceMailService
    {
        public void SendMessage(string fullName, string email, string roomId)
        {
            MimeMessage mimeMessage = new MimeMessage();

            MailboxAddress mailboxAddress = new MailboxAddress("OtelBudur Management", "otelbudurr@gmail.com");
            mimeMessage.From.Add(mailboxAddress);

            MailboxAddress mailboxAddressTo = new MailboxAddress(fullName, email);
            mimeMessage.To.Add(mailboxAddressTo);

            mimeMessage.Subject = "Evaluate Your Expression";

            var bodyBuilder = new BodyBuilder();
            bodyBuilder.HtmlBody = $"<p>Dear {fullName},</p> <p>Your reservation has been completed successfully!. Please click the link for evaluate you expression.</p> <a href='https://localhost:44382/EvaluateReservation/Evaluate?email={email}&roomId={roomId}'>Evaluate Expression</a>";
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
