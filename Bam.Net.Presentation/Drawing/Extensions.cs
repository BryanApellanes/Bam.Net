using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gma.QrCodeNet.Encoding;
using System.Drawing.Imaging;

namespace Bam.Net.Drawing
{
    public static class Extensions
    {
        public static void ToQrCodeFile(this string text, string filePath, ImageFormat format = null, ErrorCorrectionLevel ecl = ErrorCorrectionLevel.H)
        {
            format = format ?? ImageFormat.Bmp;
            ToQrCode(text).Save(filePath, format);
        }

        public static Bitmap ToQrCode(this string text, ErrorCorrectionLevel ecl = ErrorCorrectionLevel.H)
        {
            return ToQrCode(text, Color.Black, Color.White, ecl);
        }
        public static Bitmap ToQrCode(this string text, Color darkColor, Color lightColor, ErrorCorrectionLevel ecl = ErrorCorrectionLevel.H)
        {
            QrEncoder encoder = new QrEncoder(ecl);
            QrCode code = encoder.Encode(text);
            Bitmap result = new Bitmap(code.Matrix.Width, code.Matrix.Height);
            for (int X = 0; X <= code.Matrix.Width - 1; X++)
            {
                for (int Y = 0; Y <= code.Matrix.Height - 1; Y++)
                {
                    if (code.Matrix.InternalArray[X, Y])
                    {
                        result.SetPixel(X, Y, darkColor);
                    }
                    else
                    {
                        result.SetPixel(X, Y, lightColor);
                    }
                }
            }
            return result;
        }
    }
}
