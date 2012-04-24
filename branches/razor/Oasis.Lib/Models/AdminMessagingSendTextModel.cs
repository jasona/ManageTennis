using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace Oasis.Lib.Models
{
    public class AdminMessagingSendTextModel
    {
        public List<Group> TextGroups { get; set; }
        public Hashtable GroupCount { get; set; }
    }
}
