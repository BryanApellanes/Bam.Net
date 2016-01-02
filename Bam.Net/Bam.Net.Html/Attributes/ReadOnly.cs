/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace Bam.Net.Html
{
    [AttributeUsage(AttributeTargets.Property)]
    public class ReadOnly: StringInput
    {
        public override bool? BreakAfterLabel
        {
            get;
            set;
        }

        public override bool? AddValue
        {
            get;
            set;
        }

        public override bool? IsHidden
        {
            get
            {
                return false;
            }
            set
            {
                // always false
            }
        }

        public override bool? AddLabel
        {
            get;
            set;
        }

        public override TagBuilder CreateInput()
        {
            return new TagBuilder("span")
                .Name(PropertyName)
                .TextIf(Default != null, (string)Default);
        }
    }
}
