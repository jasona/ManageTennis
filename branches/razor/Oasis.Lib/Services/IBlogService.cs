using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Oasis.Lib.Models;

namespace Oasis.Lib.Services
{
    public interface IBlogService
    {
        Post GetPostByTitle(string postTitle);
        List<Post> GetLatestPosts(int count);
        List<Post> GetPostsByCategory(string category);
        List<PostCategorySummaryModel> GetCategoryCounts();
    }
}
