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
    public partial class PasswordAttribute: StringInputAttribute
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
            set { }
        }

        public override bool? AddLabel
        {
            get;
            set;
        }
        public override Tag CreateInput(object data = null)
        {
            return InputTag.OfType(InputTypes.Password).SetAttribute("name", PropertyName);
        }
    }
}
