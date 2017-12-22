﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace WebsiteBuilder.Public.Image
{
    public class ImageDto
    {
        public int ImageId { get; set; }
        public string Title { get; set; }
        public string FilePath { get; set; }
        public int WebsiteId { get; set; }
        public bool IsDeleted { get; set; }
        public HttpPostedFileBase ImageFile { get; set; }
    }
}
