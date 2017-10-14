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
            var websites = GetQuery<GetWebsitesQuery>().Execute();
            return View(websites);
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