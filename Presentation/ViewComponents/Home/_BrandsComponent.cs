using Business.Concrete;
using DataAccess.EntityFramework;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.ViewComponents.MainLayout
{
    public class _BrandsComponent : ViewComponent
    {
        PartnerManager partnerManager = new PartnerManager(new EfPartnerDal());

        public IViewComponentResult Invoke()
        {
            return View(partnerManager.TGetList());
        }
    }
}
