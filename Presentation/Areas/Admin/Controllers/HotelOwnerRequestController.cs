using Business.Concrete;
using DataAccess.EntityFramework;
using Entity.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Presentation.Areas.Admin.Models.HotelOwnerRequest;
using Presentation.Areas.Admin.Models.MailService.HotelOwnerRequest;
using System;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace Presentation.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class HotelOwnerRequestController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ApproveRequestMailService _approveRequstMailService;
        private readonly RejectedRequestMailService _rejectedRequstMailService;

        HotelManager hotelManager = new HotelManager(new EfHotelDal());
        HotelOwnerRequestManager hotelOwnerRequestManager = new HotelOwnerRequestManager(new EfHotelOwnerRequestDal());

        public HotelOwnerRequestController(UserManager<AppUser> userManager, ApproveRequestMailService approveRequstMailService, RejectedRequestMailService rejectedRequstMailService)
        {
            _userManager = userManager;
            _approveRequstMailService = approveRequstMailService;
            _rejectedRequstMailService = rejectedRequstMailService;
        }

        public IActionResult Index(int page = 1)
        {
            if (TempData["SuccessMessage"] != null)
            {
                ViewBag.SuccessMessage = TempData["SuccessMessage"];
            }

            var values = hotelOwnerRequestManager.TListWithLocation().ToPagedList(page, 10);
            return View(values);
        }

        public async Task<IActionResult> SaveAsApproved(int id)
        {
            if (ModelState.IsValid)
            {
                var request = hotelOwnerRequestManager.TGetById(id);
                request.RequestStatus = "Approved";
                hotelOwnerRequestManager.TUpdate(request);

                AppUser user = new AppUser
                {
                    FullName = request.FullName,
                    UserName = request.UserName,
                    PhoneNumber = request.PhoneNumber,
                    Email = request.Email,
                    Image = "/Templates/main/assets/img/default-user-image.jpg",
                    MemberSince = DateTime.Now,
                    PasswordLastChange = DateTime.Now,
                };

                var result = await _userManager.CreateAsync(user);

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "Hotel Owner");

                    Hotel hotel = new Hotel
                    {
                        Title = request.HotelName,
                        CoverImage = "/Templates/main/assets/img/default-image.png",
                        Status = true,
                        LocationId = request.LocationId,
                        UserId = _userManager.Users.FirstOrDefault(x => x.Email == request.Email).Id
                    };
                    hotelManager.TInsert(hotel);

                    _approveRequstMailService.SendMessage(request.FullName, request.UserName, request.Email, request.PhoneNumber, request.HotelName);
                    TempData["SuccessMessage"] = "Request Was Approved Successfully!";
                    return RedirectToAction("HotelOwnerRequest", "Admin");
                }
            }

            return View();
        }

        public IActionResult SaveAsRejected(int id)
        {
            var request = hotelOwnerRequestManager.TGetById(id);
            request.RequestStatus = "Cancelled";
            hotelOwnerRequestManager.TUpdate(request);
            _rejectedRequstMailService.SendMessage(request.FullName, request.Email);
            TempData["SuccessMessage"] = "Request Was Rejected Successfully!";
            return RedirectToAction("HotelOwnerRequest", "Admin");
        }

    }
}
