/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KLGates.Javascript
{
    public class Hierarchy: IHierarchy
    {
        public class Node: IHierarchyNode
        {

            #region IHierarchyNode Members

            public string Text
            {
                get;
                set;
            }

            public string Value
            {
                get;
                set;
            }

            public IHierarchyNode[] ChildNodes
            {
                get;
                set;
            }

            #endregion
        }

        public Hierarchy(object data)
        {
            // need a way to assign text and value
        }

        #region IHierarchy Members

        public IHierarchyNode[] Nodes
        {
            get;
            set;
        }

        #endregion
    }
}
