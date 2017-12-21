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
using WebsiteBuilder.Public.Image;

namespace WebsiteBuilder.BusinessLogic.WebsiteEditor.Queries
{
    public class GetWebsiteByIdQuery : QueryBase, IQuery<SaveWebsiteEditorDto, int>
    {
        public SaveWebsiteEditorDto Execute(int websiteId)
        {
            var website = Db.Websites
                .Include(x => x.WebsiteTexts)
                .FirstOrDefault(x => x.Id == websiteId);

            var result = new SaveWebsiteEditorDto()
            {
                WebsiteId = website.Id,
                WebsiteColor = website.Color,
                WebsiteName = website.Name,
                Texts = website.WebsiteTexts.Select(x => new TextDto()
                {
                    Id = x.Id,
                    Text = x.Text,
                    Width = x.Width,
                    Height = x.Height,
                    X = x.CoordinateX,
                    Y = x.CoordinateY
                }).ToList(),
                Images = website.WebsiteImages.Select(x=>new ImageDto()
                {
                    ImageId = x.Id,
                    WebsiteId = x.WebsiteId,
                    FilePath = x.FilePath,
                    Title = x.Title
                }).ToList()
            };

            return result;
        }
    }
}
