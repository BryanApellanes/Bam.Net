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
    public class TextArea: StringInput
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

        public override TagBuilder CreateInput()
        {
            return new TagBuilder("textarea")
                            .Attr("rows", this.Rows.ToString())
                            .Attr("cols", this.Cols.ToString())
                            .TextIf(Default != null, (string)Default);
        }

        public override bool? AddValue
        {
            get;
            set;
        }
    }
}
