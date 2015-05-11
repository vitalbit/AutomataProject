using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcAutomation.Models;
using BLL.Interface.Services;
using BLL.Interface.Entities;
using System.Web.Security;
using System.IO;
using MvcAutomation.Providers;
using XMLConvertation;
using MvcAutomation.Mappers;
using System.Text;
using RegexpressionProcess;

namespace MvcAutomation.Controllers
{
    public class HomeController : Controller
    {
        //private static string RegExpControl = "";
        private readonly IBlockService blockService;
        private readonly IUserService userService;
        private readonly ITestConvert convert;
        private readonly IContentService contentService;
        private readonly IRegExpCheck regexpCheck;

        public HomeController(IBlockService service1, IUserService service2,
            IContentService service3, ITestConvert convert, IRegExpCheck regexpCheck)
        {
            blockService = service1;
            userService = service2;
            contentService = service3;
            this.convert = convert;
            this.regexpCheck = regexpCheck;
        }

        //
        // GET: /Home/
        [AllowAnonymous]
        public ActionResult Index()
        {
            int id = blockService.GetAllBlockTypeEntities().FirstOrDefault(ent => ent.Name == "Home").Id;
            return View(blockService.GetAllBlockEntities()
                .Reverse().Where(entity => entity.BlockTypeId == id)
                .Select(block => new BlockViewModel()
                {
                    Text = block.Text,
                    Title = block.Title
                }));
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult Login(string login, string password)
        {
            if (ModelState.IsValid)
            {
                if (Membership.ValidateUser(login, password))
                {
                    FormsAuthentication.SetAuthCookie(login, true);
                    var user = userService.GetAllUserEntities().FirstOrDefault(ent => ent.Nickname == login);
                    if (user != null)
                    {
                        Response.Cookies["user_name"].Value = Convert.ToBase64String(Encoding.Default.GetBytes(user.FirstName));
                        Response.Cookies["user_name"].Expires = DateTime.Now.AddDays(2);
                    }
                    return RedirectToAction("Index");
                }
                else
                {
                    Session["LoginMessage"] = "Неправильный пароль или логин!";
                    //ModelState.AddModelError("", "Неправильный пароль или логин");
                }
            }
            return RedirectToAction("Index");
        }

        [Authorize]
        [HttpGet]
        public ActionResult Settings()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult Settings(ChangePasswordViewModel pass)
        {
            UserEntity user = userService.GetAllUserEntities().FirstOrDefault(ent => ent.Nickname == User.Identity.Name);

            if (((CustomMembershipProvider)Membership.Provider).ChangePassword(user, pass.OldPassword, pass.NewPassword))
            {
                return RedirectToAction("Index");
            }
            else
                ModelState.AddModelError("OldPassword", "Неверный пароль");
            return View();
        }

        [Authorize(Roles="User")]
        public ActionResult TestResult()
        {
            UserEntity user = userService.GetAllUserEntities().FirstOrDefault(ent => ent.Nickname == User.Identity.Name);
            List<AnswerEntity> answers = contentService.GetAllAnswerEntities().Reverse().Where(ent => ent.UserId == user.Id).ToList();
            List<TestEntity> tests = contentService.GetAllTestEntities().ToList();
            List<TestResultViewModel> results = new List<TestResultViewModel>();
            for (int i = 0; i != answers.Count; i++)
            {
                results.Add(new TestResultViewModel() { TestName = tests.FirstOrDefault(ent => ent.Id == answers[i].TestId).Name, Result = answers[i].Mark });
            }
            return View(results);
        }

        [Authorize(Roles="Admin")]
        [HttpGet]
        public ActionResult DbEdit()
        {
            List<SelectListItem> faculties = new List<SelectListItem>();

            foreach (FacultyEntity fe in userService.GetAllFacultyEntities())
                faculties.Add(new SelectListItem() { Text = fe.Name });

            List<SelectListItem> groups = new List<SelectListItem>();

            foreach (GroupEntity ge in userService.GetAllGroupEntities())
                groups.Add(new SelectListItem() { Text = ge.Name });

            List<SelectListItem> courses = new List<SelectListItem>();

            foreach (CourseEntity ce in userService.GetAllCourseEntities())
                courses.Add(new SelectListItem() { Text = ce.Number.ToString() });

            List<SelectListItem> specialities = new List<SelectListItem>();

            foreach (SpecialityEntity se in userService.GetAllSpecialityEntities())
                specialities.Add(new SelectListItem() { Text = se.Name });

            ViewBag.Faculties = faculties;
            ViewBag.Courses = courses;
            ViewBag.Groups = groups;
            ViewBag.Specialities = specialities;

            return View();
        }

        [Authorize(Roles="Admin")]
        [HttpPost]
        public ActionResult AddCourse(string course)
        {
            int num = 0;
            if (Int32.TryParse(course, out num))
            {
                if (userService.GetAllCourseEntities().Any(ent => ent.Number == num))
                    Session["CourseMessage"] = "Данный курс уже существует";
                else
                    userService.CreateCourse(new CourseEntity() { Number = num });
            }
            else
                Session["CourseMessage"] = "Номер курса должен быть числом";
            return RedirectToAction("DbEdit");
        }

        [Authorize(Roles="Admin")]
        [HttpPost]
        public ActionResult AddGroup(string group)
        {
            if (group != "")
            {
                if (userService.GetAllGroupEntities().Any(ent => ent.Name == group))
                    Session["GroupMessage"] = "Данная группа уже существует";
                else
                    userService.CreateGroup(new GroupEntity() { Name = group });
            }
            else
                Session["GroupMessage"] = "Введите название либо номер группы";
            return RedirectToAction("DbEdit");
        }

        [Authorize(Roles="Admin")]
        [HttpPost]
        public ActionResult AddSpeciality(string speciality)
        {
            if (speciality != "")
            {
                if (userService.GetAllSpecialityEntities().Any(ent => ent.Name == speciality))
                    Session["SpecialityMessage"] = "Данная специальность уже существует";
                else
                    userService.CreateSpeciality(new SpecialityEntity() { Name = speciality });
            }
            else
                Session["SpecialityMessage"] = "Введите название специальности";
            return RedirectToAction("DbEdit");
        }

        [Authorize(Roles="Admin")]
        [HttpPost]
        public ActionResult AddFaculty(string faculty)
        {
            if (faculty != "")
            {
                if (userService.GetAllFacultyEntities().Any(ent => ent.Name == faculty))
                    Session["FacultyMessage"] = "Данный факультет уже существует";
                else
                    userService.CreateFaculty(new FacultyEntity() { Name = faculty });
            }
            else
                Session["FacultyMessage"] = "Введите навание факультета";
            return RedirectToAction("DbEdit");
        }

        [Authorize(Roles="Admin")]
        [HttpGet]
        public ActionResult CreateTest()
        {
            NewTestViewModel test = new NewTestViewModel()
            {
                TestName = "",
                Description = "",
                GraphArray = new string[1],
                FinalStates = new int?[1],
                Regex = "",
                States = 1,
                Values = 1,
                ValuesArray = new string[1]
            };
            return View(test);
        }

        [Authorize(Roles="Admin")]
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
                if (test.Description == null)
                    Session["CreateMessage"] = "Описание не должно быть пусто";
                else if (test.Regex == null)
                    Session["CreateMessage"] = "Регулярное выражение не должно быть пусто";
                else if (test.TestName == null)
                    Session["CreateMessage"] = "Имя теста не должно быть пустым";
                else if (!regexpCheck.isMatchesToDescription(test.Description, test.Regex))
                    Session["CreateMessage"] = "Регулярное выражение не соответствует описанию";
                else
                {
                    AttachmentContentEntity content = new AttachmentContentEntity() { Content = convert.getFromNewTest(test.ToEntity()), FileName = test.TestName };
                    contentService.CreateAttachmentContent(content);
                    return RedirectToAction("Index");
                }
            }
            return View(test);
        }

