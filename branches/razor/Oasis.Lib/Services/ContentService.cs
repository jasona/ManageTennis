using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Oasis.Lib.Services
{
    public class ContentService : IContentService
    {
        public Content GetPage(string dirName, string pageName)
        {
            Content content;

            using (OasisEntities e = new OasisEntities())
            {
                if (dirName == null)
                    content = e.Contents.Where(c => c.PageName == pageName).FirstOrDefault<Content>();
                else
                    content = e.Contents.Where(c => c.PageName == pageName && c.DirName == dirName).FirstOrDefault<Content>();
            }

            return content;
        }

        public List<Content> GetContentByPage(int? page)
        {
            if (page == null) page = 1;
            int skip = ((Convert.ToInt32(page) - 1) * 10);
            List<Content> content;

            using (OasisEntities o = new OasisEntities())
            {
                content = o.Contents.OrderBy(c => c.Title).Skip(skip).Take(10).ToList<Content>();
            }

            return content;
        }

        public void AddContent(string dirName, string pageName, string pageTitle, string pageContent)
        {
            Content content = new Content();

            using (OasisEntities o = new OasisEntities())
            {
                if (dirName != null) content.DirName = dirName;
                content.PageName = pageName;
                content.Title = pageTitle;
                content.Content1 = pageContent;
                content.CreateDate = DateTime.Now;

                o.Contents.AddObject(content);
                o.SaveChanges();
            }
        }

        public int GetTotalContentCount()
        {
            int count = 0;

            using (OasisEntities o = new OasisEntities())
            {
                count = o.Contents.Count();
            }

            return count;
        }

        public Content GetContentById(int id)
        {
            Content content;

            using (OasisEntities o = new OasisEntities())
            {
                content = o.Contents.Where(c => c.ContentId == id).FirstOrDefault<Content>();
            }

            return content;
        }

        public void UpdateContent(string dirName, string pageName, string pageTitle, string pageContent, int contentId)
        {
            Content content;

            using (OasisEntities o = new OasisEntities())
            {
                content = o.Contents.Where(c => c.ContentId == contentId).FirstOrDefault<Content>();

                if (content != null)
                {
                    if (dirName != null) content.DirName = dirName;
                    content.PageName = pageName;
                    content.Title = pageTitle;
                    content.Content1 = pageContent;

                    o.SaveChanges();
                }
            }

        }


        public List<Content> SearchContent(string q)
        {
            List<Content> content;

            using (OasisEntities o = new OasisEntities())
            {
                content = o.Contents.Where(c => c.Title.ToLower().Contains(q) || c.Content1.Contains(q)).ToList<Content>();
            }

            return content;
        }
    }
}
