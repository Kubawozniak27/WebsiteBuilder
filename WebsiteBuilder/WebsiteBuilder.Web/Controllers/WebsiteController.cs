using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Mvc;
using WebsiteBuilder.BusinessLogic.Website.Commands;
using WebsiteBuilder.BusinessLogic.WebsiteEditor.Queries;
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

        public ActionResult PreviewWebsite(int id)
        {
            var dto = GetQuery<GetWebsiteByIdQuery>().Execute(id);
            return View("PreviewWebsite", dto);
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

        [HttpPost]
        public FileResult GenereteWebsiteToHtml(int websiteId)
        {
            HttpWebRequest request = WebRequest.Create("http://localhost:52012/Website/PreviewWebsite/" + websiteId) as HttpWebRequest;
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            string responseText;
            var encoding = ASCIIEncoding.ASCII;
            using (var reader = new StreamReader(response.GetResponseStream(), encoding))
            {
                responseText = reader.ReadToEnd();
            }
            var bytes = Encoding.UTF8.GetBytes(responseText);
            string contentType = "text/html";
            string fileName = "index.html";

            return File(bytes, contentType, fileName);
        }
    }
}