using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dork.Core.Domain.Asset
{
    public class Image : IAsset
    {
        [Required]
        public long Id { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public string Version { get; set; }
        public object Blob { get; set; }
    }
}
