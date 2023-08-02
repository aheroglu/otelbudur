using Entity.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Presentation.Areas.Admin.Models.Role;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace Presentation.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class RoleController : Controller
    {
        private readonly RoleManager<AppRole> _roleManager;
        private readonly UserManager<AppUser> _userManager;

        public RoleController(RoleManager<AppRole> roleManager, UserManager<AppUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public IActionResult Index(int page = 1)
        {
            if (TempData["SuccessMessage"] != null)
            {
                ViewBag.SuccessMessage = TempData["SuccessMessage"];
            }

            var roles = _roleManager.Roles.ToList().ToPagedList(page, 10);

            return View(roles);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddRoleViewModel addRoleViewModel)
        {
            AppRole role = new AppRole
            {
                Name = addRoleViewModel.RoleName
            };

            var result = await _roleManager.CreateAsync(role);

            if (result.Succeeded)
            {
                TempData["SuccessMessage"] = "Role Created Successfully!";
                return RedirectToAction("Role", "Admin");
            }

            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return View();
        }

        public async Task<IActionResult> Delete(int id)
        {
            var role = _roleManager.Roles.FirstOrDefault(x => x.Id == id);
            await _roleManager.DeleteAsync(role);
            TempData["SuccessMessage"] = "Role Was Deleted Successfully!";
            return RedirectToAction("Role", "Admin");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var role = _roleManager.Roles.FirstOrDefault(x => x.Id == id);

            EditRoleViewModel editRoleViewModel = new EditRoleViewModel
            {
                Id = role.Id,
                RoleName = role.Name
            };

            return View(editRoleViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditRoleViewModel editRoleViewModel)
        {
            var role = _roleManager.Roles.First(x => x.Id == editRoleViewModel.Id);
            role.Name = editRoleViewModel.RoleName;
            await _roleManager.UpdateAsync(role);
            TempData["SuccessMessage"] = "Role Was Edited Successfully!";
            return RedirectToAction("Role", "Admin");
        }

        public IActionResult Users(int id)
        {
            var role = _roleManager.Roles.FirstOrDefault(x => x.Id == id);
            var users = _userManager.GetUsersInRoleAsync(role.Name).Result;
            ViewBag.RoleName = role.Name;
            return View(users);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteSelected(int[] selectedRoles)
        {
            foreach (var roleId in selectedRoles)
            {
                var role = _roleManager.Roles.FirstOrDefault(x => x.Id == roleId);
                await _roleManager.DeleteAsync(role);
            }

            TempData["SuccessMessage"] = "Selected Roles Was Deleted Successfully!";

            return RedirectToAction("Role", "Admin");
        }
    }
}
