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
    public class AddWebsiteCommand : CommandBase, ICommand<OperationResult, WebsiteDto>
    {
        public OperationResult Execute(WebsiteDto request)
        {
            var website = new Data.Entities.Website()
            {
                Name = request.Name,
                PublishDate = DateTime.Now
            };

            Db.Websites.Add(website);
            Db.SaveChanges();

            return new OperationResult();
        }
    }
}
