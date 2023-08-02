using DataAccess.Concrete;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using Presentation.Models.MailService;
using System.Linq;
using MailKit.Security;
using Presentation.Models.ResetPassword;
using Microsoft.AspNetCore.Identity;
using Entity.Concrete;
using System.Threading.Tasks;
using System;

namespace Presentation.Controllers
{
    [AllowAnonymous]
    public class ForgotPasswordController : Controller
    {
        private readonly Context _context;
        private readonly UserManager<AppUser> _userManager;

        public ForgotPasswordController(Context context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult Index()
        {
            if (TempData["ErrorMessage"] != null)
            {
                ViewBag.ErrorMessage = TempData["ErrorMessage"];
            }

            return View();
        }

        [HttpPost]
        public IActionResult Index(ForgotPasswordMailService forgotPasswordMailService)
        {
            if (ModelState.IsValid)
            {
                var isRegistered = _context.Users.Any(x => x.Email == forgotPasswordMailService.Receiver);

                if (!isRegistered)
                {
                    TempData["ErrorMessage"] = "This Email Not Registered in OtelBudur!";
                    return RedirectToAction("Index");
                }

                MimeMessage mimeMessage = new MimeMessage();

                MailboxAddress mailboxAddress = new MailboxAddress("OtelBudur Management", "otelbudurr@gmail.com");
                mimeMessage.From.Add(mailboxAddress);

                var user = _context.Users.FirstOrDefault(x => x.Email == forgotPasswordMailService.Receiver);

                MailboxAddress mailboxAddressTo = new MailboxAddress(user.FullName, forgotPasswordMailService.Receiver);
                mimeMessage.To.Add(mailboxAddressTo);

                mimeMessage.Subject = "Reset Your Password";

                var bodyBuilder = new BodyBuilder();
                bodyBuilder.HtmlBody = $"<p>Click the link below to reset your password:</p><a href='https://localhost:44382/ForgotPassword/ResetPassword?email={forgotPasswordMailService.Receiver}'>Reset Password</a>";
                mimeMessage.Body = bodyBuilder.ToMessageBody();

                using (var client = new SmtpClient())
                {
                    client.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
                    client.Authenticate("otelbudurr@gmail.com", "vnxlkgbgxiwncnlx");
                    client.Send(mimeMessage);
                    client.Disconnect(true);

                    TempData["SuccessMessage"] = "Email Was Successfully Sent For Reset Your Password. Please Check Your Mailbox.";

                    return RedirectToAction("Index", "LogIn");
                }

            }

            return View(forgotPasswordMailService);
        }

        [HttpGet]
        public IActionResult ResetPassword(string email)
        {
            var resetPasswordViewModel = new ResetPasswordViewModel
            {
                Email = email
            };

            return View(resetPasswordViewModel);

        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel resetPasswordViewModel)
        {
            if (ModelState.IsValid)
            {
                var user = _userManager.Users.FirstOrDefault(x => x.Email == resetPasswordViewModel.Email);

                user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, resetPasswordViewModel.Password);
                user.PasswordLastChange = DateTime.Now;

                var result = await _userManager.UpdateAsync(user);

                if (result.Succeeded)
                {
                    TempData["SuccessMessage"] = "Your Password Updated Successfully! Please Sign In Again.";
                    return RedirectToAction("LogOut", "LogIn");
                }

                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }

            return View(resetPasswordViewModel);
        }
    }
}
