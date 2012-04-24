using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Oasis.Lib.Services;
using System.Web.Mvc;
using Oasis.Lib.Models;

namespace Oasis.Lib.Controllers
{
    public class AdminContentController : BaseAdminController
    {
        public IContentService ContentService { get; set; }

        public AdminContentController(IContentService contentService)
        {
            ContentService = contentService;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }

        [ValidateInput(false)]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create(string dirName, string pageName, string pageTitle, string pageContent)
        {
            ContentService.AddContent(dirName, pageName, pageTitle, pageContent);

            return View("CreateConfirm");
        }

        public ActionResult List(int? param)
        {
            if (param == null) param = 1;
            AdminContentListModel model = new AdminContentListModel();

            model.Content = ContentService.GetContentByPage(param);
            model.PageNumber = Convert.ToInt32(param);
            model.ContentCount = ContentService.GetTotalContentCount();

            return View(model);
        }

        public ActionResult Edit(int param)
        {
            AdminContentEditModel model = new AdminContentEditModel();
            Content content = ContentService.GetContentById(param);

            model.Content = content;

            return View(model);
        }

        [ValidateInput(false)]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Edit(string dirName, string pageName, string pageTitle, string pageContent, int contentId)
        {
            ContentService.UpdateContent(dirName, pageName, pageTitle, pageContent, contentId);

            return View("EditConfirm");
        }

        public ActionResult Search(string q)
        {
            AdminContentSearchModel model = new AdminContentSearchModel();
            List<Content> searchResults = ContentService.SearchContent(q);

            model.Content = searchResults;

            return View(model);
        }
    }
}
