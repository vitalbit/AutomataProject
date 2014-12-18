﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcAutomation.Models;
using BLL.Interface.Services;
using BLL.Interface.Entities;
using System.Web.Security;
using MvcAutomation.Convertation;
using System.IO;

namespace MvcAutomation.Controllers
{
    public class HomeController : Controller
    {
        private readonly IBlockService blockService;
        private readonly IUserService userService;
        private readonly IFacultyService facultyService;
        private readonly ICourseService courseService;
        private readonly IGroupService groupService;
        private readonly ISpecialityService specialityService;
        private readonly IRoleService roleService;
        private readonly IBlockTypeService blockTypeService;
        private readonly IAnswerService answerService;
        private readonly ITestService testService;
        private readonly ITestConvert convert;
        private readonly IAttachmentContentService contentService;

        public HomeController(IBlockService service1, IUserService service2,
            IFacultyService service3, ICourseService service4, IGroupService service5,
            ISpecialityService service6, IRoleService service7, IBlockTypeService service8,
            IAnswerService service9, ITestService service10, IAttachmentContentService service11)
        {
            blockService = service1;
            userService = service2;
            facultyService = service3;
            courseService = service4;
            groupService = service5;
            specialityService = service6;
            roleService = service7;
            blockTypeService = service8;
            answerService = service9;
            testService = service10;
            contentService = service11;
            this.convert = new XmlConverter();
        }

        //
        // GET: /Home/

        public ActionResult Index()
        {
            int id = blockTypeService.GetAllBlockTypeEntities().FirstOrDefault(ent => ent.Name == "Home").Id;
            return View(blockService.GetAllBlockEntities()
                .Reverse().Where(entity => entity.BlockTypeId == id)
                .Select(block => new BlockViewModel()
                {
                    Text = block.Text,
                    Title = block.Title
                }));
        }

        [HttpGet]
        public ActionResult Registration()
        {
            List<SelectListItem> faculties = new List<SelectListItem>();

            foreach (FacultyEntity fe in facultyService.GetAllFacultyEntities())
                faculties.Add(new SelectListItem() { Text = fe.Name });

            List<SelectListItem> groups = new List<SelectListItem>();

            foreach (GroupEntity ge in groupService.GetAllGroupEntities())
                groups.Add(new SelectListItem() { Text = ge.Name });

            List<SelectListItem> courses = new List<SelectListItem>();

            foreach (CourseEntity ce in courseService.GetAllCourseEntities())
                courses.Add(new SelectListItem() { Text = ce.Number.ToString() });

            List<SelectListItem> specialities = new List<SelectListItem>();

            foreach (SpecialityEntity se in specialityService.GetAllSpecialityEntities())
                specialities.Add(new SelectListItem() { Text = se.Name });

            ViewBag.Faculties = faculties;
            ViewBag.Courses = courses;
            ViewBag.Groups = groups;
            ViewBag.Specialities = specialities;

            return View();
        }

        [HttpPost]
        public ActionResult Registration(UserViewModel user)
        {
            UserEntity entity = new UserEntity()
            {
                Nickname = user.Nickname,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Password = user.Password,
                Email = user.Email,
                CourseId = courseService.GetAllCourseEntities().FirstOrDefault(ent => ent.Number == Int32.Parse(user.Course)).Id,
                GroupId = groupService.GetAllGroupEntities().FirstOrDefault(ent => ent.Name == user.Group).Id,
                FacultyId = facultyService.GetAllFacultyEntities().FirstOrDefault(ent => ent.Name == user.Faculty).Id,
                SpecialityId = specialityService.GetAllSpecialityEntities().FirstOrDefault(ent => ent.Name == user.Speciality).Id,
                RoleId = roleService.GetAllRoleEntities().FirstOrDefault(ent => ent.Name == "User").Id
            };
            userService.CreateUser(entity);
            return RedirectToAction("/Index");
        }

