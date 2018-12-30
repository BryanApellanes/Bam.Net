/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KLGates.Javascript
{
    public interface IHierarchyNode
    {
        string Text { get; set; }
        string Value { get; set; }

        IHierarchyNode[] ChildNodes { get; set; }
    }
}
