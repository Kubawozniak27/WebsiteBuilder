using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteBuilder.Web.Extensions;
using WebsiteBuilder.Web.Models.WebsiteEditor;

namespace WebsiteBuilder.Web.Controllers
{
    public class WebsiteEditorController : Controller
    {
        // GET: WebsiteEditor
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetBaseTemplates()
        {
            var vm = new WebsiteEditorViewModel()
            {
                Header = PartialView("_Header").RenderToString()
            };

            return Json(vm, JsonRequestBehavior.AllowGet);
        }


    }
}