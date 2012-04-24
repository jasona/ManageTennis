using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Oasis.Lib.Components;
using Oasis.Lib.Util;
using DMag.Components;
using System.Data.Objects;

namespace Oasis.Lib.Services
{
    public class EventService : IEventService
    {

        public List<Event> GetEventsByPage(int? page)
        {
            if (page == null) page = 1;
            List<Event> events;
            int skip = ((Convert.ToInt32(page) - 1) * 10);

            using (OasisEntities o = new OasisEntities())
            {
                events = o.Events.OrderBy(e => e.EventDate).Skip(skip).Take(10).ToList<Event>();
            }

            return events;
        }

        public int GetTotalEventsCount()
        {
            using (OasisEntities o = new OasisEntities())
            {
                int count = o.Events.Count();

                return count;
            }
        }

        public void CreateEvent(string title, string description)
        {
            Event newEvent = new Event();

            newEvent.Title = title;
            newEvent.Description = description;
        }

        public void CreateEvent(string eventTitle, string eventDescription, string memberCost, string nonMemberCost, string eventDate, string eventTime,
            int[] rank, int[] demographic, int eventType, bool ustaSanctioned, bool privateEvent, int proUserId)
        {
            Event newEvent = new Event();

            using (OasisEntities o = new OasisEntities())
            {
                decimal memberCostParsed;
                decimal nonMemberCostParsed;
                DateTime eventDateParsed;
                int rankBitMask = 0;
                int demographicBitMask = 0;

 
                newEvent.Title = eventTitle;
                newEvent.Description = eventDescription;

                if (decimal.TryParse(memberCost, out memberCostParsed))
                    newEvent.MemberCost = memberCostParsed;

                if (decimal.TryParse(nonMemberCost, out nonMemberCostParsed))
                    newEvent.NonMemberCost = nonMemberCostParsed;

                if (DateTime.TryParse(eventDate + " " + eventTime, out eventDateParsed))
                    newEvent.EventDate = eventDateParsed;

                foreach(int rankBit in rank)
                {
                    rankBitMask = rankBitMask | rankBit;
                }
                newEvent.RankingBitMask = rankBitMask;

                foreach(int demoBit in demographic)
                {
                    demographicBitMask = demographicBitMask | demoBit;
                }
                newEvent.DemographicBitMask = demographicBitMask;

                newEvent.EncodedTitle = OasisUtility.EncodeString(eventTitle + " " + eventDateParsed.ToShortDateString());
                newEvent.EventTypeId = eventType;
                newEvent.USTASanctioned = ustaSanctioned;
                newEvent.PrivateEvent = privateEvent;
                newEvent.ProUserId = proUserId;

                o.Events.AddObject(newEvent);
                o.SaveChanges();
            }

        }

        public List<User> GetAvailablePros()
        {

            return OCache<List<User>>.Get("Oasis.AvailablePros",
                CacheOptions.MediumRefresh,
                () =>
                {
                    List<User> pros = new List<User>();

                    using (OasisEntities o = new OasisEntities())
                    {
                        List<User> users = o.Users.ToList<User>();

                        foreach (User user in users)
                        {
                            if (OasisUtility.CheckBitMask(user.UserAccessBitMask, (int)UserPriviledges.Pro))
                                pros.Add(user);
                        }
                     }

                    return pros;
                });

        }

        public Event GetEventById(int param)
        {
            Event foundEvent;

            using (OasisEntities o = new OasisEntities())
            {
                foundEvent = o.Events.Where(e => e.EventId == param).FirstOrDefault<Event>();
            }

            return foundEvent;
        }

