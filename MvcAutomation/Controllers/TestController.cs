using BLL.Interface.Services;
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

        public TestController(IBlockService service, IBlockTypeService service1)
        {
            blockService = service;
            blockTypeService = service1;
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

    }
}
