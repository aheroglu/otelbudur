using Business.Concrete;
using DataAccess.Concrete;
using DataAccess.EntityFramework;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;

namespace Presentation.ViewComponents.MainLayout
{
    public class _BookingComponent : ViewComponent
    {
        Context context = new Context();

        public IViewComponentResult Invoke()
        {
            List<SelectListItem> locations = (from x in context.Locations.Where(x => x.Status == true).OrderBy(x => x.Name)
                                              select new SelectListItem
                                              {
                                                  Text = x.Name,
                                                  Value = x.Id.ToString()
                                              }).ToList();

            ViewBag.Locations = locations;

            return View();
        }
    }
}
