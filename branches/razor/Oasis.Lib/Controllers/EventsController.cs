using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Oasis.Lib.Services;
using Oasis.Lib.Models;

namespace Oasis.Lib.Controllers
{
    public class EventsController : Controller
    {
        public IEventService EventService { get; set; }

        public EventsController(IEventService eventService)
        {
            EventService = eventService;
        }

        public ActionResult List()
        {
            EventsListModel model = new EventsListModel();

            model.Events = EventService.GetLatestEvents(null);

            return View(model);
        }

        public ActionResult V(string param)
        {
            EventsViewModel model = new EventsViewModel();

            model.Event = EventService.GetEventByTitle(param);

            return View("View", model);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Register()
        {
            int eventId = Convert.ToInt32(Request.Form["eventId"].ToString());
            int userId = Convert.ToInt32(Request.Form["userId"].ToString());
            var user = Session["User"] as User;

            if (user == null) return new RedirectResult("/user/?redirect=" + Server.UrlEncode(Request.ServerVariables["SCRIPT_NAME"]));

            return View();
        }

        [ActionName("register-confirm")]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult RegisterConfirm()
        {
            RegisterConfirmModel model = new RegisterConfirmModel();

            int eventId = Convert.ToInt32(Request.Form["eventId"].ToString());
            int userId = Convert.ToInt32(Request.Form["userId"].ToString());

            EventService.RegisterForEvent(eventId, userId);

            model.EventId = eventId;
            model.UserId = userId;

            return View(model);
        }

    }
}
