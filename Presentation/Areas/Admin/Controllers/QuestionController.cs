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
    public class QuestionController : Controller
    {
        QuestionManager questionManager = new QuestionManager(new EfQuestionDal());
        QuestionAnswerManager questionAnswerManager = new QuestionAnswerManager(new EfQuestionAnswerDal());

        private readonly AnswerQuestionMailService _answerQuestionMailService;

        public QuestionController(AnswerQuestionMailService answerQuestionMailService)
        {
            _answerQuestionMailService = answerQuestionMailService;
        }

        public IActionResult Index(int page = 1)
        {
            if (TempData["SuccessMessage"] != null)
            {
                ViewBag.SuccessMessage = TempData["SuccessMessage"];
            }

            var values = questionManager.TGetList().ToPagedList(page, 10);
            return View(values);
        }

        public IActionResult Details(int id)
        {
            var question = questionManager.TGetById(id);
            return View(question);
        }

        [HttpGet]
        public IActionResult Answer(int id)
        {
            var values = questionManager.TGetById(id);
            return View(values);
        }

        [HttpPost]
        public IActionResult Answer(Question question, string body)
        {
            var values = questionManager.TGetById(question.Id);

            _answerQuestionMailService.SendMessage(values.FullName, values.Email, body);
            values.BeenAnswered = true;
            questionManager.TUpdate(values);

            QuestionAnswer questionAnswer = new QuestionAnswer
            {
                Answer = body,
                Date = DateTime.Now,
                QustionId = values.Id
            };
            questionAnswerManager.TInsert(questionAnswer);

            TempData["SuccessMessage"] = "Your Answer Was Sent Successfully!";
            return RedirectToAction("Question", "Admin");
        }

        public IActionResult SeeAnswer(int id)
        {
            var answer = questionAnswerManager.TGetById(id);
            return View(answer);
        }

    }
}