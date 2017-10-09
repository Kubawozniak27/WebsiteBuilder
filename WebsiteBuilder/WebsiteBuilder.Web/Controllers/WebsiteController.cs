using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteBuilder.BusinessLogic.Website;
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

        [HttpGet]
        public ActionResult AddWebsite()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddWebsite(WebsiteDto websiteDto)
        {
            var result = GetCommand<AddWebsiteCommand>().Execute(websiteDto);
            return View();
        }
    }
}