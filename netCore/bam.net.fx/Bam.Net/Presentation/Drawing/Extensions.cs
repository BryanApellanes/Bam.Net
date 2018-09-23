using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Imaging;
using QRCoder;
using System.IO;

namespace Bam.Net.Presentation.Drawing
{
    public static class Extensions
    {
        static Extensions()
        {
        }
        
        public static Bitmap ToQrCode(this string text, Bitmap icon)
        {
            QRCode qrCode = GetQrCode(text, QRCodeGenerator.ECCLevel.H);
            return GetGraphic(qrCode, GraphicsManager.Default.Color, GraphicsManager.Default.BackgroundColor, 25, icon);
        }

        public static Bitmap ToQrCode(this string text)
        {
            return ToQrCode(text, GraphicsManager.Default.Color, GraphicsManager.Default.BackgroundColor);
        }

        public static Bitmap ToQrCode(this string text, Color darkColor, Color lightColor, int moduleSize = 25, Bitmap icon = null, QRCodeGenerator.ECCLevel eccLevel = QRCodeGenerator.ECCLevel.H)
        {
            QRCode qrCode = GetQrCode(text, eccLevel);
            return GetGraphic(qrCode, darkColor, lightColor, moduleSize, icon);
        }

        public static void ToQrCodeFile(this string text, FileInfo file, ImageFormat format = null)
        {
            ToQrCodeFile(text, file.FullName, format);
        }

        public static void ToQrCodeFile(this string text, string filePath, ImageFormat format = null, QRCodeGenerator.ECCLevel eccLevel = QRCodeGenerator.ECCLevel.H)
        {
            ToQrCode(text, GraphicsManager.Default.Color, GraphicsManager.Default.BackgroundColor).Save(filePath, format);
        }

        public static Bitmap SaveTextAsImage(this string text, string filePath)
        {
            return SaveTextAsImage(text, new FileInfo(filePath), GraphicsManager.Default.Dimensions.Width, GraphicsManager.Default.Dimensions.Height);
        }

        public static Bitmap SaveTextAsImage(this string text, FileInfo file, Dimensions dimensions = null)
        {
            dimensions = dimensions ?? GraphicsManager.Default.Dimensions;
            return SaveTextAsImage(text, file, dimensions.Width, dimensions.Height);
        }

        public static Bitmap SaveTextAsImage(this string text, FileInfo file, int width, int height)
        {
            Bitmap bitmap = GraphicsManager.Default.GetTextImage(text, width, height, GraphicsManager.Default.Brush, GraphicsManager.Default.BackgroundBrush);
            bitmap.Save(file.FullName);
            return bitmap;
        }

        public static Bitmap Write(this Bitmap bitmap, string text, RectangleF? location = null)
        {
            return Write(bitmap, text, GraphicsManager.Default.Brush, GraphicsManager.Default.BackgroundBrush, location);
        }

        public static Bitmap Write(this Bitmap bitmap, string text, Brush brush, Brush backgroundBrush, RectangleF? location = null)
        {
            WriteOnImage(bitmap, text, brush, backgroundBrush, location);
            return bitmap;
        }

        public static Bitmap WriteOnImage(this Bitmap bitmap, string text, RectangleF? location = null)
        {            
            WriteOnImage(bitmap, text, GraphicsManager.Default.Brush, location);
            return bitmap;
        }

        public static void WriteOnImage(this Bitmap image, string text,Brush brush, RectangleF? location = null)
        {
            WriteOnImage(image, text, brush, GraphicsManager.Default.BackgroundBrush, location);
        }

        public static void WriteOnImage(this Bitmap image, string text, Brush brush, Brush textBackground, RectangleF? location = null)
        {
            location = location ?? new RectangleF(0, 0, 640, 480);
            Bitmap textImage = GraphicsManager.Default.GetTextImage(text, (int)location.Value.Width, (int)location.Value.Height, brush, textBackground);
            using (Graphics graphics = Graphics.FromImage(image))
            {
                graphics.DrawImage(textImage, location.Value);
            }
        }

        public static Bitmap ResizeCanvas(this Bitmap image, int width, int height, Color? backGroundColor = null)
        {
            return GraphicsManager.Default.ResizeCanvas(image, width, height, backGroundColor);
        }

        public static Bitmap Overlay(this Bitmap background, Bitmap foreGround, int x = 0, int y = 0)
        {
            return GraphicsManager.Default.Overlay(background, foreGround, x, y);
        }

        public static Bitmap ScaleTo(this Bitmap image, int width, int height)
        {
            return GraphicsManager.Default.ScaleTo(image, width, height);
        }

        public static Bitmap ToImage(this string text, int width, int height, Brush brush = null)
        {
            return GraphicsManager.Default.GetTextImage(text, width, height, brush ?? GraphicsManager.Default.Brush, GraphicsManager.Default.BackgroundBrush);
        }

        private static QRCode GetQrCode(string text, QRCodeGenerator.ECCLevel eccLevel)
        {
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(text, eccLevel);
            QRCode qrCode = new QRCode(qrCodeData);
            return qrCode;
        }

        private static Bitmap GetGraphic(QRCode code, Color darkColor, Color lightColor, int moduleSize = 25, Bitmap icon = null)
        {
            if (icon != null)
            {
                return code.GetGraphic(moduleSize, darkColor, lightColor, icon);
            }
            return code.GetGraphic(moduleSize, darkColor, lightColor, true);
        }
    }
}
