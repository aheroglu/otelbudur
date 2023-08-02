using Business.Concrete;
using Business.ValidationRules;
using DataAccess.Concrete;
using DataAccess.EntityFramework;
using Entity.Concrete;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Presentation.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        Context dbContext = new Context();
        NewsletterManager newsletterManager = new NewsletterManager(new EfNewsletterDal());
        QuestionManager questionManager = new QuestionManager(new EfQuestionDal());

        public IActionResult Index()
        {
            if (TempData["SuccessMessage"] != null)
            {
                ViewBag.SuccessMessage = TempData["SuccessMessage"];
            }

            if (TempData["ErrorMessage"] != null)
            {
                ViewBag.ErrorMessage = TempData["ErrorMessage"];
            }

            return View();
        }

        [HttpPost]
        public IActionResult Subscribe(Newsletter newsletter)
        {
            NewsletterValidator validator = new NewsletterValidator();
            ValidationResult results = validator.Validate(newsletter);

            bool isSubscribed = dbContext.Newsletters.Any(x => x.Email == newsletter.Email);

            if (isSubscribed)
            {
                TempData["ErrorMessage"] = "This Email Already Subscribed!";

                return RedirectToAction("Index");
            }

            if (results.IsValid)
            {
                newsletterManager.TInsert(newsletter);

                TempData["SuccessMessage"] = "Successfully Subscribed!";

                return RedirectToAction("Index");
            }

            return View("Index");
        }

        [HttpPost]
        public IActionResult SendQuestion(Question question)
        {
            QuestionValidator validator = new QuestionValidator();
            ValidationResult results = validator.Validate(question);

            if (results.IsValid)
            {
                question.BeenAnswered = false;
                questionManager.TInsert(question);
                TempData["SuccessMessage"] = "Your Question Was Sent Successfully!";
            }

            else
            {
                TempData["ErrorMessage"] = "Your Message Coult Not Be Sent. Please Try Again";
            }

            return RedirectToAction("Index");
        }
    }
}
