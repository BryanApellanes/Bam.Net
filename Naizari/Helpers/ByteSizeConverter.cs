/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Text;

namespace Naizari.Helpers
{
    public static class ByteSizeConverter
    {
        public static double MegabytesToKilobytes(this double megaBytes)
        {
            return megaBytes * 1024;
        }

        public static double KilobytesToMegabytes(this double kiloBytes)
        {
            return kiloBytes / 1024;
        }

        public static double BytesToKilobytes(this double bytes)
        {
            return bytes / 1024;
        }

        public static double KilobytesToBytes(this double kiloBytes)
        {
            return kiloBytes * 1024;
        }
    }
}
