using Entity.Concrete;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Presentation.Models.HotelOwnerLogIn;
using Presentation.Models.ManagementLogIn;
using Presentation.Models.SignIn;
using Presentation.Models.SignUp;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [AllowAnonymous]
    public class LoginController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public LoginController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Index()
        {
            if (TempData["SuccessMessage"] != null)
            {
                ViewBag.SuccessMessage = TempData["SuccessMessage"];
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(SignInViewModel signInViewModel)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(signInViewModel.UserName, signInViewModel.Password, true, true);

                if (result.IsLockedOut)
                {
                    ViewBag.LockOutMessage = "You have been temporarily blocked from the system because you have logged in too many times incorrectly. Please try again later.";
                    return View(signInViewModel);
                }

                if (result.Succeeded)
                {
                    var user = await _userManager.FindByNameAsync(signInViewModel.UserName);
                    var role = await _userManager.GetRolesAsync(user);

                    if (role.Contains("Admin"))
                    {
                        return RedirectToAction("Dashboard", "Admin");
                    }
                    if (role.Contains("Member"))
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    if (role.Contains("Staff"))
                    {
                        return RedirectToAction("Dashboard", "Staff");
                    }
                    if (role.Contains("Hotel Owner"))
                    {
                        return RedirectToAction("Dashboard", "HotelOwner");
                    }
                }

                else
                {
                    ViewBag.ErrorMessage = "Incorret user name or password!";
                }
            }

            return View();
        }

        [HttpGet]
        public IActionResult ManagementLogIn()
        {
            if (TempData["SuccessMessage"] != null)
            {
                ViewBag.SuccessMessage = TempData["SuccessMessage"];
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ManagementLogIn(ManagementLogInViewModel managementLogInViewModel)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(managementLogInViewModel.UserName, managementLogInViewModel.Password, true, true);

                if (result.IsLockedOut)
                {
                    ViewBag.LockOutMessage = "You have been temporarily blocked from the system because you have logged in too many times incorrectly. Please try again later.";
                    return View(managementLogInViewModel);
                }

                if (result.Succeeded)
                {
                    var user = await _userManager.FindByNameAsync(managementLogInViewModel.UserName);
                    var roles = await _userManager.GetRolesAsync(user);

                    if (roles.Contains("Admin"))
                    {
                        return RedirectToAction("Dashboard", "Admin");
                    }

                    else
                    {
                        ViewBag.LogInError = "Your Do Not Have Administrative Authority!";
                        return View();
                    }
                }

                else
                {
                    ViewBag.ErrorMessage = "Incorret user name or password!";
                }
            }

            return View(managementLogInViewModel);
        }

        [HttpGet]
        public IActionResult HotelOwnerLogIn()
        {
            if (TempData["SuccessMessage"] != null)
            {
                ViewBag.SuccessMessage = TempData["SuccessMessage"];
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> HotelOwnerLogIn(HotelOwnerLogInViewModel hotelOwnerLogInViewModel)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(hotelOwnerLogInViewModel.UserName, hotelOwnerLogInViewModel.Password, true, true);

                if (result.IsLockedOut)
                {
                    ViewBag.LockOutMessage = "You have been temporarily blocked from the system because you have logged in too many times incorrectly. Please try again later.";
                    return View(hotelOwnerLogInViewModel);
                }

                if (result.Succeeded)
                {
                    var user = await _userManager.FindByNameAsync(hotelOwnerLogInViewModel.UserName);
                    var roles = await _userManager.GetRolesAsync(user);

                    if (roles.Contains("Hotel Owner"))
                    {
                        return RedirectToAction("Dashboard", "HotelOwner");
                    }

                    else
                    {
                        ViewBag.LogInError = "Your Do Not Have Hotel Owner Authority!";
                        return View();
                    }
                }

                else
                {
                    ViewBag.ErrorMessage = "Incorret user name or password!";
                }
            }

            return View(hotelOwnerLogInViewModel);
        }

        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);
            return RedirectToAction("Index");
        }
    }
}
