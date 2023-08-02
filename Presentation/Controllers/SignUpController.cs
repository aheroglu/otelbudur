using Business.Concrete;
using Business.ValidationRules;
using DataAccess.Concrete;
using DataAccess.EntityFramework;
using Entity.Concrete;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Presentation.Areas.Admin.Models.HotelOwnerRequest;
using Presentation.Models.MailService;
using Presentation.Models.SignIn;
using Presentation.Models.SignUp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [AllowAnonymous]
    public class SignUpController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RegisterAsHotelOwnerMailService _registerAsHotelOwnerMailService;

        Context context = new Context();
        HotelOwnerRequestManager hotelOwnerRequestManager = new HotelOwnerRequestManager(new EfHotelOwnerRequestDal());

        public SignUpController(UserManager<AppUser> userManager, RegisterAsHotelOwnerMailService registerAsHotelOwnerMailService)
        {
            _userManager = userManager;
            _registerAsHotelOwnerMailService = registerAsHotelOwnerMailService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(SignUpViewModel signUpViewModel)
        {
            if (ModelState.IsValid)
            {
                AppUser user = new AppUser
                {
                    FullName = signUpViewModel.FullName,
                    UserName = signUpViewModel.UserName,
                    Email = signUpViewModel.Email,
                    PhoneNumber = signUpViewModel.PhoneNumber,
                    Image = "/Templates/main/assets/img/default-user-image.jpg",
                    MemberSince = DateTime.Now,
                    PasswordLastChange = DateTime.Now,
                };

                if (signUpViewModel.Password != signUpViewModel.ConfirmPassword)
                {
                    ModelState.AddModelError(string.Empty, "Paswords Do Not Match!");
                    return View(signUpViewModel);
                }

                bool isRegistered = context.Users.Any(x => x.Email == signUpViewModel.Email);
                if (isRegistered)
                {
                    ViewBag.AlreadyRegistered = "This Email Already Registered!";
                    return View(signUpViewModel);
                }

                var result = await _userManager.CreateAsync(user, signUpViewModel.Password);

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "Member");

                    TempData["SuccessMessage"] = "Your Account Created Successfully!";
                    return RedirectToAction("Index", "LogIn");
                }

                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }

            return View(signUpViewModel);
        }

        [HttpGet]
        public IActionResult RegisterAsHotelOwner()
        {
            List<SelectListItem> locations = (from x in context.Locations.Where(x => x.Status == true).OrderBy(x => x.Name)
                                              select new SelectListItem
                                              {
                                                  Text = x.Name,
                                                  Value = x.Id.ToString()
                                              }).ToList();

            ViewBag.Locations = locations;

            return View();
        }

        [HttpPost]
        public IActionResult RegisterAsHotelOwner(HotelOwnerRequest hotelOwnerRequest)
        {
            List<SelectListItem> locations = (from x in context.Locations.Where(x => x.Status == true).OrderBy(x => x.Name)
                                              select new SelectListItem
                                              {
                                                  Text = x.Name,
                                                  Value = x.Id.ToString()
                                              }).ToList();

            ViewBag.Locations = locations;

            HotelOwnerRequestValidator validator = new HotelOwnerRequestValidator();
            ValidationResult results = validator.Validate(hotelOwnerRequest);

            bool isRegistered = context.Users.Any(x => x.Email == hotelOwnerRequest.Email);
            if (isRegistered)
            {
                ViewBag.AlreadyRegistered = "This Email Already Registered!";
                return View(hotelOwnerRequest);
            }

            if (results.IsValid)
            {
                hotelOwnerRequest.Date = DateTime.Now;
                hotelOwnerRequest.RequestStatus = "Pending";
                hotelOwnerRequestManager.TInsert(hotelOwnerRequest);
                _registerAsHotelOwnerMailService.SendMessage(hotelOwnerRequest.FullName, hotelOwnerRequest.Email);
                TempData["SuccessMessage"] = "Your registration was successfully received.";
                return RedirectToAction("Index", "Home");
            }

            else
            {
                foreach (var error in results.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }
            }

            return View(hotelOwnerRequest);
        }

        [HttpGet]
        public IActionResult CreateHotelOwnerAccount(string fullName, string userName, string email, string phoneNumber, string hotelName)
        {
            var createAccountViewModel = new CreateAccountViewModel
            {
                Email = email,
                FullName = fullName,
                UserName = userName,
                PhoneNumber = phoneNumber,
                HotelName = hotelName
            };

            return View(createAccountViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> CreateHotelOwnerAccount(CreateAccountViewModel createAccountViewModel)
        {
            if (ModelState.IsValid)
            {
                var user = _userManager.Users.FirstOrDefault(x => x.Email == createAccountViewModel.Email);

                user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, createAccountViewModel.Password);
                user.PasswordLastChange = DateTime.Now;

                var result = await _userManager.UpdateAsync(user);

                if (result.Succeeded)
                {
                    TempData["SuccessMessage"] = "Your Account Was Created Successfully!";
                    return RedirectToAction("HotelOwnerLogIn", "LogIn");
                }

                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }

            return View(createAccountViewModel);
        }

    }
}
