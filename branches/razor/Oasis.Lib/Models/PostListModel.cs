using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Oasis.Lib.Models
{
    public class PostListModel
    {
        public IEnumerable<Post> Posts { get; set; }
        public IEnumerable<PostCategorySummaryModel> CategorySummaries { get; set; }
        public string FilteredByCategory { get; set; }
    }
}
