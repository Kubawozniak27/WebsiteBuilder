using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebsiteBuilder.Public.Image;
using WebsiteBuilder.Public.NavigationBar;
using WebsiteBuilder.Public.Text;

namespace WebsiteBuilder.Public.WebsiteEditor
{
    public class SaveWebsiteEditorDto
    {
        public int WebsiteId { get; set; }
        public string WebsiteName { get; set; }
        public string WebsiteColor { get; set; }
        public ICollection<WidgetToRemoveDto> WidgetsToRemove { get; set; }
        public List<WebsiteContentDto> WebsiteContents { get; set; }
        public List<NavigationBarDto> NavigationBars { get; set; }
        public List<TextDto> Texts { get; set; }
        public List<ImageDto> Images { get; set; }
        public List<NavigationBarDto> NavigationBarsToRemove { get; set; }
        public bool IsDeleteNavigationArrayFromServer { get; set; }
        public string NavigationBarStyle { get; set; }
    }
}
