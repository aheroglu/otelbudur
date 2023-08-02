using Business.Concrete;
using DataAccess.Concrete;
using DataAccess.EntityFramework;
using Entity.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using X.PagedList;

namespace Presentation.Areas.HotelOwner.Controllers
{
    [Area("HotelOwner")]
    [Authorize(Roles = "Hotel Owner")]
    public class ReviewController : Controller
    {
        private readonly Context _context;
        private readonly UserManager<AppUser> _userManager;

        RoomRatingManager roomRatingManager = new RoomRatingManager(new EfRoomRatingDal());

        public ReviewController(UserManager<AppUser> userManager, Context context)
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
            var values = roomRatingManager.TReviewsByHotel(hotelId).ToPagedList(page, 10);
            return View(values);
        }

        public IActionResult Delete(int id)
        {
            var roomRating = roomRatingManager.TGetById(id);
            roomRating.Status = false;
            roomRatingManager.TUpdate(roomRating);
            TempData["SuccessMessage"] = "Review Was Deleted Successfully!";
            return RedirectToAction("Review", "HotelOwner");
        }

        public IActionResult Details(int id)
        {
            var roomRating = roomRatingManager.TGetById(id);
            return View(roomRating);
        }
    }
}
