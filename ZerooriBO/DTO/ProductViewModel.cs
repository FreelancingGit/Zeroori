using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZerooriBO
{
    public class ProductViewModel
    {
        public string ProductName { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Nullable<int> Price { get; set; }
        public Nullable<int> ImageId { get; set; }
        public HttpPostedFileWrapper ImageFile { get; set; }
    }
}