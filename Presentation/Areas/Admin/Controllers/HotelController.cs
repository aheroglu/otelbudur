using Business.Concrete;
using Business.ValidationRules;
using DataAccess.EntityFramework;
using Entity.Concrete;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using X.PagedList;

namespace Presentation.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class HotelController : Controller
    {
        HotelManager hotelManager = new HotelManager(new EfHotelDal());
        RoomManager roomManager = new RoomManager(new EfRoomDal());
        RoomImageManager roomImageManager = new RoomImageManager(new EfRoomImageDal());

        public IActionResult Index(int page = 1)
        {
            if (TempData["SuccessMessage"] != null)
            {
                ViewBag.SuccessMessage = TempData["SuccessMessage"];
            }

            var hotels = hotelManager.THotelsWithLocation().ToPagedList(page, 10);
            return View(hotels);
        }

        public IActionResult Delete(int id)
        {
            var hotel = hotelManager.TGetById(id);

            string currentImage = hotel.CoverImage;
            string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/hotel/", currentImage);
            System.IO.File.Delete(path);

            hotel.Status = false;
            hotelManager.TUpdate(hotel);
            TempData["SuccessMessage"] = "Hotel Was Deleted Successfully!";
            return RedirectToAction("Hotel", "Admin");
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
                    if (values.CoverImage != "/Templates/main/assets/img/default-user-image.jpg")
                    {
                        string currentImage = values.CoverImage;
                        string currentImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/hotel/", currentImage);
                        System.IO.File.Delete(currentImagePath);
                    }

                    var path = Path.GetExtension(image.FileName);
                    var guidFileName = Guid.NewGuid() + path;
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/hotel/");
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
                return RedirectToAction("Hotel", "Admin");
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

        public IActionResult Rooms(int id, int page = 1)
        {
            var rooms = roomManager.TRoomsByHotel(id).ToPagedList(page, 10);
            ViewBag.HotelName = hotelManager.TGetById(id).Title;
            return View(rooms);
        }

        public IActionResult RoomImages(int id)
        {
            var values = roomImageManager.TImagesByRoom(id);
            ViewBag.RoomName = roomManager.TGetById(id).Title;
            return View(values);
        }

        public IActionResult DeleteSelected(int[] selectedHotels)
        {
            foreach (var hotelId in selectedHotels)
            {
                var hotel = hotelManager.TGetById(hotelId);

                string currentImage = hotel.CoverImage;
                string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/hotel/", currentImage);
                System.IO.File.Delete(path);

                hotel.Status = false;
                hotelManager.TUpdate(hotel);
            }

            TempData["SuccessMessage"] = "Selected Hotels Was Deleted Successfully!";
            return RedirectToAction("Hotel", "Admin");
        }
    }
}
