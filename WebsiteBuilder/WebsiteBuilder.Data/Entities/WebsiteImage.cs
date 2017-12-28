using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebsiteBuilder.Data.Entities
{
    public class WebsiteImage : BaseEntity
    {
        public int CoordinateX { get; set; }
        public int CoordinateY { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }
        public string SelectorId { get; set; }
        public int ImageId { get; set; }
        public virtual Image Image { get; set; }
    }
}