        [Authorize(Roles="Admin")]
        [HttpGet]
        public ActionResult AddTest()
        {
            List<AddTestViewModel> content = contentService.GetTestAttachmentContentEntities().Select(ent => new AddTestViewModel() { AttachmentContentId = ent.Id, FileName = ent.FileName }).ToList();
            return View(content);
        }

        [Authorize(Roles="Admin")]
        [HttpPost]
        public ActionResult AddTest(string test_name, int?[] sel_test, string description, int? count, int? time)
        {
            if (test_name == "")
                Session["TestResult"] = "Введите имя теста";
            else if (sel_test == null || sel_test.Length == 0)
                Session["TestResult"] = "Выберите хотябы один тест";
            else if (count == null || count > sel_test.Length)
                Session["TestResult"] = "Количество тестов должно быть не больше количество выбранных тестов";
            else if (time == null || time <= 0)
                Session["TestResult"] = "Время должно быть больше 0";
            else
            {
                TestEntity te = new TestEntity() { Name = test_name, TestCount = count, TestTime = time };
                contentService.CreateTest(te);
                te = contentService.GetAllTestEntities().FirstOrDefault(ent => ent.Name == test_name);
                List<AttachmentContentEntity> contents = new List<AttachmentContentEntity>();
                foreach (var id in sel_test)
                {
                    contents.Add(contentService.GetAllAttachmentContentEntities().FirstOrDefault(ent => ent.Id == id));
                }
                contentService.SetAttachmentContent(te, contents);
                int idtype = blockService.GetAllBlockTypeEntities().FirstOrDefault(ent => ent.Name == "Test").Id;
                blockService.CreateBlock(new BlockEntity() { Title = te.Name, Text = description, BlockTypeId = idtype });
                Session["TestResult"] = "Тест создан";
            }
            return RedirectToAction("AddTest");
        }

