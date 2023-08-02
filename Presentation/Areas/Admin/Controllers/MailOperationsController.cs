using Business.Concrete;
using DataAccess.EntityFramework;
using Entity.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Presentation.Areas.Admin.Models.MailService;
using Presentation.Areas.Admin.Models.MailService.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using X.PagedList;

namespace Presentation.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class MailOperationsController : Controller
    {
        private readonly SendForAdminsMailService _sendForAdminsMail;
        private readonly SendForHotelOwnersMailService _sendForHotelOwnersMail;
        private readonly SendForMembersMailService _sendForMembersMail;
        private readonly SendForStaffsMailService _sendForStaffsMail;
        private readonly SendNewsletterMailService _sendNewsletterMail;

        private readonly UserManager<AppUser> _userManager;

        NewsletterManager newsletterManager = new NewsletterManager(new EfNewsletterDal());
        EmailsSentManager emailsSentManager = new EmailsSentManager(new EfEmailsSentDal());

        public MailOperationsController(SendForAdminsMailService sendForAdminsMail, SendForHotelOwnersMailService sendForHotelOwnersMail, SendForMembersMailService sendForMembersMail, SendForStaffsMailService sendForStaffsMail, SendNewsletterMailService sendNewsletterMail, UserManager<AppUser> userManager)
        {
            _sendForAdminsMail = sendForAdminsMail;
            _sendForHotelOwnersMail = sendForHotelOwnersMail;
            _sendForMembersMail = sendForMembersMail;
            _sendForStaffsMail = sendForStaffsMail;
            _sendNewsletterMail = sendNewsletterMail;
            _userManager = userManager;
        }

        public IActionResult Index(int page = 1)
        {
            if (TempData["SuccessMessage"] != null)
            {
                ViewBag.SuccessMessage = TempData["SuccessMessage"];
            }

            var values = emailsSentManager.TGetList().OrderByDescending(x => x.Id).ToPagedList(page, 10);
            return View(values);
        }

        public IActionResult Details(int id)
        {
            var values = emailsSentManager.TGetById(id);
            return View(values);
        }

        [HttpGet]
        public IActionResult SendForAdmins() { return View(); }

        [HttpPost]
        public IActionResult SendForAdmins(SendForMembersViewModel sendForMembersViewModel)
        {
            if (ModelState.IsValid)
            {
                var admins = _userManager.GetUsersInRoleAsync("Admin").Result.ToList();

                foreach (var admin in admins)
                {
                    _sendForAdminsMail.SendMessage(admin.FullName, admin.Email, sendForMembersViewModel.Subject, sendForMembersViewModel.Content);
                }

                EmailsSent emailsSent = new EmailsSent
                {
                    SentFor = "Admins",
                    Date = DateTime.Now,
                    Content = sendForMembersViewModel.Content
                };

                emailsSentManager.TInsert(emailsSent);

                TempData["SuccessMessage"] = "Mail Was Sent For Admins!";
                return RedirectToAction("MailOperations", "Admin");
            }

            else
            {
                return View();
            }
        }

        [HttpGet]
        public IActionResult SendForMembers() { return View(); }

        [HttpPost]
        public IActionResult SendForMembers(SendForMembersViewModel sendForMembersViewModel)
        {
            if (ModelState.IsValid)
            {
                var members = _userManager.GetUsersInRoleAsync("Member").Result.ToList();

                foreach (var member in members)
                {
                    _sendForMembersMail.SendMessage(member.FullName, member.Email, sendForMembersViewModel.Subject, sendForMembersViewModel.Content);
                }

                EmailsSent emailsSent = new EmailsSent
                {
                    SentFor = "Members",
                    Date = DateTime.Now,
                    Content = sendForMembersViewModel.Content
                };

                emailsSentManager.TInsert(emailsSent);

                TempData["SuccessMessage"] = "Mail Was Sent For Members!";
                return RedirectToAction("MailOperations", "Admin");
            }

            else
            {
                return View(sendForMembersViewModel);
            }
        }

        [HttpGet]
        public IActionResult SendForStaffs() { return View(); }

        [HttpPost]
        public IActionResult SendForStaffs(SendForStaffsViewModel sendForStaffsViewModel)
        {
            if (ModelState.IsValid)
            {
                var staffs = _userManager.GetUsersInRoleAsync("Staff").Result.ToList();

                foreach (var staff in staffs)
                {
                    _sendForStaffsMail.SendMessage(staff.FullName, staff.Email, sendForStaffsViewModel.Subject, sendForStaffsViewModel.Content);
                }

                EmailsSent emailsSent = new EmailsSent
                {
                    SentFor = "Staffs",
                    Date = DateTime.Now,
                    Content = sendForStaffsViewModel.Content
                };

                emailsSentManager.TInsert(emailsSent);

                TempData["SuccessMessage"] = "Mail Was Sent For Staffs!";
                return RedirectToAction("MailOperations", "Admin");
            }

            else
            {
                return View(sendForStaffsViewModel);
            }
        }

        [HttpGet]
        public IActionResult SendForHotelOwners() { return View(); }

        [HttpPost]
        public IActionResult SendForHotelOwners(SendForHotelOwnersViewModel sendForHotelOwnersViewModel)
        {
            if (ModelState.IsValid)
            {
                var hotelOwners = _userManager.GetUsersInRoleAsync("Hotel Owner").Result.ToList();

                foreach (var hotelOwner in hotelOwners)
                {
                    _sendForHotelOwnersMail.SendMessage(hotelOwner.FullName, hotelOwner.Email, sendForHotelOwnersViewModel.Subject, sendForHotelOwnersViewModel.Content);
                }

                EmailsSent emailsSent = new EmailsSent
                {
                    SentFor = "Hotel Owners",
                    Date = DateTime.Now,
                    Content = sendForHotelOwnersViewModel.Content
                };

                emailsSentManager.TInsert(emailsSent);

                TempData["SuccessMessage"] = "Mail Was Sent For Hotel Owners!";
                return RedirectToAction("MailOperations", "Admin");
            }

            else
            {
                return View(sendForHotelOwnersViewModel);
            }
        }

        [HttpGet]
        public IActionResult SendForSubscribers() { return View(); }

        [HttpPost]
        public IActionResult SendForSubscribers(SendForSubscribersViewModel sendForSubscribersViewModel)
        {
            if (ModelState.IsValid)
            {
                var subscribers = newsletterManager.TGetList();

                foreach (var subscriber in subscribers)
                {
                    _sendNewsletterMail.SendMessage(subscriber.Email, sendForSubscribersViewModel.Subject, sendForSubscribersViewModel.Content);
                }

                EmailsSent emailsSent = new EmailsSent
                {
                    SentFor = "Subscribers",
                    Date = DateTime.Now,
                    Content = sendForSubscribersViewModel.Content
                };

                emailsSentManager.TInsert(emailsSent);

                TempData["SuccessMessage"] = "Mail Was Sent For Subscribers!";
                return RedirectToAction("MailOperations", "Admin");
            }

            else
            {
                return View(sendForSubscribersViewModel);
            }
        }

    }
}