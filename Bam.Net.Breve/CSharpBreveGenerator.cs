/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Breve
{
    public class CSharpBreveGenerator: BreveGenerator
    {
        public CSharpBreveGenerator(BreveInfo info)
            : base(info)
        {
            Language = Languages.cs;
            Format = new CSharpFormat();
        }
    }
}
