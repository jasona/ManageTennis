using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Oasis.Lib.Models
{
    public class AdminMembersListModel
    {
        public List<User> Users { get; set; }
        public int PageNumber { get; set; }
        public int UserCount { get; set; }
    }
}
