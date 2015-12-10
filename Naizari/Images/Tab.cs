/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using Naizari.Helpers;
using System.Reflection;

namespace Naizari.Images
{
    public class Tab: ImageSetProviderBase
    {
        public class TabImageSet: ImageSetBase
        {
            public TabImageSet()
                : base()
            {
            }

            public Bitmap FirstSide { get; set; }
           // public Bitmap FirstEdge { get; set; }
           // public Bitmap FirstCorner { get; set; }
            public Bitmap MiddleSection { get; set; }
            public Bitmap LastSide { get; set; }
            //public Bitmap LastCorner { get; set; }
            //public Bitmap LastEdge { get; set; }
        }

        QuarterCircles corners;

        public Tab()
        {
            corners = new QuarterCircles(1, 15);
        }

        public Tab(QuarterCircles corners, int tabHeight)
            : this()
        {
            this.corners = corners;
            this.TabHeight = tabHeight;
        }

        public Tab(QuarterCircles corners, int tabHeight, string lineColor)
            : this(corners, tabHeight)
        {
            this.corners.Line.Color = ColorTranslator.FromHtml(lineColor);
        }

        public QuarterCircles RoundedCorners
        {
            get
            {
                return this.corners;
            }
            set
            {
                this.corners = value;
            }
        }

        public int TabHeight
        {
            get;
            set;
        }

        //string lineWidth, string radius, string horizontalMargin, string verticalMargin, string htmlLineColor, string htmlBackgroundColor, string htmlFillColor)
        public static IImageSet GetTopTab(ImageSetParameters parameters)
        {
            int intLineWidth;
            int intRadius;
            int intHorizontalMargin;
            int intVerticalMargin;
            int intTabHeight;

            string lineWidth = parameters["linewidth"];
            string lineColor = parameters["linecolor"];
            string radius = parameters["radius"];
            string horizontalMargin = parameters["hmargin"];
            string verticalMargin = parameters["vmargin"];
            string htmlBackgroundColor = parameters["backgroundcolor"];
            string htmlFillColor = parameters["fillcolor"];
            string tabHeight = parameters["tabh"];

            if (int.TryParse(lineWidth, out intLineWidth))
            {
                if (int.TryParse(radius, out intRadius))
                {
                    if (int.TryParse(horizontalMargin, out intHorizontalMargin))
                    {
                        if (int.TryParse(verticalMargin, out intVerticalMargin))
                        {
                            if (int.TryParse(tabHeight, out intTabHeight))
                            {
                                Tab tabs = new Tab(new QuarterCircles(intLineWidth, intRadius,
                                    new SolidBrush(ColorTranslator.FromHtml(htmlBackgroundColor)),
                                    new SolidBrush(ColorTranslator.FromHtml(htmlFillColor)),
                                    intHorizontalMargin,
                                    intVerticalMargin), intTabHeight, lineColor);

                                return (IImageSet)tabs.GetTop();
                            }
                        }
                    }
                }
            }

            return null;
        }
        
        private TabImageSet GetTop()
        {
            TabImageSet retVal = new TabImageSet();

            //retVal.FirstCorner = corners.TopLeft;
            //retVal.LastCorner = corners.TopRight;

            int imageHeight = this.TabHeight + this.corners.VerticalMargin;


            //int midSectionHeight = imageHeight;//corners.TopLeft.Height;
            Bitmap middleSection = null;
            using (Graphics graphics = GraphicsManager.GetCanvas(1, imageHeight, corners.BackgroundBrush, out middleSection))
            {
                int y = corners.VerticalMargin +(int)(corners.Line.Width / 2);
                int lineY = this.corners.Line.Width > 1 ? y + 1 : y;
                int sectionHeight = imageHeight - this.corners.VerticalMargin -(int)(corners.Line.Width / 2);

                graphics.FillRectangle(corners.FillBrush, -5, y, 10, sectionHeight); // the tab fill
                graphics.DrawLine(corners.Line, -1, lineY, 1, lineY);// top line of tab
            }

            Bitmap firstSide = null;

            int tabLeftEdgeWidth = corners.TopLeft.Width - ((int)(this.corners.Line.Width / 2));

            using (Graphics graphics = GraphicsManager.GetCanvas(corners.TopLeft.Width, imageHeight, corners.BackgroundBrush, out firstSide))
            {
                int x = (int)(this.corners.Line.Width / 2);
                if (this.corners.Line.Width > 1)
                    x = x + 1;

                int horizLineY = imageHeight - (int)(corners.Line.Width / 2);
                graphics.DrawLine(corners.Line, -5, horizLineY, corners.TopLeft.Width + 5, horizLineY);
                graphics.FillRectangle(corners.FillBrush, x, 0, tabLeftEdgeWidth, imageHeight); // +- 5 to counteract image smoothing which causes discoloration of the selected colors
                graphics.DrawLine(corners.Line, x, -10, x, imageHeight);
                
                graphics.DrawImage(corners.TopLeft, 0, 0, corners.TopLeft.Width, corners.TopLeft.Height);
            }

            Bitmap lastSide = null;
            int tabRightEdgeFillWidth = corners.TopRight.Width - ((int)(corners.Line.Width / 2)) - corners.HorizontalMargin;
            int lineX = this.corners.Line.Width > 1 ? tabRightEdgeFillWidth - 1: tabRightEdgeFillWidth;
            using (Graphics graphics = GraphicsManager.GetCanvas(corners.TopRight.Width, imageHeight, corners.BackgroundBrush, out lastSide))
            {
                int horizLineY = imageHeight - (int)(corners.Line.Width / 2);
                if (corners.Line.Width == 1)
                    horizLineY = horizLineY - 1;

                graphics.DrawLine(corners.Line, -5, horizLineY, corners.TopRight.Width + 5, horizLineY); 
                graphics.FillRectangle(corners.FillBrush, -5, 0, tabRightEdgeFillWidth + 5, imageHeight);
                graphics.DrawLine(corners.Line, lineX, -10, lineX, imageHeight);

                graphics.DrawImage(corners.TopRight, 0, 0, corners.TopRight.Width, corners.TopRight.Height);
            }

            //Bitmap firstEdge = null;
            ////int tabLeftEdgeWidth = corners.TopLeft.Width - (corners.HorizontalMargin - (int)(this.corners.Line.Width / 2));

            //using (Graphics graphics = GraphicsManager.GetCanvas(corners.TopLeft.Width, 1, corners.BackgroundBrush, out firstEdge))
            //{
            //    int x = (int)(this.corners.Line.Width / 2);
            //    graphics.FillRectangle(corners.FillBrush, x, -5, tabLeftEdgeWidth + 5, 10); // plus 5 to counteract image smoothing which causes discoloration of the selected colors
            //    graphics.DrawLine(corners.Line, x, -10, x, 10);
            //}

            //Bitmap lastEdge = null;
            //int tabRightEdgeFillWidth = corners.TopRight.Width - ((int)(corners.Line.Width / 2)) - corners.HorizontalMargin;
            //int lineX = tabRightEdgeFillWidth;
            //using (Graphics graphics = GraphicsManager.GetCanvas(corners.TopRight.Width, 1, corners.BackgroundBrush, out lastEdge))
            //{
            //    graphics.FillRectangle(corners.FillBrush, 0, -5, tabRightEdgeFillWidth, 10);
            //    graphics.DrawLine(corners.Line, lineX, -10, lineX, 10);
            //}

            retVal.FirstSide = firstSide;
            retVal.MiddleSection = middleSection;
            retVal.LastSide = lastSide;
            retVal.RefreshState();
            return retVal;
        }
    }
}
