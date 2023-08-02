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
    public class SocialAccountController : Controller
    {
        SocialAccountManager socialAccountManager = new SocialAccountManager(new EfSocialAccountDal());

        public IActionResult Index()
        {
            if (TempData["SuccessMessage"] != null)
            {
                ViewBag.SuccessMessage = TempData["SuccessMessage"];
            }

            var socialAccounts = socialAccountManager.TGetList();
            return View(socialAccounts);
        }

        [HttpGet]
        public IActionResult Add() { return View(); }

        [HttpPost]
        public IActionResult Add(SocialAccount socialAccount)
        {
            SocialAccountValidator validator = new SocialAccountValidator();
            ValidationResult results = validator.Validate(socialAccount);

            if (results.IsValid)
            {
                socialAccountManager.TInsert(socialAccount);
                TempData["SuccessMessage"] = "Social Account Was Added Successfully!";
                return RedirectToAction("SocialAccount", "Admin");
            }

            else
            {
                foreach (var error in results.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }
            }

            return View(socialAccount);
        }

        public IActionResult Delete(int id)
        {
            var socialAccount = socialAccountManager.TGetById(id);
            socialAccountManager.TDelete(socialAccount);
            TempData["SuccessMessage"] = "Social Account Was Deleted Successfully!";
            return RedirectToAction("SocialAccount", "Admin");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var socialAccount = socialAccountManager.TGetById(id);
            return View(socialAccount);
        }

        [HttpPost]
        public IActionResult Edit(SocialAccount socialAccount)
        {
            SocialAccountValidator validator = new SocialAccountValidator();
            ValidationResult results = validator.Validate(socialAccount);

            if (results.IsValid)
            {
                var account = socialAccountManager.TGetById(socialAccount.Id);

                account.Link = socialAccount.Link;
                account.Icon = socialAccount.Icon;

                socialAccountManager.TUpdate(account);
                TempData["SuccessMessage"] = "Social Account Was Updated Successfully!";
                return RedirectToAction("SocialAccount", "Admin");
            }

            else
            {
                foreach (var error in results.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }
            }

            return View(socialAccount);
        }

        public IActionResult DeleteSelected(int[] selectedSocialAccounts)
        {
            foreach (var socialAccountId in selectedSocialAccounts)
            {
                var socialAccount = socialAccountManager.TGetById(socialAccountId);
                socialAccountManager.TDelete(socialAccount);
            }

            TempData["SuccessMessage"] = "Selected Social Accounts Was Deleted Successfully!";
            return RedirectToAction("SocialAccount", "Admin");
        }
    }
}
