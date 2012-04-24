using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Oasis.Lib.Components
{
    [Flags()]
    public enum CacheOptions
    {
        NotSet = 0x0000,
        Short = 0x0001,
        Normal = 0x0002,
        Long = 0x0004,
        Permanent = 0x008,
        NoRefresh = 0x0010,
        ImmediateRefresh = 0x0020,
        ShortRefresh = 0x0040,
        MediumRefresh = 0x0080,
        LongRefresh = 0x0100,
        LongestRefresh = 0x0200,
        PersistToFile = 0x0400, 
        VeryShort = 0x0800
    }
}
