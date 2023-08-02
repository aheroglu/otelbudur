using Business.Concrete;
using Business.ValidationRules;
using DataAccess.EntityFramework;
using Entity.Concrete;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Areas.Staff.Controllers
{
    [Area("Staff")]
    [Authorize(Roles = "Staff")]
    public class AboutController : Controller
    {
        AboutManager aboutManager = new AboutManager(new EfAboutDal());

        public IActionResult Index()
        {
            if (TempData["SuccessMessage"] != null)
            {
                ViewBag.SuccessMessage = TempData["SuccessMessage"];
            }

            var values = aboutManager.TGetById(1);

            return View(values);
        }

        [HttpGet]
        public IActionResult Edit()
        {
            var values = aboutManager.TGetById(1);
            return View(values);
        }

        [HttpPost]
        public IActionResult Edit(About about)
        {
            AboutValidator validator = new AboutValidator();
            ValidationResult results = validator.Validate(about);

            if (results.IsValid)
            {
                var values = aboutManager.TGetById(1);

                values.Title = about.Title;
                values.Content = about.Content;
                values.PhoneNumber = about.PhoneNumber;
                values.Email = about.Email;

                aboutManager.TUpdate(values);

                TempData["SuccessMessage"] = "About Page Was Updated Successfully!";

                return RedirectToAction("About", "Admin");
            }

            else
            {
                foreach (var error in results.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }
            }

            return View(about);
        }
    }
}
