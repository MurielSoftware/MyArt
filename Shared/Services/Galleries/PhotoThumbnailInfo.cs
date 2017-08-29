using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Services.Galleries
{
    public class PhotoThumbnailInfo
    {
        public virtual int MinimumWidth { get; private set; }
        public virtual int MinimumHeight { get; private set; }
        public virtual string Path { get; private set; }
        public virtual int MaximumPhotos { get; private set; }

        public PhotoThumbnailInfo(int minimumWidth, int minimumHeight, string path, int maximumPhotos)
        {
            MinimumWidth = minimumWidth;
            MinimumHeight = minimumHeight;
            Path = path;
            MaximumPhotos = maximumPhotos;
        }
    }
}
