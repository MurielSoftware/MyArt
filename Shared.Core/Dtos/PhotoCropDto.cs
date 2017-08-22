using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Core.Dtos
{
    public class PhotoCropDto : BaseDto
    {
        public virtual int PositionX { get; set; }
        public virtual int PositionY { get; set; }
        public virtual int Width { get; set; }
        public virtual int Height { get; set; }
        public virtual string Path { get; set; }
    }
}
