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
using WebsiteBuilder.Public.NavigationBar;

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
                SelectorId = x.SelectorId,
                WebsiteElementType = WebsiteElementType.Text,
                FontSize = x.FontSize,
                FontFamily = x.FontFamily,
                FontStyle = x.FontStyle,
                FontWeight = x.FontWeight,
                FontColor = x.FontColor
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
                        SelectorId = websiteImage.SelectorId,
                        X = websiteImage.CoordinateX,
                        Y = websiteImage.CoordinateY
                    });
                }
            }

            var navigationBars = Db.WebsiteNavigationBar
                .Where(x => x.WebsiteId == request)
                .Select(x => new NavigationBarDto()
                {
                    NavigationId = x.Id,
                    SectionName = x.SectionName,
                    NavigationName = x.NavigationName,
                    NavigationBarStyle = x.NavigationBarStyle
                }).ToList();

            var websiteContents = new List<WebsiteContentDto>();

            websiteContents.AddRange(texts);
            websiteContents.AddRange(images);

            var result = new SaveWebsiteEditorDto()
            {
                WebsiteId = website.Id,
                WebsiteColor = website.Color,
                WebsiteContents = websiteContents,
                NavigationBars = navigationBars
            };


            return result;
        }
    }
}
