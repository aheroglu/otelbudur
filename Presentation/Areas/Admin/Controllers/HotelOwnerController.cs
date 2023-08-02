using Entity.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using X.PagedList;

namespace Presentation.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class HotelOwnerController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;

        public HotelOwnerController(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public IActionResult Index(int page = 1)
        {
            if (TempData["SuccessMessage"] != null)
            {
                ViewBag.SuccessMessage = TempData["SuccessMessage"];
            }

            var role = _roleManager.Roles.FirstOrDefault(x => x.Name == "Hotel Owner");
            var users = _userManager.GetUsersInRoleAsync(role.Name).Result.ToPagedList(page, 10);
            return View(users);
        }

        public IActionResult Delete(int id)
        {
            var user = _userManager.Users.FirstOrDefault(x => x.Id == id);
            _userManager.DeleteAsync(user);
            TempData["SuccessMessage"] = "User Was Deleted Successfully!";
            return RedirectToAction("HotelOwner", "Admin");
        }

        public IActionResult DeleteSelected(int[] selectedHotelOwners)
        {
            foreach (var hotelOwnerId in selectedHotelOwners)
            {
                var hotelOwner = _userManager.Users.FirstOrDefault(x => x.Id == hotelOwnerId);
                _userManager.DeleteAsync(hotelOwner);
            }

            TempData["SuccessMessage"] = "Selected Users Was Deleted Successfully!";
            return RedirectToAction("HotelOwner", "Admin");
        }
    }
}
