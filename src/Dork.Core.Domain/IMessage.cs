using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dork.Core.Domain
{
    public interface IMessage
    {
        IAsset Asset { get; set; }
    }
}
