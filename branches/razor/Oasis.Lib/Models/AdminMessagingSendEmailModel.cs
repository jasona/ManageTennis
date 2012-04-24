using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace Oasis.Lib.Models
{
    public class AdminMessagingSendEmailModel
    {
        public List<Group> EmailGroups { get; set; }
        public Hashtable GroupCount { get; set; }
    }
}
