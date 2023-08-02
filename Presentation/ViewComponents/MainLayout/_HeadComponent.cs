using Microsoft.AspNetCore.Mvc;

namespace Presentation.ViewComponents
{
    public class _HeadComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
