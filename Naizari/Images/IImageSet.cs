/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Naizari.Images
{
    /// <summary>
    /// BE CAREFUL IF/WHEN CHANGING THE NAME OF THIS INTERFACE SINCE THE PIXELSERVER
    /// IS LOOKING FOR THIS INTERFACE BY ITS STRING NAME.
    /// </summary>
    public interface IImageSet: IDisposable
    {
        Bitmap this[string imageName] { get; set; }

        void RefreshState();
    }
}
