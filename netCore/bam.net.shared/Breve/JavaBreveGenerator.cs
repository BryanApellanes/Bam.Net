/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Bam.Net.Breve
{
    public class JavaBreveGenerator: BreveGenerator
    {
        public JavaBreveGenerator(BreveInfo breve)
            : base(breve)
        {
            Language = Languages.java;
            Format = new JavaFormat();
        }
    }
}
