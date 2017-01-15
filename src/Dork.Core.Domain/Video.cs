using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dork.Core.Domain
{
    public class Video : IAsset
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public string Version { get; set; }
        public object Blob { get; set; }
    }
}
