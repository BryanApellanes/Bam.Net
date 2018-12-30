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
    public partial class TextArea: StringInput
    {
        public TextArea()
        {
            this.Rows = 10;
            this.Cols = 30;
        }

        public int Rows { get; set; }
        public int Cols { get; set; }

        public override bool? IsHidden
        {
            get;
            set;
        }

        public override bool? BreakAfterLabel
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
            get;
            set;
        }

        public override bool? AddValue
        {
            get;
            set;
        }
    }
}
