using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebsiteBuilder.BusinessLogic.Infrastructure;
using WebsiteBuilder.Core.Service;
using WebsiteBuilder.Public.Website;

namespace WebsiteBuilder.BusinessLogic.Website.Commands
{
    public class GetWebsitesQuery : QueryBase, IQueryOut<List<WebsiteDto>>
    {
        public List<WebsiteDto> Execute()
        {
            var websites = Db.Websites.Select(x => new WebsiteDto()
            {
                Name = x.Name,
                PublishDate = x.PublishDate,
                WebsiteId = x.Id
            }).ToList();

            return websites;
        }
    }
}
