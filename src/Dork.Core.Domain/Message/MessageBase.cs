using Dork.Core.Domain.Asset;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dork.Core.Domain.Message
{
    public abstract class MessageBase : IMessage
    {
        public long Id { get; set; }
        public IAsset Asset { get; set; }
    }
}
