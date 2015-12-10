/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Naizari.Javascript.JsonControls;

namespace Naizari.Javascript.BoxControls
{
    public class EmptyTreeData : TreeData
    {
        public EmptyTreeData()
            : base()
        {
            this.Text = "&nbsp;";
            this.Value = "-1";
        }

        public EmptyTreeData(string text, string value)
            : this()
        {
            this.Text = text;
            this.Value = value;
        }
    }
    public class TreeData: JsonDataItem
    {
        public TreeData() : base() { }

        public TreeData(string text, string value) : base(text, value) { }

        public TreeData(string text, string value, int depth)
            : this(text, value)
        {
            this.Depth = depth;
        }

        public int Depth { get; set; }

        // these are here specifically for practice areas and groups and should not be removed
        public string ID1 { get; set; }
        public string ID2 { get; set; }
    }
}
