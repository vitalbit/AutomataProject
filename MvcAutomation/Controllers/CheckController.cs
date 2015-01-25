using BLL.Interface.Entities;
using BLL.Interface.Services;
using MvcAutomation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XMLConvertation;
using MvcAutomation.Mappers;

namespace MvcAutomation.Controllers
{
    public class CheckController : Controller
    {
        private readonly IUserService userService;
        private readonly IContentService contentService;
        private readonly ITestConvert convert;

        public CheckController(IUserService service1, IContentService service2, ITestConvert convert)
        {
            userService = service1;
            contentService = service2;
            this.convert = convert;
        }

        [HttpGet]
        public ActionResult ListAnswers()
        {
            IEnumerable<AnswerEntity> answers = contentService.GetAllAnswerEntities().Reverse();
            List<ListAnswersViewModel> answerList = new List<ListAnswersViewModel>();
            IEnumerable<UserEntity> users = userService.GetAllUserEntities();
            IEnumerable<TestEntity> tests = contentService.GetAllTestEntities();
            IEnumerable<CourseEntity> courses = userService.GetAllCourseEntities();
            IEnumerable<FacultyEntity> faculties = userService.GetAllFacultyEntities();
            IEnumerable<GroupEntity> groups = userService.GetAllGroupEntities();
            IEnumerable<SpecialityEntity> specialities = userService.GetAllSpecialityEntities();
            foreach (var answer in answers)
            {
                UserEntity user = users.FirstOrDefault(ent => ent.Id == answer.UserId);
                answerList.Add(new ListAnswersViewModel()
                {
                    AttachmentContentId = answer.Id,
                    TestName = tests.FirstOrDefault(ent => ent.Id == answer.TestId).Name,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Course = courses.FirstOrDefault(ent => ent.Id == user.CourseId).Number,
                    Faculty = faculties.FirstOrDefault(ent => ent.Id == user.FacultyId).Name,
                    Group = groups.FirstOrDefault(ent => ent.Id == user.GroupId).Name,
                    Speciality = specialities.FirstOrDefault(ent => ent.Id == user.SpecialityId).Name,
                    Mark = answer.Mark
                });
            }
            return View(answerList);
        }

        [Authorize(Roles="Admin")]
        [HttpGet]
        public ActionResult CheckAnswer(string test_name, int content_id)
        {
            AttachmentContentEntity content = contentService.GetAnswerAttachmentContentEntities().FirstOrDefault(ent => ent.Id == content_id);
            NewTestViewModel test = convert.getFromBytes(content.Content).ToView();
            test.TestName = test_name;
            return View(test);
        }

        protected override void Dispose(bool disposing)
        {
            userService.Dispose();
            contentService.Dispose();
            base.Dispose(disposing);
        }
    }
}
