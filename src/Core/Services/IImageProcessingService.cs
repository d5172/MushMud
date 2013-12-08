using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace MusicCompany.Core.Services
{
	public interface IImageProcessingService
	{
		Size GetSize(Stream imageStream);
		string GetColorSpace(Stream imageStream);
		Bitmap Resize(Stream imageStream, int width, float xDpi, float yDpi);
	}
}