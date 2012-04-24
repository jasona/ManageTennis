using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Oasis.Lib.Models
{
    public class AdminContentListModel
    {
        public List<Content> Content { get; set; }
        public int PageNumber { get; set; }
        public int ContentCount { get; set; }
    }
}
