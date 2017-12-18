using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebsiteBuilder.BusinessLogic.Infrastructure;
using WebsiteBuilder.Core.Service;
using WebsiteBuilder.Public.WebsiteEditor;

namespace WebsiteBuilder.BusinessLogic.WebsiteEditor.Commands
{
    public class SaveWebsiteCommand : CommandBase, ICommand<OperationResult, SaveWebsiteEditorDto>
    {
        public OperationResult Execute(SaveWebsiteEditorDto request)
        {
            var website = Db.Websites.FirstOrDefault(x => x.Id == request.WebsiteId);

            website.Color = request.WebsiteColor;


            Db.SaveChanges();

            return new OperationResult();
        }
    }
}
