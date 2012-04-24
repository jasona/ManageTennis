using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FlickrNet;
using System.Text.RegularExpressions;
using System.Web;

namespace Oasis.Lib.Util
{
    public static class ExtensionMethods
    {

        public static string ToFriendlyUrl(this Photoset set)
        {
            string title = set.Title.ToLower();

            // invalid chars, make into spaces
            title = Regex.Replace(title, @"[^a-z0-9\s-]", " ");
            // convert multiple spaces/hyphens into one space       
            title = Regex.Replace(title, @"[\s-]+", " ").Trim();
            // cut and trim it
            //title = title.Substring(0, title.Length <= maxLength ? title.Length : maxLength).Trim();
            // hyphens
            title = Regex.Replace(title, @"\s", "-");

            return title;
        }

        public static bool HasFile(this HttpPostedFileBase file)
        {
            return (file != null && file.ContentLength > 0) ? true : false;
        }

    }
}
