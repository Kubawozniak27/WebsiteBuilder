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

            var images = website.WebsiteImages.Where(x => x.IsDeleted == false).Select(x => new WebsiteContentDto()
            {
                Id = x.Id,
                ImageSrc = x.FilePath,
                X = x.CoordinateX,
                Y = x.CoordinateY,
                Height = x.Height,
                Width = x.Width,
                WebsiteElementType = WebsiteElementType.Image
            });

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
