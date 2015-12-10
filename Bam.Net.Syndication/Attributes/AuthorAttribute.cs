/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bam.Net.Syndication.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class AuthorAttribute: Attribute
    {
        public AuthorAttribute()
        {
        }
    }
}
