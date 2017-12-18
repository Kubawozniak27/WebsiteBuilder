using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebsiteBuilder.BusinessLogic.Infrastructure;
using WebsiteBuilder.Core.Service;
using WebsiteBuilder.Public.WebsiteEditor;

namespace WebsiteBuilder.BusinessLogic.WebsiteEditor.Queries
{
    public class GetWebsiteByIdQuery : QueryBase, IQuery<SaveWebsiteEditorDto, int>
    {
        public SaveWebsiteEditorDto Execute(int websiteId)
        {
            var website = Db.Websites.FirstOrDefault(x => x.Id == websiteId);

            var result = new SaveWebsiteEditorDto()
            {
                WebsiteId = website.Id,
                WebsiteColor = website.Color,
            };


            return result;
        }
    }
}
