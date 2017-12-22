using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteBuilder.BusinessLogic.Image.Commands;
using WebsiteBuilder.BusinessLogic.WebsiteEditor.Commands;
using WebsiteBuilder.BusinessLogic.WebsiteEditor.Queries;
using WebsiteBuilder.Data;
using WebsiteBuilder.Public.Image;
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

        public JsonResult GetWebsiteContent(int id)
        {
            var dto = GetQuery<GetWebsiteContentByIdQuery>().Execute(id);
            return Json(dto, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SaveWebsite(SaveWebsiteEditorDto request)
        {
            var result = GetCommand<SaveWebsiteCommand>().Execute(request);
            return ConvertOperationResultToJson(result);
        }

        
        public ActionResult UploadImage()
        {
            return View();
        }

        [HttpPost]
        public ActionResult UploadImage(ImageDto request)
        {
            string fileName = Path.GetFileNameWithoutExtension(request.ImageFile.FileName);
            string extension = Path.GetExtension(request.ImageFile.FileName);
            fileName += extension;
            request.FilePath = "~/Images/" + fileName;
            fileName = Path.Combine(Server.MapPath("~/Images/"), fileName);

            var result = GetCommand<UploadImageCommand>().Execute(request);

            if (result.Success)
            {
                request.ImageFile.SaveAs(fileName);
            }

            return RedirectToAction("Index", "WebsiteEditor", new { id = request.WebsiteId });
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