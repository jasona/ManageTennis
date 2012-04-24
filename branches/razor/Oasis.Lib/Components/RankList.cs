using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DMag.Components;

namespace Oasis.Lib.Components
{
    public class RankList
    {        
        private static RankList _rankList;

        private RankList() 
        {
        }

        public static RankList Instance()
        {
            if (_rankList == null) _rankList = new RankList();

            return _rankList;
        }

        public List<Rank> Ranks { get; set; }

    }
}
