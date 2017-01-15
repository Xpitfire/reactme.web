using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dork.Core.Domain
{
    public class Actor : MessageBase
    {
        public override IAsset Asset { get; set; }
    }
}
