using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebsiteBuilder.Data.Entities
{
    public class Website : BaseEntity
    {
        public string Name { get; set; }
        public DateTime PublishDate { get; set; }
        public DateTime EditDate { get; set; }
        public string Color { get; set; }
        public virtual List<WebsiteText> WebsiteTexts { get; set; }
        public virtual List<Image> Images { get; set; }
        public virtual List<WebsiteNavigationBar> WebsiteNavigationBars { get; set; }
    }
}
