/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Naizari.Javascript.JsonControls.Exceptions
{
    public class MaxFileSizeExceededException : JsonException
    {
        public MaxFileSizeExceededException(double allowedKilobytes, double attemptedKilobytes) :
            base(string.Format("Attempted to upload file of {0} Kilobytes but the maximum file size is {1} Kilobytes.", attemptedKilobytes, allowedKilobytes))
        {
            this.AllowedSize = allowedKilobytes;
            this.AttemptedSize = attemptedKilobytes;
        }

        public double AllowedSize { get; internal set; }
        public double AttemptedSize { get; internal set; }
    }
}
