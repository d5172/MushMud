using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Drawing.Drawing2D;

namespace MusicCompany.Core.Services.ImageProcessing
{
	internal class ImageProcessingService : IImageProcessingService
	{
		public Size GetSize(Stream imageStream)
		{
			//using (Image img = Image.FromStream(imageStream))
			//{
			//    return img.Size;
			//}
			return new Size(0, 0);
		}

		public string GetColorSpace(Stream imageStream)
		{
			//TODO
			return "Rgb";
		}

		public Bitmap Resize(Stream imageStream, int width, float xDpi, float yDpi)
		{
			using (Image img = Image.FromStream(imageStream))
			{
				float aspect = (float)img.Height / (float)img.Width;
				int newHeight = (int)(width * aspect);
				Bitmap bmpOut = new Bitmap(width, newHeight, img.PixelFormat);
				bmpOut.SetResolution(xDpi, yDpi);
				using (Graphics g = Graphics.FromImage(bmpOut))
				{
					g.SmoothingMode = SmoothingMode.HighQuality;
					g.PixelOffsetMode = PixelOffsetMode.HighQuality;
					g.InterpolationMode = InterpolationMode.HighQualityBicubic;
					g.DrawImage(img, 0, 0, width, newHeight);
					return bmpOut;
				}
			}
		}
	}
}