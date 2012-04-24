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
    public class AdminLoginController : Controller
    {
        public ActionResult Login()
        {
            return View();
        }

        [ActionName("validate-admin")]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult ValidateAdmin(string emailAddress, string password)
        {
            User user = null;
            bool validLogin = false;

            using (OasisEntities o = new OasisEntities())
            {
                user = o.Users.Where(u => u.EmailAddress == emailAddress && u.Password == password).FirstOrDefault<User>();

                if (user != null && OasisUtility.CheckBitMask((int)user.UserAccessBitMask, (int)UserPriviledges.Admin))
                {
                    validLogin = true;
                }

                if (!validLogin)
                {
                    return new RedirectResult(string.Format("/login/?error={0}", HttpUtility.UrlEncode("Invalid email address, password or priviledges to admin console. Please try again.")));
                }
                else
                {
                    Session["User"] = user;

                    return new RedirectResult("/");
                }
            }

        }
    }
}
