using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebsiteBuilder.Public.Website;

namespace WebsiteBuilder.Public.WebsiteEditor
{
    public class WidgetToRemoveDto
    {
        public int Id { get; set; }
        public WebsiteElementType WebsiteElementType { get; set; }
    }
}
