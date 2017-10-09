using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebsiteBuilder.Data;

namespace WebsiteBuilder.BusinessLogic.Infrastructure
{
    public abstract class QueryBase
    {
        public WebsiteBuilderDbContext Db { get; set; }
    }
}
