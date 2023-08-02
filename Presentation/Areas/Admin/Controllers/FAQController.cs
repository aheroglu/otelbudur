using Business.Concrete;
using Business.ValidationRules;
using DataAccess.EntityFramework;
using Entity.Concrete;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class FAQController : Controller
    {
        FAQManager faqManager = new FAQManager(new EfFAQDal());

        public IActionResult Index()
        {
            if (TempData["SuccessMessage"] != null)
            {
                ViewBag.SuccessMessage = TempData["SuccessMessage"];
            }

            var values = faqManager.TGetList();
            return View(values);
        }

        [HttpGet]
        public IActionResult Add() { return View(); }

        [HttpPost]
        public IActionResult Add(FAQ faq)
        {
            FAQValidator validator = new FAQValidator();
            ValidationResult results = validator.Validate(faq);

            if (results.IsValid)
            {
                faqManager.TInsert(faq);
                TempData["SuccessMessage"] = "FAQ Was Added Successfully!";
                return RedirectToAction("FAQ", "Admin");
            }

            else
            {
                foreach (var error in results.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }
            }

            return View(faq);
        }

        public IActionResult Delete(int id)
        {
            var faq = faqManager.TGetById(id);
            faqManager.TDelete(faq);
            TempData["SuccessMessage"] = "FAQ Was Deleted Succesfully!";
            return RedirectToAction("FAQ", "Admin");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var values = faqManager.TGetById(id);
            return View(values);
        }

        [HttpPost]
        public IActionResult Edit(FAQ faq)
        {
            FAQValidator validator = new FAQValidator();
            ValidationResult results = validator.Validate(faq);

            if (results.IsValid)
            {
                var values = faqManager.TGetById(faq.Id);
                values.Title = faq.Title;
                values.Description = faq.Description;
                faqManager.TUpdate(values);
                TempData["SuccessMessage"] = "FAQ Was Updated Successfully!";
                return RedirectToAction("FAQ", "Admin");
            }

            else
            {
                foreach (var error in results.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }
            }

            return View(faq);
        }

        public IActionResult DeleteSelected(int[] selectedFAQs)
        {
            foreach (var faqId in selectedFAQs)
            {
                var faq = faqManager.TGetById(faqId);
                faqManager.TDelete(faq);
            }

            TempData["SuccessMessage"] = "Selected FAQ's Was Deleted Successfully!";
            return RedirectToAction("FAQ", "Admin");
        }
    }
}
