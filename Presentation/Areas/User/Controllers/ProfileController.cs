using DataAccess.Concrete;
using Entity.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Presentation.Areas.User.Models;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace Presentation.Areas.User.Controllers
{
    [Area("User")]
    public class ProfileController : Controller
    {
        private readonly Context _context;
        private readonly UserManager<AppUser> _userManager;

        public ProfileController(Context context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index(int page = 1)
        {
            if (TempData["SuccessMessage"] != null)
            {
                ViewBag.SuccessMessage = TempData["SuccessMessage"];
            }

            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var values = _context.Bookings.Include(x => x.Room.Hotel.Location).Where(x => x.UserId == user.Id).OrderByDescending(x => x.Id).ToList().ToPagedList(page, 5);
            return View(values);
        }

        public async Task<IActionResult> Details()
        {
            if (!string.IsNullOrEmpty(User.Identity.Name))
            {
                var loggedUser = await _userManager.FindByNameAsync(User.Identity.Name);
                if (loggedUser != null)
                {
                    ViewBag.FullName = _context.Users.FirstOrDefault(x => x.Id == loggedUser.Id).FullName;
                    ViewBag.Image = _context.Users.FirstOrDefault(x => x.Id == loggedUser.Id).Image;
                }
            }

            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            UserDetailsViewModel userDetailsViewModel = new UserDetailsViewModel
            {
                FullName = user.FullName,
                UserName = user.UserName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                MemberSince = user.MemberSince,
                PasswordLastChange = user.PasswordLastChange
            };

            return View(userDetailsViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Edit()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            EditUserViewModel editUserViewModel = new EditUserViewModel
            {
                FullName = user.FullName,
                UserName = user.UserName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
            };

            return View(editUserViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditUserViewModel editUserViewModel, IFormFile image)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            user.FullName = editUserViewModel.FullName;
            user.UserName = editUserViewModel.UserName;
            user.Email = editUserViewModel.Email;
            user.PhoneNumber = editUserViewModel.PhoneNumber;

            if (image != null && image.Length > 0)
            {
                // Delete Current Image
                if (user.Image != "/Templates/main/assets/img/default-user-image.jpg")
                {
                    string currentImage = user.Image;
                    string currentImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/user/", currentImage);
                    System.IO.File.Delete(currentImagePath);
                }

                var path = Path.GetExtension(image.FileName);
                var guidFileName = Guid.NewGuid() + path;
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/user/");
                var createImage = Path.Combine(filePath, guidFileName);

                if (!Directory.Exists(filePath))
                {
                    Directory.CreateDirectory(filePath);
                }

                using (var fileStream = new FileStream(createImage, FileMode.Create))
                {
                    image.CopyTo(fileStream);
                }

                user.Image = guidFileName;
            }

            else
            {
                user.Image = user.Image;
            }

            var result = await _userManager.UpdateAsync(user);

            if (result.Succeeded)
            {
                TempData["SuccessMessage"] = "Your Account Updated Successfully!";
                return RedirectToAction("Profile", "User");
            }

            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }


            return View(editUserViewModel);
        }

        [HttpGet]
        public IActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel changePasswordViewModel)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(User.Identity.Name);

                user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, changePasswordViewModel.Password);
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

            return View(changePasswordViewModel);
        }

    }
}
