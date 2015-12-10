/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Bam.Javascript
{
    public interface IHierarchy
    {
        IHierarchyNode[] Nodes { get; set; }
    }
}
