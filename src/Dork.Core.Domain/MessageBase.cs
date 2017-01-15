using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dork.Core.Domain
{
    public abstract class MessageBase : IMessage
    {
        public abstract IAsset Asset { get; set; }


    }
}
