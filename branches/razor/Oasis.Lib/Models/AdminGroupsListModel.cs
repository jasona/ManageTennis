using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace Oasis.Lib.Models
{
    public class AdminGroupsListModel
    {
        public List<Group> Groups { get; set; }
        public Hashtable NumberOfMembers { get; set; }
    }
}
