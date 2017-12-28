using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebsiteBuilder.Public.Text;

namespace WebsiteBuilder.Data.Entities
{
    public class WebsiteText : BaseEntity
    {
        public string Text { get; set; }
        public int CoordinateX { get; set; }
        public int CoordinateY { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }
        public string SelectorId { get; set; }
        public int FontSize { get; set; }
        public FontWeightDto FontWeight { get; set; }
        public FontStyleDto FontStyle { get; set; }
        public FontFamilyDto FontFamily { get; set; }
        public string FontColor { get; set; }
        public int WebsiteId { get; set; }
    }
}
