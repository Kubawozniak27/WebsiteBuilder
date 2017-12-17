using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebsiteBuilder.Public.Website
{
    public class WebsiteDto
    {
        public int WebsiteId { get; set; }
        public string Name { get; set; }
        public DateTime PublishDate { get; set; }
        public DateTime EditDate { get; set; }
        public string PublishDateShortDate
        {
            get { return PublishDate.ToShortDateString(); }
            set { Convert.ToDateTime(value); }
        }
        public string EditDateShortDate
        {
            get { return EditDate.ToShortDateString(); }
            set { Convert.ToDateTime(value); }
        }
    }
}
