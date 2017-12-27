using Ionic.Zip;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Mvc;
using WebsiteBuilder.BusinessLogic.Image.Queries;
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
            var dto = GetQuery<GetWebsiteContentByIdQuery>().Execute(id);
            return View("PreviewWebsite", dto);
        }

        public ActionResult PreviewWebsiteForGenereteHtml(int id)
        {
            var dto = GetQuery<GetWebsiteContentByIdQuery>().Execute(id);
            return View("PreviewWebsiteForGenereteHtml", dto);
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
            HttpWebRequest request = WebRequest.Create("http://localhost:52012/Website/PreviewWebsiteForGenereteHtml/" + websiteId) as HttpWebRequest;
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            string responseWebsite;
            string responseBoostrapStyles;
            var encoding = ASCIIEncoding.UTF8;

            using (var reader = new StreamReader(response.GetResponseStream(), encoding))
            {
                responseWebsite = reader.ReadToEnd();
            }

            using ( var streamReader = new StreamReader(Server.MapPath("~/Content/bootstrap.min.css")))
            {
                responseBoostrapStyles = streamReader.ReadToEnd();
            }

            var bytesForWebsite = Encoding.UTF8.GetBytes(responseWebsite);
            var bytesForBoostrapStyles = Encoding.ASCII.GetBytes(responseBoostrapStyles);

            var images = GetQuery<GetImagesByWebsiteIdQuery>().Execute(websiteId);

            using (MemoryStream ms = new MemoryStream())
            {
                using (var archive = new ZipArchive(ms, ZipArchiveMode.Create, true))
                {
                    var zipArchiveEntry = archive.CreateEntry("index.html", CompressionLevel.Optimal);
                    using (var zipStream = zipArchiveEntry.Open())
                    {
                        zipStream.Write(bytesForWebsite, 0, bytesForWebsite.Length);
                    }

                    zipArchiveEntry = archive.CreateEntry("css/bootstrap.min.css", CompressionLevel.Optimal);
                    using (var zipStream = zipArchiveEntry.Open())
                    {
                        zipStream.Write(bytesForBoostrapStyles, 0, bytesForBoostrapStyles.Length);
                    }

                    foreach (var item in images)
                    {
                        string path = Server.MapPath(item.ImageSrc);
                        byte[] imgdata = System.IO.File.ReadAllBytes(path);
                        zipArchiveEntry = archive.CreateEntry("Images/"+item.Name, CompressionLevel.Fastest);
                        using (var zipStream = zipArchiveEntry.Open())
                        {
                            zipStream.Write(imgdata, 0, imgdata.Length);
                        }
                    }

                }
                return File(ms.ToArray(), "application/zip", "index.zip");
            }
        }
    }
}