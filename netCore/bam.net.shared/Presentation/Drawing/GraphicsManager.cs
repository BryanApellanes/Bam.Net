/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using Bam.Net.Drawing;

namespace Bam.Net.Presentation.Drawing
{
    public class GraphicsManager
    {
        public GraphicsManager()
        {
            Font = new Font(FontFamily.GenericSansSerif, 10, GraphicsUnit.Pixel);
            Brush = Brushes.Black;
            BackgroundBrush = Brushes.Transparent;
            Dimensions = new Dimensions { Width = 600, Height = 480 };
            Color = Color.Black;
            BackgroundColor = Color.Transparent;
        }

        static List<Bitmap> images;
        static GraphicsManager()
        {
            Default = new GraphicsManager();
            images = new List<Bitmap>();
            AppDomain.CurrentDomain.DomainUnload += new EventHandler(DisposeImages);
        }

        static void DisposeImages(object sender, EventArgs e)
        {
            foreach (Bitmap image in images)
            {
                image.Dispose();
            }
        }

        public static GraphicsManager Default { get; set; }
        
        public Color Color { get; set; }
        public Color BackgroundColor { get; set; }
        public Font Font { get; set; }
        public Brush Brush { get; set; }
        public Brush BackgroundBrush { get; set; }
        public Dimensions Dimensions { get; set; }

        // TODO: fix this using rectangles see; ImageProcess from external project
        public Bitmap Overlay(Bitmap background, Bitmap foreground, int x, int y)
        {
            Bitmap result = new Bitmap(background.Width, background.Height);
            using (Graphics graphics = Graphics.FromImage(result))
            {
                SetHighQuality(graphics);
                graphics.DrawImage(background, new Point(0, 0));
                graphics.DrawImage(foreground, new Point(x, y));
                return result;
            }
        }

        public Bitmap ScaleTo(Bitmap bitmap, float width, float height, Color? backgroundColor = null)
        {
            backgroundColor = backgroundColor ?? Color.Transparent;

            float scale = Math.Min(width / bitmap.Width, height / bitmap.Height);
            Bitmap result = new Bitmap((int)width, (int)height);
            using (Graphics graphics = Graphics.FromImage(result))
            {
                SetHighQuality(graphics);

                int scaleWidth = (int)(bitmap.Width * scale);
                int scaleHeight = (int)(bitmap.Height * scale);
                graphics.FillRectangle(new SolidBrush(backgroundColor.Value), new RectangleF(0, 0, width, height));
                graphics.DrawImage(bitmap, (width - scaleWidth) / 2, (height - scaleHeight) / 2, scaleWidth, scaleHeight);
                return result;
            }
        }

        private static void SetHighQuality(Graphics graphics)
        {
            graphics.InterpolationMode = InterpolationMode.High;
            graphics.CompositingQuality = CompositingQuality.HighQuality;
            graphics.SmoothingMode = SmoothingMode.AntiAlias;
        }

        public Bitmap ResizeCanvas(Bitmap bitmap, int width, int height, Color? backgroundColor = null)
        {
            backgroundColor = backgroundColor ?? Color.Transparent;

            Bitmap result = new Bitmap(width, height);
            Graphics graphics = Graphics.FromImage(result);
            graphics.InterpolationMode = InterpolationMode.High;
            graphics.CompositingQuality = CompositingQuality.HighQuality;
            graphics.SmoothingMode = SmoothingMode.AntiAlias;
            graphics.FillRectangle(new SolidBrush(backgroundColor.Value), new RectangleF(0, 0, width, height));
            graphics.DrawImage(bitmap, 0, 0, bitmap.Width, bitmap.Height);
            return result;
        }

        public Bitmap GetRoundedRectangle(int width, int height, int radius, int borderWidth)
        {
            return GetRoundedRectangle(width, height, radius, borderWidth, Brushes.White);
        }

        public Bitmap GetRoundedRectangle(int width, int height, int radius, int borderWidth, Brush backgroundBrush)
        {
            GraphicsPath path = RoundedRectangle.Create(borderWidth, borderWidth, width - (borderWidth * 2), height - (borderWidth * 2), radius, RoundedRectangle.RectangleCorners.All);
            return FromPath(path, width, height, borderWidth, Color.Black, backgroundBrush);
        }

        public static void DisposeImages()
        {
            DisposeImages(null, null);
        }

        public Bitmap GetTextImage(string text, int width, int height, Brush brush, Brush backgroundBrush)
        {
            Bitmap image = new Bitmap(width, height);
            int size = width >= height ? width: height;            
            Font font = new Font(FontFamily.GenericSansSerif, size, GraphicsUnit.Pixel);
            using (Graphics graphics = Graphics.FromImage(image))
            {
                graphics.FillRectangle(backgroundBrush, graphics.ClipBounds);
                SizeF stringSize = graphics.MeasureString(text, font);
                float scale = image.Width / stringSize.Width;
                if (scale < 1)
                {
                    graphics.ScaleTransform(scale, scale);
                }
                graphics.DrawString(text, font, brush, new PointF());
            }
            return image;
        }

        public Bitmap FromPath(GraphicsPath path, int width, int height, int borderWidth, Color borderColor, Brush backgroundBrush)
        {
            Bitmap canvas = null;
            using (Graphics graphics = GetCanvas(width, height, backgroundBrush, out canvas))
            {
                graphics.DrawPath(new Pen(borderColor, borderWidth), path);
            }

            return canvas;
        }

        public Graphics GetCanvas(int width, int height, Brush backgroundBrush, out Bitmap image)
        {
            return GetCanvas(width, height, backgroundBrush, true, out image);
        }

        static object canvasLock = new object();
        public Graphics GetCanvas(int width, int height, Brush backgroundBrush, bool retry, out Bitmap image)
        {
            try
            {
                lock (canvasLock)
                {
                    image = new Bitmap(width, height);
                    images.Add(image);
                    Graphics graphics = Graphics.FromImage(image);
                    graphics.FillRegion(backgroundBrush, new Region(new Rectangle(0, 0, width, height)));
                    return graphics;
                }
            }
            catch (Exception ex)
            {
                if (retry)
                {
                    DisposeImages();
                    return GetCanvas(width, height, backgroundBrush, false, out image);
                }
                else
                {
                    throw ex;
                }
            }
        }
    }
}
