/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Naizari.Javascript.JsonControls
{
    public struct JsonDimensions
    {
        int width;
        int height;
        public JsonDimensions(int width, int height)
        {
            this.width = width;
            this.height = height;
        }

        public int Width
        {
            get { return this.width; }
            set { this.width = value; }
        }
        public int Height
        {
            get { return this.height; }
            set { this.height = value; }
        }

        public override string ToString()
        {
            return string.Format("{0}Width: {1}, Height: {2}{3}", "{", this.Width, this.Height, "}");
        }
    }
}
