using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dork.Core.Domain
{
    public class ContentBase : EntityBase
    {
        public string MimeType { get; set; }
        public int Size { get; set; }
        public string MediaId { get; set; }
        public string ThumbnailId { get; set; }
    }

    public static class MimeType
    {
        public const string ImageJpeg = "image/jpeg";
        public const string ImagePng = "image/png";
        public const string ImageGif = "image/gif";
        public const string VideoMp4 = "video/mp4";
    } 
}
