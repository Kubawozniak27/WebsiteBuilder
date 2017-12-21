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

namespace WebsiteBuilder.BusinessLogic.WebsiteEditor.Queries
{
    public class GetWebsiteContentByIdQuery : QueryBase,IQuery<SaveWebsiteEditorDto, int>
    {
        public SaveWebsiteEditorDto Execute(int request)
        {
            var website = Db.Websites
                .Include(x => x.WebsiteTexts)
                .FirstOrDefault(x => x.Id == request);

            var result = new SaveWebsiteEditorDto()
            {
                WebsiteId = website.Id,
                WebsiteColor = website.Color,
                Texts = website.WebsiteTexts.Select(x => new TextDto()
                {
                    Text = x.Text,
                    Width = x.Width,
                    Height = x.Height,
                    X = x.CoordinateX,
                    Y = x.CoordinateY
                }).ToList()
            };

            return result;
        }
    }
}
