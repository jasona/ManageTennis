using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OasisClub.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /About/

        public ActionResult About()
        {
            return View();
        }

        //
        // GET: /Staff/

        public ActionResult Staff()
        {
            return View();
        }
        
        //
        // GET: /Construction/

        public ActionResult Construction()
        {
            return View();
        }

        //
        // GET: /C4Academy/

        public ActionResult C4Academy()
        {
            return View();
        }

        //
        // GET: /Membership/

        public ActionResult Membership()
        {
            return View();
        }

        //
        // GET: /Members/

        public ActionResult Members()
        {
            return View();
        }

        //
        // GET: /Calendar/

        public ActionResult Calendar()
        {
            return View();
        }

        //
        // GET: /FAQ/

        public ActionResult FAQ()
        {
            return View();
        }

        //
        // GET: /Contact/

        public ActionResult Contact()
        {
            return View();
        }

        public void SubmitContact(string Name, string Email, string Subject, string Message)
        {
            // TODO: Contact Submit Logic
        }

        //
        // GET: /Policy/

        public ActionResult Policy()
        {
            return View();
        }
    }
}
