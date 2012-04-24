using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Oasis.Lib.Services
{
    public interface IEventService
    {
        List<Event> GetEventsByPage(int? page);
        List<Event> GetLatestEvents(int? eventTypeId);
        Event GetEventById(int param);
        Event GetEventByTitle(string param);
        int GetTotalEventsCount();
        void RegisterForEvent(int eventId, int userId);
        int GetRSVPCount(int eventId);
        bool CheckEventRegistration(int eventId, int userId);
        List<EventRegistration> GetRegistrationList(int eventId);
        void CreateEvent(string eventTitle, string eventDescription, string memberCost, string nonMemberCost, string eventDate, string eventTime,
            int[] rank, int[] demographic, int eventType, bool ustaSanctioned, bool privateEvent, int proUserId);
        void UpdateEvent(string eventTitle, string eventDescription, string memberCost, string nonMemberCost, string eventDate, string eventTime,
            int[] rank, int[] demographic, int eventType, bool ustaSanctioned, bool privateEvent, int proUserId, int eventId);
        List<User> GetAvailablePros();
    }
}
