using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebsiteBuilder.Public.Text;
using WebsiteBuilder.Public.Website;

namespace WebsiteBuilder.Public.WebsiteEditor
{
    public class WebsiteContentDto
    {
        public int? Id { get; set; }
        public WebsiteElementType  WebsiteElementType { get; set; }
        public int FontSize { get; set; }
        public FontWeightDto FontWeight { get; set; }
        public FontStyleDto FontStyle { get; set; }
        public FontFamilyDto FontFamily { get; set; }
        public string FontColor { get; set; }
        public string ImageSrc { get; set; }
        public int ImageId { get; set; }
        public string Text { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public string SelectorId { get; set; }
    }
}
