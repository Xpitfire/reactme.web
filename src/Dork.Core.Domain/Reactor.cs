using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dork.Core.Domain
{
    public class Reactor : MessageBase
    {
        public override IAsset Asset { get; set; }
    }
}
