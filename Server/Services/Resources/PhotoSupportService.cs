using Shared.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Core.Context;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using Server.Daos;
using Shared.Core.Dtos;
using System.IO;

namespace Server.Services.Resources
{
    public class PhotoSupportService : BaseService
    {
        private GenericDao _genericDao;

        public PhotoSupportService(IUnitOfWork unitOfWork) 
            : base(unitOfWork)
        {
            _genericDao = new GenericDao(unitOfWork);
        }

        public void Crop(PhotoCropDto photoCropDto)
        {
            Image sourceImage;
            using (FileStream sourceImageStream = new FileStream(photoCropDto.Path, FileMode.Open, FileAccess.ReadWrite))
            {
                sourceImage = Image.FromStream(sourceImageStream);
                sourceImage = CropImage(sourceImage, photoCropDto.PositionX, photoCropDto.PositionY, photoCropDto.Width, photoCropDto.Height);               
            }
            SaveImage(sourceImage, photoCropDto.Path);
            //}
        }

        public Image Resize(Image image, int width, int height)
        {
            Rectangle rectangle = new Rectangle(0, 0, width, height);
            Bitmap bitmap = new Bitmap(width, height);
            bitmap.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (Graphics graphics = Graphics.FromImage(bitmap))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (ImageAttributes imageAttributes = new ImageAttributes())
                {
                    imageAttributes.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, rectangle, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, imageAttributes);
                }
            }

            return bitmap;
        }

        private Image CropImage(Image image, int x, int y, int w, int h)
        {
            Bitmap target = new Bitmap(w, h);
            using (Graphics graphics = Graphics.FromImage(target))
            {
                graphics.DrawImage(image, new Rectangle(0, 0, w, h), new Rectangle(x, y, w, h), GraphicsUnit.Pixel);
            }
            return target;
        }

        private void SaveImage(Image image, string path)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                image.Save(ms, ImageFormat.Jpeg);

                using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate, FileAccess.ReadWrite))
                {
                    ms.WriteTo(fs);
                }
            }
        }
    }
}
