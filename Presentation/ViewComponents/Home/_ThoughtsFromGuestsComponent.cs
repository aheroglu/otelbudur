using Microsoft.AspNetCore.Mvc;

namespace Presentation.ViewComponents.Home
{
    public class _ThoughtsFromGuestsComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}