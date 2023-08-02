using Business.Concrete;
using DataAccess.EntityFramework;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.ViewComponents.About
{
    public class _AboutAreaComponent : ViewComponent
    {
        AboutManager aboutManager = new AboutManager(new EfAboutDal());

        public IViewComponentResult Invoke()
        {
            return View(aboutManager.TGetList());
        }
    }
}
