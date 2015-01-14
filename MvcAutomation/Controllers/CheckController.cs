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
        private const int PerPage = 10;
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
            foreach (var answer in answers.Take(PerPage))
            {
                UserEntity user = userService.GetAllUserEntities().FirstOrDefault(ent => ent.Id == answer.UserId);
                answerList.Add(new ListAnswersViewModel()
                {
                    Page = 0,
                    AttachmentContentId = answer.Id,
                    TestName = testService.GetAllTestEntities().FirstOrDefault(ent => ent.Id == answer.TestId).Name,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Course = courseService.GetAllCourseEntities().FirstOrDefault(ent => ent.Id == user.CourseId).Number,
                    Faculty = facultyService.GetAllFacultyEntities().FirstOrDefault(ent => ent.Id == user.FacultyId).Name,
                    Group = groupService.GetAllGroupEntities().FirstOrDefault(ent => ent.Id == user.GroupId).Name,
                    Speciality = specialityService.GetAllSpecialityEntities().FirstOrDefault(ent => ent.Id == user.SpecialityId).Name,
                    Mark = answer.Mark
                });
            }
            return View(answerList);
        }

        [HttpPost]
        public ActionResult ListAnswers(int page)
        {
            IEnumerable<AnswerEntity> answers = answerService.GetAllAnswerEntities().Reverse();
            List<ListAnswersViewModel> answerList = new List<ListAnswersViewModel>();
            page++;
            foreach (var answer in answers.Skip(answers.Count() > PerPage * page ? PerPage * page : PerPage * (page - 1)).Take(PerPage))
            {
                UserEntity user = userService.GetAllUserEntities().FirstOrDefault(ent => ent.Id == answer.UserId);
                answerList.Add(new ListAnswersViewModel()
                {
                    Page = page,
                    AttachmentContentId = answer.Id,
                    TestName = testService.GetAllTestEntities().FirstOrDefault(ent => ent.Id == answer.TestId).Name,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Course = courseService.GetAllCourseEntities().FirstOrDefault(ent => ent.Id == user.CourseId).Number,
                    Faculty = facultyService.GetAllFacultyEntities().FirstOrDefault(ent => ent.Id == user.FacultyId).Name,
                    Group = groupService.GetAllGroupEntities().FirstOrDefault(ent => ent.Id == user.GroupId).Name,
                    Speciality = specialityService.GetAllSpecialityEntities().FirstOrDefault(ent => ent.Id == user.SpecialityId).Name,
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
    }
}
