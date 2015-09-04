using BLL.Interface.Entities;
using BLL.Interface.Services;
using MvcAutomation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Systems;
using XMLConvertation;
using MvcAutomation.Mappers;

namespace MvcAutomation.Controllers
{
    [Authorize]
    public class TestController : Controller
    {
        private readonly IBlockService blockService;
        private readonly IContentService contentService;
        private readonly IUserService userService;
        private readonly ITestConvert converter;
        private readonly IGradeSystem grade;

        public TestController(IBlockService service1, IContentService service2, 
            IUserService service3, ITestConvert converter, IGradeSystem grade)
        {
            blockService = service1;
            contentService = service2;
            userService = service3;
            this.converter = converter;
            this.grade = grade;
        }

        public ActionResult Index()
        {
            int id = blockService.GetAllBlockTypeEntities().FirstOrDefault(ent => ent.Name == "Test").Id;
            return View(blockService.GetAllBlockEntities()
                .Reverse().Where(ent => ent.BlockTypeId == id)
                .Select(block => new BlockViewModel()
                {
                    Text = block.Text,
                    Title = block.Title
                }));
        }

        [HttpGet]
        public ActionResult StartTest(string test_name, int num)
        {
            TestEntity test = contentService.GetAllTestEntities().FirstOrDefault(ent => ent.Name == test_name);
            List<AttachmentContentEntity> contents = contentService.GetAttachmentContents(test).ToList();
            num = new Random().Next(contents.Count);
            NewTestViewModel newTest = converter.getFromBytes(contents[num].Content).ToView();
            if (Request.Cookies["time"] == null)
            {
                Response.Cookies["time"].Value = (test.TestTime * 60).ToString();
                Response.Cookies["time"].Expires = DateTime.Now.AddDays(2);
            }
            else
            {
                int remain = (Int32.Parse(Request.Cookies["time"].Value) - Math.Abs(DateTime.Now.Minute - Int32.Parse(Request.Cookies["min"].Value)) * 60);
                if (remain < 1)
                    remain = 1;
                Response.Cookies["time"].Value = remain.ToString();
            }
            PassingTestViewModel passing = new PassingTestViewModel()
            {
                TestNum = num,
                TestName = test.Name,
                Description = newTest.Description,
                Regex = newTest.Regex,
                Values = 1,
                ValuesArray = new string[1],
                States = 1,
                FinalStates = new int?[1],
                GraphArray = new string[1],
                TestCount = test.TestCount,
                TestTime = test.TestTime * 60
            };
            return View(passing);
        }

        [HttpPost]
        public ActionResult StartTest(PassingTestViewModel test, string TestWork)
        {
            ModelState.Clear();
            //if (TestWork == "Добавить значение")
            //{
            //    ++test.Values;
            //    string[] temp = test.ValuesArray;
            //    test.ValuesArray = new string[test.Values];
            //    temp.CopyTo(test.ValuesArray, 0);
            //    test = GetNewGraph(test);
            //}
            //if (TestWork == "Добавить состояние")
            //{
            //    ++test.States;
            //    int?[] temp = test.FinalStates;
            //    test.FinalStates = new int?[test.States];
            //    temp.CopyTo(test.FinalStates, 0);
            //    string[] temp2 = test.GraphArray;
            //    test.GraphArray = new string[test.States * test.Values];
            //    temp2.CopyTo(test.GraphArray, 0);
            //}
            //if (TestWork == "Отправить")
            //{
            //    NewTestViewModel newTest = new NewTestViewModel()
            //    {
            //        FinalStates = test.FinalStates,
            //        GraphArray = test.GraphArray,
            //        Description = test.Description,
            //        Regex = test.Regex,
            //        States = test.States,
            //        TestName = test.TestName,
            //        Values = test.Values,
            //        ValuesArray = test.ValuesArray
            //    };
            //    if (Request.Cookies["time"] != null)
            //        Response.Cookies["time"].Expires = DateTime.Now.AddSeconds(5);
            //    AttachmentContentEntity content = new AttachmentContentEntity() { Content = converter.getFromNewTest(newTest.ToEntity()) };
            //    UserEntity user = userService.GetAllUserEntities().LastOrDefault(ent => ent.Nickname == User.Identity.Name);
            //    TestEntity testEnt = contentService.GetAllTestEntities().FirstOrDefault(ent => ent.Name == test.TestName);

            //    List<AttachmentContentEntity> contents = contentService.GetAttachmentContents(testEnt).ToList();
            //    NewTestViewModel rightTest = converter.getFromBytes(contents[test.TestNum].Content).ToView();
            //    double mark = grade.GradeTest(newTest.ToEntity(), rightTest.ToEntity());

            //    content.FileName = User.Identity.Name + Guid.NewGuid().ToString();
            //    contentService.CreateAttachmentContent(content);
            //    int id = contentService.GetAllAttachmentContentEntities().FirstOrDefault(ent => ent.FileName == content.FileName).Id;

            //    AnswerEntity answer = new AnswerEntity() { Id = id, TestId = testEnt.Id, UserId = user.Id, Mark = mark };
            //    contentService.CreateAnswer(answer);

            //    return RedirectToAction("Index");
            //}
            return View(test);
        }

        private PassingTestViewModel GetNewGraph(PassingTestViewModel test)
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
            contentService.Dispose();
            userService.Dispose();
            base.Dispose(disposing);
        }
    }
}
