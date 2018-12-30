/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace Bam.Net.Presentation.Html
{
    [AttributeUsage(AttributeTargets.Property)]
    public class Hidden: StringInput
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
                return true;
            }
            set
            {
                // always true
            }
        }

        public override bool? AddLabel
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

        public override TagBuilder CreateInput()
        {
            return CreateInput("hidden")
                .Name(this.PropertyName)
                .ValueIf(Default != null, (string)Default);
        }
    }
}
