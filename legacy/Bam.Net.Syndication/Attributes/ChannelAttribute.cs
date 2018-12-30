/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bam.Net.Syndication.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ChannelAttribute: Attribute
    {
        public ChannelAttribute() { }
        public ChannelAttribute(string title)
        {
            this.Title = title;
        }

        public string Title { get; set; }
    }
}
