using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebsiteBuilder.BusinessLogic.Infrastructure;
using WebsiteBuilder.Core.Service;
using WebsiteBuilder.Public.Image;

namespace WebsiteBuilder.BusinessLogic.Image.Commands
{
    public class UploadImageCommand : CommandBase, ICommand<OperationResult, ImageDto>
    {
        public OperationResult Execute(ImageDto request)
        {
            Db.WebsiteImages.Add(new Data.Entities.WebsiteImage()
            {
                WebsiteId = request.WebsiteId,
                FilePath = request.FilePath,
                Title = request.Title,
            });

            Db.SaveChanges();

            return new OperationResult();
        }
    }
}
