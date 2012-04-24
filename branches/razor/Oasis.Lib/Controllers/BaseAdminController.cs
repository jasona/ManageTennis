using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Oasis.Lib.Util;
using System.Web;
using Oasis.Lib.Components;

namespace Oasis.Lib.Controllers
{
    public class BaseAdminController : Controller
    {

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            User user = Session["User"] as User;

            if (user == null) { Response.Redirect("/login/?error=" + HttpUtility.UrlEncode("For security reasons, your session has expired. Please log in again to continue.")); return; }
            if (!OasisUtility.CheckBitMask(user.UserAccessBitMask, (int)UserPriviledges.Admin)) Response.Redirect("/login/?error=" + HttpUtility.UrlEncode("You do not have priviledges to access this system, sorry."));

            base.OnActionExecuting(filterContext);
        }

    }
}
