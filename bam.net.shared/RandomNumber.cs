/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net
{
    public abstract class RandomNumber
    {
        public static int UpTo(int max = 100)
        {
            return RandomHelper.Next(max);
        }

        public static int Between(int min, int max)
        {
            return RandomHelper.Next(min, max);
        }
    }
}
