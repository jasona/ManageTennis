using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Oasis.Lib.Services
{
    public interface IContentService
    {
        Content GetPage(string dirName, string pageName);
        List<Content> GetContentByPage(int? page);
        Content GetContentById(int id);
        int GetTotalContentCount();
        void AddContent(string dirName, string pageName, string pageTitle, string pageContent);
        void UpdateContent(string dirName, string pageName, string pageTitle, string pageContent, int contentId);
        List<Content> SearchContent(string q);
    }
}