        [HttpPost]
        public ActionResult Login(string login, string password)
        {
            var user = userService.GetAllUserEntities().FirstOrDefault(ent => ent.Nickname == login && ent.Password == password);
            if (user != null)
            {
                string role = roleService.GetAllRoleEntities().FirstOrDefault(ent => ent.Id == user.RoleId).Name;
                TempData["user"] = user;
                TempData["role"] = role;
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Settings()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Settings(string oldPassword, string newPassword, string repeatPassword)
        {
            UserEntity user = (UserEntity)TempData["user"];
            string old = userService.GetAllUserEntities().FirstOrDefault(ent => ent.Nickname == user.Nickname).Password;
            if (newPassword == repeatPassword && old == oldPassword)
            {
                user.Password = newPassword;
                userService.UpdateUser(user);
                ViewBag.Message = "Пароль изменен!";
                return View();
            }
            ViewBag.Message = "Ошибка изменения пароля!";
            return View();
        }

        public ActionResult TestResult()
        {
            UserEntity user = (UserEntity)TempData["user"];
            List<AnswerEntity> answers = answerService.GetAllAnswerEntities().Reverse().Where(ent => ent.UserId == user.Id).ToList();
            List<TestEntity> tests = testService.GetAllTestEntities().Where(ent => answers.Any(ent2 => ent2.TestId == ent.Id)).ToList();
            List<TestResultViewModel> results = new List<TestResultViewModel>();
            for (int i = 0; i != answers.Count; i++)
            {
                results.Add(new TestResultViewModel() { TestName = tests[i].Name, Result = answers[i].Mark });
            }
            return View(results);
        }

        [HttpGet]
        public ActionResult DbEdit()
        {
            List<SelectListItem> faculties = new List<SelectListItem>();

            foreach (FacultyEntity fe in facultyService.GetAllFacultyEntities())
                faculties.Add(new SelectListItem() { Text = fe.Name });

            List<SelectListItem> groups = new List<SelectListItem>();

            foreach (GroupEntity ge in groupService.GetAllGroupEntities())
                groups.Add(new SelectListItem() { Text = ge.Name });

            List<SelectListItem> courses = new List<SelectListItem>();

            foreach (CourseEntity ce in courseService.GetAllCourseEntities())
                courses.Add(new SelectListItem() { Text = ce.Number.ToString() });

            List<SelectListItem> specialities = new List<SelectListItem>();

            foreach (SpecialityEntity se in specialityService.GetAllSpecialityEntities())
                specialities.Add(new SelectListItem() { Text = se.Name });

            ViewBag.Faculties = faculties;
            ViewBag.Courses = courses;
            ViewBag.Groups = groups;
            ViewBag.Specialities = specialities;

            return View();
        }

        [HttpPost]
        public ActionResult AddCourse(string course)
        {
            int num = 0;
            Int32.TryParse(course, out num);
            if (courseService.GetAllCourseEntities().Any(ent => ent.Number == num))
                return RedirectToAction("DbEdit");
            else
                courseService.CreateCourse(new CourseEntity() { Number = num });
            return RedirectToAction("DbEdit");
        }

        [HttpPost]
        public ActionResult AddGroup(string group)
        {
            if (groupService.GetAllGroupEntities().Any(ent => ent.Name == group))
                return RedirectToAction("DbEdit");
            else
                groupService.CreateGroup(new GroupEntity() { Name = group });
            return RedirectToAction("DbEdit");
        }

        [HttpPost]
        public ActionResult AddSpeciality(string speciality)
        {
            if (specialityService.GetAllSpecialityEntities().Any(ent => ent.Name == speciality))
                return RedirectToAction("DbEdit");
            else
                specialityService.CreateSpeciality(new SpecialityEntity() { Name = speciality });
            return RedirectToAction("DbEdit");
        }

        [HttpPost]
        public ActionResult AddFaculty(string faculty)
        {
            if (facultyService.GetAllFacultyEntities().Any(ent => ent.Name == faculty))
                return RedirectToAction("DbEdit");
            else
                facultyService.CreateFaculty(new FacultyEntity() { Name = faculty });
            return RedirectToAction("DbEdit");
        }

        public ActionResult CreateTest()
        {
            NewTestViewModel test = new NewTestViewModel()
            {
                GraphArray = new string[1],
                FinalStates = new int?[1],
                Regex = "",
                States = 1,
                Values = 1,
                ValuesArray = new string[1]
            };
            return View(test);
        }

        [HttpPost]
        public ActionResult CreateTest(NewTestViewModel test, string TestWork)
        {
            ModelState.Clear();
            if (TestWork == "Добавить значение")
            {
                ++test.Values;
                string[] temp = test.ValuesArray;
                test.ValuesArray = new string[test.Values];
                temp.CopyTo(test.ValuesArray, 0);
                test = GetNewGraph(test);
            }
            if (TestWork == "Добавить состояние")
            {
                ++test.States;
                int?[] temp = test.FinalStates;
                test.FinalStates = new int?[test.States];
                temp.CopyTo(test.FinalStates, 0);
                string[] temp2 = test.GraphArray;
                test.GraphArray = new string[test.States * test.Values];
                temp2.CopyTo(test.GraphArray, 0);
            }
            if (TestWork == "Отправить")
            {
                AttachmentContentEntity content = new AttachmentContentEntity() { Content = convert.getFromNewTest(test) };
                contentService.CreateAttachmentContent(content);
                return RedirectToAction("Index");
            }
            return View(test);
        }
        [HttpGet]
        public ActionResult AddNews()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddNews(string title1, string text)
        {
            int id = blockTypeService.GetAllBlockTypeEntities().FirstOrDefault(ent => ent.Name == "Home").Id;
            BlockEntity block = new BlockEntity() { Title = title1, Text = text, BlockTypeId = id };
            blockService.CreateBlock(block);
            return View();
        }

        [HttpGet]
        public ActionResult AddMaterial()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddMaterial(string description, HttpPostedFileBase file)
        {
            if (file != null && file.ContentLength > 0)
            {
                string fileName = Path.GetFileName(file.FileName);
                string path = Path.Combine(Server.MapPath("~/Material/"), fileName);
                file.SaveAs(path);
                int id = blockTypeService.GetAllBlockTypeEntities().FirstOrDefault(ent => ent.Name == "Material").Id;
                BlockEntity block = new BlockEntity() { Title = fileName, Text = description, BlockTypeId = id };
                blockService.CreateBlock(block);
            }
            return View();
        }

        public ActionResult LogOff()
        {
            TempData["user"] = null;
            TempData["role"] = null;
            return RedirectToAction("Index");
        }

        private NewTestViewModel GetNewGraph(NewTestViewModel test)
        {
            string[] temp = test.GraphArray;
            test.GraphArray = new string[test.States * test.Values];
            for (int i = 0; i != test.States; i++)
            {
                for (int j = 0; j != test.Values - 1; j++)
                    test.GraphArray[i * test.Values + j] = temp[i * (test.Values - 1) + j];
            }
            return test;
        }
    }
}
