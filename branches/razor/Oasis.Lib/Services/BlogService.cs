using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Oasis.Lib.Models;

namespace Oasis.Lib.Services
{
    public class BlogService : IBlogService
    {
        public Post GetPostByTitle(string postTitle)
        {
            Post post;

            using (OasisEntities e = new OasisEntities())
            {
                post = e.Posts.Where(p => p.TitleEncoded == postTitle).FirstOrDefault<Post>();
            }

            return post;
        }


        public List<Post> GetLatestPosts(int count)
        {
            List<Post> posts;

            using (OasisEntities e = new OasisEntities())
            {
                posts = e.Posts.OrderByDescending(p => p.PostDate).Take(count).ToList<Post>();
            }

            return posts;
        }




        public List<Post> GetPostsByCategory(string category)
        {
            List<Post> posts;

            using (OasisEntities e = new OasisEntities())
            {
                posts = e.Posts.Where(p => p.Category == category).ToList<Post>();
            }

            
            return posts;
        }


        public List<PostCategorySummaryModel> GetCategoryCounts()
        {
            List<PostCategorySummaryModel> categories = new List<PostCategorySummaryModel>();

            using (OasisEntities e = new OasisEntities())
            {
                categories = e.Posts.GroupBy(p => p.Category).Select(p => new PostCategorySummaryModel() { Category = p.Key, PostCount = p.Count() }).ToList<PostCategorySummaryModel>();
            }

            return categories;
        }
    }
}
