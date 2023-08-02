using Business.Concrete;
using DataAccess.EntityFramework;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.ViewComponents.MainLayout
{
    public class _FooterComponent : ViewComponent
    {
        AboutManager aboutManager = new AboutManager(new EfAboutDal());

        public IViewComponentResult Invoke()
        {
            return View(aboutManager.TGetList());
        }
    }
}
