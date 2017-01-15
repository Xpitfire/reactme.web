using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dork.Core.Domain.Asset
{
    public interface IAsset
    {
        [Required]
        long Id { get; set; }
        string Name { get; set; }
        string Path { get; set; }
        string Version { get; set; }
        object Blob { get; set; }
    }
}
