using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteBuilder.BusinessLogic.Website.Commands;
using WebsiteBuilder.Public.Website;

namespace WebsiteBuilder.Web.Controllers
{
    public class WebsiteController : BaseController
    {
        // GET: Website
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult GetWebsitesPagedTable()
        {
            var result = GetQuery<GetWebsitesQuery>().Execute();
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult AddWebsite()
        {
            return View();
        }


        [HttpPost]
        public JsonResult AddWebsite(WebsiteDto websiteDto)
        {
            var result = GetCommand<AddWebsiteCommand>().Execute(websiteDto);
            return ConvertOperationResultToJson(result);
        }
    }
}