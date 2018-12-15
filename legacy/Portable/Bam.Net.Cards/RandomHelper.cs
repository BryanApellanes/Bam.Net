/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bam.Net
{
    public static class RandomHelper
    {
        static Random random = new Random();
        public static int Next()
        {
            lock (random)
            {
                return random.Next();
            }
        }

        public static int Next(int max)
        {
            lock (random)
            {
                return random.Next(max);
            }
        }

        public static int Next(int min, int max)
        {
            lock (random)
            {
                return random.Next(min, max);
            }
        }

        public static bool Bool()
        {
            return Next(1, 100) > 50;
        }
    }
}
