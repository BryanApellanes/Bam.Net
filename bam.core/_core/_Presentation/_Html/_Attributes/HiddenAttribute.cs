/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Bam.Net.Presentation.Html
{
    [AttributeUsage(AttributeTargets.Property)]
    public partial class HiddenAttribute: StringInputAttribute
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
        
        public override Tag CreateInput(object data = null)
        {
            return new Tag("input", new {type="hidden", name=PropertyName}, data);
        }
    }
}
