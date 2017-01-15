using Dork.Core.Domain.Asset;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dork.Core.Domain.Message
{
    public interface IMessage
    {
        long Id { get; set; }
        IAsset Asset { get; set; }
    }
}
