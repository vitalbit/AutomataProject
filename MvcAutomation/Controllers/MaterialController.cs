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

        public MaterialController(IBlockService service)
        {
            blockService = service;
        }
        //
        // GET: /Material/

        public ActionResult Index()
        {
            int id = blockService.GetAllBlockTypeEntities().FirstOrDefault(ent => ent.Name == "Material").Id;
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
            base.Dispose(disposing);
        }
    }
}
