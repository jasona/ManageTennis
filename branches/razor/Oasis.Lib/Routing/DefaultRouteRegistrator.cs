using MvcTurbine.Routing;
using System.Web.Routing;
using System.Web.Mvc;
using System.Configuration;
using System;

namespace Oasis.Lib.Routing
{
    public class DefaultRouteRegistrator : IRouteRegistrator
    {

        public void Register(RouteCollection routes)
        {

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            if (ConfigurationManager.AppSettings["SiteType"] == "admin")
            {

                routes.MapRoute(
                "AdminMessaging",
                "messaging/{action}/{param}",
                new { controller = "AdminMessaging", action = "Index", param = UrlParameter.Optional }
                );

                routes.MapRoute(
                    "AdminContent",
                    "site-content/{action}/{param}",
                    new { controller = "AdminContent", action = "Index", param = UrlParameter.Optional }
                    );

                routes.MapRoute(
                    "AdminEvents",
                    "events/{action}/{param}",
                    new { controller = "AdminEvents", action = "Index", param = UrlParameter.Optional }
                    );

                routes.MapRoute(
                    "AdminMembers",
                    "members/{action}/{param}",
                    new { controller = "AdminMembers", action = "Index", param = UrlParameter.Optional }
                    );

                routes.MapRoute(
                    "Dashboard",
                    "",
                    new { controller = "AdminGeneral", action = "Dashboard" }
                    );

                routes.MapRoute(
                    "AdminLoginPage",
                    "login/{action}",
                    new { controller = "AdminLogin", action = "Login" }
                    );

                routes.MapRoute(
                    "AdminPageContentRoute",
                    "content/pages/{action}/{id}",
                    new { controller = "AdminPage", action = "List", id = UrlParameter.Optional }
                    );

            }
            else if (ConfigurationManager.AppSettings["SiteType"] == "secure")
            {

                routes.MapRoute(
                    "MakePayment",
                    "make-payment/{action}",
                    new { controller = "Payment", action = "DisplayForm" }
                    );

                routes.MapRoute(
                    "InvoiceAction",
                    "invoice/{action}/{id}",
                    new { controller = "InvoicePayment", action = "display", id = UrlParameter.Optional }
                    );

            }
            else if (ConfigurationManager.AppSettings["SiteType"] == "web")
            {

                routes.MapRoute(
                    "EventsRoute",
                    "events/{action}/{param}",
                    new { controller = "Events", action = "List", param = UrlParameter.Optional }
                    );

                routes.MapRoute(
                    "UserRoute",
                    "user/{action}",
                    new { controller = "User", action = "Index" }
                    );

                routes.MapRoute(
                    "Contact_SubmitContact_Route",
                    "api/SubmitContact",
                    new { controller = "Content", action = "SubmitContact" }
                    );

                routes.MapRoute(
                    "ContactRoute",
                    "contact/",
                    new { controller = "Content", action = "Contact" }
                    );

                routes.MapRoute(
                    "GalleryRoute",
                    "gallery",
                    new { controller = "Gallery", action = "Index" }
                    );

                routes.MapRoute(
                    "GalleryPhotosetRoute",
                    "gallery/{photoSetName}",
                    new { controller = "Gallery", action = "ViewPhotoSet" }
                    );

                routes.MapRoute(
                    "GalleryPhotoRoute",
                    "gallery/{photoSetName}/{photoId}",
                    new { controller = "Gallery", action = "ViewPhoto" }
                    );


                routes.MapRoute(
                    "BlogListRoute",
                    "blog",
                    new { controller = "Blog", action = "PostList" }
                    );

                routes.MapRoute(
                    "BlogRoute",
                    "blog/{postTitle}",
                    new { controller = "Blog", action = "ViewPost", postTitle = UrlParameter.Optional }
                    );

                routes.MapRoute(
                    "BlogCategoryRoute",
                    "blog/categories/{category}",
                    new { controller = "Blog", action = "ViewByCategory" }
                    );

                routes.MapRoute(
                    "HomeRoutes",
                    "{pageName}",
                    new { controller = "Content", action = "LoadContent", dirName = UrlParameter.Optional, pageName = "index" }
                    );

                routes.MapRoute(
                    "SubdirRoutes",
                    "{dirName}/{pageName}",
                    new { controller = "Content", action = "LoadContent", dirName = UrlParameter.Optional, pageName = "index" }
                    );
            }
            else
            {
                throw new Exception("SiteType is not set in the web.config to a valid value.");
            }

        }
    }
}
