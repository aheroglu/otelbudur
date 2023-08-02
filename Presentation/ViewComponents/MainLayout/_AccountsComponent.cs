using Business.Concrete;
using DataAccess.EntityFramework;
using Entity.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.ViewComponents.MainLayout
{
    public class _SocialAccountComponent : ViewComponent
    {
        SocialAccountManager socialAccount = new SocialAccountManager(new EfSocialAccountDal());

        public IViewComponentResult Invoke()
        {
            return View(socialAccount.TGetList());
        }
    }
}
