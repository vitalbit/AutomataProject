using BLL.Interface.Entities;
using BLL.Interface.Services;
using MvcAutomation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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

        public CheckController(IAnswerService service1, IUserService service2, ICourseService service3,
            IFacultyService service4, IGroupService service5, ISpecialityService service6)
        {
            answerService = service1;
            userService = service2;
            courseService = service3;
            facultyService = service4;
            groupService = service5;
            specialityService = service6;
        }

        public ActionResult ListAnswers()
        {
            IEnumerable<AnswerEntity> answers = answerService.GetAllAnswerEntities().Reverse();
            List<ListAnswersViewModel> answerList = new List<ListAnswersViewModel>();
            foreach (var answer in answers)
            {
                UserEntity user = userService.GetAllUserEntities().FirstOrDefault(ent => ent.Id == answer.UserId);
                answerList.Add(new ListAnswersViewModel()
                {
                    AttachmentContentId = answer.Id,
                    TestId = answer.TestId,
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
        public ActionResult CheckAnswer(int test_id, int content_id)
        {
            return View();
        }
    }
}
