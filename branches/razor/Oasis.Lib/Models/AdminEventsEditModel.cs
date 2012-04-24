using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Oasis.Lib.Models
{
    public class AdminEventsEditModel
    {
        public Event Event { get; set; }
        public List<User> AvailablePros { get; set; }
    }
}
