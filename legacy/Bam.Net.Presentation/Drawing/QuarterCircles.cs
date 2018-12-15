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
using Bam.Net.Presentation.Drawing;

namespace Bam.Net.Drawing
{
    public class QuarterCircles
    {
        private const int topLeftStart = 180;
        private const int topRightStart = 270;
        private const int bottomRightStart = 0;
        private const int bottomLeftStart = 90;

        Brush backgroundBrush;
        Brush fillBrush;
        Pen line;

        public QuarterCircles(int lineWidth, int radius, GraphicsManager graphicsManager = null)
        {            
            Radius = radius;
            Line.Width = lineWidth;
            HorizontalMargin = 0;
            VerticalMargin = 0;
            GraphicsManager = graphicsManager ?? GraphicsManager.Default;
        }

        public QuarterCircles(int lineWidth, int radius, Brush backgroundBrush)
            : this(lineWidth, radius)
        {
            BackgroundBrush = backgroundBrush;
        }

        public QuarterCircles(int lineWidth, int radius, Brush backgroundBrush, Brush fillBrush)
            : this(lineWidth, radius, backgroundBrush)
        {
            FillBrush = fillBrush;
        }

        public QuarterCircles(int lineWidth, int radius, Brush backgroundBrush, Brush fillBrush, int horizontalMargin, int verticalMargin)
            : this(lineWidth, radius, backgroundBrush, fillBrush)
        {
            HorizontalMargin = horizontalMargin;
            VerticalMargin = verticalMargin;
        }

        #region properties
        public GraphicsManager GraphicsManager { get; set; }
        public Pen Line
        {
            get
            {
                if (this.line == null)
                    this.Line = new Pen(Color.Black, 1);

                return line;
            }

            set
            {
                this.line = value;
                this.SetEllipseDimension();
            }
        }

        public Brush BackgroundBrush
        {
            get
            {
                if (this.backgroundBrush == null)
                    return Brushes.White;

                return this.backgroundBrush;
            }
            set
            {
                this.backgroundBrush = value;
            }
        }

        public Brush FillBrush
        {
            get
            {
                if (this.fillBrush == null)
                    this.fillBrush = Brushes.White;

                return this.fillBrush;
            }

            set
            {
                this.fillBrush = value;
            }
        }

        public int HorizontalMargin { get; set; }
        public int VerticalMargin { get; set; }
        int radius;
        public int Radius
        {
            get { return this.radius; }
            set
            {
                if (value < 3)
                    value = 3;
                this.radius = value;
                this.SetEllipseDimension();
            }
        }

        private int Height
        {
            get
            {
                return (int)(this.Radius + (int)(this.Line.Width / 2) + this.VerticalMargin);
            }
        }

        private int Width
        {
            get
            {
                return (int)(this.Radius + (int)(this.Line.Width / 2) + this.HorizontalMargin);
            }
        }

        
        #endregion

        int ellipseDimension;
        private void SetEllipseDimension()
        {
             this.ellipseDimension = (int)(this.Radius * 2);
        }

        Bitmap topLeft;
        public Bitmap TopLeft
        {
            get
            {
                if (topLeft == null)
                {
                    using (Graphics graphics = GraphicsManager.GetCanvas(this.Width - this.HorizontalMargin, this.Height, this.BackgroundBrush, out topLeft))
                    {
                        Rectangle ellipseArea = new Rectangle((int)(this.Line.Width / 2),
                            this.VerticalMargin + (int)(this.Line.Width / 2),
                            this.ellipseDimension,
                            this.ellipseDimension);

                        graphics.FillEllipse(this.FillBrush, ellipseArea);
                        graphics.DrawArc(this.Line, ellipseArea, topLeftStart, 90);                        
                    }
                }
                return topLeft;
            }
        }

        Bitmap topRight;
        public Bitmap TopRight
        {
            get
            {
                using (Graphics graphics = GraphicsManager.GetCanvas(this.Width, this.Height, this.BackgroundBrush, out topRight))
                {
                    Rectangle ellipseArea = new Rectangle(-(this.Radius + (int)(this.Line.Width / 2)), 
                        this.VerticalMargin + (int)(this.Line.Width / 2), 
                        this.ellipseDimension, 
                        this.ellipseDimension);

                    graphics.FillEllipse(this.FillBrush, ellipseArea);
                    graphics.DrawArc(this.Line, ellipseArea, topRightStart, 90);
                }
                return this.topRight;
            }
        }

        Bitmap bottomRight;
        public Bitmap BottomRight
        {
            get
            {
                using (Graphics graphics = GraphicsManager.GetCanvas(this.Width, this.Height, this.BackgroundBrush, out bottomRight))
                {
                    Rectangle ellipseArea = new Rectangle(-(this.Radius + (int)(this.Line.Width / 2) + this.HorizontalMargin), 
                        -(this.VerticalMargin + this.Radius + (int)(this.Line.Width / 2 )),
                        this.ellipseDimension,
                        this.ellipseDimension);

                    graphics.FillEllipse(this.FillBrush, ellipseArea);
                    graphics.DrawArc(this.Line, ellipseArea, bottomRightStart, 90);
                }
                return this.bottomRight;
            }                
        }

        Bitmap bottomLeft;
        public Bitmap BottomLeft
        {
            get
            {
                using (Graphics graphics = GraphicsManager.GetCanvas(this.Width, this.Height, this.BackgroundBrush, out bottomLeft))
                {
                    Rectangle ellipseArea = new Rectangle(this.HorizontalMargin + ((int)this.Line.Width / 2), 
                        -(this.VerticalMargin + this.Radius + ((int)this.Line.Width / 2)), 
                        this.ellipseDimension, 
                        this.ellipseDimension);

                    graphics.FillEllipse(this.FillBrush, ellipseArea);
                    graphics.DrawArc(this.Line, ellipseArea, bottomLeftStart, 90);
                }
                return this.bottomLeft;
            }
        }
    }
}
