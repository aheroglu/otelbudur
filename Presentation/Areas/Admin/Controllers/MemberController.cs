using Entity.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace Presentation.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class MemberController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;

        public MemberController(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
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

            var role = _roleManager.Roles.FirstOrDefault(x => x.Name == "Member");
            var users = _userManager.GetUsersInRoleAsync(role.Name).Result.ToPagedList(page, 10);
            return View(users);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var user = _userManager.Users.FirstOrDefault(x => x.Id == id);

            string currentImage = user.Image;
            string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/user/", currentImage);
            System.IO.File.Delete(path);

            await _userManager.DeleteAsync(user);
            TempData["SuccessMessage"] = "User Was Deleted Successfully!";
            return RedirectToAction("Member", "Admin");
        }

        public async Task<IActionResult> DeleteSelected(int[] selectedMembers)
        {
            foreach (var userId in selectedMembers)
            {
                var user = _userManager.Users.FirstOrDefault(x => x.Id == userId);

                string currentImage = user.Image;
                string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/user/", currentImage);
                System.IO.File.Delete(path);

                await _userManager.DeleteAsync(user);
            }

            TempData["SuccessMessage"] = "Selected Members Was Deleted Successfully!";
            return RedirectToAction("Member", "Admin");
        }
    }
}
