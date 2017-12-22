using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebsiteBuilder.Public.Website;

namespace WebsiteBuilder.Public.WebsiteEditor
{
    public class WebsiteContentDto
    {
        public int? Id { get; set; }
        public WebsiteElementType  WebsiteElementType { get; set; }
        public string ImageSrc { get; set; }
        public string Text { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
    }
}
