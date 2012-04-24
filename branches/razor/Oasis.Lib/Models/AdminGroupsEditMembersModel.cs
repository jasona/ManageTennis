using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Oasis.Lib.Models
{
    public class AdminGroupsEditMembersModel
    {
        public int GroupId { get; set; }
        public List<User> GroupMembers { get; set; }
        public List<User> NotGroupMembers { get; set; }
    }
}
