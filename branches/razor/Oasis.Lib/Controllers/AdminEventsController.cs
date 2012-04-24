using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Oasis.Lib.Services;
using System.Web.Mvc;
using Oasis.Lib.Models;

namespace Oasis.Lib.Controllers
{
    public class AdminEventsController : BaseAdminController
    {
        IEventService EventService { get; set; }

        public AdminEventsController(IEventService eventService)
        {
            EventService = eventService;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create()
        {
            AdminEventsCreateModel model = new AdminEventsCreateModel();
            List<User> pros = EventService.GetAvailablePros();

            model.AvailablePros = pros;

            return View(model);
        }

        [ValidateInput(false)]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create(string eventTitle, string eventDescription, string memberCost, string nonMemberCost, string eventDate, string eventTime,
            int[] rank, int[] demographic, int eventType, bool ustaSanctioned, bool privateEvent, int proUserId)
        {
            EventService.CreateEvent(eventTitle, eventDescription, memberCost, nonMemberCost, eventDate, eventTime, rank, demographic, eventType, ustaSanctioned, privateEvent, proUserId);

            return View("CreateConfirm");
        }

        public ActionResult List(int? param)
        {
            if (param == null) param = 1;
            AdminEventsListModel model = new AdminEventsListModel();

            model.Events = EventService.GetEventsByPage(param);
            model.PageNumber = Convert.ToInt32(param);
            model.EventCount = EventService.GetTotalEventsCount();

            return View(model);
        }

        public ActionResult Edit(int param)
        {
            AdminEventsEditModel model = new AdminEventsEditModel();
            Event foundEvent = EventService.GetEventById(param);

            model.Event = foundEvent;
            model.AvailablePros = EventService.GetAvailablePros();

            return View(model);
        }

        [ValidateInput(false)]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Edit(string eventTitle, string eventDescription, string memberCost, string nonMemberCost, string eventDate, string eventTime,
            int[] rank, int[] demographic, int eventType, bool ustaSanctioned, bool privateEvent, int proUserId, int eventId)
        {
            EventService.UpdateEvent(eventTitle, eventDescription, memberCost, nonMemberCost, eventDate, eventTime, rank, demographic, eventType,
                ustaSanctioned, privateEvent, proUserId, eventId);

            return View("EditConfirm");
        }

        [ActionName("view-rsvps")]
        public ActionResult ViewRSVPs(int param)
        {
            AdminEventsRSVPListModel model = new AdminEventsRSVPListModel();

            model.EventRegistrations = EventService.GetRegistrationList(param);
            model.EventName = Request.QueryString["name"];

            return View(model);
        }

    }
}