        [Authorize(Roles="Admin")]
        [HttpGet]
        public ActionResult AddNews()
        {
            return View();
        }

        [Authorize(Roles="Admin")]
        [HttpPost]
        public ActionResult AddNews(string title1, string text)
        {
            int id = blockService.GetAllBlockTypeEntities().FirstOrDefault(ent => ent.Name == "Home").Id;
            BlockEntity block = new BlockEntity() { Title = title1, Text = text, BlockTypeId = id };
            blockService.CreateBlock(block);
            return RedirectToAction("Index"); //View();
        }

        [Authorize(Roles="Admin")]
        [HttpGet]
        public ActionResult AddMaterial()
        {
            return View();
        }

        [Authorize(Roles="Admin")]
        [HttpPost]
        public ActionResult AddMaterial(string description, HttpPostedFileBase file)
        {
            if (file != null && file.ContentLength > 0)
            {
                string fileName = Path.GetFileName(file.FileName);
                string path = Path.Combine(Server.MapPath("~/Material/"), fileName);
                file.SaveAs(path);
                int id = blockService.GetAllBlockTypeEntities().FirstOrDefault(ent => ent.Name == "Material").Id;
                BlockEntity block = new BlockEntity() { Title = fileName, Text = description, BlockTypeId = id };
                blockService.CreateBlock(block);
                Session["FileResult"] = "Материал успешно добавлен";
            }
            else
                Session["FileResult"] = "Загрузите файл";
            return View();
        }

        [Authorize]
        public ActionResult LogOff()
        {
            Response.Cookies["user_name"].Value = null;
            FormsAuthentication.SignOut();
            return RedirectToAction("Index");
        }

        [AllowAnonymous]
        public ActionResult Message()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult NotFound()
        {
            return View();
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

        protected override void Dispose(bool disposing)
        {
            blockService.Dispose();
            userService.Dispose();
            contentService.Dispose();
            base.Dispose(disposing);
        }
    }
}
