/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Naizari.Data.Common;

namespace Naizari.Javascript.DataControls
{
    public class ContextMenuItem
    {
        public ContextMenuItem()
        {
        }

        public ContextMenuItem(string text, string action)
        {
            this.Action = action;
            this.Text = text;
        }

        public ContextMenuItem(string text, string action, string dataid)
            : this(text, action)
        {
            this.DataID = dataid;
        }

        public string Text { get; set; }
        public string Action { get; set; }
        public string DataID { get; set; }

        public static ContextMenuItem FromNode(Node itemNode)
        {
            ContextMenuItem ret = new ContextMenuItem();
            ret.DataID = itemNode.ID.ToString();
            ret.Text = itemNode.Text;
            ret.Action = itemNode.Value;
            return ret;
        }
    }
}
