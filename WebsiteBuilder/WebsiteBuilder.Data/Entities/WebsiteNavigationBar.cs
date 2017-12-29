using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebsiteBuilder.Data.Entities
{
    public class WebsiteNavigationBar : BaseEntity
    {
        public int WebsiteId { get; set; }
        public virtual Website Website { get; set; }
        public string SectionName { get; set; }
        public string NavigationName { get; set; }
        public string NavigationBarStyle { get; set; }
    }
}
