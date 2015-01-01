using BLL.Interface.Entities;
using BLL.Interface.Services;
using MvcAutomation.Convertation;
using MvcAutomation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcAutomation.Controllers
{
    [Authorize]
    public class TestController : Controller
    {
        private readonly IBlockService blockService;
        private readonly IBlockTypeService blockTypeService;
        private readonly ITestService testService;
        private readonly IAttachmentContentService contentService;
        private readonly IUserService userService;
        private readonly IAnswerService answerService;
        private readonly ITestConvert converter;

        public TestController(IBlockService service, IBlockTypeService service1,
            ITestService service2, IAttachmentContentService service3, IUserService service4,
            IAnswerService service5) : base()
        {
            blockService = service;
            blockTypeService = service1;
            testService = service2;
            contentService = service3;
            userService = service4;
            answerService = service5;
            this.converter = new XmlConverter();
        }

        public ActionResult Index()
        {
            int id = blockTypeService.GetAllBlockTypeEntities().FirstOrDefault(ent => ent.Name == "Test").Id;
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
            TestEntity test = testService.GetAllTestEntities().FirstOrDefault(ent => ent.Name == test_name);
            List<AttachmentContentEntity> contents = testService.GetAttachmentContents(test).ToList();
            NewTestViewModel newTest = converter.getFromBytes(contents[num].Content);
            PassingTestViewModel passing = new PassingTestViewModel()
            {
                TestNum = num,
                TestName = test.Name,
                Regex = newTest.Regex,
                Values = 1,
                ValuesArray = new string[1],
                States = 1,
                FinalStates = new int?[1],
                GraphArray = new string[1],
                TestCount = test.TestCount,
                TestTime = test.TestTime
            };
            return View(passing);
        }

        [HttpPost]
        public ActionResult StartTest(PassingTestViewModel test, string TestWork)
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
                NewTestViewModel newTest = new NewTestViewModel()
                {
                    FinalStates = test.FinalStates,
                    GraphArray = test.GraphArray,
                    Regex = test.Regex,
                    States = test.States,
                    TestName = test.TestName,
                    Values = test.Values,
                    ValuesArray = test.ValuesArray
                };
                AttachmentContentEntity content = new AttachmentContentEntity() { Content = converter.getFromNewTest(newTest) };
                UserEntity user = userService.GetAllUserEntities().LastOrDefault(ent => ent.Nickname == User.Identity.Name);
                TestEntity testEnt = testService.GetAllTestEntities().FirstOrDefault(ent => ent.Name == test.TestName);
                content.FileName = User.Identity.Name + Guid.NewGuid().ToString();
                contentService.CreateAttachmentContent(content);
                int id = contentService.GetAllAttachmentContentEntities().FirstOrDefault(ent => ent.FileName == content.FileName).Id;

                AnswerEntity answer = new AnswerEntity() { Id = id, TestId = testEnt.Id, UserId = user.Id };
                answerService.CreateAnswer(answer);

                return RedirectToAction("Index");
            }
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
    }
}
