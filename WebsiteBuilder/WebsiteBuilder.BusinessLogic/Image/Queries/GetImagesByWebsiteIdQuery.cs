using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebsiteBuilder.BusinessLogic.Infrastructure;
using WebsiteBuilder.Core.Service;
using WebsiteBuilder.Public.Image;

namespace WebsiteBuilder.BusinessLogic.Image.Queries
{
    public class GetImagesByWebsiteIdQuery : QueryBase, IQuery<List<ImageNameAndSrcDto>, int>
    {
        public List<ImageNameAndSrcDto> Execute(int request)
        {
            var images = Db.Images
                .Where(x => x.WebsiteId == request)
                .Select(x => new ImageNameAndSrcDto()
                {
                    ImageSrc =  x.ImagePath ,
                    Name = x.ImageName
                }).ToList();

            return images;
        }
    }
}
