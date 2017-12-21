using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebsiteBuilder.Data.Entities
{
    public class WebsiteText : BaseEntity
    {
        public string Text { get; set; }
        public int CoordinateX { get; set; }
        public int CoordinateY { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }
        public int WebsiteId { get; set; }
    }
}
