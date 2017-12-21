using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebsiteBuilder.Public.Image;
using WebsiteBuilder.Public.Text;

namespace WebsiteBuilder.Public.WebsiteEditor
{
    public class SaveWebsiteEditorDto
    {
        public int WebsiteId { get; set; }
        public string WebsiteName { get; set; }
        public string WebsiteColor { get; set; }
        public List<TextDto> Texts { get; set; }
        public List<ImageDto> Images { get; set; }
    }
}
