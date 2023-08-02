using Business.Concrete;
using DataAccess.EntityFramework;
using Entity.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.Areas.Admin.Models.MailService;
using System;
using X.PagedList;

namespace Presentation.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ContactController : Controller
    {
        private readonly AnswerContactMailService _answerContactMailService;

        ContactManager contactManager = new ContactManager(new EfContactDal());
        ContactAnswerManager contactAnswerManager = new ContactAnswerManager(new EfContactAnswerDal());

        public ContactController(AnswerContactMailService answerContactMailService)
        {
            _answerContactMailService = answerContactMailService;
        }

        public IActionResult Index(int page = 1)
        {
            if (TempData["SuccessMessage"] != null)
            {
                ViewBag.SuccessMessage = TempData["SuccessMessage"];
            }

            var values = contactManager.TGetList().ToPagedList(page, 10);
            return View(values);
        }

        public IActionResult Details(int id)
        {
            var contact = contactManager.TGetById(id);
            return View(contact);
        }

        [HttpGet]
        public IActionResult Answer(int id)
        {
            var contact = contactManager.TGetById(id);
            return View(contact);
        }

        [HttpPost]
        public IActionResult Answer(Contact contact, string body)
        {
            var values = contactManager.TGetById(contact.Id);

            string fullName = values.FirstName + " " + values.LastName;
            _answerContactMailService.SendMessage(fullName, values.Email, body);
            values.BeenRead = true;
            contactManager.TUpdate(values);

            ContactAnswer contactAnswer = new ContactAnswer
            {
                Answer = body,
                ContactId = values.Id,
                Date = DateTime.Now
            };
            contactAnswerManager.TInsert(contactAnswer);

            TempData["SuccessMessage"] = "Your Answer Was Sent Successfully!";
            return RedirectToAction("Contact", "Admin");
        }

        public IActionResult SeeAnswer(int id)
        {
            var answer = contactAnswerManager.TGetById(id);
            return View(answer);
        }
    }
}
