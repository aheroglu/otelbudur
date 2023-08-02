using DataAccess.Concrete;
using Entity.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace Presentation.Areas.Staff.ViewComponents.StaffLayout
{
    public class _StaffHeaderComponent : ViewComponent
    {
        private readonly Context _context;
        private readonly UserManager<AppUser> _userManager;

        public _StaffHeaderComponent(Context context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            if (!string.IsNullOrEmpty(User.Identity.Name))
            {
                var user = await _userManager.FindByNameAsync(User.Identity.Name);
                if (user != null)
                {
                    ViewBag.FullName = _context.Users.FirstOrDefault(x => x.Id == user.Id).FullName;
                    ViewBag.Image = _context.Users.FirstOrDefault(x => x.Id == user.Id).Image;
                }
            }

            return View();
        }
    }
}
