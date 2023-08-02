using Business.Abstract;
using Business.Concrete;
using DataAccess.EntityFramework;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.ViewComponents.MainLayout
{
    public class _FAQComponent : ViewComponent
    {
        FAQManager faqManager = new FAQManager(new EfFAQDal());

        public IViewComponentResult Invoke()
        {
            return View(faqManager.TGetList());
        }
    }
}
