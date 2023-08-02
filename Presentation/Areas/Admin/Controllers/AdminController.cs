using DataAccess.Concrete;
using Entity.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Presentation.Areas.Admin.Models.Admin;
using Presentation.Areas.Admin.Models.MailService.Admin;
using Presentation.Areas.Admin.Models.Staff;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace Presentation.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly Context _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;

        private readonly CreateAdminMailService _createAdminMailService;

        public AdminController(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager, CreateAdminMailService createAdminMailService, Context context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _createAdminMailService = createAdminMailService;
            _context = context;
        }

        public IActionResult Index(int page = 1)
        {
            if (TempData["SuccessMessage"] != null)
            {
                ViewBag.SuccessMessage = TempData["SuccessMessage"];
            }

            var role = _roleManager.Roles.FirstOrDefault(x => x.Name == "Admin");
            var users = _userManager.GetUsersInRoleAsync(role.Name).Result.ToPagedList(page, 10);
            return View(users);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddAdminViewModel addAdminViewModel)
        {
            if (ModelState.IsValid)
            {
                bool isRegistered = _context.Users.Any(x => x.Email == addAdminViewModel.Email);
                if (isRegistered)
                {
                    ViewBag.AlreadyRegistered = "This Email Already Registered!";
                    return View(addAdminViewModel);
                }

                AppUser user = new AppUser
                {
                    FullName = addAdminViewModel.FullName,
                    UserName = addAdminViewModel.UserName,
                    Email = addAdminViewModel.Email,
                    PhoneNumber = addAdminViewModel.PhoneNumber,
                    Image = "/Templates/main/assets/img/default-user-image.jpg",
                    MemberSince = DateTime.Now,
                    PasswordLastChange = DateTime.Now,
                };

                var result = await _userManager.CreateAsync(user, addAdminViewModel.Password);

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "Admin");
                    _createAdminMailService.SendMessage(addAdminViewModel.FullName, addAdminViewModel.Email, addAdminViewModel.Password);
                    TempData["SuccessMessage"] = "Admin Was Created Successfully!";
                    return RedirectToAction("Admin", "Admin");
                }

                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }

            return View(addAdminViewModel);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var user = _userManager.Users.FirstOrDefault(x => x.Id == id);

            string currentImage = user.Image;
            string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/user/", currentImage);
            System.IO.File.Delete(path);

            await _userManager.DeleteAsync(user);
            TempData["SuccessMessage"] = "User Was Deleted Successfully!";
            return RedirectToAction("Admin", "Admin");
        }

        public async Task<IActionResult> DeleteSelected(int[] selectedAdmins)
        {
            foreach (var adminId in selectedAdmins)
            {
                var admin = _userManager.Users.FirstOrDefault(x => x.Id == adminId);

                string currentImage = admin.Image;
                string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/user/", currentImage);
                System.IO.File.Delete(path);

                await _userManager.DeleteAsync(admin);
            }

            TempData["SuccessMessage"] = "Selected Users Was Deleted Successfully!";
            return RedirectToAction("Admin", "Admin");
        }
    }
}
