using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Oasis.Lib.Models
{
    public class AdminEventsRSVPListModel
    {
        public string EventName { get; set; }
        public List<EventRegistration> EventRegistrations { get; set; }
    }
}
