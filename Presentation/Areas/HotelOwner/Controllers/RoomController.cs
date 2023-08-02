using Business.Concrete;
using Business.ValidationRules;
using DataAccess.Concrete;
using DataAccess.EntityFramework;
using Entity.Concrete;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging.Abstractions;
using System;
using System.IO;
using System.Linq;
using X.PagedList;

namespace Presentation.Areas.HotelOwner.Controllers
{
    [Area("HotelOwner")]
    [Authorize(Roles = "Hotel Owner")]
    public class RoomController : Controller
    {
        private readonly Context _context;
        private readonly UserManager<AppUser> _userManager;

        RoomManager roomManager = new RoomManager(new EfRoomDal());
        RoomImageManager roomImageManager = new RoomImageManager(new EfRoomImageDal());
        RoomRatingManager roomRatingManager = new RoomRatingManager(new EfRoomRatingDal());

        public RoomController(UserManager<AppUser> userManager, Context context)
        {
            _userManager = userManager;
            _context = context;
        }

        public IActionResult Index(int page = 1)
        {
            if (TempData["SuccessMessage"] != null)
            {
                ViewBag.SuccessMessage = TempData["SuccessMessage"];
            }

            int userId = Convert.ToInt32(_userManager.GetUserId(User));
            var hotelId = _context.Hotels.FirstOrDefault(x => x.UserId == userId).Id;
            var values = roomManager.TRoomsByHotel(hotelId).ToPagedList(page, 10);
            return View(values);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(Room room, IFormFile image)
        {
            RoomValidator validator = new RoomValidator();
            ValidationResult results = validator.Validate(room);

            if (results.IsValid)
            {
                if (image != null && image.Length > 0)
                {
                    var path = Path.GetExtension(image.FileName);
                    var guidFileName = Guid.NewGuid() + path;
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/room/");
                    var createImage = Path.Combine(filePath, guidFileName);

                    if (!Directory.Exists(filePath))
                    {
                        Directory.CreateDirectory(filePath);
                    }

                    using (var fileStream = new FileStream(createImage, FileMode.Create))
                    {
                        image.CopyTo(fileStream);
                    }

                    room.CoverImage = guidFileName;
                }
                else
                {
                    room.CoverImage = "/Templates/main/assets/img/default-image.png";
                }

                int userId = Convert.ToInt32(_userManager.GetUserId(User));
                var hotelId = _context.Hotels.FirstOrDefault(x => x.UserId == userId).Id;

                room.HotelId = hotelId;
                room.Status = true;
                room.ReservationId = null;
                roomManager.TInsert(room);
                TempData["SuccessMessage"] = "Room Was Added Successfully!";
                return RedirectToAction("Room", "HotelOwner");
            }

            else
            {
                foreach (var error in results.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }
            }

            return View(room);
        }

        public IActionResult Delete(int id)
        {
            var room = roomManager.TGetById(id);

            string currentImage = room.CoverImage;
            string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/room/", currentImage);
            System.IO.File.Delete(path);

            room.Status = false;
            roomManager.TUpdate(room);
            TempData["SuccessMessage"] = "Room Was Deleted Successfully!";
            return RedirectToAction("Room", "HotelOwner");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var room = roomManager.TGetById(id);
            return View(room);
        }

        [HttpPost]
        public IActionResult Edit(Room room, IFormFile image)
        {
            RoomValidator validator = new RoomValidator();
            ValidationResult results = validator.Validate(room);

            if (results.IsValid)
            {
                var values = roomManager.TGetById(room.Id);

                if (image != null && image.Length > 0)
                {
                    // Delete Current Image
                    if (values.CoverImage == "/Templates/main/assets/img/default-image.png")
                    {
                        var currentImage = values.CoverImage;
                        var currentImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/image/room/");
                        System.IO.File.Delete(currentImagePath);
                    }

                    var path = Path.GetExtension(image.FileName);
                    var guidFileName = Guid.NewGuid() + path;
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/room");
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

                values.Title = room.Title;
                values.About = room.About;
                values.Price = room.Price;
                values.AdultCount = room.AdultCount;
                values.ChildCount = room.ChildCount;
                values.Parking = room.Parking;
                values.WiFi = room.WiFi;
                values.Breakfast = room.Breakfast;
                values.RoomService = room.RoomService;
                values.Reception = room.Reception;
                values.Pool = room.Pool;
                values.Gym = room.Gym;

                roomManager.TUpdate(values);
                TempData["SuccessMessage"] = "Room Was Updated Successfully!";
                return RedirectToAction("Room", "HotelOwner");
            }

            else
            {
                foreach (var error in results.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }
            }

            return View(room);
        }

        public IActionResult Images(int id)
        {
            var images = roomImageManager.TImagesByRoom(id);
            ViewBag.RoomName = roomManager.TGetById(id).Title;
            return View(images);
        }

        [HttpGet]
        public IActionResult AddImage(int id)
        {
            TempData["RoomId"] = id;
            return View();
        }

        [HttpPost]
        public IActionResult AddImage(IFormFile image)
        {
            RoomImage roomImage = new RoomImage();

            if (image != null && image.Length > 0)
            {
                var path = Path.GetExtension(image.FileName);
                var guidFileName = Guid.NewGuid() + path;
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/room/");
                var createImage = Path.Combine(filePath, guidFileName);

                if (!Directory.Exists(filePath))
                {
                    Directory.CreateDirectory(filePath);
                }

                using (var fileStream = new FileStream(createImage, FileMode.Create))
                {
                    image.CopyTo(fileStream);
                }

                roomImage.Image = guidFileName;
            }

            else
            {
                ViewBag.ErrorMessage = "You must be select image!";
                return View();
            }

            roomImage.RoomId = (int)TempData["RoomId"];
            roomImage.Status = true;
            roomImageManager.TInsert(roomImage);
            TempData["SuccessMessage"] = "Image Was Added!";
            return RedirectToAction("Room", "HotelOwner");
        }

        public IActionResult DeleteImage(int id)
        {
            var image = roomImageManager.TGetById(id);

            string currentImage = image.Image;
            string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/room/", currentImage);
            System.IO.File.Delete(path);

            image.Status = false;
            roomImageManager.TUpdate(image);
            TempData["SuccessMessage"] = "Image Was Deleted Successfully!";
            return RedirectToAction("Room", "HotelOwner");
        }

        public IActionResult DeleteSelected(int[] selectedRooms)
        {
            foreach (var roomId in selectedRooms)
            {
                var room = roomManager.TGetById(roomId);

                string currentImage = room.CoverImage;
                string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/room/", currentImage);
                System.IO.File.Delete(path);

                room.Status = false;
                roomManager.TUpdate(room);
            }

            TempData["SuccessMessage"] = "Selected Rooms Was Deleted Successfully!";
            return RedirectToAction("Room", "HotelOwner");
        }

        public IActionResult Reviews(int id)
        {
            var reviews = roomRatingManager.TReviewsByRoom(id);
            return View(reviews);
        }

    }
}
