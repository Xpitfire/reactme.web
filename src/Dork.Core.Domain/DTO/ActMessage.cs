using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dork.Core.Domain
{
    public class ActMessage : MessageBase
    {
        public IList<User> Receivers { get; set; }
        public string Description { get; set; }
    }
}
