using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebsiteBuilder.Data.Entities
{
    public class WebsiteImage:BaseEntity
    {
        public string Title { get; set; }
        public string FilePath { get; set; }
        public int WebsiteId { get; set; }
        public virtual Website Website { get; set; }
    }
}
