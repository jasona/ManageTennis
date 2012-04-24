using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Oasis.Lib.Services;
using System.Net.Mail;
using Oasis.Lib.Models;
using FlickrNet;
using System.Configuration;

namespace Oasis.Lib.Controllers
{
    public class ContentController : Controller
    {

        public IContentService ContentService { get; private set; }
        public IBlogService BlogService { get; private set; }

        public ContentController(IContentService contentService, IBlogService blogService)
        {
            ContentService = contentService;
            BlogService = blogService;
        }

        //public ActionResult LoadContent(string dirName, string pageName)
        //{
        //    var page = ContentService.GetPage(dirName, pageName);

        //    if (page == null) page = new Content();

        //    if (pageName == "index") return View("Index");
        //    else return View("Content", page);
        //}

        public ActionResult LoadContent(string pageName)
        {
            pageName = pageName.ToLower();

            if (pageName == "index")
            {
                var im = new IndexModel();
                var blm = new BlogListModel();
                var posts = BlogService.GetLatestPosts(5);

                blm.ListTitle = "NEWSFLASH";
                blm.Posts = posts;

                im.BlogList = blm;

                return View(pageName, im);
            }
            else if (pageName == "juniors")
            {
                var jm = new JuniorsModel();
                var blm = new BlogListModel();
                var posts = BlogService.GetPostsByCategory("juniors");

                blm.ListTitle = "JUNIORS UPDATES";
                blm.Posts = posts;

                jm.BlogList = blm;

                return View(pageName, jm);
            }
            else if (pageName == "adults")
            {
                var am = new AdultsModel();
                var blm = new BlogListModel();
                var posts = BlogService.GetPostsByCategory("adults");

                blm.ListTitle = "ADULTS UPDATES";
                blm.Posts = posts;

                am.BlogList = blm;

                return View(pageName, am);
            }
            else if (pageName == "construction")
            {
                var cm = new ConstructionModel();
                var blm = new BlogListModel();
                var posts = BlogService.GetPostsByCategory("construction");

                blm.ListTitle = "CONSTRUCTION UPDATES";
                blm.Posts = posts;

                cm.BlogList = blm;

                return View(pageName, cm);
            }
            else if (pageName == "about-us")
            {
                var am = new AboutUsModel();
                var blm = new BlogListModel();
                var posts = BlogService.GetPostsByCategory("general");

                blm.ListTitle = "CLUB NEWS";
                blm.Posts = posts;

                am.BlogList = blm;

                return View(pageName, am);

            }
            else if (pageName == "gallery")
            {
                Flickr flickr = new Flickr(ConfigurationManager.AppSettings["FlickrKey"], ConfigurationManager.AppSettings["FlickrSecret"]);

                FoundUser user = flickr.PeopleFindByEmail("oasis.tennis@yahoo.com");

                var photos = flickr.PhotosetsGetList(user.UserId);

                foreach (var set in photos)
                {
                    Response.Write(set.Title + "<br>");
                }

                return View(pageName);
            }
            else
            {
                return View(pageName);
            }
        }

        public ActionResult Contact()
        {
            return View();
        }

        public string SubmitContact(string Name, string Email, string Reason, string Message, bool NewsletterSubscribe)
        {
            string message = "Your email has been sent! We will be in touch with you soon!";
            string to = "jason@oasistennis.com";

            
            try
            {
                SmtpClient client = new SmtpClient();
                MailMessage mail = new MailMessage();

                switch (Reason)
                {
                    case "Adult Programs":
                        to = "chill@oasistennis.com";
                        break;
                    case "Junior Programs":
                        to = "rmorant@oasistennis.com";
                        break;
                    case "Oasis Membership":
                        to = "mfeste@oasistennis.com";
                        break;

                }

                mail.To.Add(to);
                mail.Subject = "Email from website: " + Reason;
                mail.IsBodyHtml = true;
                mail.Body = "From: " + Name + " (" + Email + ")<br>Opted in to Newsletter: " + NewsletterSubscribe + "<br><br>Message:<br>" + Message;

                client.Send(mail);
            }
            catch (Exception e)
            {
                message = "I'm sorry, there was an error sending your email. Please email website@oasistennis.com directly for details.<br><br>Exception: " + e.Message;
            }

            return message;
        }

    }
}
