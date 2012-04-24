using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Oasis.Lib.Components
{
    public enum UserPriviledges : int
    {
        User = 1,
        Member = 2,
        Pro = 4,
        Employee = 8,
        Admin = 16
    }
}
