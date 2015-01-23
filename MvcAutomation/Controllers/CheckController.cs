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
        private readonly IAnswerService answerService;
        private readonly IUserService userService;
        private readonly ICourseService courseService;
        private readonly IFacultyService facultyService;
        private readonly IGroupService groupService;
        private readonly ISpecialityService specialityService;
        private readonly IAttachmentContentService contentService;
        private readonly ITestService testService;
        private readonly ITestConvert convert;

        public CheckController(IAnswerService service1, IUserService service2, ICourseService service3,
            IFacultyService service4, IGroupService service5, ISpecialityService service6,
            IAttachmentContentService service7, ITestService service8, ITestConvert convert)
        {
            answerService = service1;
            userService = service2;
            courseService = service3;
            facultyService = service4;
            groupService = service5;
            specialityService = service6;
            contentService = service7;
            testService = service8;
            this.convert = convert;
        }

        [HttpGet]
        public ActionResult ListAnswers()
        {
            IEnumerable<AnswerEntity> answers = answerService.GetAllAnswerEntities().Reverse();
            List<ListAnswersViewModel> answerList = new List<ListAnswersViewModel>();
            IEnumerable<UserEntity> users = userService.GetAllUserEntities();
            IEnumerable<TestEntity> tests = testService.GetAllTestEntities();
            IEnumerable<CourseEntity> courses = courseService.GetAllCourseEntities();
            IEnumerable<FacultyEntity> faculties = facultyService.GetAllFacultyEntities();
            IEnumerable<GroupEntity> groups = groupService.GetAllGroupEntities();
            IEnumerable<SpecialityEntity> specialities = specialityService.GetAllSpecialityEntities();
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
            answerService.Dispose();
            userService.Dispose();
            courseService.Dispose();
            facultyService.Dispose();
            groupService.Dispose();
            specialityService.Dispose();
            contentService.Dispose();
            base.Dispose(disposing);
        }
    }
}
