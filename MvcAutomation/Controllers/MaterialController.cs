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
    public class MaterialController : Controller
    {
        private readonly IBlockService blockService;
        private readonly IBlockTypeService blockTypeService;

        public MaterialController(IBlockService service, IBlockTypeService service1)
        {
            blockService = service;
            blockTypeService = service1;
        }
        //
        // GET: /Material/

        public ActionResult Index()
        {
            int id = blockTypeService.GetAllBlockTypeEntities().FirstOrDefault(ent => ent.Name == "Material").Id;
            return View(blockService.GetAllBlockEntities()
                .Reverse().Where(ent => ent.BlockTypeId == id)
                .Select(block => new BlockViewModel()
                {
                    Text = block.Text,
                    Title = block.Title
                }));
        }

        protected override void Dispose(bool disposing)
        {
            blockService.Dispose();
            blockTypeService.Dispose();
            base.Dispose(disposing);
        }
    }
}
