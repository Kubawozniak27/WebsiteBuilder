using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteBuilder.BusinessLogic.WebsiteEditor.Commands;
using WebsiteBuilder.BusinessLogic.WebsiteEditor.Queries;
using WebsiteBuilder.Public.WebsiteEditor;
using WebsiteBuilder.Web.Extensions;
using WebsiteBuilder.Web.Models.WebsiteEditor;

namespace WebsiteBuilder.Web.Controllers
{
    public class WebsiteEditorController : BaseController
    {
        // GET: WebsiteEditor
        public ActionResult Index(int id)
        {
            var dto = GetQuery<GetWebsiteByIdQuery>().Execute(id);
            return View("Index", dto);
        }

        [HttpPost]
        public JsonResult SaveWebsite(SaveWebsiteEditorDto request)
        {
            var result = GetCommand<SaveWebsiteCommand>().Execute(request);
            return ConvertOperationResultToJson(result);
        }

        

        public JsonResult GetBaseTemplates()
        {
            var vm = new WebsiteEditorViewModel()
            {
                Text = PartialView("_Text").RenderToString(),
                Image = PartialView("_Image").RenderToString(),
                Menu = PartialView("_Menu").RenderToString()
            };

            return Json(vm, JsonRequestBehavior.AllowGet);
        }


    }
}