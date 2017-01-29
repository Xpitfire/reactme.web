using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dork.Core.Domain
{
    public abstract class MessageBase : EntityBase
    {
        public Content Content { get; set; }
        public string SenderId { get; set; }
        public IList<string> Tags { get; set; }
        public long DateCreated { get; set; }
        public long DateReceived { get; set; }
        public long DateViewed { get; set; }
    }
}
