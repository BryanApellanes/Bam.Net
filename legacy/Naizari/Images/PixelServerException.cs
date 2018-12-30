/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Naizari.Images
{
    public class PixelServerException: Exception
    {
        public PixelServerException(string message)
            : base(message)
        { }

        public PixelServerException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
