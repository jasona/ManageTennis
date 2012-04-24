using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Oasis.Lib.Models
{
    public class AdminEventsListModel
    {
        public List<Event> Events { get; set; }
        public int PageNumber { get; set; }
        public int EventCount { get; set; }
    }
}
