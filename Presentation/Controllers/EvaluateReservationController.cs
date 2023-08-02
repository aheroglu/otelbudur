using Business.Concrete;
using DataAccess.EntityFramework;
using Entity.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Presentation.Models.EvaluateReservation;
using System.Linq;

namespace Presentation.Controllers
{
    public class EvaluateReservationController : Controller
    {
        private readonly UserManager<AppUser> _userManager;

        RoomRatingManager ratingManager = new RoomRatingManager(new EfRoomRatingDal());

        public EvaluateReservationController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult Evaluate(string fullName, string email, string roomId)
        {
            var evaluateReservationViewModel = new EvaluateReservationViewModel
            {
                Email = email,
                FullName = fullName,
                RoomId = int.Parse(roomId)
            };

            return View(evaluateReservationViewModel);
        }

        [HttpPost]
        public IActionResult Evaluate(EvaluateReservationViewModel evaluateReservationViewModel, int rate)
        {
            var userId = _userManager.Users.FirstOrDefault(x => x.Email == evaluateReservationViewModel.Email).Id;

            RoomRating rating = new RoomRating
            {
                UserId = userId,
                Rate = rate,
                RoomId = evaluateReservationViewModel.RoomId,
                Comment = evaluateReservationViewModel.Comment,
                Status = true
            };

            ratingManager.TInsert(rating);
            TempData["SuccessMessage"] = "Your Evaluation Was Sent Successfully!";
            return RedirectToAction("Index", "Home");
        }
    }
}
