using DataAccess.Concrete;
using Entity.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Presentation.Areas.Admin.Models.MailService.Staff;
using Presentation.Areas.Admin.Models.Staff;
using Presentation.Models.SignIn;
using Presentation.Models.SignUp;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace Presentation.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class StaffController : Controller
    {
        private readonly Context _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;

        private readonly CreateStaffMailService _createStaffMailService;

        public StaffController(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager, CreateStaffMailService createStaffMailService, Context context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _createStaffMailService = createStaffMailService;
            _context = context;
        }

        public IActionResult Index(int page = 1)
        {
            if (TempData["SuccessMessage"] != null)
            {
                ViewBag.SuccessMessage = TempData["SuccessMessage"];
            }

            var role = _roleManager.Roles.FirstOrDefault(x => x.Name == "Staff");
            var users = _userManager.GetUsersInRoleAsync(role.Name).Result.ToPagedList(page, 10);
            return View(users);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddStaffViewModel addStaffViewModel)
        {
            if (ModelState.IsValid)
            {
                bool isRegistered = _context.Users.Any(x => x.Email == addStaffViewModel.Email);
                if (isRegistered)
                {
                    ViewBag.AlreadyRegistered = "This Email Already Registered!";
                    return View(addStaffViewModel);
                }

                AppUser user = new AppUser
                {
                    FullName = addStaffViewModel.FullName,
                    UserName = addStaffViewModel.UserName,
                    Email = addStaffViewModel.Email,
                    PhoneNumber = addStaffViewModel.PhoneNumber,
                    Image = "/Templates/main/assets/img/default-user-image.jpg",
                    MemberSince = DateTime.Now,
                    PasswordLastChange = DateTime.Now,
                };

                var result = await _userManager.CreateAsync(user, addStaffViewModel.Password);

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "Staff");
                    _createStaffMailService.SendMessage(addStaffViewModel.FullName, addStaffViewModel.Email, addStaffViewModel.Password);
                    TempData["SuccessMessage"] = "Staff Was Created Successfully!";
                    return RedirectToAction("Staff", "Admin");
                }

                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }

            return View(addStaffViewModel);
        }

        public IActionResult Delete(int id)
        {
            var user = _userManager.Users.FirstOrDefault(x => x.Id == id);

            string currentImage = user.Image;
            string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/user/", currentImage);
            System.IO.File.Delete(path);

            _userManager.DeleteAsync(user);
            TempData["SuccessMessage"] = "User Was Deleted Successfully!";
            return RedirectToAction("Staff", "Admin");
        }

        public IActionResult DeleteSelected(int[] selectedStaffs)
        {
            foreach (var staffId in selectedStaffs)
            {
                var staff = _userManager.Users.FirstOrDefault(x => x.Id == staffId);

                string currentImage = staff.Image;
                string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/user/", currentImage);
                System.IO.File.Delete(path);

                _userManager.DeleteAsync(staff);
            }

            TempData["SuccessMessage"] = "Selected Staffs Was Deleted Successfully!";
            return RedirectToAction("Staff", "Admin");
        }
    }
}
