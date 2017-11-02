/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Documentation
{
    public delegate void DocumentationRenderDelegate(Dictionary<string, List<DocInfo>> docInfosByType, StringBuilder renderInto);
    
}
