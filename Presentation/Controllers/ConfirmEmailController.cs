using Entity.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Presentation.Models.ConfirmEmail;
using Presentation.Models.MailService;
using System.Linq;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    public class ConfirmEmailController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ConfirmEmailMailService _confirmEmailMailService;

        public ConfirmEmailController(UserManager<AppUser> userManager, ConfirmEmailMailService confirmEmailMailService)
        {
            _userManager = userManager;
            _confirmEmailMailService = confirmEmailMailService;
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
        public IActionResult Index(ConfirmEmailViewModel confirmEmailViewModel)
        {
            if (ModelState.IsValid)
            {
                bool isRegistered = _userManager.Users.Any(x => x.Email == confirmEmailViewModel.Email);
                if (!isRegistered)
                {
                    TempData["ErrorMessage"] = "This email not registered. Pleas enter your correct email!";
                    return RedirectToAction("Index");
                }

                _confirmEmailMailService.SendMail(confirmEmailViewModel.Email);
                TempData["SuccessMessage"] = "Confirmation email was sent, please check your mailbox!";
                return RedirectToAction("Index", "Home");
            }

            else
            {
                return View(confirmEmailViewModel);
            }
        }

        public async Task<IActionResult> Confirm(ConfirmEmailViewModel confirmEmailViewModel)
        {
            var user = _userManager.Users.FirstOrDefault(x => x.Email == confirmEmailViewModel.Email);
            user.EmailConfirmed = true;
            var result = await _userManager.UpdateAsync(user);

            if (result.Succeeded)
            {
                TempData["SuccessMessage"] = "Your email address was successfully confirmed!";
                return RedirectToAction("Index", "Home");
            }

            else
            {
                TempData["ErrorMessage"] = "Error!";
                return RedirectToAction("Index", "Home");
            }
        }
    }
}
