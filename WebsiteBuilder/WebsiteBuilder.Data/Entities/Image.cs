using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebsiteBuilder.Data.Entities
{
    public class Image : BaseEntity
    {
        public string ImagePath { get; set; }
        public string ImageName { get; set; }
        public int WebsiteId { get; set; }
        public virtual List<WebsiteImage> WebsiteImages { get; set; }
        public virtual Website Website { get; set; }
    }
}
