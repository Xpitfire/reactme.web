using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dork.Core.Domain
{
    public class Note : EntityBase
    {
        public string Text { get; set; }
        public Rank Rank { get; set; } = Rank.Unranked;
    }

    public enum Rank
    {
        Unranked = 0,

        Awesome = 5,
        Nice = 4,
        Okay = 3,
        Meh = 2,
        Awful = 1
    }
}
