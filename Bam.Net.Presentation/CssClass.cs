/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bam.Net.Analytics.Data;

namespace Bam.Net.Html
{
    public class CssClass
    {
        List<Style> _styles;
        public CssClass(string name)
        {
            this._styles = new List<Style>();
            this.Name = name;
        }

        public string Name { get; set; }

        public CssClass Add(string name, string value)
        {
            return Add(new Style(name, value));
        }

        public CssClass Add(Style style)
        {
            this._styles.Add(style);
            return this;
        }
    }
}