        public void UpdateEvent(string eventTitle, string eventDescription, string memberCost, string nonMemberCost, string eventDate, string eventTime, int[] rank, int[] demographic, int eventType, bool ustaSanctioned, bool privateEvent, int proUserId, int eventId)
        {
            Event updateEvent;

            using (OasisEntities o = new OasisEntities())
            {
                decimal memberCostParsed;
                decimal nonMemberCostParsed;
                DateTime eventDateParsed;
                int rankBitMask = 0;
                int demographicBitMask = 0;

                updateEvent = o.Events.Where(e => e.EventId == eventId).FirstOrDefault<Event>();

                updateEvent.Title = eventTitle;
                updateEvent.Description = eventDescription;

                if (decimal.TryParse(memberCost, out memberCostParsed))
                    updateEvent.MemberCost = memberCostParsed;

                if (decimal.TryParse(nonMemberCost, out nonMemberCostParsed))
                    updateEvent.NonMemberCost = nonMemberCostParsed;

                if (DateTime.TryParse(eventDate + " " + eventTime, out eventDateParsed))
                    updateEvent.EventDate = eventDateParsed;

                foreach (int rankBit in rank)
                {
                    rankBitMask = rankBitMask | rankBit;
                }
                updateEvent.RankingBitMask = rankBitMask;

                foreach (int demoBit in demographic)
                {
                    demographicBitMask = demographicBitMask | demoBit;
                }
                updateEvent.DemographicBitMask = demographicBitMask;

                updateEvent.EncodedTitle = OasisUtility.EncodeString(eventTitle + " " + eventDateParsed.ToShortDateString());
                updateEvent.EventTypeId = eventType;
                updateEvent.USTASanctioned = ustaSanctioned;
                updateEvent.PrivateEvent = privateEvent;
                updateEvent.ProUserId = proUserId;

                o.SaveChanges();
            }
        }

        public List<Event> GetLatestEvents(int? eventTypeId)
        {
            List<Event> events;

            using (OasisEntities o = new OasisEntities())
            {
                if (eventTypeId != null)
                {
                    events = o.Events.Where(e => e.EventTypeId == eventTypeId && e.EventDate >= DateTime.Now).OrderBy(e => e.EventDate).Take(10).ToList<Event>();
                }
                else
                {
                    events = o.Events.Where(e => e.EventDate >= DateTime.Now).OrderBy(e => e.EventDate).Take(10).ToList<Event>();
                }
            }

            return events;
        }

        public Event GetEventByTitle(string param)
        {
            Event eve;

            using (OasisEntities o = new OasisEntities())
            {
                eve = o.Events.Where(e => e.EncodedTitle == param).FirstOrDefault<Event>();
            }

            return eve;
        }

        public void RegisterForEvent(int eventId, int userId)
        {

            using (OasisEntities o = new OasisEntities())
            {
                EventRegistration registration = new EventRegistration();

                registration.EventId = eventId;
                registration.UserId = userId;
                registration.RegistrationDate = DateTime.Now;

                o.EventRegistrations.AddObject(registration);
                o.SaveChanges();
            }

        }

        public bool CheckEventRegistration(int eventId, int userId)
        {
            bool found = false;

            using (OasisEntities o = new OasisEntities())
            {
                var registration = o.EventRegistrations.Where(e => e.EventId == eventId && e.UserId == userId).FirstOrDefault<EventRegistration>();

                found = (registration != null ? true : false);
            }

            return found;
        }

        public int GetRSVPCount(int eventId)
        {
            int rsvpCount = 0;

            using (OasisEntities o = new OasisEntities())
            {
                rsvpCount = o.EventRegistrations.Where(e => e.EventId == eventId).Count();
            }

            return rsvpCount;
        }

        public List<EventRegistration> GetRegistrationList(int eventId)
        {
            List<EventRegistration> registrations = new List<EventRegistration>();

            using (OasisEntities o = new OasisEntities())
            {
                IEnumerable<EventRegistration> regQuery = o.EventRegistrations.Include("User").Include("Event").Where(er => er.EventId == eventId).OrderBy(er => er.RegistrationDate);
                
                ((ObjectQuery)regQuery).MergeOption = MergeOption.NoTracking;
                
                registrations = regQuery.ToList<EventRegistration>();
            }

            return registrations;
        }

    }
}
