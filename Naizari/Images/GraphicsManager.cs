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

namespace Naizari.Images
{
    public abstract class GraphicsManager
    {
        static List<Bitmap> images;
        static GraphicsManager()
        {
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

        public static Bitmap GetRoundedRectangle(int width, int height, int radius, int borderWidth)
        {
            return GetRoundedRectangle(width, height, radius, borderWidth, Brushes.White);
        }

        public static Bitmap GetRoundedRectangle(int width, int height, int radius, int borderWidth, Brush backgroundBrush)
        {
            GraphicsPath path = RoundedRectangle.Create(borderWidth, borderWidth, width - (borderWidth * 2), height - (borderWidth * 2), radius, RoundedRectangle.RectangleCorners.All);
            return FromPath(path, width, height, borderWidth, Color.Black, backgroundBrush);
        }

        public static void DisposeImages()
        {
            DisposeImages(null, null);
        }

        public static Bitmap GetStringImage(int width, int height, string words, Font font, Brush brush)
        {
            return GetStringImage(width, height, words, font, brush, Brushes.White);
        }

        public static Bitmap GetStringImage(int width, int height, string words, Font font, Brush brush, Brush backgroundBrush)
        {
            Bitmap canvas = null;
            using (Graphics graphics = GetCanvas(width, height, backgroundBrush, out canvas))
            {
                StringFormat format = new StringFormat();
                format.Alignment = StringAlignment.Center;
                graphics.DrawString(words, font, brush, width / 2, (height / 2) - (font.Height / 2), format);
            }

            return canvas;
        }

        public static Bitmap FromPath(GraphicsPath path, int width, int height, int borderWidth, Color borderColor, Brush backgroundBrush)
        {
            Bitmap canvas = null;
            //Graphics graphics = Graphics.FromImage(canvas);

            using (Graphics graphics = GetCanvas(width, height, backgroundBrush, out canvas))
            {
                //graphics.FillRegion(backgroundBrush, new Region(new Rectangle(0, 0, width, height)));

                graphics.DrawPath(new Pen(borderColor, (float)borderWidth), path);

            }

            return canvas;
        }

        public static Graphics GetCanvas(int width, int height, Brush backgroundBrush, out Bitmap image)
        {
            return GetCanvas(width, height, backgroundBrush, true, out image);
        }

        static object canvasLock = new object();
        public static Graphics GetCanvas(int width, int height, Brush backgroundBrush, bool retry, out Bitmap image)
        {
            try
            {
                lock (canvasLock)
                {
                    image = new Bitmap(width, height);
                    images.Add(image);
                    Graphics graphics = Graphics.FromImage(image);
                    //graphics.SmoothingMode = SmoothingMode.HighQuality;
                    //graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    graphics.FillRegion(backgroundBrush, new Region(new Rectangle(0, 0, width, height)));
                    return graphics;
                }
            }
            catch (Exception ex)
            {
                if (retry)
                {
                    GraphicsManager.DisposeImages();
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
