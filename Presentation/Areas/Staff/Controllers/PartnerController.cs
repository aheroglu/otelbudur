using Business.Concrete;
using Business.ValidationRules;
using DataAccess.EntityFramework;
using Entity.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System;
using FluentValidation.Results;

namespace Presentation.Areas.Staff.Controllers
{
    [Area("Staff")]
    [Authorize(Roles = "Staff")]
    public class PartnerController : Controller
    {
        PartnerManager partnerManager = new PartnerManager(new EfPartnerDal());

        public IActionResult Index()
        {
            if (TempData["SuccessMessage"] != null)
            {
                ViewBag.SuccessMessage = TempData["SuccessMessage"];
            }

            var partners = partnerManager.TGetList();
            return View(partners);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(Partner partner, IFormFile image)
        {
            PartnerValidator validator = new PartnerValidator();
            ValidationResult results = validator.Validate(partner);

            if (results.IsValid)
            {
                if (image != null && image.Length > 0)
                {
                    var path = Path.GetExtension(image.FileName);
                    var guidFileName = Guid.NewGuid() + path;
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/partner/");
                    var createImage = Path.Combine(filePath, guidFileName);

                    if (!Directory.Exists(filePath))
                    {
                        Directory.CreateDirectory(filePath);
                    }

                    using (var fileStream = new FileStream(createImage, FileMode.Create))
                    {
                        image.CopyTo(fileStream);
                    }

                    partner.Image = guidFileName;
                }

                else
                {
                    partner.Image = "/Templates/main/assets/img/default-image.png";
                }

                partnerManager.TInsert(partner);
                TempData["SuccessMessage"] = "Partner Was Added Succeffully!";
                return RedirectToAction("Partner", "Admin");
            }

            else
            {
                foreach (var error in results.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }
            }

            return View(partner);
        }

        public IActionResult Delete(int id)
        {
            var partner = partnerManager.TGetById(id);

            string currentImage = partner.Image;
            string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/partner/", currentImage);
            System.IO.File.Delete(path);

            partnerManager.TDelete(partner);
            TempData["SuccessMessage"] = "Partner Was Deleted Successfully!";
            return RedirectToAction("Partner", "Admin");
        }
    }
}
