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
        public DateTime? PublishDate { get; set; }
    }
}
