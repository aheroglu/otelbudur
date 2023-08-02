using Business.Concrete;
using Business.ValidationRules;
using DataAccess.EntityFramework;
using Entity.Concrete;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;

namespace Presentation.Areas.HotelOwner.Controllers
{
    [Area("HotelOwner")]
    [Authorize(Roles = "Hotel Owner")]
    public class HotelController : Controller
    {
        private readonly UserManager<AppUser> _userManager;

        HotelManager hotelManager = new HotelManager(new EfHotelDal());

        public HotelController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            if (TempData["SuccessMessage"] != null)
            {
                ViewBag.SuccessMessage = TempData["SuccessMessage"];
            }

            int userId = Convert.ToInt32(_userManager.GetUserId(User));
            var values = hotelManager.THotelByUserId(userId);
            return View(values);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var hotel = hotelManager.TGetById(id);
            return View(hotel);
        }

        [HttpPost]
        public IActionResult Edit(Hotel hotel, IFormFile image)
        {
            HotelValidator validator = new HotelValidator();
            ValidationResult results = validator.Validate(hotel);

            if (results.IsValid)
            {
                var values = hotelManager.TGetById(hotel.Id);

                if (image != null && image.Length > 0)
                {
                    // Delete Current Image
                    if (values.CoverImage != "/Templates/main/assets/img/default-image.png")
                    {
                        string currentImage = values.CoverImage;
                        string currentImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/hotel/", currentImage);
                        System.IO.File.Delete(currentImagePath);
                    }

                    var path = Path.GetExtension(image.FileName);
                    var guidFileName = Guid.NewGuid() + path;
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/hotel");
                    var createImage = Path.Combine(filePath, guidFileName);

                    if (!Directory.Exists(filePath))
                    {
                        Directory.CreateDirectory(filePath);
                    }

                    using (var fileStream = new FileStream(createImage, FileMode.Create))
                    {
                        image.CopyTo(fileStream);
                    }

                    values.CoverImage = guidFileName;
                }

                else
                {
                    values.CoverImage = values.CoverImage;
                }

                values.Title = hotel.Title;
                values.Details = hotel.Details;
                hotelManager.TUpdate(values);
                TempData["SuccessMessage"] = "Hotel Was Updated Successfully!";
                return RedirectToAction("Hotel", "HotelOwner");
            }

            else
            {
                foreach (var error in results.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }
            }

            return View(hotel);
        }

        public IActionResult Details(int id)
        {
            var hotel = hotelManager.TGetById(id);
            return View(hotel);
        }
    }
}
