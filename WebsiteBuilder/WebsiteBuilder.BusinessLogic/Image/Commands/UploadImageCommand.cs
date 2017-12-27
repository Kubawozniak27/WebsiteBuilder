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
            Db.Images.Add(new Data.Entities.Image()
            {
                ImagePath = request.ImagePath,
                ImageName = request.ImageFile.FileName,
                WebsiteId = request.WebsiteId
            });

            Db.SaveChanges();

            return new OperationResult();
        }
    }
}
