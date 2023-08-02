using Microsoft.AspNetCore.Mvc;

namespace Presentation.Areas.User.ViewComponents.UserLayout
{
    public class _UserSidebarComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
