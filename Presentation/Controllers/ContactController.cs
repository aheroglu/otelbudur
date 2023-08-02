using Business.Concrete;
using Business.ValidationRules;
using DataAccess.EntityFramework;
using Entity.Concrete;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [AllowAnonymous]
    public class ContactController : Controller
    {
        ContactManager contactManager = new ContactManager(new EfContactDal());

        public IActionResult Index()
        {
            if (TempData["SuccessMessage"] != null)
            {
                ViewBag.SuccessMessage = TempData["SuccessMessage"];
            }

            return View();
        }

        [HttpPost]
        public IActionResult Index(Contact contact)
        {
            ContactValidator validator = new ContactValidator();
            ValidationResult results = validator.Validate(contact);

            if (results.IsValid)
            {
                contact.BeenRead = false;
                contactManager.TInsert(contact);
                TempData["SuccessMessage"] = "You Message Was Sent Successfully!";
                return RedirectToAction("Index");
            }

            else
            {
                foreach (var x in results.Errors)
                {
                    ModelState.AddModelError(x.PropertyName, x.ErrorMessage);
                }
            }

            return View();
        }

    }
}
