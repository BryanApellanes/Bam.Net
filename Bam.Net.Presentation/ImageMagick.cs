using Bam.Net.CommandLine;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Bam.Net.Presentation
{
    public class ImageMagick
    {
        static ImageMagick()
        {
            ImageMagickExePath = "C:\\Program Files\\ImageMagick-7.0.7-Q16\\magick.exe";
        }

        public static string ImageMagickExePath { get; set; }

        public static Bitmap SetDpi(string inputImagePath, int dpi)
        {
            return SetDpi(inputImagePath, dpi, new FileInfo(inputImagePath).Directory.FullName);
        }

        public static Bitmap SetDpi(string inputImagePath, int dpi, string outputFolder)
        {
            return SetDpi(inputImagePath, dpi, outputFolder, out FileInfo ignore);
        }

        public static Bitmap SetDpi(string inputImagePath, int dpi, string outputFolder, out FileInfo imageFile)
        {
            string fileName = Path.GetFileName(inputImagePath);
            string resultImage = Path.Combine(outputFolder, $"{dpi}dpi_{fileName}");
            Bitmap result = null;
            ImageMagickExePath.Run($"convert -units PixelsPerInch {inputImagePath} -density {dpi} {resultImage}", (o, a) => result = new Bitmap(resultImage), 60000);
            imageFile = new FileInfo(resultImage);
            return result;
        }
    }
}
