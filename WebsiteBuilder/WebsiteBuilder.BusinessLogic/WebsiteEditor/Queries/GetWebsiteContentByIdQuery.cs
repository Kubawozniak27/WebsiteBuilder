using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebsiteBuilder.BusinessLogic.Infrastructure;
using WebsiteBuilder.Core.Service;
using WebsiteBuilder.Public.WebsiteEditor;
using System.Data.Entity;
using WebsiteBuilder.Public.Text;
using WebsiteBuilder.Public.Website;

namespace WebsiteBuilder.BusinessLogic.WebsiteEditor.Queries
{
    public class GetWebsiteContentByIdQuery : QueryBase,IQuery<SaveWebsiteEditorDto, int>
    {
        public SaveWebsiteEditorDto Execute(int request)
        {
            var website = Db.Websites
                .Include(x => x.WebsiteTexts)
                .Include(x => x.Images)
                .FirstOrDefault(x => x.Id == request);

            var texts = website.WebsiteTexts.Select(x => new WebsiteContentDto()
            {
                Id = x.Id,
                Text = x.Text,
                X = x.CoordinateX,
                Y = x.CoordinateY,
                Height = x.Height,
                Width = x.Width,
                WebsiteElementType = WebsiteElementType.Text
            });

            var websiteImages = Db.Images
                .Include(x => x.WebsiteImages)
                .Where(x => x.WebsiteId == request).ToList();


            var images = new List<WebsiteContentDto>();

            foreach (var image in websiteImages)
            {
                foreach (var websiteImage in image.WebsiteImages)
                {
                    images.Add(new WebsiteContentDto()
                    {
                        Id = websiteImage.Id,
                        ImageId = image.Id,
                        ImageSrc = image.ImagePath,
                        WebsiteElementType = WebsiteElementType.Image,
                        Height = websiteImage.Height,
                        Width = websiteImage.Width,
                        X = websiteImage.CoordinateX,
                        Y = websiteImage.CoordinateY
                    });
                }
            }


            

            var websiteContents = new List<WebsiteContentDto>();

            websiteContents.AddRange(texts);
            websiteContents.AddRange(images);

            var result = new SaveWebsiteEditorDto()
            {
                WebsiteId = website.Id,
                WebsiteColor = website.Color,
                WebsiteContents = websiteContents
            };


            return result;
        }
    }
}
