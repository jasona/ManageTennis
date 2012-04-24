using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Oasis.Lib.Services;
using Oasis.Lib.Models;

namespace Oasis.Lib.Controllers
{
    public class BlogController : Controller
    {

        public IBlogService BlogService { get; set; }

        public BlogController(IBlogService blogService)
        {
            BlogService = blogService;
        }

        public ActionResult PostList()
        {
            PostListModel plm = new PostListModel();


            var posts = BlogService.GetLatestPosts(5);
            var categories = BlogService.GetCategoryCounts();

            plm.Posts = posts;
            plm.CategorySummaries = categories;

            return View(plm);
        }

        public ActionResult ViewPost(string postTitle)
        {
            var post = BlogService.GetPostByTitle(postTitle);

            return View(post);
        }

        public ActionResult ViewByCategory(string category)
        {
            PostListModel plm = new PostListModel();


            var posts = BlogService.GetPostsByCategory(category);
            var categories = BlogService.GetCategoryCounts();

            plm.Posts = posts;
            plm.CategorySummaries = categories;
            plm.FilteredByCategory = category;

            return View("PostList", plm);
        }

    }
}
