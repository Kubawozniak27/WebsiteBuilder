using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebsiteBuilder.Public.NavigationBar
{
    public class NavigationBarDto
    {
        public int? NavigationId { get; set; }
        public int WebsiteId { get; set; }
        public string SectionName { get; set; }
        public string NavigationName { get; set; }
        public string NavigationBarStyle { get; set; }
    }
}
