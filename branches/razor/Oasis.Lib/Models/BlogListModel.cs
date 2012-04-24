using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Oasis.Lib.Models
{
    public class BlogListModel
    {
        public string ListTitle { get; set; }
        public IEnumerable<Post> Posts { get; set; }
    }
}
